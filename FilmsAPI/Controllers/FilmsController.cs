using System.Collections.Generic;
using FilmsApi.Model;
using FilmsApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FilmsApi.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class FilmsController : Controller
    {   
        private readonly  FilmService _filmService;
        public FilmsController(FilmService filmService){
            _filmService = filmService;
        }
         
    //     [HttpGet]
    //     public ActionResult<List<Film>> Get() =>
    //         _filmService.Get();


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("{name}", Name = "GetFilmName")]

        public ActionResult<List<Film>> GetFilmName(string name){

            var film = _filmService.GetFilmName(name.ToLower());

            if (film == null){
                return NotFound();
            }

            return film;
        }

        [HttpGet("{id:length(24)}", Name = "GetFilm")]
        
        public ActionResult<Film> Get(string id){

            var film = _filmService.Get(id);

            if (film == null){
               
                return NotFound();
           
            }

            return film;
        } 


        
        [HttpGet]
       public ActionResult<List<Film>> GetFilms() =>    
                _filmService.GetFilms("Comedy");

        [HttpPost]
        public ActionResult<Film> Create(Film film)
        {
            _filmService.Create(film);

            return CreatedAtRoute("GetFilm", new {id = film.Id.ToString()}, film);
        }   


        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Film filmIn)
        {
            var film = _filmService.Get(id);

            if (film == null)
            {
                return NotFound();
            }

            _filmService.Update(id, filmIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var book = _filmService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _filmService.Remove(book.Id);

            return NoContent();
        }
    }
}