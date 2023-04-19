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
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _environment;

        public AuthorController(ApplicationDbContext db,IWebHostEnvironment environment)
        {
            this._db = db;
            this._environment = environment;
        }
        public IActionResult Index()
        {
            IEnumerable<Author> objAuthorList = this._db.AuthorList;
            return View(objAuthorList);
        }
       
       
       
        private string UploadImage(Author obj)
        {
            string uniqueFileName = string.Empty;

            if (obj.ImagePath != null)
            {
                string uploadFolder = Path.Combine(_environment.WebRootPath, "Content/Image/Author/");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + obj.ImagePath.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    obj.ImagePath.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
        private void DeleteImage(string path)
        {
            string filePath = Path.Combine(_environment.WebRootPath, "Content/Image/Author/", path);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

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
                _db.AuthorList.Add(obj);
                _db.SaveChanges();
                TempData["Success"] = "New Author created successfully";
                return RedirectToAction("Index");
            
            
        }
        public IActionResult Edit(int? id)
        {
            if(id== null||id==0)return NotFound();
            var author = _db.AuthorList.Find(id);
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
                    DeleteImage(obj.Path);
                    obj.Path = UploadImage(obj);
                }
                

                _db.AuthorList.Update(obj);
                _db.SaveChanges();
                TempData["Success"] = "Author Information Edit Successfully";
                return RedirectToAction("Index");
            
           
        }
        public IActionResult Details(int? id)
        {
            if(id==null||id==0)return NotFound();
            var author = _db.AuthorList.Find(id);
            var bookList = _db.BookList.Where(a => a.AuthorId == id).ToList();
            var tuple= Tuple.Create(author, bookList);
            return View(tuple);
        }
        
        public IActionResult Delete(int? id) {
            if(id==null ||id==0)return NotFound();
            var author = _db.AuthorList.Find(id);
            if(author == null) return NotFound();
            var bookList = _db.BookList.Where(a => a.AuthorId == id).ToList();
            foreach(var obj in bookList) {
                DeleteImage(obj.Path);
                _db.BookList.Remove(obj); 
            }
            DeleteImage(author.Path);
            _db.AuthorList.Remove(author);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        
    }
}
