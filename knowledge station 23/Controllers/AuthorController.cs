using knowledge_station_23.Data;
using knowledge_station_23.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using System;

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
        //Get
        public IActionResult Create()
        {
            return View();
        }
        //post
        public IActionResult Create(Author obj)
        {
            var uniqueFileName = UploadImage(obj);
            Db.AuthorList.Add(obj);
            Db.SaveChanges();
            return RedirectToAction("Index");
        }
        private string UploadImage(Author obj)
        {
            string uniqueFileName = string.Empty;
            if (obj.ImagePath != null)
            {
                string uploadFolder = Path.Combine(environment.WebRootPath, "Image/");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + obj.ImagePath.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    obj.ImagePath.CopyTo(fileStream);
                }
            }
           
            return uniqueFileName;
        }
    }
}
