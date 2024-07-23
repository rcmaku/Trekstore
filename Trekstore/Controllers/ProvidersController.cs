using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Trekstore.Areas.Identity.Data;
using Trekstore.Models;

namespace Trekstore.Controllers
{
    public class ProvidersController : Controller
    {
        private readonly TrekstorDbContext _context;

        public ProvidersController(TrekstorDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Administrador")]
        // GET: Providers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Providers.ToListAsync());
        }

        // GET: Providers/Details/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var providers = await _context.Providers
                .FirstOrDefaultAsync(m => m.ProviderID == id);
            if (providers == null)
            {
                return NotFound();
            }

            return View(providers);
        }

        // GET: Providers/Create
        [Authorize(Roles = "Administrador")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Providers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProviderID,Name,Telephone,Address")] Providers providers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(providers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(providers);
        }

        // GET: Providers/Edit/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var providers = await _context.Providers.FindAsync(id);
            if (providers == null)
            {
                return NotFound();
            }
            return View(providers);
        }

        // POST: Providers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProviderID,Name,Telephone,Address")] Providers providers)
        {
            if (id != providers.ProviderID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(providers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProvidersExists(providers.ProviderID))
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
            return View(providers);
        }

        // GET: Providers/Delete/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var providers = await _context.Providers
                .FirstOrDefaultAsync(m => m.ProviderID == id);
            if (providers == null)
            {
                return NotFound();
            }

            return View(providers);
        }

        // POST: Providers/Delete/5
        [Authorize(Roles = "Administrador")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var providers = await _context.Providers.FindAsync(id);
            if (providers != null)
            {
                _context.Providers.Remove(providers);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProvidersExists(int id)
        {
            return _context.Providers.Any(e => e.ProviderID == id);
        }
    }
}
