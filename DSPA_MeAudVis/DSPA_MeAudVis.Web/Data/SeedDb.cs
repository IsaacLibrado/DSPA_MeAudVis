using DSPA_MeAudVis.Web.Data.Entities;
using DSPA_MeAudVis.Web.Helpers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSPA_MeAudVis.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext dataContext;
        private readonly IUserHelper userHelper;

        public SeedDb(DataContext dataContext, IUserHelper userHelper)
        {
            this.dataContext = dataContext;
            this.userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await dataContext.Database.EnsureCreatedAsync();

            await userHelper.CheckRoleAsync("Administrator");
            await userHelper.CheckRoleAsync("Intern");

            if(!dataContext.Administrators.Any())
            {
                var admin = await CheckUserAsync(20060067,"Brad","Pit", "2224567896", "brad@gmail.com","123456","Administrator");
                await CheckAdminAsync(admin);
                admin = await CheckUserAsync(20060068, "Pekora", "Usada", "2224567898", "pekopeko@gmail.com", "123456", "Administrator");
                await CheckAdminAsync(admin);
            }
        }

        private async Task CheckAdminAsync(User user)
        {
            dataContext.Administrators.Add(new Administrator { User = user });
            await dataContext.SaveChangesAsync();
        }

        private async Task<User> CheckUserAsync(int regNumber,string firstName, string lastName, string phoneNumber, 
            string email, string password, string role)
        {
            var user = await userHelper.GetUserByEmailAsync(email);
            if(user==null)
            {
                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    PhoneNumber = phoneNumber,
                    RegistrationNumber = regNumber,
                    Email = email,
                    UserName = regNumber.ToString()
                };

                var result = await userHelper.AddUserAsync(user, password);
                if(result!=IdentityResult.Success)
                {
                    throw new InvalidOperationException("User could not have been created");
                }
                await userHelper.AddUserToRoleAsync(user, role);
            }

            return user;
        }
    }
}
