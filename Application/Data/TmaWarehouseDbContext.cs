using Microsoft.EntityFrameworkCore;
using Application.Models.ItemModel;
using Application.Models.Request;
using Application.Models;

namespace TmaWarehouse.Data
{
    public class TmaWarehouseDbContext : DbContext
    {
        public TmaWarehouseDbContext(DbContextOptions<TmaWarehouseDbContext> options)
            : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<ItemGroup> ItemGroups { get; set; }
        public DbSet<ItemMeasurement> ItemMeasurements { get; set; }
    }
}