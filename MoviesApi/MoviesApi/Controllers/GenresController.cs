using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Services;

namespace MoviesApi.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class GenresController : ControllerBase
	{
		private readonly IGenresService _genresService;

		public GenresController(IGenresService genresService)
		{
			_genresService = genresService;
		}

		 

		[HttpGet]
		public async Task<IActionResult> GetAllAsync()
		{
			var generas= await _genresService.GetAll();
			return Ok(generas);



		}
		[HttpPost]
		public async Task<IActionResult> CreateAsync(CreateGenreDto dto)
		{
			var genre =new Genre { Name=dto.Name};

			await _genresService.Add(genre);
			
			return Ok(genre);


		}


		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateAsync( byte id ,[FromBody] CreateGenreDto dto )
		{
			var denre = await _genresService.GetById(id);

			if( denre == null )
			{
				return NotFound($"not Genre with id : {id}");
			}

			denre.Name=dto.Name;
			_genresService.Update(denre);

			return Ok(denre);

		}

		[HttpDelete]
		public async Task<IActionResult> DeleteAsync(byte id)
		{
			var denre = await _genresService.GetById(id);

			if (denre == null)
			{
				return NotFound($"not Genre with id : {id}");
			}

			_genresService.Delete(denre);

			return Ok("it delted sucsesfly");

		}




	}
}
