using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Application.Models.Request;
using TmaWarehouse.Data;

namespace TmaWarehouse.Controllers
{
    public class RequestsController : Controller
    {
        private readonly TmaWarehouseDbContext _context;

        public RequestsController(TmaWarehouseDbContext context)
        {
            _context = context;
        }

        // GET: Requests
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewBag.EmployeeNameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.QuantitySortParm = sortOrder == "Quantity" ? "quant_desc" : "Quantity";
            ViewBag.CommentSortParm = sortOrder == "Comment" ? "comment_desc" : "Comment";
            ViewBag.StatusSortParm = sortOrder == "Status" ? "status_desc" : "Status";

            var requests = _context.Requests.AsQueryable();
            if (!String.IsNullOrEmpty(searchString))
            {
                requests = requests.Where(s => s.EmployeeName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    requests = requests.OrderByDescending(s => s.EmployeeName);
                    break;
                case "Quantity":
                    requests = requests.OrderBy(s => s.Quantity);
                    break;
                case "quant_desc":
                    requests = requests.OrderByDescending(s => s.Quantity);
                    break;
                case "Comment":
                    requests = requests.OrderBy(s => s.Comment);
                    break;
                case "comment_desc":
                    requests = requests.OrderByDescending(s => s.Comment);
                    break;
                case "Status":
                    requests = requests.OrderBy(s => s.Status);
                    break;
                case "status_desc":
                    requests = requests.OrderByDescending(s => s.Status);
                    break;
                default:
                    requests = requests.OrderBy(s => s.EmployeeName);
                    break;
            }

            return View(await requests.ToListAsync());
        }

        // GET: Requests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Requests == null)
            {
                return NotFound();
            }

            var request = await _context.Requests
                .Include(r => r.Measurement)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        // GET: Requests/Create
        public IActionResult Create()
        {
            ViewData["MeasurementId"] = new SelectList(_context.ItemMeasurements, "Id", "Id");
            return View();
        }

        // POST: Requests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmployeeName,ItemId,MeasurementId,Quantity,Comment,Status")] Request request)
        {
            if (ModelState.IsValid)
            {
                _context.Add(request);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MeasurementId"] = new SelectList(_context.ItemMeasurements, "Id", "Id", request.MeasurementId);
            return View(request);
        }

        // GET: Requests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Requests == null)
            {
                return NotFound();
            }

            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }
            ViewData["MeasurementId"] = new SelectList(_context.ItemMeasurements, "Id", "Id", request.MeasurementId);
            return View(request);
        }

        // POST: Requests/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeName,ItemId,MeasurementId,Quantity,Comment,Status")] Request request)
        {
            if (id != request.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(request);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequestExists(request.Id))
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
            ViewData["MeasurementId"] = new SelectList(_context.ItemMeasurements, "Id", "Id", request.MeasurementId);
            return View(request);
        }

        // GET: Requests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Requests == null)
            {
                return NotFound();
            }

            var request = await _context.Requests
                .Include(r => r.Measurement)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        // POST: Requests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Requests == null)
            {
                return Problem("Entity set 'TmaWarehouseDbContext.Request'  is null.");
            }
            var request = await _context.Requests.FindAsync(id);
            if (request != null)
            {
                _context.Requests.Remove(request);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RequestExists(int id)
        {
          return (_context.Requests?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
