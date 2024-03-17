using Microsoft.EntityFrameworkCore;
using Application.Models.ItemModel;
using Application.Models.Request;
using Application.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using TmaWarehouse.Areas.Identity.Data;

namespace TmaWarehouse.Data
{
    public class TmaWarehouseDbContext : IdentityDbContext<TmaWarehouseUser>
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