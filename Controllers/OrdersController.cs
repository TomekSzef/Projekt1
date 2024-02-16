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
namespace AutoWebApp.Controllers
{
    public class OrdersController : Controller
    {
        private readonly AutoWebAppContext _context;

        public OrdersController(AutoWebAppContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Index", "Home");
            }
            var data = await _context.Order.Include(p => p.SpareParts)
                             .ThenInclude(o => o.Orders).ThenInclude(e => e.Customer).ToListAsync();
            return View(data);
        }   

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!IsAdminLoggedIn())
            {
                return RedirectToAction("Index", "Home");
            }
            if (_context.Order == null)
            {
                return Problem("Entity set 'AutoWebAppContext.Order'  is null.");
            }
            var order = await _context.Order.FindAsync(id);
            if (order != null)
            {
                _context.Order.Remove(order);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
          return (_context.Order?.Any(e => e.OrderID == id)).GetValueOrDefault();
        }

        private bool IsAdminLoggedIn()
        {
            return HttpContext.Session.Get<bool>("_IsAdmin");
        }
    }
}
