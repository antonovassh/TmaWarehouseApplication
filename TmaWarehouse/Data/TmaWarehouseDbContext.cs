using Microsoft.EntityFrameworkCore;
using Application.Models;
using Application.Models.ItemModel;
using Application.Models.Request;

namespace TmaWarehouse.Data

{
    public class TmaWarehouseDbContext : DbContext
    {
        public TmaWarehouseDbContext (DbContextOptions<TmaWarehouseDbContext> options)
            : base(options)
        {
        }

        public DbSet<Item> Item { get; set; } = default!;

        public DbSet<Request> Request { get; set; }

        public DbSet<ItemGroup> ItemGroup { get; set; }

        public DbSet<ItemMeasurement> ItemMeasurement { get; set; }
    }
}
