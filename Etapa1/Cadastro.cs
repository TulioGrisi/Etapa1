using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etapa1
{
    public class Cadastro
    {
        public int seq { get; set; }
        public string nomeCompleto { get; set; }
        public string cpfCnpj { get; set; }
        public DateTime dtNasc { get; set; }
        public DateTime dtCadastro { get; set; }
        public string tEndereco { get; set; }
        public string logradouro { get; set; }
        public int nro { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public string uf { get; set; }
        public string cep { get; set; }
        public string ddd1 { get; set; }
        public string fone1 { get; set; }
        public string ddd2 { get; set; }
        public string fone2 { get; set; }
        public string cel { get; set; }
        public string email { get; set; }
    }
}
