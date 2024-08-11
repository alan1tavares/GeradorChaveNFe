using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UseCase;

namespace GeradorChaveNFe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            uFComboBox.ItemsSource = UfRepository.GetItems();
            uFComboBox.DisplayMemberPath = "Nome";
            uFComboBox.SelectedValuePath = "Codigo";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var nfeInput = new NfeIpunt
            {
                CodigoUf = (int) uFComboBox.SelectedValue,
                Mes = int.Parse(mesTextBox.Text),
                Ano = int.Parse(anoTextBox.Text),
                CNPJ = cnpjTextBox.Text,
                Serie = int.Parse(serieTextBox.Text),
                NumeroNotaInicial = int.Parse(numeroNotaInicialTextBox.Text),
                NumeroNotaFinal = int.Parse(numeroNotaFinalTextBox.Text),
            };

            var chaves = ChaveNfeUseCase.Gerar(nfeInput);
            foreach (var item in chaves)
            {
                chavesTextBox.AppendText(item + "\n");
            }
        }
    }
}