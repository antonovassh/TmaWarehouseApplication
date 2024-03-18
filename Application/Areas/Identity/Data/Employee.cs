using Microsoft.AspNetCore.Identity;

namespace TmaWarehouse.Areas.Identity.Data
{
    public class Employee : IdentityRole
    {
        public string Name { get; set; }
    }
}

