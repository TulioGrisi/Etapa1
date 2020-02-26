using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Etapa1.Models
{
    public class DataSender
    {
        public Entidade CriaEntidade(Entidade ent, Cadastro origem, PgContext pc, List<Guid> guids)
        {
            //Nome
            ent.nome = origem.nomeCompleto;

            //CPF/CNPJ
            var doc = RemoveMask(origem.cpfCnpj);
            if (Convert.ToInt32(doc.Length.ToString()) > 11)
            {
                ent.cnpj = doc;
                ent.pessoaFisica = false;
            }
            else
            {
                ent.cpf = doc;
                ent.pessoaFisica = true;
            }

            //Data de nascimento
            ent.dtNasc = origem.dtNasc;
            //Data do cadastro
            ent.dtCadastro = origem.dtCadastro;

            //Guid
            ent.guid = Guid.NewGuid();
            //Verifica se o guid já foi utilizado
            if (guids.Count() > 0)
            {
                bool existe;
                do
                {
                    existe = false;
                    foreach (Guid g in guids)
                    {
                        if (ent.guid == g)
                        {
                            existe = true;
                            ent.guid = Guid.NewGuid();
                            break;
                        }
                    }
                } while (existe == true);
            }

            char pf;
            if (ent.pessoaFisica)
                pf = '1';
            else
                pf = '0';

            pc.Database.ExecuteSqlCommand("INSERT INTO dbo.\"Entidade\"(nome, cpf, cnpj, \"dtNasc\", \"dtCadastro\", \"pessoaFisica\", guid) VALUES(" +
            "'" + ent.nome + "', " +
            "'" + ent.cpf + "', " +
            "'" + ent.cnpj + "', " +
            "'" + ent.dtNasc + "', " +
            "'" + ent.dtCadastro + "', " +
            "'" + pf + "', " +
            "'" + ent.guid + "');");
            
            return ent;
        }

        public void CriaEndereco(Endereco end, Cadastro origem, PgContext pc)
        {
            //Logradouro
            end.logradouro = origem.logradouro;
            //Tipo do logradouro
            end.tipoLogradouro = origem.tEndereco;
            //Numero
            end.numero = origem.nro;

            //Bairro
            end.bairro = origem.bairro;
            if (end.bairro.Length > 15)
            {
                end.bairro = end.bairro.Remove(15);
            }
            
            //CEP
            end.cep = RemoveMask(origem.cep);
            //Municipio
            end.municipio = origem.cidade;
            //UF
            end.uf = origem.uf;

            pc.Database.ExecuteSqlCommand("INSERT INTO dbo.\"Endereco\"(\"fkEntidade\", logradouro, \"tipoLogradouro\", numero, bairro, cep, municipio, uf) VALUES(" +
                "" + end.fkEntidade + ", " +
                "'" + end.logradouro + "', " +
                "'" + end.tipoLogradouro + "', " +
                "" + end.numero + ", " +
                "'" + end.bairro + "', " +
                "'" + end.cep + "', " +
                "'" + end.municipio + "', " +
                "'" + end.uf + "');");
        }

        public void CriaTelefone(Telefone tel, Cadastro origem, PgContext pc)
        {
            //Se for pessoa física, priorize celular, se não, priorize telefone
            if (pc.Entidade.Find(tel.fkEntidade).pessoaFisica && origem.cel != "")
            {
                tel.numero = origem.cel;
                if (origem.ddd1 != "")
                    tel.ddd = origem.ddd1;
                else
                    tel.ddd = origem.ddd2;
            }
            //Priorizar telefone
            else
            {
                //Se houver telefone1 e ddd1, adcione os dois
                if (origem.fone1 != "" && origem.ddd1 != "")
                {
                    tel.ddd = origem.ddd1;
                    tel.numero = origem.fone1;
                }
                else
                {
                    //Se houver telefone2 e ddd2, adcione os dois
                    if (origem.fone2 != "" && origem.ddd2 != "")
                    {
                        tel.ddd = origem.ddd2;
                        tel.numero = origem.fone2;
                    }
                    //Se não houver telefone1 e ddd1 ou telefone2 e ddd2
                    else
                    {
                        //Se houver ddd1, adcione. Senão, adcione ddd2
                        if (origem.ddd1 != "")
                            tel.ddd = origem.ddd1;
                        else
                            tel.ddd = origem.ddd2;

                        //Se houver telefone1, adcione
                        if (origem.fone1 != "")
                            tel.numero = origem.fone1;
                        //Se não houver telefone1
                        else
                        {
                            //Se houver teledone2, adcione. Senão, adcione celular
                            if (origem.fone2 != "")
                                tel.numero = origem.fone2;
                            else
                                tel.numero = origem.cel;
                        }
                    }
                }
            }

            tel.numero = RemoveMask(tel.numero);

            pc.Database.ExecuteSqlCommand("INSERT INTO dbo.\"Telefone\"(\"fkEntidade\", ddd, numero) VALUES(" +
                "" + tel.fkEntidade + ", " +
                "'" + tel.ddd + "', " +
                "'" + tel.numero + "');");
        }

        public Email CriaEmail(Email em, Cadastro origem, PgContext pc, List<int> ents, List<string> emails)
        {
            //Verifica se há email na origem
            if (origem.email != "")
            {
                bool existe = false;
                int cont = 0;
                //Verifica se o email já foi enviado ao banco relacionado a mesma entidade
                foreach (int e in ents)
                {
                    if (e == em.fkEntidade && emails[cont] == origem.email)
                        existe = true;

                    cont++;
                }
                //Se não existir a relação, envia ao banco
                if (!existe)
                {
                    em.endereco = origem.email;
                    pc.Database.ExecuteSqlCommand("INSERT INTO dbo.\"Email\"(\"fkEntidade\", endereco) VALUES(" +
                        "" + em.fkEntidade + ", " +
                        "'" + em.endereco + "');");
                }
            }
            return em;
        }

        public int GetForeignKey(PgContext pc, Guid guid)
        {
            Guid guid1;
            int cont = 0;
            //Encontra a o id da entidade correspondente através do guid
            do
            {
                bool nul = true;
                while (nul)
                {
                    cont++;
                    nul = false;
                    try
                    {
                        if (pc.Entidade.Find(cont).guid == null)
                        {

                        }
                    }
                    catch (NullReferenceException e)
                    {
                        nul = true;
                    }
                }

                guid1 = pc.Entidade.Find(cont).guid;
            } while (guid1 != guid);

            return cont;
        }

        private string RemoveMask(string s)
        {
            s = s.Replace(".", "");
            s = s.Replace("-", "");
            s = s.Replace("/", "");
            s = s.Replace(" ", "");
            return s;
        }
    }
}
