using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AutoWebApp.Data;
using AutoWebApp.Models;
using AutoWebApp.Tools;
using Microsoft.AspNetCore.Http;

namespace AutoWebApp.Controllers
{
    public class SparePartsController : Controller
    {
        private readonly AutoWebAppContext _context;

        public SparePartsController(AutoWebAppContext context)
        {
            _context = context;
        }

        //GET: SpareParts/ShowParts
        public IActionResult ShowParts(string? carBrand)
        {
            if(carBrand == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return _context.SparePart != null ?
                          View(_context.SparePart.OrderBy(s => s.PartName).Where(m => m.VehicleModel.StartsWith(carBrand))) :
                          Problem("Entity set 'AutoWebAppContext.SparePart'  is null.");
        }

        //GET: SpareParts/ShowOrder
        public IActionResult ShowOrder()
        {
            if (!IsUserLoggedIn())
            {
                return View("Index");
            }
            var currentOrder = HttpContext.Session.Get<List<int>>("_Order");
            if (currentOrder == null)
            {
                currentOrder = new List<int>();
            }
            HttpContext.Session.Set<List<int>>("_Order", currentOrder);

            return _context.SparePart != null ?
                          View(_context.SparePart.OrderByDescending(s => s.PartName)) :
                          Problem("Entity set 'AutoWebAppContext.SparePart'  is null.");
        }

        //GET: SpareParts/AddToOrder
        public IActionResult AddToOrder(int id, bool delete=false)
        {
            var currentOrder = HttpContext.Session.Get<List<int>>("_Order");
            if(currentOrder == null)
            {
                currentOrder = new List<int>();
            }
            if (delete)
            {
                currentOrder.Remove(id);
            }
            else
            {
                currentOrder.Add(id);
            }
            HttpContext.Session.Set<List<int>>("_Order", currentOrder);
            return _context.SparePart != null ?
                          View("ShowOrder",_context.SparePart) :
                          Problem("Entity set 'AutoWebAppContext.SparePart'  is null.");
        }

        //Post: SpareParts/Order
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Order()
        {
            if(HttpContext.Session.GetInt32("_CurrentUserID") == null || HttpContext.Session.Get<List<int>>("_Order").Count <= 0)
            {
                return RedirectToAction("Index", "Home");
            }
            var currentUser = HttpContext.Session.GetInt32("_CurrentUserID") ?? 0;
            Order currentOrder = new();
            currentOrder.CustomerID = currentUser;
            _context.Order.Add(currentOrder);
            _context.SaveChanges();

            OrderSparePart orderPart = new();
            foreach(var p in HttpContext.Session.Get<List<int>>("_Order"))
            {
                currentOrder.SpareParts.Add(_context.SparePart.FirstOrDefault(e => e.PartID == p));
                _context.SaveChanges();
            }
            HttpContext.Session.Set<List<int>>("_Order", new());
            return RedirectToAction("Index", "Home");
        }


        // GET: SpareParts
        public async Task<IActionResult> Index()
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Index", "Home");
            }
            return _context.SparePart != null ? 
                          View(await _context.SparePart.OrderByDescending(s => s.PartName).ToListAsync()) :
                          Problem("Entity set 'AutoWebAppContext.SparePart'  is null.");
        }

        // GET: SpareParts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SparePart == null)
            {
                return NotFound();
            }

            var sparePart = await _context.SparePart
                .FirstOrDefaultAsync(m => m.PartID == id);
            if (sparePart == null)
            {
                return NotFound();
            }

            return View(sparePart);
        }

        // GET: SpareParts/Create
        public IActionResult Create()
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        // POST: SpareParts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PartID,PartName,VehicleModel,Description,Price")] SparePart sparePart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sparePart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sparePart);
        }

        // GET: SpareParts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null || _context.SparePart == null)
            {
                return NotFound();
            }

            var sparePart = await _context.SparePart.FindAsync(id);
            if (sparePart == null)
            {
                return NotFound();
            }
            return View(sparePart);
        }

        // POST: SpareParts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PartID,PartName,VehicleModel,Description,Price")] SparePart sparePart)
        {
            if (id != sparePart.PartID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sparePart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SparePartExists(sparePart.PartID))
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
            return View(sparePart);
        }

        // GET: SpareParts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null || _context.SparePart == null)
            {
                return NotFound();
            }

            var sparePart = await _context.SparePart
                .FirstOrDefaultAsync(m => m.PartID == id);
            if (sparePart == null)
            {
                return NotFound();
            }

            return View(sparePart);
        }

        // POST: SpareParts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SparePart == null)
            {
                return Problem("Entity set 'AutoWebAppContext.SparePart'  is null.");
            }
            var sparePart = await _context.SparePart.FindAsync(id);
            if (sparePart != null)
            {
                _context.SparePart.Remove(sparePart);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SparePartExists(int id)
        {
          return (_context.SparePart?.Any(e => e.PartID == id)).GetValueOrDefault();
        }

        private bool IsAdminLoggedIn()
        {
            return HttpContext.Session.Get<bool>("_IsAdmin");
        }

        private bool IsUserLoggedIn()
        {
            return HttpContext.Session.Get<bool>("_IsLoggedIn");
        }
    }


}
