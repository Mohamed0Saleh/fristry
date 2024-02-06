using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApi.Models;
using MoviesApi.Services;

namespace MoviesApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MoviesController : ControllerBase
	{
		private readonly IMapper _mapper;

		private readonly IMoviesService _moviesService;
		private readonly IGenresService _genresService;


		public MoviesController(IMoviesService moviesService, IGenresService genresService, IMapper mapper)
		{
			_moviesService = moviesService;
			_genresService = genresService;
			_mapper = mapper;
		}

		private new List<string> _allowExtentions = new List<string> { ".jpg", ".png" };
		private long _MaxallposterSize = 1048576;

	 


		[HttpGet("GetAllAsync")]
		public async Task<IActionResult> GetAllAsync()
		{
			var movis = await _moviesService.GetAll();
			var data = _mapper.Map<IEnumerable<MovieDetailsDto>>(movis);
			return Ok(data);



		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var movie = await _moviesService.GetById(id);
			if (movie == null) { return NotFound(); }


			var dto = _mapper.Map<MovieDetailsDto>(movie);


			return Ok(dto);

		}




		[HttpGet("GetByGenreId")]
		public async Task<IActionResult> GetByGenreIdAsync(byte id)
		{


			var movis = await _moviesService.GetAll(id);
			var dto = _mapper.Map<IEnumerable<MovieDetailsDto>>(movis);


			return Ok(dto);
			 

		}



		[HttpPost("CreateAsync")]
		public async Task<IActionResult> CreateAsync([FromForm] MovieDto dto)
		{
			if(dto.Poster ==null) { return BadRequest("poster is requred"); }

			if (!_allowExtentions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
				return BadRequest("only \".jpg\",\".png\"  it allwo");
			if (dto.Poster.Length > _MaxallposterSize) return BadRequest("the max size is 1M");

			var isvalidgenre = await _genresService.IsvaledGenre(dto.GenreId);
			if (!isvalidgenre) { return BadRequest("invaled genre id!!!!"); }

			using var datastreame = new MemoryStream();
			await dto.Poster.CopyToAsync(datastreame);

			var movi = _mapper.Map<Movie>(dto);
			movi.Poster=datastreame.ToArray();

			await _moviesService.Add(movi);

			return Ok(movi);


		}


		[HttpDelete]
		public async Task<IActionResult> DeleteAsync(int id)
		{
			var movie = await _moviesService.GetById(id);

			if (movie == null)
			{
				return NotFound($"not movie with id : {id}");
			}

			_moviesService.Delete(movie);

			return Ok("it delted sucsesfly");
		}




		[HttpPut("{id}")]

		public async Task<IActionResult> UpdateAsync(int id,[FromBody] MovieDto dto){

			var movie = await _moviesService.GetById(id);

			if (movie == null)
			{
				return NotFound($"not movie with id : {id}");
			}

			var isvalidgenre = await _genresService.IsvaledGenre(dto.GenreId);
			if (!isvalidgenre) { return BadRequest("invaled genre id!!!!"); }


			if (dto.Poster != null) {
				if (!_allowExtentions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
					return BadRequest("only \".jpg\",\".png\"  it allwo");
				if (dto.Poster.Length > _MaxallposterSize) return BadRequest("the max size is 1M");
				using var datastreame = new MemoryStream();
				await dto.Poster.CopyToAsync(datastreame);

				movie.Poster= datastreame.ToArray();
			}


			movie.Title = dto.Title;
			movie.Year = dto.Year;
			movie.Storeline = dto.Storeline;
			movie.Rate = dto.Rate;
			movie.GenreId = dto.GenreId;


			_moviesService.Update(movie);
			return Ok(movie);



		}



	}
}
