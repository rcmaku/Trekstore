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
    public class SalesDetailsController : Controller
    {
        private readonly TrekstorDbContext _context;

        public SalesDetailsController(TrekstorDbContext context)
        {
            _context = context;
        }

        // GET: SalesDetails
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var trekstorDbContext = _context.SalesDetails.Include(s => s.Clients).Include(s => s.Product);
            return View(await trekstorDbContext.ToListAsync());
        }

        // GET: SalesDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesDetails = await _context.SalesDetails
                .Include(s => s.Clients)
                .Include(s => s.Product)
                .FirstOrDefaultAsync(m => m.SalesDetailsID == id);
            if (salesDetails == null)
            {
                return NotFound();
            }

            return View(salesDetails);
        }

        // GET: SalesDetails/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "ClientId");
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductDescription");
            return View();
        }

        // POST: SalesDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SalesDetailsID,Amount,Date,ProductId,ClientId")] SalesDetails salesDetails)
        {
            if (ModelState.IsValid)
            {
                var product = await _context.Products.FindAsync(salesDetails.ProductId);
                if (product != null && product.InStock >= salesDetails.Amount)
                {
                    product.InStock -= salesDetails.Amount;
                    _context.Add(salesDetails);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Insufficient stock for the selected product.");

            }
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "ClientId", salesDetails.ClientId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductDescription", salesDetails.ProductId);
            return View(salesDetails);
        }

        // GET: SalesDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesDetails = await _context.SalesDetails.FindAsync(id);
            if (salesDetails == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "ClientId", salesDetails.ClientId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductDescription", salesDetails.ProductId);
            return View(salesDetails);
        }

        // POST: SalesDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SalesDetailsID,Amount,Date,ProductId,ClientId")] SalesDetails salesDetails)
        {
            if (id != salesDetails.SalesDetailsID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salesDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesDetailsExists(salesDetails.SalesDetailsID))
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
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "ClientId", salesDetails.ClientId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductDescription", salesDetails.ProductId);
            return View(salesDetails);
        }

        // GET: SalesDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesDetails = await _context.SalesDetails
                .Include(s => s.Clients)
                .Include(s => s.Product)
                .FirstOrDefaultAsync(m => m.SalesDetailsID == id);
            if (salesDetails == null)
            {
                return NotFound();
            }

            return View(salesDetails);
        }

        // POST: SalesDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salesDetails = await _context.SalesDetails.FindAsync(id);
            if (salesDetails != null)
            {
                _context.SalesDetails.Remove(salesDetails);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalesDetailsExists(int id)
        {
            return _context.SalesDetails.Any(e => e.SalesDetailsID == id);
        }
    }
}
