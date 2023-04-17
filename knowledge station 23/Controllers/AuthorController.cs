using knowledge_station_23.Data;
using knowledge_station_23.Models;
using Microsoft.AspNetCore.Mvc;

namespace knowledge_station_23.Controllers
{
    public class AuthorController : Controller
    {
        private readonly ApplicationDbContext Db;
        public AuthorController(ApplicationDbContext db)
        {
            this.Db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Author> objCategoryList = this.Db.AuthorList;
            return View(objCategoryList);
        }
    }
}
