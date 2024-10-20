using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MotorbikeStore.Models;

namespace MotorbikeStore.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin
        public async Task<IActionResult> Index()
        {
              return _context.Motorbikes != null ? 
                          View(await _context.Motorbikes.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Motorbikes'  is null.");
        }

        // GET: Admin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Motorbikes == null)
            {
                return NotFound();
            }

            var motorbike = await _context.Motorbikes
                .FirstOrDefaultAsync(m => m.MotorbikeId == id);
            if (motorbike == null)
            {
                return NotFound();
            }

            return View(motorbike);
        }

        // GET: Admin/Create
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            var salesData = _context.SalesRecords
                .GroupBy(s => s.SaleDate.Month)
                .Select(g => new { Month = g.Key, TotalSales = g.Sum(s => s.QuantitySold) })
                .OrderBy(s => s.Month)
                .ToList();

            ViewBag.SalesData = salesData.Select(s => s.TotalSales).ToList();
            ViewBag.SalesLabels = salesData.Select(s => new DateTime(1, s.Month, 1).ToString("MMMM")).ToList();

            return View();
        }


        // POST: Admin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MotorbikeId,Name,Brand,Description,Price,Stock,ImageUrl")] Motorbike motorbike)
        {
            if (ModelState.IsValid)
            {
                _context.Add(motorbike);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(motorbike);
        }

        // GET: Admin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Motorbikes == null)
            {
                return NotFound();
            }

            var motorbike = await _context.Motorbikes.FindAsync(id);
            if (motorbike == null)
            {
                return NotFound();
            }
            return View(motorbike);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MotorbikeId,Name,Brand,Description,Price,Stock,ImageUrl")] Motorbike motorbike)
        {
            if (id != motorbike.MotorbikeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(motorbike);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MotorbikeExists(motorbike.MotorbikeId))
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
            return View(motorbike);
        }

        // GET: Admin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Motorbikes == null)
            {
                return NotFound();
            }

            var motorbike = await _context.Motorbikes
                .FirstOrDefaultAsync(m => m.MotorbikeId == id);
            if (motorbike == null)
            {
                return NotFound();
            }

            return View(motorbike);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Motorbikes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Motorbikes'  is null.");
            }
            var motorbike = await _context.Motorbikes.FindAsync(id);
            if (motorbike != null)
            {
                _context.Motorbikes.Remove(motorbike);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MotorbikeExists(int id)
        {
          return (_context.Motorbikes?.Any(e => e.MotorbikeId == id)).GetValueOrDefault();
        }
    }
}
