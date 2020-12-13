using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Store.Model
{
    [ModelMetadataType(typeof(TerritoryMetadata))]
    public partial class Territory : ModelBase
    {
        public class TerritoryMetadata
        {
            [Required(ErrorMessage = "La descripción del territorio es requerida.")]
            [Display(Name = "Descripción", Prompt = "Digite la descripción del territorio")]
            [StringLength(50)]
            public string TerritoryDescription { get; set; }

        }
    }
}
