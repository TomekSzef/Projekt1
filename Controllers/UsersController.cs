using AutoWebApp.Data;
using AutoWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoWebApp.Tools;
namespace AutoWebApp.Controllers
{
    public class UsersController : Controller
    {

        private readonly AutoWebAppContext _context;

        public UsersController(AutoWebAppContext context)
        {
            _context = context;
        }
        // GET: UsersController
        public async Task<IActionResult> Index()
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Index", "Home");
            }
            return _context.User != null ?
                          View(await _context.User.ToListAsync()) :
                          Problem("Entity set 'AutoWebAppContext.Users'  is null.");
        }

        //GET: UsersController/Login
        public IActionResult Login()
        {
            return View();
        }

        //POST: UsersController/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login([Bind("UserID,FirstName,LastName,NIP,Email,Password")] User user)
        {
            var currentUser = _context.User.Where(m => m.Email == user.Email && m.Password == user.Password).FirstOrDefault();
            if(currentUser is null)
            {
                return View();
            }
            HttpContext.Session.SetInt32("_CurrentUserID", currentUser.UserID);
            HttpContext.Session.SetString("_CurrentFirstName", currentUser.FirstName);
            HttpContext.Session.Set<bool>("_IsAdmin", IsUserAdmin(currentUser.UserID));
            HttpContext.Session.Set<bool>("_IsLoggedIn", true);
            HttpContext.Session.Set<List<int>>("_Order", new List<int>());
            return RedirectToAction("Index","Home");
        }

        //GET: UsersController/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.SetInt32("_CurrentUserID", 0);
            HttpContext.Session.SetString("_CurrentFirstName", String.Empty);
            HttpContext.Session.Set<bool>("_IsAdmin", false);
            HttpContext.Session.Set<bool>("_IsLoggedIn", false);
            HttpContext.Session.Set<List<int>>("_Order", new List<int>());
            return RedirectToAction("Index", "Home");
        }

        //GET: UsersController/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: SpareParts/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("UserID,FirstName,LastName,NIP,Email,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: SpareParts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: UsersController/Create
        public ActionResult Create()
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: SpareParts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserID,FirstName,LastName,NIP,Email,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: UsersController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserID,FirstName,LastName,NIP,Email,Password")] User user)
        {
            if (id != user.UserID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: UsersController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: UsersController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.User == null)
            {
                return Problem("Entity set 'AutoWebAppContext.User'  is null.");
            }
            var user = await _context.User.FindAsync(id);
            if (user != null)
            {
                _context.User.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return (_context.User?.Any(e => e.UserID == id)).GetValueOrDefault();
        }

        private bool IsUserAdmin(int id)
        {
            return (_context.User?.FirstOrDefault(e => e.UserID == id).IsAdmin ?? false);
        }

        private bool IsAdminLoggedIn()
        {
            return HttpContext.Session.Get<bool>("_IsAdmin");
        }
    }
}
