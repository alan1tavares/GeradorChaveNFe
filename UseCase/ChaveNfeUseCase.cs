namespace UseCase
{
    public record NfeIpunt
    {
        public required int CodigoUf { get; set; }
        public required int Mes { get; set; }
        public required int Ano { get; set; }
        public required string CNPJ { get; set; }
        public required int Serie { get; set; }
        public required int NumeroNotaInicial { get; set; }
        public required int NumeroNotaFinal { get; set; }

        private List<int>? _codigoNumerico { get; set; }
        public List<int>? CodigoNumerico 
        {
            get => _codigoNumerico;
            set
            {
                if (value != null && (value.Count != (NumeroNotaFinal-NumeroNotaInicial+1)))
                {
                    throw new ArgumentException("Quantidade de CodigoNumericos inferior ao intervalo de Notas (Incial e Final)");
                }
                _codigoNumerico = value;
            }
        }
    }
    public class ChaveNfeUseCase
    {
        public static List<string> Gerar(NfeIpunt nfeIpunt)
        {
            const string modeloNFe = "55";
            const string emissaoNormal = "1";

            var result = new List<string>();

            string ano = nfeIpunt.Ano.ToString();
            ano = ano.Substring(ano.Length - 2);

            var random = new Random();

            for (int i = nfeIpunt.NumeroNotaInicial; i <= nfeIpunt.NumeroNotaFinal; i++)
            {
                var codigoNumerico = nfeIpunt.CodigoNumerico == null ? 
                    random.Next(1, 99999999) : nfeIpunt.CodigoNumerico[i - nfeIpunt.NumeroNotaInicial];
                 
                var numeroNfe = i;
                var chave = $"{nfeIpunt.CodigoUf}{ano}{nfeIpunt.Mes}{nfeIpunt.CNPJ}{modeloNFe}" +
                    $"{nfeIpunt.Serie:000}{numeroNfe:000000000}{emissaoNormal}" +
                    $"{codigoNumerico:00000000}";
                int dv = calcularDigitoVerificador(chave);
                chave += dv;

                result.Add(chave);
            }
            return result;
        }

        private static int calcularDigitoVerificador(string chave)
        {
            ArgumentException.ThrowIfNullOrEmpty(chave);
            if (chave.Length != 43)
            {
                throw new ArgumentException("Chave diferente de 43 dígitos");
            }

            int[] fatorMultiplicacao = { 2, 3, 4, 5, 6, 7, 8, 9 };
            int soma = 0;

            for (int i = 43 - 1, j = 0; i >= 0; i--, j++)
            {
                var numero = int.Parse(chave[i].ToString());
                soma += numero * fatorMultiplicacao[j % fatorMultiplicacao.Length];
            }

            int resto = soma % 11;
            int dv = 0;
            if (resto > 1)
            {
                dv = 11 - resto;
            }

            return dv;
        }

    }
}
