
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WashingCar.DAL;
using WashingCar.DAL.Entities;
using WashingCar.Helpers;
using WashingCar.Models;
using WashingCar.Services;

namespace WashingCar.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly DataBaseContext _context;
        private readonly IDropDownListHelper _dropDownListHelper;
        private readonly IUserHelper _userHelper;

        public VehiclesController(DataBaseContext context, IDropDownListHelper dropDownListHelper, IUserHelper userHelper)
        {
            _context = context;              
            _dropDownListHelper = dropDownListHelper;
            _userHelper = userHelper;

        }

        private string GetUserFullName()
        {
            return _context.Users
                .Where(u => u.Email == User.Identity.Name)
                .Select(u => u.FullName)
                .FirstOrDefault();
        }


        [Authorize]
        public async Task<IActionResult> Create()
        {
            ViewBag.UserFullName = GetUserFullName();
            AddVehicleViewModel addVehicleViewModel = new()
            {
                Services = await _dropDownListHelper.GetDDLServicesAsync(),
            };

            return View(addVehicleViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddVehicleViewModel addVehicleViewModel)
        {
            if (ModelState.IsValid)
            {
                User user = await _userHelper.GetUserAsync(User.Identity.Name);
                try
                {
                    Vehicle vehicle = new()
                    {

                        
                        CreatedDate = DateTime.Now,
                        Service = await _context.Services.FindAsync(addVehicleViewModel.ServiceId),
                        Owner = user.ToString(),
                        NumberPlate= addVehicleViewModel.NumberPlate,
                        User= user,
                    };


                    _context.Add(vehicle);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Create));
                }
               
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            addVehicleViewModel.Services = await _dropDownListHelper.GetDDLServicesAsync();
            return View(addVehicleViewModel);
        }

    }
}
