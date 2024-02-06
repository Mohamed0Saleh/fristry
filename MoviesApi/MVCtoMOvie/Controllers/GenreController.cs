using Microsoft.AspNetCore.Mvc;
using MVCtoMOvie.Models;
using Newtonsoft.Json;

namespace MVCtoMOvie.Controllers
{
    public class GenreController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:44360/api");
        private readonly HttpClient _client;
        public GenreController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<GenreViewModel> list = new List<GenreViewModel>();
            HttpResponseMessage respon = _client.GetAsync(baseAddress + "/Genres/GetAllAsync").Result;
            if (respon.IsSuccessStatusCode)
            {
                string data =respon.Content.ReadAsStringAsync().Result;
                list=JsonConvert.DeserializeObject<List<GenreViewModel>>(data);

            }
            return View(list);
        }
    }
}
