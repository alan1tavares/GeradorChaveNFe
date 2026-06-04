# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

WPF desktop application (.NET 8.0, Windows-only) for generating Brazilian NFe (Nota Fiscal Eletrônica) access keys. The app accepts fiscal data as input, runs the Brazilian fiscal authority's check-digit algorithm, and outputs one or more 44-digit NFe keys.

## Commands

```bash
# Build the solution
dotnet build GeradorChaveNFe.sln

# Run tests
dotnet test Teste/Teste.csproj

# Run a single test by name
dotnet test Teste/Teste.csproj --filter "FullyQualifiedName~TestMethodName"

# Publish single-file executable (mirrors CI)
dotnet publish GeradorChaveNFe/GeradorChaveNFe.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -o publish/
```

Tests require no external services. All builds target `win-x64`.

## Architecture

**Mac note:** `GeradorChaveNFe/` (WPF) only compiles on Windows. On Mac, trabalhe com `UseCase/` e `Teste/` normalmente — o projeto WPF é publicado via CI no Windows.

Three projects in one solution:

```
GeradorChaveNFe/   WPF UI — form inputs + result display
UseCase/           Business logic — key generation algorithm + state data
Teste/             NUnit unit tests — tests UseCase directly
```

**Data flow:** `MainWindow.xaml.cs` collects user input → instantiates `NfeIpunt` record → calls `ChaveNfeUseCase.Gerar()` → displays the returned `List<string>` of keys in the UI.

### Key Generation Algorithm (`UseCase/ChaveNfeUseCase.cs`)

The core logic assembles a 43-character numeric string:

| Field | Length |
|---|---|
| UF code | 2 |
| Year (last 2 digits) | 2 |
| Month | 2 |
| CNPJ | 14 |
| Model (hardcoded `55`) | 2 |
| Series | 3 |
| Invoice number | 9 |
| Emission type (hardcoded `1`) | 1 |
| Random numeric code | 8 |

A check digit is then appended using the modulo-11 algorithm with factors `[2,3,4,5,6,7,8,9]` cycling from right to left. When `NumeroNotaInicial < NumeroNotaFinal`, the method generates one key per invoice number in that range.

### Input Model (`UseCase/ChaveNfeUseCase.cs`)

`NfeIpunt` is a record (note the typo — keep it for now to avoid breaking the UI):

```csharp
record NfeIpunt(string CodigoUf, string Mes, string Ano, string CNPJ,
                string Serie, string NumeroNotaInicial, string NumeroNotaFinal,
                string? CodigoNumerico = null);
```

`CodigoNumerico` is optional; when null the algorithm generates a random 8-digit code per key.

### State Data (`UseCase/UfRepository.cs`)

Static list of all 27 Brazilian states as `UFModel { Codigo, Nome }`. `Codigo` is the 2-digit IBGE code used in NFe keys.

## Release Process

Pushing a tag matching `v*` triggers `.github/workflows/deploy.yaml`, which publishes a self-contained `win-x64` single-file executable and creates a GitHub Release with a ZIP archive.
