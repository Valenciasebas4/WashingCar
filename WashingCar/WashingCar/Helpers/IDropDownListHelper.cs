using WashingCar.DAL.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WashingCar.Helpers
{
    public interface IDropDownListHelper
    {
        Task<IEnumerable<SelectListItem>> GetDDLServicesAsync();

    }
}
