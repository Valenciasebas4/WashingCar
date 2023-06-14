using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WashingCar.DAL.Entities
{
    public class Vehicle : Entity
    {
        #region Properties

        [Display(Name = "Servicio.")]
        public virtual Service Service { get; set; }
        

        [Display(Name = "Propietario.")]
        //[Required(ErrorMessage = "El campo {0} es obligatorio")]
        public virtual User Owner { get; set; }

        [Display(Name = "Número de Placa")]
        [MaxLength(6)]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string NumberPlate { get; set; }

        [Display(Name = "Detalles Vehiculos")]
        public Guid VehicleDetailsId { get; set; }

        public List<VehicleDetails> VehiclesDetails { get; set; }
        #endregion
    }
}
