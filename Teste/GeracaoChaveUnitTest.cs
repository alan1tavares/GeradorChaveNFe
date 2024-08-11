using UseCase;

namespace Teste
{
    public class GeracaoChaveUnitTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1GeracaoChave()
        {
            var chaves = ChaveNfeUseCase.Gerar(new NfeIpunt
            {
                CodigoUf = 43,
                Ano = 2017,
                Mes = 12,
                CNPJ = "07364617000135",
                Serie = 0,
                NumeroNotaInicial = 12014,
                NumeroNotaFinal = 12014,
                CodigoNumerico = new List<int>() { 12014 }
            });

            Assert.That(chaves, Has.Count.EqualTo(1));
            Assert.That(chaves[0], Is.EqualTo("43171207364617000135550000000120141000120146"));
        }

        [Test]
        public void Test2GeracaoChave()
        {
            var chaves = ChaveNfeUseCase.Gerar(new NfeIpunt
            {
                CodigoUf = 13,
                Ano = 2018,
                Mes = 10,
                CNPJ = "17921427000125",
                Serie = 1,
                NumeroNotaInicial = 30,
                NumeroNotaFinal = 30,
                CodigoNumerico = new List<int>() { 88725117 }
            });

            Assert.That(chaves, Has.Count.EqualTo(1));
            Assert.That(chaves[0], Is.EqualTo("13181017921427000125550010000000301887251172"));
        }

        [Test]
        public void TestListaGeracaoChave()
        {
            var chaves = ChaveNfeUseCase.Gerar(new NfeIpunt
            {
                CodigoUf = 43,
                Ano = 2017,
                Mes = 12,
                CNPJ = "07364617000135",
                Serie = 0,
                NumeroNotaInicial = 12014,
                NumeroNotaFinal = 12016,
                CodigoNumerico = new List<int>() { 12014, 12015, 12016 }
            });

            Assert.That(chaves, Has.Count.EqualTo(3));
            Assert.That(chaves[0], Is.EqualTo("43171207364617000135550000000120141000120146"));
            Assert.That(chaves[1], Is.EqualTo("43171207364617000135550000000120151000120151"));
            Assert.That(chaves[2], Is.EqualTo("43171207364617000135550000000120161000120167"));
        }
    }
}