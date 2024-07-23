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
        [Authorize(Roles = "Administrador, Supervisor, Ventas")]
        public async Task<IActionResult> Index(string searchString)
        {
            var salesDetails = from s in _context.SalesDetails.Include(s => s.Clients).Include(s => s.Product).Include(s=>s.TipoDePago)
                               select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                salesDetails = salesDetails.Where(s => s.Clients.FirstName.Contains(searchString) || s.Clients.LastName.Contains(searchString));
            }

            return View(await salesDetails.ToListAsync());
        }

        // GET: SalesDetails/Details/5
        [Authorize(Roles = "Administrador, Supervisor")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesDetails = await _context.SalesDetails
                .Include(s => s.Clients)
                .Include(s => s.Product)
                .Include(s => s.TipoDePago)
                .FirstOrDefaultAsync(m => m.SalesDetailsID == id);
            if (salesDetails == null)
            {
                return NotFound();
            }

            return View(salesDetails);
        }

        // GET: SalesDetails/Create
        [Authorize(Roles = "Administrador, Supervisor, Ventas")]
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "FirstName");
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName");
            ViewData["TipoDePagoID"] = new SelectList(_context.TipoDePago, "tipoPagoID", "tipoPago");
            return View();
        }

        // POST: SalesDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = " Administrador, Supervisor,Ventas")]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("SalesDetailsID,Amount,Date,ProductId,ClientId, TipoDePagoID")] SalesDetails salesDetails)
        {
            if (ModelState.IsValid)
            {
                var product = await _context.Products.FindAsync(salesDetails.ProductId);
                if (product != null && product.InStock >= salesDetails.Amount)
                {
                    product.InStock -= salesDetails.Amount;
                    _context.Add(salesDetails);
                    await _context.SaveChangesAsync();

                    if (product.InStock <= 10)
                    {
                        TempData["AlertMessage"] = "Producto bajo en existencias, adquirir más productos.";
                    }

                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", "Existencias insuficientes para el producto seleccionado.");
            }

            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "FirstName", salesDetails.ClientId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName", salesDetails.ProductId);
            ViewData["TipoDePagoID"] = new SelectList(_context.TipoDePago, "tipoPagoID", "tipoPago", salesDetails.TipoDePagoID);
            return View(salesDetails);
        }

        // GET: SalesDetails/Edit/5
        [Authorize(Roles = "Administrador, Supervisor")]
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
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "FirstName", salesDetails.ClientId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductDescription", salesDetails.ProductId);
            ViewData["TipoDePagoID"] = new SelectList(_context.TipoDePago, "tipoPagoID", "tipoPago", salesDetails.TipoDePagoID);
            return View(salesDetails);
        }

        // POST: SalesDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrador, Supervisor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SalesDetailsID,Amount,Date,ProductId,ClientId,TipoDePagoID")] SalesDetails salesDetails)
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
            ViewData["ClientId"] = new SelectList(_context.Client, "ClientId", "FirstName", salesDetails.ClientId);
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName", salesDetails.ProductId);
            ViewData["TipoDePagoID"] = new SelectList(_context.TipoDePago, "tipoPagoID", "tipoPago", salesDetails.TipoDePagoID);
            return View(salesDetails);
        }

        // GET: SalesDetails/Delete/5
        [Authorize(Roles = "Administrador, Supervisor")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesDetails = await _context.SalesDetails
                .Include(s => s.Clients)
                .Include(s => s.Product)
                .Include(s => s.TipoDePago)
                .FirstOrDefaultAsync(m => m.SalesDetailsID == id);
            if (salesDetails == null)
            {
                return NotFound();
            }

            return View(salesDetails);
        }

        // POST: SalesDetails/Delete/5
        [Authorize(Roles = "Administrador, Supervisor")]

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

