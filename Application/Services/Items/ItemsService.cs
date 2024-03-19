using Application.Models;
using Microsoft.EntityFrameworkCore;
using TmaWarehouse.Data;

namespace TmaWarehouse.Services.Items;

public class ItemsService : IItemsService
{
    private readonly TmaWarehouseDbContext _context;

    public ItemsService(TmaWarehouseDbContext context)
    {
        _context = context;
    }

    public Task<List<Item>> GetAllAsync(string sortOrder, string searchString)
    {
        var items = _context.Items.Include(i => i.Group).AsQueryable();
        if (!String.IsNullOrEmpty(searchString))
        {
            items = items.Where(s => s.Name.Contains(searchString));
        }

        items = sortOrder switch
        {
            "name_desc" => items.OrderByDescending(s => s.Name),
            "Price" => items.OrderBy(s => s.Price),
            "price_desc" => items.OrderByDescending(s => s.Price),
            "Group" => items.OrderBy(s => s.Group.Name),
            "group_desc" => items.OrderByDescending(s => s.Group.Name),
            _ => items.OrderBy(s => s.Name),
        };

        return items.ToListAsync();
    }

    public Task<Item?> GetByIdAsync(int id)
    {
        return _context.Items.Where(i => i.Id == id).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(Item request)
    {
        await _context.AddAsync(request);
        await _context.SaveChangesAsync();
    }

    public Task UpdateAsync(Item request)
    {
        _context.Update(request);

        return _context.SaveChangesAsync();
    }

    public async Task DeleteByIdAsync(int id)
    {
        var item = await GetByIdAsync(id);
        _context.Items.Remove(item!);
        await _context.SaveChangesAsync();
    }
}