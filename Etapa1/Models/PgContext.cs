using System.Data.Entity;

namespace Etapa1.Models
{
    public class PgContext:DbContext
    {
        public PgContext() : base("PgCadastro")
        {
            
        }

        //Tabelas da base de dados
        public DbSet<Entidade> Entidade { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<Telefone> Telefone { get; set; }
        public DbSet<Email> Email { get; set; }
    }
}
