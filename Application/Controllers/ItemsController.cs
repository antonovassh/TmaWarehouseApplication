using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Application.Models;
using TmaWarehouse.Data;
using Application.Models.ItemModel;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using TmaWarehouse.Models.ViewModel;
using Microsoft.AspNetCore.Hosting;

namespace TmaWarehouse.Controllers

{
    [Authorize(Roles = "Employee, Coordinator")]
    public class ItemsController : Controller
    {
        private readonly TmaWarehouseDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ItemsController(TmaWarehouseDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Items
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        { 
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";
            ViewBag.GroupSortParm = sortOrder == "Group" ? "group_desc" : "Group";

            var items = _context.Items.Include(i => i.Group).AsQueryable();
            if (!String.IsNullOrEmpty(searchString))
            {
                items = items.Where(s => s.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    items = items.OrderByDescending(s => s.Name);
                    break;
                case "Price":
                    items = items.OrderBy(s => s.Price);
                    break;
                case "price_desc":
                    items = items.OrderByDescending(s => s.Price);
                    break;
                case "Group":
                    items = items.OrderBy(s => s.Group.Name);
                    break;
                case "group_desc":
                    items = items.OrderByDescending(s => s.Group.Name);
                    break;
                default:
                    items = items.OrderBy(s => s.Name);
                    break;
            }


            return View(await items.ToListAsync());
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Order(int? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.Group)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            var viewModel = new OrderViewModel
            {
                ItemId = item.Id,
                ItemName = item.Name,
                Measurement = item.Measurement,
                Quantity = item.Quantity,
                Price = item.Price
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitOrder()
        {

            TempData["Message"] = "Request created";
            return RedirectToAction("Index"); 
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            ViewBag.Groups = new List<ItemGroup>
     {
         new ItemGroup
         {
             Id = 1, Name = "Electronics"
         },
         new ItemGroup
         {
             Id = 2, Name = "Apparel"
         },
         new ItemGroup
         {
             Id = 3, Name = "Home goods"
         }
     };
            ViewBag.Measurements = new List<ItemMeasurement>
     {
         new ItemMeasurement
         {
             Id = 1, Name = "pcs"
         },
         new ItemMeasurement
         {
             Id = 2, Name = "kg"
         },
         new ItemMeasurement
         {
             Id = 2, Name = "litres"
         }

     };
            return View();
        }
        // POST: Items/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,GroupId,MeasurementId,Quantity,Price,Status,StorageLocation,ContactPerson,Photo")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupId"] = new SelectList(_context.Set<ItemGroup>(), "Id", "Id", item.GroupId);
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            ViewData["GroupId"] = new SelectList(_context.Set<ItemGroup>(), "Id", "Id", item.GroupId);
            return View(item);
        }

        // POST: Items/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,GroupId,MeasurementId,Quantity,Price,Status,StorageLocation,ContactPerson,Photo")] Item item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.Id))
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
            ViewData["GroupId"] = new SelectList(_context.Set<ItemGroup>(), "Id", "Id", item.GroupId);
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.Group)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Items == null)
            {
                return Problem("Entity set 'TmaWarehouseContext.Item'  is null.");
            }
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public ActionResult DisplayImage(string imageName)
        {
            string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", imageName);
            return PhysicalFile(imagePath, "image/jpeg"); 
        }

        private bool ItemExists(int id)
        {
            return (_context.Items?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
