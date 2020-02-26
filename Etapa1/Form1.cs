using Etapa1.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Etapa1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btTransferir_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Deseja transferir os dados do banco SQLServer para o Banco Postgres?", "Transferência de dados", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                //Lista para verificar se um novo guid é único
                List<Guid> guids = new List<Guid>();
                //Listas para impedir a que o mesmo email seja cadastrado mais de uma vez para a mesma entidade
                List<int> ents = new List<int>();
                List<string> emails = new List<string>();

                for (int i = 1; i<=7; i++)
                {
                    PgContext pc = new PgContext();
                    Entidade ent = new Entidade();
                    Endereco end = new Endereco();
                    Telefone tel = new Telefone();
                    Email em = new Email();

                    var origem = CadastroModel.GetCadastro(i);

                    if (origem != null)
                    {
                        DataSender ds = new DataSender();

                        //Entidade
                        ent = ds.CriaEntidade(ent, origem, pc, guids);
                        guids.Add(ent.guid);

                        //Foreign key
                        int fk = ds.GetForeignKey(pc, ent.guid);
                        end.fkEntidade = fk;
                        tel.fkEntidade = fk;
                        em.fkEntidade = fk;

                        //Endereco
                        ds.CriaEndereco(end, origem, pc);

                        //Telefone
                        ds.CriaTelefone(tel, origem, pc);

                        //Email
                        em = ds.CriaEmail(em, origem, pc, ents, emails);

                        ents.Add(fk);
                        emails.Add(em.endereco);
                    }
                }
                MessageBox.Show("Dados transferidos com sucesso!");
            }
            
        }
    }
}
