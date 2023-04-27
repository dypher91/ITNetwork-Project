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
    public class PersonInsurancesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonInsurancesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PersonInsurances
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PersonInsurance.Include(p => p.Person);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PersonInsurances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PersonInsurance == null)
            {
                return NotFound();
            }

            var personInsurance = await _context.PersonInsurance
                .Include(p => p.Person)
                .FirstOrDefaultAsync(m => m.PersonInsuranceId == id);
            if (personInsurance == null)
            {
                return NotFound();
            }

            return View(personInsurance);
        }

        // GET: PersonInsurances/Create
        public IActionResult Create()
        {
            //ViewData["PersonId"] = new SelectList(_context.InsuracePersonData, "PersonId", "Address");
            ViewBag.PersonId = new SelectList(_context.InsuracePersonData, "PersonId", "SocialNumber");
            return View();
        }

        // POST: PersonInsurances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonInsuranceId,ValueOfInsurance,IntrestOfInsurance,InsuranceStart,InsuranceEnd,PersonId")] PersonInsurance personInsurance)
        {
            if (ModelState.IsValid)
            {
                //route the social number path
               // var person = _context.InsuracePersonData.SingleOrDefault(p => p.SocialNumber == personInsurance.PersonId);
                //change the value of person id to social number
              // personInsurance.PersonId = person.PersonId;

                _context.Add(personInsurance);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "Insurances");
            }
            //ViewData["PersonId"] = new SelectList(_context.InsuracePersonData, "PersonId", "Address", personInsurance.PersonId);
            ViewBag.PersonId = new SelectList(_context.InsuracePersonData, "PersonId", "SocialNumber");

            return View(personInsurance);
        }

        // GET: PersonInsurances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PersonInsurance == null)
            {
                return NotFound();
            }

            var personInsurance = await _context.PersonInsurance.FindAsync(id);
            if (personInsurance == null)
            {
                return NotFound();
            }
            ViewData["PersonId"] = new SelectList(_context.InsuracePersonData, "PersonId", "SocialNumber", personInsurance.PersonId);
            return View(personInsurance);
        }

        // POST: PersonInsurances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonInsuranceId,ValueOfInsurance,IntrestOfInsurance,InsuranceStart,InsuranceEnd,PersonId")] PersonInsurance personInsurance)
        {
            if (id != personInsurance.PersonInsuranceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personInsurance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonInsuranceExists(personInsurance.PersonInsuranceId))
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
            ViewData["PersonId"] = new SelectList(_context.InsuracePersonData, "PersonId", "SocialNumber", personInsurance.PersonId);
            return View(personInsurance);
        }

        // GET: PersonInsurances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PersonInsurance == null)
            {
                return NotFound();
            }

            var personInsurance = await _context.PersonInsurance
                .Include(p => p.Person)
                .FirstOrDefaultAsync(m => m.PersonInsuranceId == id);
            if (personInsurance == null)
            {
                return NotFound();
            }

            return View(personInsurance);
        }

        // POST: PersonInsurances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PersonInsurance == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PersonInsurance'  is null.");
            }
            var personInsurance = await _context.PersonInsurance.FindAsync(id);
            if (personInsurance != null)
            {
                _context.PersonInsurance.Remove(personInsurance);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonInsuranceExists(int id)
        {
          return (_context.PersonInsurance?.Any(e => e.PersonInsuranceId == id)).GetValueOrDefault();
        }
    }
}
