using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using TmaWarehouse.Areas.Identity.Data;
using TmaWarehouse.Data;
using TmaWarehouse.Services.Items;
using TmaWarehouse.Services.Requests;

namespace TmaWarehouse.Services.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTmaDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("TmaWarehouseContext") ??
            throw new ConfigurationErrorsException("Connection string 'TmaWarehouseContext' not found.");

        services.AddDbContext<TmaWarehouseDbContext>(options =>
            options.UseSqlServer(connection));

        return services;
    }

    public static IServiceCollection AddTmaServices(this IServiceCollection services)
    {
        services.AddScoped<IItemsService, ItemsService>();
        services.AddScoped<IRequestsService, RequestsService>();

        return services;
    }

    public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
    {
        services.AddDefaultIdentity<TmaWarehouseUser>(options => options.SignIn.RequireConfirmedAccount = false)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<TmaWarehouseDbContext>();

        services.Configure<IdentityOptions>(options =>
        {
            // Password settings.
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;

            // Lockout settings.
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            // User settings.
            options.User.AllowedUserNameCharacters =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = false;
        });

        services.ConfigureApplicationCookie(options =>
        {
            // Cookie settings
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

            options.LoginPath = "/Identity/Account/Login";
            options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            options.SlidingExpiration = true;
        });


        return services;
    }
}
