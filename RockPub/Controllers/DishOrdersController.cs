using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RockPub.DataBase;
using RockPub.Models;

namespace RockPub.Controllers
{
    public class DishOrdersController : Controller
    {
        private readonly DataBaseContext _context;

        public DishOrdersController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: DishOrders
        public async Task<IActionResult> Index()
        {
            var dataBaseContext = _context.DishOrders.Include(d => d.Dish).Include(d => d.Order);
            return View(await dataBaseContext.ToListAsync());
        }

        // GET: DishOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dishOrder = await _context.DishOrders
                .Include(d => d.Dish)
                .Include(d => d.Order)
                .FirstOrDefaultAsync(m => m.DishOrderId == id);
            if (dishOrder == null)
            {
                return NotFound();
            }

            return View(dishOrder);
        }

        // GET: DishOrders/Create
        public IActionResult Create()
        {
            ViewData["DishId"] = new SelectList(_context.Dishes, "DishId", "DishId");
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId");
            return View();
        }

        // POST: DishOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DishOrderId,DishId,OrderId,Quantity")] DishOrder dishOrder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dishOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DishId"] = new SelectList(_context.Dishes, "DishId", "DishId", dishOrder.DishId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", dishOrder.OrderId);
            return View(dishOrder);
        }

        // GET: DishOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dishOrder = await _context.DishOrders.FindAsync(id);
            if (dishOrder == null)
            {
                return NotFound();
            }
            ViewData["DishId"] = new SelectList(_context.Dishes, "DishId", "DishId", dishOrder.DishId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", dishOrder.OrderId);
            return View(dishOrder);
        }

        // POST: DishOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DishOrderId,DishId,OrderId,Quantity")] DishOrder dishOrder)
        {
            if (id != dishOrder.DishOrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dishOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DishOrderExists(dishOrder.DishOrderId))
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
            ViewData["DishId"] = new SelectList(_context.Dishes, "DishId", "DishId", dishOrder.DishId);
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", dishOrder.OrderId);
            return View(dishOrder);
        }

        // GET: DishOrders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dishOrder = await _context.DishOrders
                .Include(d => d.Dish)
                .Include(d => d.Order)
                .FirstOrDefaultAsync(m => m.DishOrderId == id);
            if (dishOrder == null)
            {
                return NotFound();
            }

            return View(dishOrder);
        }

        // POST: DishOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dishOrder = await _context.DishOrders.FindAsync(id);
            _context.DishOrders.Remove(dishOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DishOrderExists(int id)
        {
            return _context.DishOrders.Any(e => e.DishOrderId == id);
        }
    }
}
