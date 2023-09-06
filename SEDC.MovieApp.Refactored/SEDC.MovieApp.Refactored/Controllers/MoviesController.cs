using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEDC.MovieApp.Refactored.DTOs.Movies;
using SEDC.MovieApp.Refactored.Service.Interfaces;
using SEDC.MovieApp.Refactored.Shared.Shared;
using System.Security.Claims;

namespace SEDC.MovieApp.Refactored.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        [Authorize]
        //GET ALL
        [HttpGet]
        public ActionResult<List<MovieDto>> Get()
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                var claims = identity.Claims;

                if (identity.FindFirst("username").Value != "darko123")
                {
                    return StatusCode(StatusCodes.Status403Forbidden);
                }

                return Ok(_movieService.GetAllMovie());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred");
            }
        }

        //GET BY ID
        [HttpGet("{id}")]
        public ActionResult<MovieDto> GetById(int id)
        {
            try
            {
                if(id <= 0)
                {
                    return BadRequest("Id can not be null");
                }
                return Ok(_movieService.GetMovieById(id));
            }
            catch (ResourceNotFoundException)
            {
                return NotFound("The movie was not found");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred");
            }
        }

        //ADD
        [HttpPost]
        public IActionResult AddMovie([FromBody] AddMovieDto addMovieDto)
        {
            try
            {
                _movieService.AddMovie(addMovieDto);
                return StatusCode(StatusCodes.Status201Created, "Movie was created!!");
            }
            catch(GeneralException)
            {
                return BadRequest("Invalid data for movie");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred");
            }
        }

        //DELETE
        [HttpDelete("{id}")]
        public IActionResult DeleteMovie(int id)
        {
            try
            {
                _movieService.DeleteMovie(id);
                return Ok("Movie was deleted");
            }
            catch (ResourceNotFoundException)
            {
                return NotFound($"Movie with id {id} was not found and can not be deleted");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred");
            }
        }

        //UPDATE
        [HttpPut]
        public IActionResult UpdateMovie([FromBody] UpdateMovieDto updateMovieDto)
        {
            try
            {
                _movieService.UpdateMovie(updateMovieDto);
                return StatusCode(StatusCodes.Status204NoContent, "Movie was updated");
            }
            catch (ResourceNotFoundException)
            {
                return NotFound($"Movie with id {updateMovieDto.Id} was not found");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred");
            }
        }

        [HttpGet("filter")]
        public ActionResult<List<MovieDto>> FilterMovies(int? year, int? genre)
        {
            try
            {
               return Ok(_movieService.FilterMovies(year, genre));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred");
            }
        }
    }
}
