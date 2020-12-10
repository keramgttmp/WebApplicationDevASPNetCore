using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Store.Model
{
    [ModelMetadataType(typeof(RegionMetadata))]
    public partial class Region : ModelBase
    {
        public class RegionMetadata
        {
            [Required(ErrorMessage = "La descripción de la region es requerida.")]
            [Display(Name = "Region", Prompt = "Digite la descripción de la región")]
            [StringLength(50)]
            public string RegionDescription { get; set; }
        }
    }
}
