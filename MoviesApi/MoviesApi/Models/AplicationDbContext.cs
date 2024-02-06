using Microsoft.EntityFrameworkCore;

namespace MoviesApi.Models
{
	public class AplicationDbContext :DbContext
	{
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options ):base(options) 
        {
            
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }



    }
}
