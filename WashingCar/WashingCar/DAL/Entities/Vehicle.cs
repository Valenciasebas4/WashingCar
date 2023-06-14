using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WashingCar.DAL.Entities
{
    public class Vehicle : Entity
    {
        #region Properties

        [Display(Name = "Servicio.")]
        public virtual Service Service { get; set; }
        //public Guid ServiceID { get; set; }
        
        [Display(Name = "Propietario.")]
        public virtual User User { get; set; }
        public string UserId { get; internal set; }

        [Display(Name = "Propietario")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Owner { get; set; }

        [Display(Name = "Número de Placa")]
        [MaxLength(6)]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string NumberPlate { get; set; }

    

      

        #endregion
    }
}
