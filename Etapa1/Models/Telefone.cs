using System.ComponentModel.DataAnnotations.Schema;

namespace Etapa1.Models
{
    [Table("Telefone")]
    public class Telefone
    {
        public int id { get; set; }
        public int fkEntidade { get; set; }
        public string ddd { get; set; }
        public string numero { get; set; }
    }
}
