using System;
using System.Collections.Generic;
using System.Data;
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
    [Authorize(Roles = "admin")]
    public class InsurancesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InsurancesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Insurances
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Insurance.Include(i => i.Person);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Insurances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Insurance == null)
            {
                return NotFound();
            }

            var insurance = await _context.Insurance
                .Include(i => i.Person)
                .FirstOrDefaultAsync(m => m.InsuranceId == id);
            if (insurance == null)
            {
                return NotFound();
            }

            return View(insurance);
        }

        // GET: Insurances/Create
        public IActionResult Create()
        {
            var ins = new Insurance();
            ins.TypeOfInsurance = new List<SelectListItem>();
            ins.TypeOfInsurance.Add(new SelectListItem { Text = "Vyber", Value = "0" });
            ins.TypeOfInsurance.Add(new SelectListItem { Text = "Životní pojištění", Value = "1" });
            ins.TypeOfInsurance.Add(new SelectListItem { Text = "Cestovní pojištění", Value = "2" });
            ins.TypeOfInsurance.Add(new SelectListItem { Text = "Pojištění vozidel", Value = "3" });
            ins.TypeOfInsurance.Add(new SelectListItem { Text = "Pojištění majetku", Value = "4" });
            ins.TypeOfInsurance.Add(new SelectListItem { Text = "Pojištění odpovědnosti", Value = "5" });
            ins.TypeOfInsurance.Add(new SelectListItem { Text = "Úrazové pojištění", Value = "6" });

            //ViewData["DescriptionOfInsurance"] = new SelectList(_context.InsuranceInfo, "DescriptionOfInsurance", "DescriptionOfInsurance");
            ViewData["PersonId"] = new SelectList(_context.InsuracePersonData, "PersonId", "SocialNumber");
            //ViewBag.DescriptionId = new SelectList(_context.InsuranceInfo, "DescriptionOfInsurance", "DescriptionOfInsurance");
            ViewData["DescriptionId"] = new SelectList(_context.InsuranceInfo, "DescriptionId", "DescriptionOfInsurance");

            return View(ins);
        }

        // POST: Insurances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InsuranceId,Name,DescriptionOfInsurance,PersonId,DescriptionId")] Insurance insurance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(insurance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonId"] = new SelectList(_context.InsuracePersonData, "PersonId", "SocialNumber", insurance.PersonId);
            ViewData["DescriptionId"] = new SelectList(_context.InsuranceInfo, "DescriptionId", "DescriptionOfInsurance", insurance.DescriptionId);

            return View(insurance);
        }

        // GET: Insurances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Insurance == null)
            {
                return NotFound();
            }

            var insurance = await _context.Insurance.FindAsync(id);
            if (insurance == null)
            {
                return NotFound();
            }
            ViewData["PersonId"] = new SelectList(_context.InsuracePersonData, "PersonId", "SocialNumber", insurance.PersonId);
            return View(insurance);
        }

        // POST: Insurances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InsuranceId,Name,DescriptionOfInsurance,PersonId,DescriptionId")] Insurance insurance)
        {
            if (id != insurance.InsuranceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(insurance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsuranceExists(insurance.InsuranceId))
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
            ViewData["PersonId"] = new SelectList(_context.InsuracePersonData, "PersonId", "SocialNumber", insurance.PersonId);
            return View(insurance);
        }

        // GET: Insurances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Insurance == null)
            {
                return NotFound();
            }

            var insurance = await _context.Insurance
                .Include(i => i.Person)
                .FirstOrDefaultAsync(m => m.InsuranceId == id);
            if (insurance == null)
            {
                return NotFound();
            }

            return View(insurance);
        }

        // POST: Insurances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Insurance == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Insurance'  is null.");
            }
            var insurance = await _context.Insurance.FindAsync(id);
            if (insurance != null)
            {
                _context.Insurance.Remove(insurance);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InsuranceExists(int id)
        {
          return (_context.Insurance?.Any(e => e.InsuranceId == id)).GetValueOrDefault();
        }
    }
}
