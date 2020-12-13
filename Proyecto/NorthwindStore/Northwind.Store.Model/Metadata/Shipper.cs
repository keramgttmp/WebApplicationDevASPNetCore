using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Store.Model
{
    [ModelMetadataType(typeof(ShipperMetadata))]
    public partial class Shipper : ModelBase
    {
        public class ShipperMetadata
        {
            [Required(ErrorMessage = "El nombre de la compañía es requerida.")]
            [Display(Name = "Nombre de la Compañía", Prompt = "Digite el nombre de la compañía")]
            [StringLength(50)]
            public string CompanyName { get; set; }
        }
    }
}
