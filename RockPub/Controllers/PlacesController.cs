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
    public class PlacesController : Controller
    {
        private readonly DataBaseContext _context;

        public PlacesController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: Places
        public async Task<IActionResult> Index(string hallName, int placeNumber, PlaceSort sort = PlaceSort.PlaceNumberAsc)
        {
            IQueryable<Place> places = _context.Places.Include(x => x.Hall);

            if (!String.IsNullOrEmpty(hallName))
            {
                places = places.Where(x => x.Hall.HallName.Contains(hallName));
            }

            if (placeNumber != 0)
            {
                places = places.Where(x => x.PlaceNumber == placeNumber);
            }

            switch (sort)
            {
                case PlaceSort.PlaceNumberDesc:
                    places = places.OrderByDescending(x => x.PlaceNumber);
                    break;
                case PlaceSort.HallNameAsc:
                    places = places.OrderBy(x => x.Hall.HallName);
                    break;
                case PlaceSort.HallNameDesc:
                    places = places.OrderByDescending(x => x.Hall.HallName);
                    break;
                default:
                    places = places.OrderBy(x => x.PlaceNumber);
                    break;
            }

            ViewBag.Sort = (List<SelectListItem>)Enum.GetValues(typeof(PlaceSort)).Cast<PlaceSort>()
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

            ViewBag.HallName = hallName;
            ViewBag.PlaceNumber = placeNumber;

            return View(await places.ToListAsync());
        }

        // GET: Places/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var place = await _context.Places
                .Include(p => p.Hall)
                .FirstOrDefaultAsync(m => m.PlaceId == id);
            if (place == null)
            {
                return NotFound();
            }

            return View(place);
        }

        // GET: Places/Create
        public IActionResult Create()
        {
            ViewData["HallId"] = new SelectList(_context.Halls, "HallId", "HallName");
            return View();
        }

        // POST: Places/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlaceId,PlaceNumber,HallId")] Place place)
        {
            if (ModelState.IsValid)
            {
                _context.Add(place);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["HallId"] = new SelectList(_context.Halls, "HallId", "HallName", place.HallId);
            return View(place);
        }

        // GET: Places/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var place = await _context.Places.FindAsync(id);
            if (place == null)
            {
                return NotFound();
            }
            ViewData["HallId"] = new SelectList(_context.Halls, "HallId", "HallName", place.HallId);
            return View(place);
        }

        // POST: Places/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlaceId,PlaceNumber,HallId")] Place place)
        {
            if (id != place.PlaceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(place);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlaceExists(place.PlaceId))
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
            ViewData["HallId"] = new SelectList(_context.Halls, "HallId", "HallName", place.HallId);
            return View(place);
        }

        // GET: Places/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var place = await _context.Places
                .Include(p => p.Hall)
                .FirstOrDefaultAsync(m => m.PlaceId == id);
            if (place == null)
            {
                return NotFound();
            }

            return View(place);
        }

        // POST: Places/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var place = await _context.Places.FindAsync(id);
            _context.Places.Remove(place);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlaceExists(int id)
        {
            return _context.Places.Any(e => e.PlaceId == id);
        }
    }
}
