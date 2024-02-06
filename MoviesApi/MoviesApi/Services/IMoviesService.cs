namespace MoviesApi.Services
{
	public interface IMoviesService
	{
		Task<IEnumerable<Movie>> GetAll( Byte genreId =0);

		Task<Movie> GetById(int id);

		Task<Movie> Add(Movie movie);

		Movie Update(Movie movie);	
		Movie Delete(Movie movie);




	}
}
