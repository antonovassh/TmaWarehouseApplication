using Application.Models;

namespace TmaWarehouse.Services.Items;

public interface IItemsService
{
    Task CreateAsync(Item request);
    Task DeleteByIdAsync(int id);
    Task<List<Item>> GetAllAsync(string sortOrder, string searchString);
    Task<Item?> GetByIdAsync(int id);
    Task UpdateAsync(Item request);
}