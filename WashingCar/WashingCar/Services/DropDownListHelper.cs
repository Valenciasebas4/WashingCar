using WashingCar.DAL;
using WashingCar.DAL.Entities;
using WashingCar.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WashingCar.Services
{
    public class DropDownListHelper : IDropDownListHelper
    {
        public readonly DataBaseContext _context;

        public DropDownListHelper(DataBaseContext context)
        {
            _context = context;
        }



        public async Task<IEnumerable<SelectListItem>> GetDDLServicesAsync()
        {
            List<SelectListItem> listServices = await _context.Services
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString(),
                })
                .OrderBy(c => c.Text)
                .ToListAsync();

            listServices.Insert(0, new SelectListItem
            {
                Text = "Seleccione un servicio...",
                Value = Guid.Empty.ToString(),
                Selected = true
            });

            return listServices;
        }


    }
}
