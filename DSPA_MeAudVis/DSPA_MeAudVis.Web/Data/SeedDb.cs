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

            //roles
            await userHelper.CheckRoleAsync("Administrator");
            await userHelper.CheckRoleAsync("Intern");
            await userHelper.CheckRoleAsync("Applicant");
            await userHelper.CheckRoleAsync("Owner");

            if (!dataContext.Administrators.Any())
            {
                var admin = await CheckUserAsync(20060067, "Samuel", "Librado", "4424331292", "samuelisaaclibradoalmada@gmail.com", "samLIB67-", "Administrator");
                await CheckAdminAsync(admin);
                admin = await CheckUserAsync(7047541, "David", "Hernandez", "2221975824", "david.hdezalv29@gmail.com", "davHER24-", "Administrator");
                await CheckAdminAsync(admin);
                admin = await CheckUserAsync(20058343, "Arturo", "Villegas", "2229074543", "percentnevada3@gmail.com", "artVIL43-", "Administrator");
                await CheckAdminAsync(admin);
            }

            if (!dataContext.Interns.Any())
            {
                var intern = await CheckUserAsync(3060912, "Julio", "Gamesa", "2223436324", "julio@gmail.com", "julGAM24-", "Intern");
                await CheckInternAsync(intern);
            }

            if (!dataContext.Owners.Any())
            {
                var owner = await CheckUserAsync(6666, "Miguel", "Ochoa", "2225675423", "miguelochoa@gmail.com", "migOCH66-", "Owner");
                await CheckOwnerAsync(owner);
            }

            if (!dataContext.ApplicantTypes.Any())
            {
                dataContext.ApplicantTypes.Add(new ApplicantType { Type = "Student" });
                dataContext.ApplicantTypes.Add(new ApplicantType { Type = "Teacher" });
                await dataContext.SaveChangesAsync();
            }

            if (!dataContext.Applicants.Any())
            {
                var applicant = await CheckUserAsync(20060069, "Lalo", "Momento", "2224567810", "lalomomento@gmail.com", "lalMOM10-", "Applicant");
                await CheckApplicantAsync(applicant);
            }


            if (!dataContext.Handbooks.Any())
            {
                var owner = dataContext.Owners.FirstOrDefault();
                dataContext.Handbooks.Add(new Handbook { Name = "Microphone handbook", Owner=owner });
                await dataContext.SaveChangesAsync();
            }  

            if (!dataContext.Loans.Any())
            {
                var applicant = dataContext.Applicants.FirstOrDefault();
                var intern = dataContext.Interns.FirstOrDefault();
                dataContext.Loans.Add(new Loan { Applicant = applicant, Intern=intern });
                await dataContext.SaveChangesAsync();
            }

            if (!dataContext.Statuses.Any())
            {
                dataContext.Statuses.Add(new Status { StatusName = "Available" });
                dataContext.Statuses.Add(new Status { StatusName = "Loaned" });
                dataContext.Statuses.Add(new Status { StatusName = "Returned" });
                dataContext.Statuses.Add(new Status { StatusName = "Broken" });
                dataContext.Statuses.Add(new Status { StatusName = "Reserved" });
                await dataContext.SaveChangesAsync();
            }

            if (!dataContext.Materials.Any())
            {
                var status = dataContext.Statuses.FirstOrDefault(m=>m.Id==1);
                dataContext.Materials.Add(new Material { Name = "HDMI", Label = "MAV1", Brand = "Sony", MaterialModel = "PS5", SerialNum = "Z10954", Status = status });
                await dataContext.SaveChangesAsync();
            }

            if (!dataContext.LoanDetails.Any())
            {
                var loan = dataContext.Loans.FirstOrDefault();
                var status = dataContext.Statuses.FirstOrDefault(m => m.Id == 3);
                var material = dataContext.Materials.FirstOrDefault();
                dataContext.LoanDetails.Add(new LoanDetail { Loan=loan, DateTimeIn = DateTime.Now.AddDays(3), DateTimeOut = DateTime.Now, Material =material, Status=status, Observations=string.Empty });
                await dataContext.SaveChangesAsync();
            }

        }

        private async Task CheckInternAsync(User user)
        {
            dataContext.Interns.Add(new Intern { User = user, DepartureTime=9, EntryTime=7 });
            await dataContext.SaveChangesAsync();
        }

        private async Task CheckOwnerAsync(User user)
        {
            dataContext.Owners.Add(new Owner { User = user});
            await dataContext.SaveChangesAsync();
        }

        private async Task CheckAdminAsync(User user)
        {
            dataContext.Administrators.Add(new Administrator { User = user});
            await dataContext.SaveChangesAsync();
        }

        private async Task CheckApplicantAsync(User user)
        {
            var applicantType = dataContext.ApplicantTypes.FirstOrDefault();
            dataContext.Applicants.Add(new Applicant { User = user, Type=applicantType, Debtor=false });
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
