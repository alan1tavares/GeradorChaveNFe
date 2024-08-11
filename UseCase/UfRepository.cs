namespace UseCase
{
    public class UFModel
    {
        public int Codigo { get; set; }
        public required string Nome { get; set; }
    }

    public class UfRepository
    {
        public static List<UFModel> GetItems()
        {
            return new List<UFModel>
            {
                new UFModel { Codigo = 11, Nome = "Rondônia (RO)"},
                new UFModel { Codigo = 12, Nome = "Acre (AC)"},
                new UFModel { Codigo = 13, Nome = "Amazonas (AM)"},
                new UFModel { Codigo = 14, Nome = "Roraima (RR)"},
                new UFModel { Codigo = 15, Nome = "Pará (PA)"},
                new UFModel { Codigo = 16, Nome = "Amapá (AP)"},
                new UFModel { Codigo = 17, Nome = "Tocantins (TO)"},
                new UFModel { Codigo = 21, Nome = "Maranhão (MA)"},
                new UFModel { Codigo = 22, Nome = "Piauí (PI)"},
                new UFModel { Codigo = 23, Nome = "Ceará (CE)"},
                new UFModel { Codigo = 24, Nome = "Rio Grande do Norte (RN)"},
                new UFModel { Codigo = 25, Nome = "Paraíba (PB)"},
                new UFModel { Codigo = 26, Nome = "Pernambuco (PE)"},
                new UFModel { Codigo = 27, Nome = "Alagoas (AL)"},
                new UFModel { Codigo = 28, Nome = "Sergipe (SE)"},
                new UFModel { Codigo = 29, Nome = "Bahia (BA)"},
                new UFModel { Codigo = 31, Nome = "Minas Gerais (MG)"},
                new UFModel { Codigo = 32, Nome = "Espírito Santo (ES)"},
                new UFModel { Codigo = 33, Nome = "Rio de Janeiro (RJ)"},
                new UFModel { Codigo = 35, Nome = "São Paulo (SP)"},
                new UFModel { Codigo = 41, Nome = "Paraná (PR)"},
                new UFModel { Codigo = 42, Nome = "Santa Catarina (SC)"},
                new UFModel { Codigo = 43, Nome = "Rio Grande do Sul (RS)"},
                new UFModel { Codigo = 50, Nome = "Mato Grosso do Sul (MS)"},
                new UFModel { Codigo = 51, Nome = "Mato Grosso (MT)"},
                new UFModel { Codigo = 52, Nome = "Goiás (GO)"},
                new UFModel { Codigo = 53, Nome = "Distrito Federal (DF)"}
            };
        }
    }
}
