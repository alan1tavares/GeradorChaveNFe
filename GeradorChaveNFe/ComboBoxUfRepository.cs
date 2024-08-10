using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeradorChaveNFe
{
    class ComboBoxUFModel
    {
        public int Codigo { get; set; }
        public required string Nome { get; set; }
    }

    class ComboBoxUfRepository
    {
        public static List<ComboBoxUFModel> GetItems()
        {
            return new List<ComboBoxUFModel>
            {
                new ComboBoxUFModel { Codigo = 11, Nome = "Rondônia (RO)"},
                new ComboBoxUFModel { Codigo = 12, Nome = "Acre (AC)"},
                new ComboBoxUFModel { Codigo = 13, Nome = "Amazonas (AM)"},
                new ComboBoxUFModel { Codigo = 14, Nome = "Roraima (RR)"},
                new ComboBoxUFModel { Codigo = 15, Nome = "Pará (PA)"},
                new ComboBoxUFModel { Codigo = 16, Nome = "Amapá (AP)"},
                new ComboBoxUFModel { Codigo = 17, Nome = "Tocantins (TO)"},
                new ComboBoxUFModel { Codigo = 21, Nome = "Maranhão (MA)"},
                new ComboBoxUFModel { Codigo = 22, Nome = "Piauí (PI)"},
                new ComboBoxUFModel { Codigo = 23, Nome = "Ceará (CE)"},
                new ComboBoxUFModel { Codigo = 24, Nome = "Rio Grande do Norte (RN)"},
                new ComboBoxUFModel { Codigo = 25, Nome = "Paraíba (PB)"},
                new ComboBoxUFModel { Codigo = 26, Nome = "Pernambuco (PE)"},
                new ComboBoxUFModel { Codigo = 27, Nome = "Alagoas (AL)"},
                new ComboBoxUFModel { Codigo = 28, Nome = "Sergipe (SE)"},
                new ComboBoxUFModel { Codigo = 29, Nome = "Bahia (BA)"},
                new ComboBoxUFModel { Codigo = 31, Nome = "Minas Gerais (MG)"},
                new ComboBoxUFModel { Codigo = 32, Nome = "Espírito Santo (ES)"},
                new ComboBoxUFModel { Codigo = 33, Nome = "Rio de Janeiro (RJ)"},
                new ComboBoxUFModel { Codigo = 35, Nome = "São Paulo (SP)"},
                new ComboBoxUFModel { Codigo = 41, Nome = "Paraná (PR)"},
                new ComboBoxUFModel { Codigo = 42, Nome = "Santa Catarina (SC)"},
                new ComboBoxUFModel { Codigo = 43, Nome = "Rio Grande do Sul (RS)"},
                new ComboBoxUFModel { Codigo = 50, Nome = "Mato Grosso do Sul (MS)"},
                new ComboBoxUFModel { Codigo = 51, Nome = "Mato Grosso (MT)"},
                new ComboBoxUFModel { Codigo = 52, Nome = "Goiás (GO)"},
                new ComboBoxUFModel { Codigo = 53, Nome = "Distrito Federal (DF)"}
            };
        }
    }
}
