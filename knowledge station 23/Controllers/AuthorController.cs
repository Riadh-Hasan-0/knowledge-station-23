using knowledge_station_23.Data;
using knowledge_station_23.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.JSInterop.Implementation;
using System;

namespace knowledge_station_23.Controllers
{
    public class AuthorController : Controller
    {
        private readonly ApplicationDbContext Db;
        private readonly IWebHostEnvironment environment;

        public AuthorController(ApplicationDbContext db,IWebHostEnvironment environment)
        {
            this.Db = db;
            this.environment = environment;
        }
        public IActionResult Index()
        {
            IEnumerable<Author> objCategoryList = this.Db.AuthorList;
            return View(objCategoryList);
        }
       
       
       
        private string UploadImage(Author obj)
        {
            string uniqueFileName = string.Empty;
            if (obj.ImagePath != null)
            {
                string uploadFolder = Path.Combine(environment.WebRootPath, "Content/Image/Author/");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + obj.ImagePath.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    obj.ImagePath.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
        //Get
        public IActionResult Create()
        {
            return View();
        }
        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Author obj)
        {
            
                obj.Path = UploadImage(obj);
                Db.AuthorList.Add(obj);
                Db.SaveChanges();
                TempData["Success"] = "New Author created successfully";
                return RedirectToAction("Index");
            
            TempData["Success"] = "failed to save";
            return View(obj);
        }
        public IActionResult Edit(int? id)
        {
            if(id== null||id==0)return NotFound();
            var author = Db.AuthorList.Find(id);
            if (author == null) return NotFound();
            return View(author);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Author obj)
        {
            if (obj == null ) return NotFound();
            
                if (obj.ImagePath != null)
                {
                    obj.Path = UploadImage(obj);
                }
                Db.AuthorList.Update(obj);
                Db.SaveChanges();
                TempData["Success"] = "Author Information Edit Successfully";
                return RedirectToAction("Index");
            
            return View(obj);
           
        }
        public IActionResult Details(int? id)
        {
            if(id==null||id==0)return NotFound();
            var author = Db.AuthorList.Find(id);
            var bookList = Db.BookList.Where(a => a.AuthorId == id).ToList();
            var tuple= Tuple.Create(author, bookList);
            return View(tuple);
        }
        public IActionResult Delete(int? id) {
         
            return View();
        }
        
    }
}
