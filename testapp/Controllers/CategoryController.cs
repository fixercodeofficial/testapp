using Microsoft.AspNetCore.Mvc;
using testapp.Data;
using testapp.Models;

namespace testapp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            //var objCategoryList=_db.Categories.ToList();
            IEnumerable<Category> objCategoryList = _db.Categories;
            return View(objCategoryList);
        }
        //GET
        public IActionResult Create()
        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)                  //ADD AND REDIRECT TO INDEX PAGE
        {
            if(obj.Name==obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Give a proper input");
            }
            if (ModelState.IsValid)                                //   Validation
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Created Successfully";
                return RedirectToAction("Index");
            }
            return View(obj);   
        }
        //GET
        public IActionResult Edit(int? id)
        {
            if(id==null || id==0)
            {
                return NotFound();
            }
            //var category = _db.Categories.Find();
            var categorydbfirst = _db.Categories.FirstOrDefault(c => c.Id == id);
            //var categorydbsingle = _db.Categories.SingleOrDefault(c => c.Id == id);
            if (categorydbfirst==null)
            {
                return NotFound();
            }
            return View(categorydbfirst);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)                  //ADD AND REDIRECT TO INDEX PAGE
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Give a proper input");
            }
            if (ModelState.IsValid)                                //   Validation
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Updated Successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categorydbfirst = _db.Categories.Find(id);
            if (categorydbfirst == null)
            {
                return NotFound();
            }
            return View(categorydbfirst);
        }
        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)                 
        {
            var obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction("Index");          
        }
    }
}
