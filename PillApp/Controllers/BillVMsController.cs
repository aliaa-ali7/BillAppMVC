using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Reporting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PillApp.Models;

namespace PillApp.Controllers
{
    public class BillVMsController : Controller
    {
        private readonly AppDbContext _context;
         IWebHostEnvironment webHostEnvironment;
       

        public BillVMsController(AppDbContext context, IWebHostEnvironment webHostEnvi)
        {
            _context = context;
            webHostEnvironment = webHostEnvi;

        }

        // GET: BillVMs
        public async Task<IActionResult> Index(string? searchTerm)
        {
            var appDbContext = _context.BillViewModel
                .AsNoTracking()
                .Include(b => b.Customer)
                .Include(b => b.Product)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                appDbContext = appDbContext.Where(x => x.Product.Name.Contains(searchTerm));
            }

            var result = await appDbContext
                .Select(x => new BillVM
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    Product = x.Product,
                    Customer = x.Customer,
                    DateBill = x.DateBill,
                    Quantity = x.Quantity,
                    Price = x.Price,
                    Value = (x.Quantity * x.Price),
                })
                .ToListAsync();

            return View(result);
        }


        // GET: BillVMs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billVM = await _context.BillViewModel
                .Include(b => b.Customer)
                .Include(b => b.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (billVM == null)
            {
                return NotFound();
            }

            var price = (billVM.Quantity * billVM.Price);

            ViewData["Value"] = (billVM.Quantity * billVM.Price);
            return View(billVM);
        }

        // GET: BillVMs/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id");
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id");
            return View();
        }

        // POST: BillVMs/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CustomerId,ProductId,Quantity,Price,DateBill")] BillVM billVM)
        {
            if (ModelState.IsValid)
            {
                _context.Add(billVM);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", billVM.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", billVM.ProductId);
            return View(billVM);
        }

        // GET: BillVMs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billVM = await _context.BillViewModel.FindAsync(id);
            if (billVM == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", billVM.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", billVM.ProductId);
            return View(billVM);
        }

        // POST: BillVMs/Edit/5
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CustomerId,ProductId,Quantity,Price,DateBill")] BillVM billVM)
        {
            if (id != billVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(billVM);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillVMExists(billVM.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", billVM.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", billVM.ProductId);
            return View(billVM);
        }

        // GET: BillVMs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billVM = await _context.BillViewModel
                .Include(b => b.Customer)
                .Include(b => b.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (billVM == null)
            {
                return NotFound();
            }

            return View(billVM);
        }

        // POST: BillVMs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var billVM = await _context.BillViewModel.FindAsync(id);
            _context.BillViewModel.Remove(billVM);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BillVMExists(int id)
        {
            return _context.BillViewModel.Any(e => e.Id == id);
        }


        public ActionResult Exit()
        {
            return View();
        }


        public IActionResult Print()
        {

            string path = webHostEnvironment.WebRootPath + @"\Report\BillReport.rdlc";
        Dictionary<string, string> parameters = new Dictionary<string, string>();
        parameters.Add("Param", "Bill RDLC Report");
            var data = _context.BillVMs;

        LocalReport localReport = new LocalReport(path);
        localReport.AddDataSource("BillDataSet", data);
            var report = localReport.Execute(RenderType.Pdf, 1, parameters, "");

            return File(report.MainStream, "application/pdf");


        }
    }
}
