
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WashingCar.DAL;
using WashingCar.Helpers;
using WashingCar.Models;

namespace WashingCar.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly DataBaseContext _context;
        private readonly IDropDownListHelper _dropDownListHelper;

        public VehiclesController(DataBaseContext context, IDropDownListHelper dropDownListHelper)
        {
            _context = context;
            
            _dropDownListHelper = dropDownListHelper;
    
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

    }
}
