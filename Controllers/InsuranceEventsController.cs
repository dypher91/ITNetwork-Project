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
    [Authorize]
    public class InsuranceEventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InsuranceEventsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InsuranceEvents
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.InsuranceEvent.Include(i => i.Person);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: InsuranceEvents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.InsuranceEvent == null)
            {
                return NotFound();
            }

            var insuranceEvent = await _context.InsuranceEvent
                .Include(i => i.Person)
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (insuranceEvent == null)
            {
                return NotFound();
            }

            return View(insuranceEvent);
        }

        // GET: InsuranceEvents/Create
        public IActionResult Create()
        {
            ViewBag.PersonId = new SelectList(_context.InsuracePersonData, "PersonId", "SocialNumber");
            return View();
        }

        // POST: InsuranceEvents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventId,EventName,EventDescription,PersonId")] InsuranceEvent insuranceEvent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(insuranceEvent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonId"] = new SelectList(_context.InsuracePersonData, "PersonId", "SocialNumber", insuranceEvent.PersonId);
            return View(insuranceEvent);
        }

        // GET: InsuranceEvents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.InsuranceEvent == null)
            {
                return NotFound();
            }

            var insuranceEvent = await _context.InsuranceEvent.FindAsync(id);
            if (insuranceEvent == null)
            {
                return NotFound();
            }
            ViewData["PersonId"] = new SelectList(_context.InsuracePersonData, "PersonId", "SocialNumber", insuranceEvent.PersonId);
            return View(insuranceEvent);
        }

        // POST: InsuranceEvents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventId,EventName,EventDescription,PersonId")] InsuranceEvent insuranceEvent)
        {
            if (id != insuranceEvent.EventId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(insuranceEvent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsuranceEventExists(insuranceEvent.EventId))
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
            ViewData["PersonId"] = new SelectList(_context.InsuracePersonData, "PersonId", "SocialNumber", insuranceEvent.PersonId);
            return View(insuranceEvent);
        }

        // GET: InsuranceEvents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.InsuranceEvent == null)
            {
                return NotFound();
            }

            var insuranceEvent = await _context.InsuranceEvent
                .Include(i => i.Person)
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (insuranceEvent == null)
            {
                return NotFound();
            }

            return View(insuranceEvent);
        }

        // POST: InsuranceEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.InsuranceEvent == null)
            {
                return Problem("Entity set 'ApplicationDbContext.InsuranceEvent'  is null.");
            }
            var insuranceEvent = await _context.InsuranceEvent.FindAsync(id);
            if (insuranceEvent != null)
            {
                _context.InsuranceEvent.Remove(insuranceEvent);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InsuranceEventExists(int id)
        {
          return (_context.InsuranceEvent?.Any(e => e.EventId == id)).GetValueOrDefault();
        }
    }
}
