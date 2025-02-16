using Microsoft.AspNetCore.Mvc;

namespace Compnay.Demo.PL.Controllers
{
    public class MoviesController : Controller
    {
        public string GetMovie(int id, string title)
        {
            return $"id = {(id > 0 ? id : "Not Found")}, title = {title}";
        }

    }
}
