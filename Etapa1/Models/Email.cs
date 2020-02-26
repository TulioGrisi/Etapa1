using System.ComponentModel.DataAnnotations.Schema;

namespace Etapa1.Models
{
    [Table("Email")]
    public class Email
    {
        public int id { get; set; }
        public int fkEntidade { get; set; }
        public string endereco { get; set; }
    }
}
