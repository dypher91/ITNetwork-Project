using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PojisteniApp.Data;
using PojisteniApp.Models;

namespace PojisteniApp.Controllers
{
    [Authorize(Roles="admin")]
    public class InsuracePersonDatasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InsuracePersonDatasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InsuracePersonDatas
        /*
        public async Task<IActionResult> Index()
        {
              return _context.InsuracePersonData != null ? 
                          View(await _context.InsuracePersonData.Include(p => p.PersonInsurances).Include(p => p.Insurances)
                          .Include(p => p.InsuranceEvents).ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.InsuracePersonData'  is null.");
        }
        */

        public async Task<IActionResult> Index(string sortOrder,string currentFilter,string searchString,int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["AdressSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var students = from s in _context.InsuracePersonData
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.Address);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.Address);
                    break;
                default:
                    students = students.OrderBy(s => s.LastName);
                    break;
            }

            int pageSize = 5;
            return View(await PaginatedList<InsuracePersonData>.CreateAsync(students.AsNoTracking(), pageNumber ?? 1, pageSize));
        }




        // GET: InsuracePersonDatas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.InsuracePersonData == null)
            {
                return NotFound();
            }

            var insuracePersonData = await _context.InsuracePersonData
                .Include(p => p.PersonInsurances)
                .Include(p => p.Insurances)
                .Include(p => p.InsuranceEvents)
                .FirstOrDefaultAsync(m => m.PersonId == id);
            if (insuracePersonData == null)
            {
                return NotFound();
            }

            return View(insuracePersonData);
        }

        // GET: InsuracePersonDatas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: InsuracePersonDatas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonId,FirstName,LastName,SocialNumber,Email,PhoneNumber,Address,City,PostZipCode")] InsuracePersonData insuracePersonData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(insuracePersonData);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "PersonInsurances");
            }
            return View(insuracePersonData);
        }

        // GET: InsuracePersonDatas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.InsuracePersonData == null)
            {
                return NotFound();
            }

            var insuracePersonData = await _context.InsuracePersonData.FindAsync(id);
            if (insuracePersonData == null)
            {
                return NotFound();
            }
            return View(insuracePersonData);
        }

        // POST: InsuracePersonDatas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonId,FirstName,LastName,SocialNumber,Email,PhoneNumber,Address,City,PostZipCode")] InsuracePersonData insuracePersonData)
        {
            if (id != insuracePersonData.PersonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(insuracePersonData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsuracePersonDataExists(insuracePersonData.PersonId))
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
            return View(insuracePersonData);
        }

        // GET: InsuracePersonDatas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.InsuracePersonData == null)
            {
                return NotFound();
            }

            var insuracePersonData = await _context.InsuracePersonData
                .FirstOrDefaultAsync(m => m.PersonId == id);
            if (insuracePersonData == null)
            {
                return NotFound();
            }

            return View(insuracePersonData);
        }

        // POST: InsuracePersonDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.InsuracePersonData == null)
            {
                return Problem("Entity set 'ApplicationDbContext.InsuracePersonData'  is null.");
            }
            var insuracePersonData = await _context.InsuracePersonData.FindAsync(id);
            if (insuracePersonData != null)
            {
                _context.InsuracePersonData.Remove(insuracePersonData);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InsuracePersonDataExists(int id)
        {
          return (_context.InsuracePersonData?.Any(e => e.PersonId == id)).GetValueOrDefault();
        }
    }
}
