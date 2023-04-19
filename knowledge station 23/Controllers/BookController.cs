using knowledge_station_23.Data;
using knowledge_station_23.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace knowledge_station_23.Controllers
{
    public class BookController : Controller
    {

        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _environment;
        public BookController(ApplicationDbContext db, IWebHostEnvironment environment)
        {
            this._db = db;
            this._environment = environment;
        }
        public IActionResult Index()
        {
            TempData["Success"] = "Welcome";
            IEnumerable<Book> objBookList = this._db.BookList;
            return View(objBookList);
        }
        private string UploadImage(Book obj)
        {
            string uniqueFileName = string.Empty;

            if (obj.ImagePath != null)
            {
                string uploadFolder = Path.Combine(_environment.WebRootPath, "Content/Image/Book/");
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
            string filePath = Path.Combine(_environment.WebRootPath, "Content/Image/Book/", path);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

        }
        public IActionResult Create()
        {
            var authors = this._db.AuthorList.ToList();
            ViewBag.Authors = new SelectList(authors, "Id", "Name");
            return View(); 
        }
        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book obj)
        {

            obj.Path = UploadImage(obj);
            _db.BookList.Add(obj);
            _db.SaveChanges();
            TempData["Success"] = "New Book added successfully";
            return RedirectToAction("Index");

        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) return NotFound();
            var book = _db.BookList.Find(id);
            if (book == null) return NotFound();
            var authors = this._db.AuthorList.ToList();

            ViewBag.Authors = new SelectList(authors, "Id", "Name", book.AuthorId);

            return View(book);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Book obj)
        {
            if (obj == null) return NotFound();

            if (obj.ImagePath != null)
            {
                DeleteImage(obj.Path);
                obj.Path = UploadImage(obj);
            }


            _db.BookList.Update(obj);
            _db.SaveChanges();
            TempData["Success"] = "Book Information Edit Successfully";
            return RedirectToAction("Index");

        }
        public IActionResult Details(int? id)
        {
            if (id == null || id == 0) return NotFound();
            var book = _db.BookList.Find(id);
            var author = _db.AuthorList.Where(a => a.Id == book.AuthorId).SingleOrDefault();
            var tuple = Tuple.Create(book,author.Id,author.Name);
            return View(tuple);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) return NotFound();
            var book = _db.BookList.Find(id);
            if (book == null) return NotFound();
            
            DeleteImage(book.Path);
            _db.BookList.Remove(book);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
