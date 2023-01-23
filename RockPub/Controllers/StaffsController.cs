using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RockPub.DataBase;
using RockPub.Enums;
using RockPub.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace RockPub.Controllers
{
    public class StaffsController : Controller
    {
        private readonly DataBaseContext _context;

        public StaffsController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: Staffs
        public async Task<IActionResult> Index(string name, string email, DateTime dateFrom, DateTime dateTo, int salaryFrom, int salaryTo,
            StaffSort sort = StaffSort.SurnameAsc)
        {
            IQueryable<Staff> dataBaseContext = _context.Staffs.Include(s => s.Position);
            if (!String.IsNullOrEmpty(name))
            {
                dataBaseContext = dataBaseContext.Where(x => x.Name.Contains(name) || x.Surname.Contains(name) || x.Patronymic.Contains(name));
            }

            if (!String.IsNullOrEmpty(email))
            {
                dataBaseContext = dataBaseContext.Where(x => x.Email.Contains(email));
            }

            dataBaseContext = dataBaseContext.Where(x => x.BirthdayDate >= dateFrom);

            if (dateTo.Year != 1)
            {
                dataBaseContext = dataBaseContext.Where(x => x.BirthdayDate <= dateTo);
            }

            if (salaryTo == 0)
            {
                salaryTo = dataBaseContext.Max(x => x.Salary);
            }

            dataBaseContext = dataBaseContext.Where(x => x.Salary <= salaryTo);
            dataBaseContext = dataBaseContext.Where(x => x.Salary >= salaryFrom);

            switch (sort)
            {
                case StaffSort.NameAsc:
                    dataBaseContext = dataBaseContext.OrderBy(x => x.Name);
                    break;
                case StaffSort.NameDesc:
                    dataBaseContext = dataBaseContext.OrderByDescending(x => x.Name);
                    break;
                case StaffSort.SurnameDesc:
                    dataBaseContext = dataBaseContext.OrderByDescending(x => x.Surname);
                    break;
                case StaffSort.PatronymicAsc:
                    dataBaseContext = dataBaseContext.OrderBy(x => x.Patronymic);
                    break;
                case StaffSort.PatronymicDesc:
                    dataBaseContext = dataBaseContext.OrderByDescending(x => x.Patronymic);
                    break;
                case StaffSort.SalaryAsc:
                    dataBaseContext = dataBaseContext.OrderBy(x => x.Salary);
                    break;
                case StaffSort.BirthdayDateAsc:
                    dataBaseContext = dataBaseContext.OrderByDescending(x => x.Salary);
                    break;
                case StaffSort.EmailAsc:
                    dataBaseContext = dataBaseContext.OrderBy(x => x.Email);
                    break;
                case StaffSort.EmailDesc:
                    dataBaseContext = dataBaseContext.OrderByDescending(x => x.Email);
                    break;
                default:
                    dataBaseContext = dataBaseContext.OrderBy(x => x.Surname);
                    break;
            }

            ViewBag.Sort = (List<SelectListItem>)Enum.GetValues(typeof(StaffSort)).Cast<StaffSort>()
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
            ViewBag.Email = email;
            ViewBag.DateFrom = dateFrom;
            ViewBag.DateTo = dateTo;
            ViewBag.SalaryFrom = salaryFrom;
            ViewBag.SalaryTo = salaryTo;

            return View(await dataBaseContext.ToListAsync());
        }

        // GET: Staffs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _context.Staffs
                .Include(s => s.Position)
                .Include(x => x.Orders)
                .FirstOrDefaultAsync(m => m.StaffId == id);
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }

        // GET: Staffs/Create
        public IActionResult Create()
        {
            ViewData["PositionId"] = new SelectList(_context.Positions, "PositionId", "PositionId");
            return View();
        }

        // POST: Staffs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StaffId,Name,Surname,Patronymic,BirthdayDate,Salary,Phone,Email,EmploymentDate,PositionId")] Staff staff)
        {
            if (ModelState.IsValid)
            {
                _context.Add(staff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PositionId"] = new SelectList(_context.Positions, "PositionId", "PositionId", staff.PositionId);
            return View(staff);
        }

        // GET: Staffs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _context.Staffs.FindAsync(id);
            if (staff == null)
            {
                return NotFound();
            }
            ViewData["PositionId"] = new SelectList(_context.Positions, "PositionId", "PositionId", staff.PositionId);
            return View(staff);
        }

        // POST: Staffs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StaffId,Name,Surname,Patronymic,BirthdayDate,Salary,Phone,Email,EmploymentDate,PositionId")] Staff staff)
        {
            if (id != staff.StaffId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(staff);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StaffExists(staff.StaffId))
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
            ViewData["PositionId"] = new SelectList(_context.Positions, "PositionId", "PositionId", staff.PositionId);
            return View(staff);
        }

        // GET: Staffs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _context.Staffs
                .Include(s => s.Position)
                .FirstOrDefaultAsync(m => m.StaffId == id);
            if (staff == null)
            {
                return NotFound();
            }

            return View(staff);
        }

        // POST: Staffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var staff = await _context.Staffs.FindAsync(id);
            _context.Staffs.Remove(staff);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StaffExists(int id)
        {
            return _context.Staffs.Any(e => e.StaffId == id);
        }
    }
}
