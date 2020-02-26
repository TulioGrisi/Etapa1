using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Etapa1.Models
{
    [Table("Entidade")]
    public class Entidade
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string cpf { get; set; }
        public string cnpj { get; set; }
        public DateTime dtNasc { get; set; }
        public DateTime dtCadastro { get; set; }
        public bool pessoaFisica { get; set; }
        public Guid guid { get; set; }
    }
}
