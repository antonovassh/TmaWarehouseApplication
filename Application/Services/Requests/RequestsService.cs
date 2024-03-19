using Application.Models.Request;
using Microsoft.EntityFrameworkCore;
using TmaWarehouse.Data;

namespace TmaWarehouse.Services.Requests;

public class RequestsService : IRequestsService
{
    private readonly TmaWarehouseDbContext _context;

    public RequestsService(TmaWarehouseDbContext context)
    {
        _context = context;
    }

    public Task<List<Request>> GetAllAsync(string sortOrder, string searchString)
    {
        var requests = _context.Requests.AsQueryable();
        if (!String.IsNullOrEmpty(searchString))
        {
            requests = requests.Where(s => s.Id.ToString().Contains(searchString));
        }

        requests = sortOrder switch
        {
            "name_desc" => requests.OrderByDescending(s => s.EmployeeName),
            "Quantity" => requests.OrderBy(s => s.Quantity),
            "quant_desc" => requests.OrderByDescending(s => s.Quantity),
            "Comment" => requests.OrderBy(s => s.Comment),
            "comment_desc" => requests.OrderByDescending(s => s.Comment),
            "Status" => requests.OrderBy(s => s.Status),
            "status_desc" => requests.OrderByDescending(s => s.Status),
            _ => requests.OrderBy(s => s.EmployeeName),
        };

        return requests.ToListAsync();
    }

    public Task<Request?> GetByIdAsync(int id)
    {
        return _context.Requests
            .Include(r => r.Measurement)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<Request?> ConfirmAsync(int id)
    {
        var request = await _context.Requests.FirstOrDefaultAsync(r => r.Id == id);

        if (request == null)
        {
            return null;
        }

        var item = request.Item;

        if (item.Quantity > 1)
        {
            item.Quantity = 1;
        }

        _context.SaveChanges();

        return request;
    }

    public async Task CreateAsync(Request request)
    {
        await _context.AddAsync(request);
        await _context.SaveChangesAsync();
    }

    public Task UpdateAsync(Request request)
    {
        _context.Update(request);

        return _context.SaveChangesAsync();
    }

    public async Task DeleteByIdAsync(int id)
    {
        var item = await GetByIdAsync(id);
        _context.Requests.Remove(item!);
        await _context.SaveChangesAsync();
    }
}
