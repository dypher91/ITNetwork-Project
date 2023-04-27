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
    public class InsuranceInfoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InsuranceInfoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InsuranceInfoes
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
              return _context.InsuranceInfo != null ? 
                          View(await _context.InsuranceInfo.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.InsuranceInfo'  is null.");
        }

        // GET: InsuranceInfoes/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.InsuranceInfo == null)
            {
                return NotFound();
            }

            var insuranceInfo = await _context.InsuranceInfo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (insuranceInfo == null)
            {
                return NotFound();
            }

            return View(insuranceInfo);
        }

        // GET: InsuranceInfoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: InsuranceInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DescriptionOfInsurance,DescriptionId")] InsuranceInfo insuranceInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(insuranceInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(insuranceInfo);
        }

        // GET: InsuranceInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.InsuranceInfo == null)
            {
                return NotFound();
            }

            var insuranceInfo = await _context.InsuranceInfo.FindAsync(id);
            if (insuranceInfo == null)
            {
                return NotFound();
            }
            return View(insuranceInfo);
        }

        // POST: InsuranceInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DescriptionOfInsurance,DescriptionId")] InsuranceInfo insuranceInfo)
        {
            if (id != insuranceInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(insuranceInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsuranceInfoExists(insuranceInfo.Id))
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
            return View(insuranceInfo);
        }

        // GET: InsuranceInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.InsuranceInfo == null)
            {
                return NotFound();
            }

            var insuranceInfo = await _context.InsuranceInfo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (insuranceInfo == null)
            {
                return NotFound();
            }

            return View(insuranceInfo);
        }

        // POST: InsuranceInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.InsuranceInfo == null)
            {
                return Problem("Entity set 'ApplicationDbContext.InsuranceInfo'  is null.");
            }
            var insuranceInfo = await _context.InsuranceInfo.FindAsync(id);
            if (insuranceInfo != null)
            {
                _context.InsuranceInfo.Remove(insuranceInfo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InsuranceInfoExists(int id)
        {
          return (_context.InsuranceInfo?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
