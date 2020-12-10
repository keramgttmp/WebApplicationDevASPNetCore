using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Store.Model
{
    [ModelMetadataType(typeof(EmployeeMetadata))]
    public partial class Employee : ModelBase
    {
        /// <summary>
        /// Fotografía en formato Base64. Para utilizar en presentación.
        /// </summary>
        [NotMapped]
        public string PictureBase64 { get; set; }

        public class EmployeeMetadata
        {
            [Required(ErrorMessage = "El apellido es requerido.")]
            [Display(Name = "Apellido", Prompt = "Digite el apellido")]
            public string LastName { get; set; }
            [Required(ErrorMessage = "El nombre es requerido.")]
            [Display(Name = "Nombre", Prompt = "Digite el nombre")]
            public string FirstName { get; set; }
        }
    }
}
