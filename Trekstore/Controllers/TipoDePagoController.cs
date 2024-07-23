using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Trekstore.Areas.Identity.Data;
using Trekstore.Models;

namespace Trekstore.Controllers
{
    public class TipoDePagoController : Controller
    {
        private readonly TrekstorDbContext _context;

        public TipoDePagoController(TrekstorDbContext context)
        {
            _context = context;
        }

        // GET: TipoDePago
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoDePago.ToListAsync());
        }

        // GET: TipoDePago/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoDePago = await _context.TipoDePago
                .FirstOrDefaultAsync(m => m.tipoPagoID == id);
            if (tipoDePago == null)
            {
                return NotFound();
            }

            return View(tipoDePago);
        }

        // GET: TipoDePago/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoDePago/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("tipoPagoID,tipoPago")] TipoDePago tipoDePago)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoDePago);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoDePago);
        }

        // GET: TipoDePago/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoDePago = await _context.TipoDePago.FindAsync(id);
            if (tipoDePago == null)
            {
                return NotFound();
            }
            return View(tipoDePago);
        }

        // POST: TipoDePago/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("tipoPagoID,tipoPago")] TipoDePago tipoDePago)
        {
            if (id != tipoDePago.tipoPagoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoDePago);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoDePagoExists(tipoDePago.tipoPagoID))
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
            return View(tipoDePago);
        }

        // GET: TipoDePago/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoDePago = await _context.TipoDePago
                .FirstOrDefaultAsync(m => m.tipoPagoID == id);
            if (tipoDePago == null)
            {
                return NotFound();
            }

            return View(tipoDePago);
        }

        // POST: TipoDePago/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoDePago = await _context.TipoDePago.FindAsync(id);
            if (tipoDePago != null)
            {
                _context.TipoDePago.Remove(tipoDePago);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoDePagoExists(int id)
        {
            return _context.TipoDePago.Any(e => e.tipoPagoID == id);
        }
    }
}
