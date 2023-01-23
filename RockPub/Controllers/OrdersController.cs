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
    public class OrdersController : Controller
    {
        private readonly DataBaseContext _context;

        public OrdersController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var dataBaseContext = _context.Orders.Include(o => o.Place).Include(o => o.Staff);
            return View(await dataBaseContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(x => x.DishOrders)
                .ThenInclude(x => x.Dish)
                .ThenInclude(x => x.Category)
                .Include(o => o.Place)
                .Include(o => o.Staff)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["PlaceId"] = new SelectList(_context.Places, "PlaceId", "PlaceNumber");
            ViewData["StaffId"] = new SelectList(_context.Staffs, "StaffId", "Email");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,Date,PlaceId,IsCompleted,IsPaid,Note,StaffId")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlaceId"] = new SelectList(_context.Places, "PlaceId", "PlaceNumber", order.PlaceId);
            ViewData["StaffId"] = new SelectList(_context.Staffs, "StaffId", "Email", order.StaffId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["PlaceId"] = new SelectList(_context.Places, "PlaceId", "PlaceNumber", order.PlaceId);
            ViewData["StaffId"] = new SelectList(_context.Staffs, "StaffId", "Email", order.StaffId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,Date,PlaceId,IsCompleted,IsPaid,Note,StaffId")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    if (order.IsCompleted)
                    {
                        List<DishOrder> dishOrders = _context.DishOrders.Include(x => x.Dish).Where(x => x.OrderId == order.OrderId).ToList();
                        foreach(DishOrder item in dishOrders)
                        {
                            item.Dish.Quantity -= item.Quantity;
                            _context.Update(item.Dish);
                        }
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
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
            ViewData["PlaceId"] = new SelectList(_context.Places, "PlaceId", "PlaceNumber", order.PlaceId);
            ViewData["StaffId"] = new SelectList(_context.Staffs, "StaffId", "Email", order.StaffId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Place)
                .Include(o => o.Staff)
                .FirstOrDefaultAsync(m => m.OrderId == id);
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
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}
