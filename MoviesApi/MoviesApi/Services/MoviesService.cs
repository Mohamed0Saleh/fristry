using Microsoft.EntityFrameworkCore;
using MoviesApi.Models;

namespace MoviesApi.Services
{
	public class MoviesService : IMoviesService

	{
		private readonly AplicationDbContext _context;

		public MoviesService(AplicationDbContext context)
		{
			_context = context;
		}

		public async Task<Movie> Add(Movie movie)
		{
			  await _context.Movies.AddAsync(movie);
			_context.SaveChanges();
			return movie;
		}

		public Movie Delete(Movie movie)
		{
			_context.Remove(movie);
			_context.SaveChanges();

			return movie;
		}

		public async Task<IEnumerable<Movie>> GetAll(Byte genreId = 0)
		{
			return (IEnumerable<Movie>)await _context.Movies
				.Where(m=>m.GenreId == genreId || genreId==0)
				.Include(m => m.Genre).OrderBy(m => m.Rate).ToListAsync();
		}

		public async Task<Movie> GetById(int id)
		{
			return await _context.Movies.Include(m => m.Genre).SingleOrDefaultAsync(m => m.Id == id);
		}

		public Movie Update(Movie movie)
		{
			_context.Update(movie);
			_context.SaveChanges();

			return movie;
		}
	}
}
