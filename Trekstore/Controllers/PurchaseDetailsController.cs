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

    public class PurchaseDetailsController : Controller
    {

        private readonly TrekstorDbContext _context;

        public PurchaseDetailsController(TrekstorDbContext context)
        {
            _context = context;
        }
        [Authorize]
        // GET: PurchaseDetails
        public async Task<IActionResult> Index()
        {
            var trekstorDbContext = _context.PurchaseDetails.Include(p => p.Product).Include(p => p.Provider);
            return View(await trekstorDbContext.ToListAsync());
        }

        // GET: PurchaseDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseDetails = await _context.PurchaseDetails
                .Include(p => p.Product)
                .Include(p => p.Provider)
                .FirstOrDefaultAsync(m => m.purch_id == id);
            if (purchaseDetails == null)
            {
                return NotFound();
            }

            return View(purchaseDetails);
        }

        // GET: PurchaseDetails/Create
        public IActionResult Create()
        {
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductId", "ProductDescription");
            ViewData["ProviderID"] = new SelectList(_context.Providers, "ProviderID", "ProviderID");
            return View();
        }

        // POST: PurchaseDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("purch_id,Amount,PurchDate,ProductID,ProviderID")] PurchaseDetails purchaseDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(purchaseDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductId", "ProductDescription", purchaseDetails.ProductID);
            ViewData["ProviderID"] = new SelectList(_context.Providers, "ProviderID", "ProviderID", purchaseDetails.ProviderID);
            return View(purchaseDetails);
        }

        // GET: PurchaseDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseDetails = await _context.PurchaseDetails.FindAsync(id);
            if (purchaseDetails == null)
            {
                return NotFound();
            }
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductId", "ProductDescription", purchaseDetails.ProductID);
            ViewData["ProviderID"] = new SelectList(_context.Providers, "ProviderID", "ProviderID", purchaseDetails.ProviderID);
            return View(purchaseDetails);
        }

        // POST: PurchaseDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("purch_id,Amount,PurchDate,ProductID,ProviderID")] PurchaseDetails purchaseDetails)
        {
            if (id != purchaseDetails.purch_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchaseDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseDetailsExists(purchaseDetails.purch_id))
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
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductId", "ProductDescription", purchaseDetails.ProductID);
            ViewData["ProviderID"] = new SelectList(_context.Providers, "ProviderID", "ProviderID", purchaseDetails.ProviderID);
            return View(purchaseDetails);
        }

        // GET: PurchaseDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseDetails = await _context.PurchaseDetails
                .Include(p => p.Product)
                .Include(p => p.Provider)
                .FirstOrDefaultAsync(m => m.purch_id == id);
            if (purchaseDetails == null)
            {
                return NotFound();
            }

            return View(purchaseDetails);
        }

        // POST: PurchaseDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var purchaseDetails = await _context.PurchaseDetails.FindAsync(id);
            if (purchaseDetails != null)
            {
                _context.PurchaseDetails.Remove(purchaseDetails);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseDetailsExists(int id)
        {
            return _context.PurchaseDetails.Any(e => e.purch_id == id);
        }
    }
}
