using System.ComponentModel.DataAnnotations;

namespace WashingCar.DAL.Entities
{
    public class VehicleDetails : Entity
    {
        [Display(Name = "Vehiculo.")]
        public virtual Vehicle Vehicle { get; set; }

        [Display(Name = "Fecha de entrega")]
        public virtual DateTime? DeliveryDate { get; set; }
    }
}
