using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WashingCar.DAL;
using WashingCar.DAL.Entities;
using WashingCar.Helpers;

namespace WashingCar.Controllers
{
    public class VehiclesDetailsController : Controller
    {
        private readonly DataBaseContext _context;
        
        private readonly IUserHelper _userHelper;

        public VehiclesDetailsController(DataBaseContext context, IUserHelper userHelper)
        {
            _context = context;
            
            _userHelper = userHelper;

        }

        private string GetUserId()
        {
            return _context.Users
                .Where(u => u.Email == User.Identity.Name)
                .Select(u => u.Id)
                .FirstOrDefault();
        }
       

        private string GetUserFullName()
        {
            return _context.Users
                .Where(u => u.Email == User.Identity.Name)
                .Select(u => u.FullName)
                .FirstOrDefault();
        }




        public async Task<IActionResult> Index()
        {
            ViewBag.UserFullName = GetUserFullName();

            
            ViewBag.UserId = GetUserId();


            return View(await _context.VehiclesDetails
                .Include(o => o.Vehicle.Owner)
                .Include(o => o.Vehicle.Service)
                .ToListAsync());
            Problem("Entity set 'DataBaseContext.VehiclesDetails'  is null.");
            
        }

        public async Task<IActionResult> MyServices()
        {
            ViewBag.UserFullName = GetUserFullName();


            ViewBag.UserId = GetUserId();


            return View(await _context.VehiclesDetails
                .Include(o => o.Vehicle.Owner)
                .Include(o => o.Vehicle.Service)
                .ToListAsync());
            Problem("Entity set 'DataBaseContext.VehiclesDetails'  is null.");

        }

        public async Task<IActionResult> Edit(Guid? id)
        {
            
            if (id == null || _context.VehiclesDetails == null) return NotFound();

            var vehicleDetails = await _context.VehiclesDetails.FindAsync(id);
            if (vehicleDetails == null) return NotFound();

            return View(vehicleDetails);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Guid id, VehicleDetails vehicleDetails)
        {
            if (id != vehicleDetails.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    vehicleDetails.ModifiedDate = DateTime.Now;
                    _context.Update(vehicleDetails);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                        ModelState.AddModelError(string.Empty, "Ya existe una categoria con el mismo nombre.");
                    else
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(vehicleDetails);
        }
    }
}
