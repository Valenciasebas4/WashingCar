﻿using System.Diagnostics.Metrics;
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
            // await PopulateUserAsync("Bill", "Gates", "bill_gates_user@yopmail.com", "4005656656", "Street Microsoft", "405060", UserType.User);
            await _context.SaveChangesAsync();
        }
/*
        private async Task PopulateUserAsync()
        {
            
            if (!_context.Users.Any())
            {
                _context.Users.Add(new User { FirstName = "Sebastian", LastName = "Londoño", Email = "sebas@yopmail.com", PhoneNumber = "3142393101", Address = "Barbosa", Document = "1035234145", UserType = UserType.Admin });

            }
            
        }
*/
        




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


        

        
    }
}
