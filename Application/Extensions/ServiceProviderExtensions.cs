using Microsoft.AspNetCore.Identity;
using TmaWarehouse.Areas.Identity.Data;

namespace TmaWarehouse.Extensions;

public static class ServiceProviderExtensions
{
    public async static Task<IServiceProvider> SetupIdentityForLocalRun(
        this IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var roles = new[] { "Coordinator", "Employee", "Administrator" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<TmaWarehouseUser>>();

            string coordinatorEmail = "coordinator@coordinator.com";
            string coordinatorPassword = "_8780;iqY97(";

            if (await userManager.FindByEmailAsync(coordinatorEmail) == null)
            {
                var user = new TmaWarehouseUser();
                user.UserName = coordinatorEmail;
                user.Email = coordinatorPassword;

                await userManager.CreateAsync(user, coordinatorPassword);

                await userManager.AddToRoleAsync(user, "Coordinator");
            }

            string employeeEmail = "employee@employee.com";
            string employeePassword = "5{]`7£T$nT3U";

            if (await userManager.FindByEmailAsync(employeeEmail) == null)
            {
                var user = new TmaWarehouseUser();
                user.UserName = employeeEmail;
                user.Email = employeeEmail;

                await userManager.CreateAsync(user, employeePassword);

                await userManager.AddToRoleAsync(user, "Employee");
            }
        }

        return serviceProvider;
    }
}
