using System.Diagnostics.Metrics;
using WashingCar.DAL.Entities;
using WashingCar.Enum;
using WashingCar.Helpers;


namespace WashingCar.DAL
{
    public class SeederDb
    {
        private readonly DataBaseContext _context;
        private readonly IUserHelper _userHelper;
        

        public SeederDb(DataBaseContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
            
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await PopulateRolesAsync();
            await PopulateUserAsync("Sebastian", "Londoño", "sebas@yopmail.com", "3142393101", "Barbosa", "1035234145", UserType.Admin);
            await PopulateUserAsync("Jessica", "Gomez", "jess@yopmail.com", "3142393101", "Barbosa", "1035232261", UserType.User);
            await PopulateServiceAsync();
            // await PopulateUserAsync("Bill", "Gates", "bill_gates_user@yopmail.com", "4005656656", "Street Microsoft", "405060", UserType.User);
            await _context.SaveChangesAsync();
        }

        private async Task PopulateRolesAsync()
        {
            await _userHelper.AddRoleAsync(UserType.Admin.ToString());
            await _userHelper.AddRoleAsync(UserType.User.ToString());
        }

        private async Task PopulateUserAsync(string firstName, string lastName, string email, string phone, string address, string document, UserType userType)
        {
            User user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
              

                user = new User
                {
                    CreatedDate = DateTime.Now,
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,               
                    UserType = userType
               
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());
            }
        }


        private async Task PopulateServiceAsync()
        {
            if (!_context.Services.Any())
            {
                _context.Services.Add(new Service { Name = "Lavada simple", Price = 25000 , CreatedDate= DateTime.Now });
                _context.Services.Add(new Service { Name = "Lavada + Polishada", Price = 50000, CreatedDate = DateTime.Now });
                _context.Services.Add(new Service { Name = "Lavada + Aspirada de Cojinería", Price = 30000, CreatedDate = DateTime.Now });
                _context.Services.Add(new Service { Name = "Lavada Full", Price = 65000 , CreatedDate = DateTime.Now });
                _context.Services.Add(new Service { Name = "Lavada en seco del Motor", Price = 80000 , CreatedDate = DateTime.Now });
                _context.Services.Add(new Service { Name = "Lavada Chasis", Price = 90000 , CreatedDate = DateTime.Now });
            }
        }


        






    }
}
