using testapp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testapp.Data;
using testapp.Models;
using static testapp.Helper;


namespace testapp.Controllers
{
	public class UserController : Controller
	{
        private readonly ApplicationDbContext _db;

        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
		{
            return View(await _db.Users.ToListAsync());
        }
        
        public async Task<IActionResult> Create(int id = 0)
        {
            if (id == 0)
                return View(new User());
            else
            {
                var transactionModel = await _db.Users.FindAsync(id);
                if (transactionModel == null)
                {
                    return NotFound();
                }
                return View(transactionModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User obj)
        {
            if (ModelState.IsValid)
            {
                //Insert
                if (obj.Id == 0)
                {
                   
                    obj.CreatDateTime = DateTime.Now;
                    _db.Add(obj);
                    await _db.SaveChangesAsync();

                }
                //Update
                else
                {
                    _db.Update(obj);
                    await _db.SaveChangesAsync();
                    
                }
                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll", _db.Users.ToList()) });
            }
            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "Create", User) });
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionModel = await _db.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transactionModel == null)
            {
                return NotFound();
            }

            return View(transactionModel);
        }

        // POST: Transaction/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transactionModel = await _db.Users.FindAsync(id);
            _db.Users.Remove(transactionModel);
            await _db.SaveChangesAsync();
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAll", _db.Users.ToList()) });
        }

        private bool TransactionModelExists(int id)
        {
            return _db.Users.Any(e => e.Id == id);
        }

    }
}
