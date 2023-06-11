using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastrucure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync (UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    DisplayName = "Khalel",
                    Email = "ahmedqweasz887@gmail.com",
                    UserName = "AhmedKhalel",
                    Address = new Address()
                    {
                        FirstName = "Ahmed",
                        LastName = "Khalel",
                        Street = "8th st",
                        City = "Tanta",
                        State = "Eg",
                        ZipCode = "38532"
                    }
                };
                await userManager.CreateAsync (user ,"Qweasz111*");

            }
        }
    }
}
