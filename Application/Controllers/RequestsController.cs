using Microsoft.AspNetCore.Mvc;
using Application.Models.Request;
using Microsoft.AspNetCore.Authorization;
using TmaWarehouse.Services.Requests;

namespace TmaWarehouse.Controllers;

[Authorize(Roles = "Coordinator")]
public class RequestsController : Controller
{
    private readonly IRequestsService _requestService;

    public RequestsController(IRequestsService requestService)
    {
        _requestService = requestService;
    }

    // GET: Requests
    public async Task<IActionResult> Index(string sortOrder, string searchString)
    {
        ViewBag.EmployeeNameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        ViewBag.QuantitySortParm = sortOrder == "Quantity" ? "quant_desc" : "Quantity";
        ViewBag.CommentSortParm = sortOrder == "Comment" ? "comment_desc" : "Comment";
        ViewBag.StatusSortParm = sortOrder == "Status" ? "status_desc" : "Status";

        var result = await _requestService.GetAllAsync(sortOrder, searchString);

        return View(result);
    }

    // GET: Requests/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var request = await _requestService.GetByIdAsync(id);

        if (request == null)
        {
            return NotFound();
        }

        return View(request);
    }


		// POST
    [HttpPost]
    public async Task<IActionResult> Confirm(int id)
		{
        var request = await _requestService.ConfirmAsync(id);

        if (request == null)
			{
				return NotFound(); 
			}

			return View(request); 
		}

		// GET: Requests/Create
		public IActionResult Create()
    {
        return View();
    }

    // POST: Requests/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("Id,EmployeeName,ItemId,MeasurementId,Quantity,Comment,Status")]
        Request request)
    {
        if (ModelState.IsValid)
        {
            await _requestService.CreateAsync(request);

            return RedirectToAction(nameof(Index));
        }

        return View(request);
    }

    // GET: Requests/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var request = await _requestService.GetByIdAsync(id);

        if (request == null)
        {
            return NotFound();
        }

        return View(request);
    }

    // POST: Requests/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        [Bind("Id,EmployeeName,ItemId,MeasurementId,Quantity,Comment,Status")]
        Request request)
    {
        if (id != request.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await _requestService.UpdateAsync(request);


            return RedirectToAction(nameof(Index));
        }

        return View(request);
    }

    // POST: Requests/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _requestService.DeleteByIdAsync(id);

        return RedirectToAction(nameof(Index));
    }
}
