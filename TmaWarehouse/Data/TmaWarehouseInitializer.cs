using System;
using System.Collections.Generic;
using System.Linq;
using Application.Models;
using Application.Models.ItemModel;
using Application.Models.Request;
using Microsoft.EntityFrameworkCore;
using TmaWarehouse.Data;

namespace TmaWarehouse.DataAccess
{
    public static class TmaWarehouseInitializer
    {
        public static void SeedData(TmaWarehouseDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.ItemGroup.Any())
            {
                return; // Data already seeded
            }

            var itemGroups = new List<ItemGroup>
            {
                new ItemGroup { Id = 1, Name = "Electronics" },
                new ItemGroup { Id = 2, Name = "Apparel" },
                new ItemGroup { Id = 3, Name = "Home goods" }
            };
            context.ItemGroup.AddRange(itemGroups);
            context.SaveChanges();

            var itemMeasurements = new List<ItemMeasurement>
            {
                new ItemMeasurement { Id = 1, Name = "pcs" }
            };
            context.ItemMeasurement.AddRange(itemMeasurements);
            context.SaveChanges();

            var items = new List<Item>
            {
                new Item { Id = 1, Name = "Computer", GroupId = 1, MeasurementId = 1, Quantity = 1, Price = 5000, Status = "Available" },
                new Item { Id = 2, Name = "Laptop", GroupId = 1, MeasurementId = 1, Quantity = 1, Price = 4500, Status = "Available" },
                new Item { Id = 3, Name = "Smartphone", GroupId = 1, MeasurementId = 1, Quantity = 1, Price = 4200, Status = "Available" },
                new Item { Id = 4, Name = "Headphones", GroupId = 1, MeasurementId = 1, Quantity = 1, Price = 2500, Status = "Available" }
            };
            context.Item.AddRange(items);
            context.SaveChanges();

            var requests = new List<Request>
            {
                new Request { Id = 1, EmployeeName = "John Stivensen", ItemId = 1, MeasurementId = 1, Quantity = 1 },
                new Request { Id = 2, EmployeeName = "Marry Ellise", ItemId = 2, MeasurementId = 1, Quantity = 2 }
            };
            context.Request.AddRange(requests);
            context.SaveChanges();
        }
    }
}
