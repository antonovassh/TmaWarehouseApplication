using Application.Models.Request;

namespace TmaWarehouse.Services.Requests;

public interface IRequestsService
{
    Task<Request?> ConfirmAsync(int id);

    Task CreateAsync(Request request);

    Task DeleteByIdAsync(int id);

    Task<List<Request>> GetAllAsync(string sortOrder, string searchString);

    Task<Request?> GetByIdAsync(int id);

    Task UpdateAsync(Request request);
}