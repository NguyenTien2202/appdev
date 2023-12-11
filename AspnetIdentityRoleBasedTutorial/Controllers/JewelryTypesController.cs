using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspnetIdentityRoleBasedTutorial.Data;
using AspnetIdentityRoleBasedTutorial.Models;

namespace AspnetIdentityRoleBasedTutorial.Controllers
{
    public class JewelryTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JewelryTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: JewelryTypes
        public async Task<IActionResult> Index()
        {
              return _context.JewelryTypes != null ? 
                          View(await _context.JewelryTypes.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.JewelryTypes'  is null.");
        }

        // GET: JewelryTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.JewelryTypes == null)
            {
                return NotFound();
            }

            var jewelryType = await _context.JewelryTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jewelryType == null)
            {
                return NotFound();
            }

            return View(jewelryType);
        }

        // GET: JewelryTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JewelryTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TypeName")] JewelryType jewelryType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jewelryType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jewelryType);
        }

        // GET: JewelryTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.JewelryTypes == null)
            {
                return NotFound();
            }

            var jewelryType = await _context.JewelryTypes.FindAsync(id);
            if (jewelryType == null)
            {
                return NotFound();
            }
            return View(jewelryType);
        }

        // POST: JewelryTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TypeName")] JewelryType jewelryType)
        {
            if (id != jewelryType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jewelryType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JewelryTypeExists(jewelryType.Id))
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
            return View(jewelryType);
        }

        // GET: JewelryTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.JewelryTypes == null)
            {
                return NotFound();
            }

            var jewelryType = await _context.JewelryTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jewelryType == null)
            {
                return NotFound();
            }

            return View(jewelryType);
        }

        // POST: JewelryTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.JewelryTypes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.JewelryTypes'  is null.");
            }
            var jewelryType = await _context.JewelryTypes.FindAsync(id);
            if (jewelryType != null)
            {
                _context.JewelryTypes.Remove(jewelryType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JewelryTypeExists(int id)
        {
          return (_context.JewelryTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
