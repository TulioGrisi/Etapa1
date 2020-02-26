using Etapa1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Etapa1
{
    static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //MessageBox.Show(CadastroModel.GetCadastro(2).nomeCompleto.ToString());

            /*
            PgCadastroContext cc = new PgCadastroContext();
            Entidade en = new Entidade();
            en.nome = "a";
            cc.Entidade.Add(en);
            cc.Database.ExecuteSqlCommand("INSERT INTO public.\"Entidade\"(nome)VALUES('a'); ");
            */

            Application.Run(new Form1());
        }
    }
}
