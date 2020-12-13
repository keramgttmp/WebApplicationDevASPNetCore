using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Store.Model
{
    [ModelMetadataType(typeof(SupplierMetadata))]
    public partial class Supplier : ModelBase
    {
        public class SupplierMetadata
        {
            [Required(ErrorMessage = "El nombre de la compañía es requerida.")]
            [Display(Name = "Nombre de la compañía", Prompt = "Digite el nombre de la compañía")]
            [StringLength(50)]
            public string CompanyName { get; set; }
            [Required(ErrorMessage = "El nombre del contacto es requerido.")]
            [Display(Name = "Nombre del Contacto", Prompt = "Digite el nombre del contacto")]

            [StringLength(30)]
            public string ContactName { get; set; }
        }
    }
}
