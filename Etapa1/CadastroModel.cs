using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etapa1
{
    class CadastroModel
    {
        public static Cadastro GetCadastro(int idx)
        {
            Cadastro cadastro = null;
            List<Cadastro> cadastros = new List<Cadastro>();
            using (SqlConnection con = new SqlConnection(@"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=teste;Data Source=localhost\SQLEXPRESS"))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM cadastro WHERE seq = " + idx, con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr != null)
                        {
                            while (dr.Read())
                            {
                                cadastro = new Cadastro();
                                cadastro.seq = Convert.ToInt32(dr["seq"]);
                                cadastro.nomeCompleto = dr["nomeCompleto"].ToString();
                                cadastro.cpfCnpj = dr["cpfCnpj"].ToString();
                                cadastro.dtNasc = Convert.ToDateTime(dr["dtNasc"]);
                                cadastro.dtCadastro = Convert.ToDateTime(dr["dtCadastro"]);
                                cadastro.tEndereco = dr["tEndereco"].ToString();
                                cadastro.logradouro = dr["logradouro"].ToString();
                                cadastro.nro = Convert.ToInt32(dr["nro"]);
                                cadastro.bairro = dr["bairro"].ToString();
                                cadastro.cidade = dr["cidade"].ToString();
                                cadastro.uf = dr["uf"].ToString();
                                cadastro.cep = dr["cep"].ToString();
                                cadastro.ddd1 = dr["ddd1"].ToString();
                                cadastro.fone1 = dr["fone1"].ToString();
                                cadastro.ddd2 = dr["ddd2"].ToString();
                                cadastro.fone2 = dr["fone2"].ToString();
                                cadastro.cel = dr["cel"].ToString();
                                cadastro.email = dr["email"].ToString();
                            }
                        }
                        return cadastro;
                    }
                }
            }
        }
    }
}
