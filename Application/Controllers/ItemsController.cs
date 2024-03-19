using Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TmaWarehouse.Models.ViewModel;
using TmaWarehouse.Services.Items;

namespace TmaWarehouse.Controllers;

[Authorize(Roles = "Employee, Coordinator")]
public class ItemsController : Controller
{
    private readonly IItemsService _itemsService;

    public ItemsController(IItemsService itemsService)
    {
        _itemsService = itemsService;
    }

    // GET: Items
    public async Task<IActionResult> Index(string sortOrder, string searchString)
    {
        ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";
        ViewBag.GroupSortParm = sortOrder == "Group" ? "group_desc" : "Group";

        var result = _itemsService.GetAllAsync(sortOrder, searchString);

        return View(result);
    }

    // GET: Items/Details/5
    public async Task<IActionResult> Order(int id)
    {
        var item = await _itemsService.GetByIdAsync(id);

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
        return View();
    }

    // POST: Items/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("Id,Name,GroupId,MeasurementId,Quantity,Price,Status,StorageLocation,ContactPerson,Photo")]
        Item item)
    {
        if (ModelState.IsValid)
        {
            await _itemsService.CreateAsync(item);

            return RedirectToAction(nameof(Index));
        }

        return View(item);
    }

    // GET: Items/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var item = await _itemsService.GetByIdAsync(id);

        if (item == null)
        {
            return NotFound();
        }

        return View(item);
    }

    // POST: Items/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        [Bind("Id,Name,GroupId,MeasurementId,Quantity,Price,Status,StorageLocation,ContactPerson,Photo")] Item item)
    {
        if (id != item.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await _itemsService.UpdateAsync(item);
        
            return RedirectToAction(nameof(Index));
        }

        return View(item);
    }

    // POST: Items/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _itemsService.DeleteByIdAsync(id);

        return RedirectToAction(nameof(Index));
    }
}
