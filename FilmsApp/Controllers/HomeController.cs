using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FilmsApp.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace FilmsApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // public IActionResult Index()
        // {

        //     return View();
        // }

        public async Task<IActionResult> Index() {
           
            List<Film> filmsList = new List<Film>();

        using (var httpClientHandler = new HttpClientHandler())
        {
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
            using(var httpClilent = new HttpClient(httpClientHandler)) {
                    using(var response = await httpClilent.GetAsync("https://localhost:5001/api/Films"))
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            filmsList = JsonConvert.DeserializeObject<List<Film>>(apiResponse);
                        }
            }
        }
            return View(filmsList);
        }


        public ViewResult AddFilm() => View();

        [HttpPost]
        public async Task<IActionResult> AddFilm(Film film){
            Film filmReceived = new Film();
        using (var httpClientHandler = new HttpClientHandler())
        {
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
            using(var httpClilent = new HttpClient(httpClientHandler)){
                StringContent content = new StringContent(JsonConvert.SerializeObject(film), Encoding.UTF8, "application/json");
                using(var response = await httpClilent.PostAsync("https://localhost:5001/api/Films", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    filmReceived = JsonConvert.DeserializeObject<Film>(apiResponse);
                }
            }
        }

            return View(filmReceived);
        }

        public ViewResult GetFilm() => View();

        [HttpPost]
        public async Task<IActionResult> GetFilm(string name)
        {
            List<Film> filmReceived = new List<Film>();
            using (var httpClientHandler = new HttpClientHandler())
            {
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
                 using(var httpClilent = new HttpClient(httpClientHandler))
                 {
                      using(var response = await httpClilent.GetAsync("https://localhost:5001/api/Films/"+name))
                      {

                        string apiResponse = await response.Content.ReadAsStringAsync();
                        filmReceived = JsonConvert.DeserializeObject<List<Film>>(apiResponse); 

                      }
                 }
            }

            return View(filmReceived);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        // Similar method is added above in lamda expression form.

        // public IActionResult AddFilm(){

        //     return View();
        // } 

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
