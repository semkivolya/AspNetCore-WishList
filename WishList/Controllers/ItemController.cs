using Microsoft.AspNetCore.Mvc;
using WishList.Data;
using WishList.Models;
using Microsoft.EntityFrameworkCore;

namespace WishList.Controllers
{
    public class ItemController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItemController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(nameof(Index), _context.Items.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(nameof(Create));
        }

        [HttpPost]
        public IActionResult Create(Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }


        public IActionResult Delete(int id)
        {
            var item = _context.Items.FirstOrDefaultAsync(x => x.Id == id);
            if (item != null)
            {
                _context.Remove(item);
                return RedirectToAction(nameof(Index));
            }
            return NotFound();
        }
    }
}
