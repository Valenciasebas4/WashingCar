
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        private string GetUserId()
        {
            return _context.Users
                .Where(u => u.Email == User.Identity.Name)
                .Select(u => u.Id.ToString())
                .FirstOrDefault();
        }

        private string GetUserFullName()
        {
            return _context.Users
                .Where(u => u.Email == User.Identity.Name)
                .Select(u => u.FullName)
                .FirstOrDefault();
        }

        private string GetServiceName()
        {
            string userId = GetUserId(); // Obtener el ID del usuario actual
            var training = _context.Vehicles
                .Where(t => t.Owner.Id == userId) // Filtrar por el ID del usuario
                .Select(t => t.Service.Name)
                .FirstOrDefault();

            return training;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.UserFullName = GetUserFullName();

            ViewBag.TrainingName = GetServiceName();
            ViewBag.UserId = GetUserId();


            return View(await _context.Vehicles
                .Include(o => o.Owner)
                .Include(o => o.Service)
                .ToListAsync());
            Problem("Entity set 'DataBaseContext.UserTrainings'  is null.");
            
        }
/*
        public async Task<IActionResult> MyServices()
        {
            ViewBag.UserFullName = GetUserFullName();

            ViewBag.TrainingName = GetServiceName();
            ViewBag.UserId = GetUserId();


            return View(await _context.Vehicles
                .Include(o => o.Owner)
                .Include(o => o.Service)
                .ToListAsync());
            Problem("Entity set 'DataBaseContext.Vehicles'  is null.");

        }
*/
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

                // Calcular la fecha mínima permitida para el nuevo registro
                DateTime maxDate = DateTime.Now.Date.AddDays(-2);
                // Verificar si ya existe un vehículo con la misma placa y fecha actual
                bool vehicleExists = await _context.Vehicles
                    .AnyAsync(v => v.NumberPlate == addVehicleViewModel.NumberPlate &&
                                   v.CreatedDate > maxDate);
                if (vehicleExists)
                {
                    ModelState.AddModelError(string.Empty, "Ya se ha registrado un vehículo con la misma placa en los últimos 2 días.");
                }
                else
                {
                    try
                    {
                        Vehicle vehicle = new()
                        {
                            CreatedDate = DateTime.Now,
                            Service = await _context.Services.FindAsync(addVehicleViewModel.ServiceId),
                            Owner= user,
                            NumberPlate = addVehicleViewModel.NumberPlate,
                        };

                        VehicleDetails vehicleDetails = new()
                        {
                            CreatedDate = DateTime.Now,
                            Vehicle = vehicle,

                        };

                       


                        _context.Add(vehicle);
                        _context.Add(vehicleDetails);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(MyServices));
                    }

                    catch (Exception exception)
                    {
                        ModelState.AddModelError(string.Empty, exception.Message);
                    }
                }
            }

            addVehicleViewModel.Services = await _dropDownListHelper.GetDDLServicesAsync();
            return View(addVehicleViewModel);
        }

        
    }
}
