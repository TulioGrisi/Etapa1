using System.ComponentModel.DataAnnotations.Schema;

namespace Etapa1.Models
{
    [Table("Endereco")]
    public class Endereco
    {
        public int id { get; set; }
        public int fkEntidade { get; set; }
        public string logradouro { get; set; }
        public string tipoLogradouro { get; set; }
        public int numero { get; set; }
        public string bairro { get; set; }
        public string cep { get; set; }
        public string municipio { get; set; }
        public string uf { get; set; }
    }
}
