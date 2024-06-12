using FilmesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesApi.Data
{
    public class FilmeContext : DbContext
    {
        // base faz a passagem para o construtor da classe extendida(DbContext)
        public FilmeContext(DbContextOptions<FilmeContext> opts) : base(opts){ }
        protected FilmeContext()
        {
        }

        // propriedade de acesso Dbset 
        public DbSet<Movie> Filmes { get; set; }    
    }
}
