using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Store.Model
{
    [ModelMetadataType(typeof(ProductMetadata))]
    public partial class Product : ModelBase
    {
        /// <summary>
        /// Fotografía en formato Base64. Para utilizar en presentación.
        /// </summary>
        [NotMapped]
        public string PictureBase64 { get; set; }

        public class ProductMetadata
        {
            [Required(ErrorMessage = "El nombre del producto es requerido.")]
            [Display(Name = "Producto", Prompt = "Digite el nombre del producto")]
            public string ProductName { get; set; }
        }
    }
}
