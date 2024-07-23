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
        [Authorize(Roles = "Administrador, Supervisor")]
        // GET: PurchaseDetails
        public async Task<IActionResult> Index(string searchString, DateTime? startDate, DateTime? endDate)
        {
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentStartDate"] = startDate?.ToString("yyyy-MM-dd");
            ViewData["CurrentEndDate"] = endDate?.ToString("yyyy-MM-dd");

            var purchaseDetails = _context.PurchaseDetails
                                           .Include(p => p.Product)
                                           .Include(p => p.Provider)
                                           .Include(p => p.TipoDePago)
                                           .AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                purchaseDetails = purchaseDetails.Where(s => s.Product.ProductName.Contains(searchString));
            }

            if (startDate.HasValue && endDate.HasValue)
            {
                purchaseDetails = purchaseDetails.Where(p => p.PurchDate >= startDate && p.PurchDate <= endDate);
            }

            return View(await purchaseDetails.ToListAsync());
        }

        [Authorize(Roles = "Administrador")]
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
                .Include(p => p.TipoDePago)
                .FirstOrDefaultAsync(m => m.purch_id == id);
            if (purchaseDetails == null)
            {
                return NotFound();
            }

            return View(purchaseDetails);
        }

        // GET: PurchaseDetails/Create
        [Authorize(Roles = "Administrador, Ventas")]
        public IActionResult Create()
        {
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductId", "ProductName");
            ViewData["ProviderID"] = new SelectList(_context.Providers, "ProviderID", "Name");
            ViewData["TipoDePagoID"] = new SelectList(_context.TipoDePago, "tipoPagoID", "tipoPago");

            return View();
        }

        // POST: PurchaseDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador, Ventas")]
        public async Task<IActionResult> Create([Bind("purch_id,Amount,PurchDate,ProductID,ProviderID, TipoDePagoID")] PurchaseDetails purchaseDetails)
        {
            if (ModelState.IsValid)
            {
                var product = await _context.Products.FindAsync(purchaseDetails.ProductID);

                product.InStock += purchaseDetails.Amount;
                _context.Add(purchaseDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductId", "ProductName", purchaseDetails.ProductID);
            ViewData["ProviderID"] = new SelectList(_context.Providers, "ProviderID", "Name", purchaseDetails.ProviderID);
            ViewData["TipoDePagoID"] = new SelectList(_context.TipoDePago, "tipoPagoID", "tipoPago", purchaseDetails.TipoDePagoID);
            return View(purchaseDetails);
        }

        // GET: PurchaseDetails/Edit/5
        [Authorize(Roles = "Administrador")]
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
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductId", "ProductName", purchaseDetails.ProductID);
            ViewData["ProviderID"] = new SelectList(_context.Providers, "ProviderID", "Name", purchaseDetails.ProviderID);
            ViewData["TipoDePagoID"] = new SelectList(_context.TipoDePago, "tipoPagoID", "tipoPago", purchaseDetails.TipoDePagoID);
            return View(purchaseDetails);
        }

        // POST: PurchaseDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("purch_id,Amount,PurchDate,ProductID,ProviderID, tipoPagoID")] PurchaseDetails purchaseDetails)
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
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductId", "ProductName", purchaseDetails.Product);
            ViewData["ProviderID"] = new SelectList(_context.Providers, "ProviderID", "Name", purchaseDetails.ProviderID);
            ViewData["TipoDePagoID"] = new SelectList(_context.TipoDePago, "tipoPagoID", "tipoPago", purchaseDetails.TipoDePagoID);
            return View(purchaseDetails);
        }

        // GET: PurchaseDetails/Delete/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseDetails = await _context.PurchaseDetails
                .Include(p => p.Product)
                .Include(p => p.Provider)
                .Include(p=> p.TipoDePago)
                .FirstOrDefaultAsync(m => m.purch_id == id);
            if (purchaseDetails == null)
            {
                return NotFound();
            }

            return View(purchaseDetails);
        }

        // POST: PurchaseDetails/Delete/5
        [Authorize(Roles = "Administrador")]
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

