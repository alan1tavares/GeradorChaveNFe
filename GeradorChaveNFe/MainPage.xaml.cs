using UseCase;

namespace GeradorChaveNFe;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        uFPicker.ItemsSource = UfRepository.GetItems();
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
            CNPJ = cnpjEntry.Text,
            Serie = int.Parse(serieEntry.Text),
            NumeroNotaInicial = int.Parse(numeroNotaInicialEntry.Text),
            NumeroNotaFinal = int.Parse(numeroNotaFinalEntry.Text),
        };

        var chaves = ChaveNfeUseCase.Gerar(nfeInput);
        chavesEditor.Text = string.Join("\n", chaves);
    }
}
