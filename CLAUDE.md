# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

.NET MAUI desktop application (.NET 10, cross-platform) for generating Brazilian NFe (Nota Fiscal Eletrônica) access keys. The app accepts fiscal data as input, runs the Brazilian fiscal authority's check-digit algorithm, and outputs one or more 44-digit NFe keys. Targets macOS (Mac Catalyst) and Windows.

## Commands

```bash
# Build (Mac)
dotnet build GeradorChaveNFe/GeradorChaveNFe.csproj -f net10.0-maccatalyst

# Build (Windows)
dotnet build GeradorChaveNFe/GeradorChaveNFe.csproj -f net10.0-windows10.0.19041.0

# Run tests
dotnet test Teste/Teste.csproj

# Run a single test by name
dotnet test Teste/Teste.csproj --filter "FullyQualifiedName~TestMethodName"

# Publish (Mac)
dotnet publish GeradorChaveNFe/GeradorChaveNFe.csproj -f net10.0-maccatalyst -c Release

# Publish (Windows, self-contained)
dotnet publish GeradorChaveNFe/GeradorChaveNFe.csproj -f net10.0-windows10.0.19041.0 -c Release --self-contained
```

Tests require no external services.

## Architecture

Three projects in one solution:

```
GeradorChaveNFe/   .NET MAUI UI — ContentPage with form inputs and result display
UseCase/           Business logic — key generation algorithm + state data
Teste/             NUnit unit tests — tests UseCase directly
```

**Data flow:** `MainPage.xaml.cs` collects user input → instantiates `NfeIpunt` record → calls `ChaveNfeUseCase.Gerar()` → displays the returned `List<string>` of keys in the `Editor`.

### MAUI Project Structure (`GeradorChaveNFe/`)

| File | Purpose |
|---|---|
| `MauiProgram.cs` | App entry point, configures fonts and services |
| `App.xaml.cs` | Creates the root `Window` with `MainPage` |
| `MainPage.xaml` | Two-column UI: form on the left, NFe keys on the right |
| `MainPage.xaml.cs` | Reads inputs, calls `ChaveNfeUseCase.Gerar()`, shows result |
| `Platforms/MacCatalyst/` | Mac Catalyst entry point (`AppDelegate`, `Program`, `Info.plist`) |
| `Platforms/Windows/` | Windows entry point (`App.xaml`, `Package.appxmanifest`) |
| `Resources/` | App icon, splash screen, fonts (OpenSans) |

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

### Input Model

`NfeIpunt` is a record (note the typo in the name — keep it to avoid breaking tests):

```csharp
record NfeIpunt { CodigoUf, Mes, Ano, CNPJ, Serie, NumeroNotaInicial, NumeroNotaFinal, CodigoNumerico? }
```

`CodigoNumerico` is optional; when null the algorithm generates a random 8-digit code per key.

### State Data (`UseCase/UfRepository.cs`)

Static list of all 27 Brazilian states as `UFModel { Codigo, Nome }`. `Codigo` is the 2-digit IBGE code used in NFe keys. The MAUI `Picker` binds to `Nome` for display and reads `Codigo` on selection.

## Release Process

Pushing a tag matching `v*` triggers `.github/workflows/deploy.yaml`, which runs two parallel jobs:
- **deploy-mac** (macos-latest): publishes `net10.0-maccatalyst` → `GeradorChaveNFe-mac.zip`
- **deploy-windows** (windows-latest): publishes `net10.0-windows10.0.19041.0` → `GeradorChaveNFe-windows.zip`

Both artifacts are attached to the GitHub Release automatically.
