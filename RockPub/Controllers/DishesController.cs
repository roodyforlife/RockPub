using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RockPub.DataBase;
using RockPub.Enums;
using RockPub.Models;

namespace RockPub.Controllers
{
    public class DishesController : Controller
    {
        private readonly DataBaseContext _context;

        public DishesController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: Dishes
        public async Task<IActionResult> Index(int quantityFrom, int quantityTo, int weightFrom, int weightTo, string name, DishSort sort = DishSort.NameAsc)
        {
            IQueryable<Dish> dataBaseContext = _context.Dishes.Include(d => d.Category);

            if (!String.IsNullOrEmpty(name))
            {
                dataBaseContext = dataBaseContext.Where(x => x.Name.Contains(name));
            }

            if (quantityTo == 0)
            {
                quantityTo = dataBaseContext.Max(x => x.Quantity);
            }

            if (weightTo == 0)
            {
                weightTo = dataBaseContext.Max(x => x.Weight);
            }

            dataBaseContext = dataBaseContext.Where(x => x.Quantity >= quantityFrom);
            dataBaseContext = dataBaseContext.Where(x => x.Quantity <= quantityTo);
            dataBaseContext = dataBaseContext.Where(x => x.Weight>= weightFrom);
            dataBaseContext = dataBaseContext.Where(x => x.Weight <= weightTo);

            switch (sort)
            {
                case DishSort.NameDesc:
                    dataBaseContext = dataBaseContext.OrderByDescending(x => x.Name);
                    break;
                case DishSort.CostAsc:
                    dataBaseContext = dataBaseContext.OrderBy(x => x.Cost);
                    break;
                case DishSort.CostDesc:
                    dataBaseContext = dataBaseContext.OrderByDescending(x => x.Cost);
                    break;
                case DishSort.KilocaloriesAsc:
                    dataBaseContext = dataBaseContext.OrderBy(x => x.Kilocalories);
                    break;
                case DishSort.KilocaloriesDesc:
                    dataBaseContext = dataBaseContext.OrderByDescending(x => x.Kilocalories);
                    break;
                case DishSort.WeightAsc:
                    dataBaseContext = dataBaseContext.OrderBy(x => x.Weight);
                    break;
                case DishSort.CreateDateAsc:
                    dataBaseContext = dataBaseContext.OrderBy(x => x.CreateDate);
                    break;
                case DishSort.CreateDesc:
                    dataBaseContext = dataBaseContext.OrderByDescending(x => x.CreateDate);
                    break;
                default:
                    dataBaseContext = dataBaseContext.OrderBy(x => x.Name);
                    break;
            }

            ViewBag.Sort = (List<SelectListItem>)Enum.GetValues(typeof(DishSort)).Cast<DishSort>()
               .Select(x => new SelectListItem
               {
                   Text = x.GetType()
           .GetMember(x.ToString())
           .FirstOrDefault()
           .GetCustomAttribute<DisplayAttribute>()?
           .GetName(),
                   Value = x.ToString(),
                   Selected = (x == sort)
               }).ToList();

            ViewBag.Name = name;
            ViewBag.QuantityFrom = quantityFrom;
            ViewBag.QuantityTo = quantityTo;
            ViewBag.WeightFrom = weightFrom;
            ViewBag.WeightTo = weightTo;

            return View(await dataBaseContext.ToListAsync());
        }

        // GET: Dishes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = await _context.Dishes
                .Include(d => d.Category)
                .FirstOrDefaultAsync(m => m.DishId == id);
            if (dish == null)
            {
                return NotFound();
            }

            return View(dish);
        }

        // GET: Dishes/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId");
            return View();
        }

        // POST: Dishes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DishId,Name,Description,Quantity,Cost,Kilocalories,Protein,Fat,Carbohydrates,Weight,CreateDate,CategoryId")] Dish dish)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dish);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", dish.CategoryId);
            return View(dish);
        }

        // GET: Dishes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = await _context.Dishes.FindAsync(id);
            if (dish == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", dish.CategoryId);
            return View(dish);
        }

        // POST: Dishes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DishId,Name,Description,Quantity,Cost,Kilocalories,Protein,Fat,Carbohydrates,Weight,CreateDate,CategoryId")] Dish dish)
        {
            if (id != dish.DishId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dish);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DishExists(dish.DishId))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", dish.CategoryId);
            return View(dish);
        }

        // GET: Dishes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = await _context.Dishes
                .Include(d => d.Category)
                .FirstOrDefaultAsync(m => m.DishId == id);
            if (dish == null)
            {
                return NotFound();
            }

            return View(dish);
        }

        // POST: Dishes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dish = await _context.Dishes.FindAsync(id);
            _context.Dishes.Remove(dish);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DishExists(int id)
        {
            return _context.Dishes.Any(e => e.DishId == id);
        }
    }
}
