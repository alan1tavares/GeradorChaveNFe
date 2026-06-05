using UseCase;

namespace GeradorChaveNFe;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        uFPicker.ItemsSource = UfRepository.GetItems();
    }

    private void OnCnpjTextChanged(object sender, TextChangedEventArgs e)
    {
        var entry = (Entry)sender;
        var digits = new string(e.NewTextValue.Where(char.IsDigit).Take(14).ToArray());

        var formatted = digits.Length switch
        {
            <= 2  => digits,
            <= 5  => $"{digits[..2]}.{digits[2..]}",
            <= 8  => $"{digits[..2]}.{digits[2..5]}.{digits[5..]}",
            <= 12 => $"{digits[..2]}.{digits[2..5]}.{digits[5..8]}/{digits[8..]}",
            _     => $"{digits[..2]}.{digits[2..5]}.{digits[5..8]}/{digits[8..12]}-{digits[12..]}"
        };

        if (entry.Text != formatted)
            entry.Text = formatted;
    }

    private void OnLimparFormularioClicked(object sender, EventArgs e)
    {
        uFPicker.SelectedIndex = -1;
        mesEntry.Text = string.Empty;
        anoEntry.Text = string.Empty;
        cnpjEntry.Text = string.Empty;
        serieEntry.Text = string.Empty;
        numeroNotaInicialEntry.Text = string.Empty;
        numeroNotaFinalEntry.Text = string.Empty;
        chavesEditor.Text = string.Empty;
        totalLabel.Text = "Total: 0 chaves";
        statusLabel.Text = string.Empty;
    }

    private async void OnCopiarTudoClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(chavesEditor.Text)) return;
        await Clipboard.SetTextAsync(chavesEditor.Text);
    }

    private void OnGerarChavesClicked(object sender, EventArgs e)
    {
        if (uFPicker.SelectedItem is not UFModel uf)
            return;

        var nfeInput = new NfeIpunt
        {
            CodigoUf = uf.Codigo,
            Mes = int.Parse(mesEntry.Text),
            Ano = int.Parse(anoEntry.Text),
            CNPJ = new string(cnpjEntry.Text.Where(char.IsDigit).ToArray()),
            Serie = int.Parse(serieEntry.Text),
            NumeroNotaInicial = int.Parse(numeroNotaInicialEntry.Text),
            NumeroNotaFinal = int.Parse(numeroNotaFinalEntry.Text),
        };

        var chaves = ChaveNfeUseCase.Gerar(nfeInput);
        chavesEditor.Text = string.Join("\n", chaves);
        totalLabel.Text = $"Total: {chaves.Count} chaves";
        statusLabel.Text = $"✓ {chaves.Count} chaves geradas com sucesso.";
    }
}
