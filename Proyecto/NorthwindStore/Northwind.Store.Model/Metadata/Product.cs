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

            [Required(ErrorMessage = "El id del producto es requerido.")]
            [Display(Name = "IdProducto", Prompt = "Digite el id del producto")]
            public int ProductId { get; set; }
            
            [Required(ErrorMessage = "El SupplierID del producto es requerido.")]
            [Display(Name = "SupplierID", Prompt = "Digite el SupplierID del producto")]
            [Column("SupplierID")]
            public int? SupplierId { get; set; }

            [Required(ErrorMessage = "La CategoryID del producto es requerido.")]
            [Display(Name = "CategoryID", Prompt = "Digite la CategoryID del producto")]
            [Column("CategoryID")]
            public int? CategoryId { get; set; }

            [Required(ErrorMessage = "La propiedad QuantityPerUnit del producto es requerido.")]
            [Display(Name = "QuantityPerUnit", Prompt = "Digite la propiedad QuantityPerUnit del producto")]
            [StringLength(20)]
            public string QuantityPerUnit { get; set; }

            [Required(ErrorMessage = "La propiedad UnitPrice del producto es requerido.")]
            [Display(Name = "UnitPrice", Prompt = "Digite La propiedad UnitPrice del producto")]
            [Column(TypeName = "money")]
            public decimal? UnitPrice { get; set; }

            [Required(ErrorMessage = "La propiedad UnitsInStock del  producto es requerido.")]
            [Display(Name = "UnitsInStock", Prompt = "Digite La propiedad UnitsInStock del  del producto")]
            public short? UnitsInStock { get; set; }

            [Required(ErrorMessage = "La propiedad UnitsOnOrder del producto es requerido.")]
            [Display(Name = "UnitsOnOrder", Prompt = "La propiedad UnitsOnOrder del producto")]
            public short? UnitsOnOrder { get; set; }

            [Required(ErrorMessage = "La propiedad ReorderLevel producto es requerido.")]
            [Display(Name = "ReorderLevel", Prompt = "Digite La propiedad ReorderLevel del producto")]
            public short? ReorderLevel { get; set; }

            [Required(ErrorMessage = "La propiedad Discontinued del producto es requerido.")]
            [Display(Name = "Discontinued", Prompt = "Digite La propiedad Discontinued  del producto")]
            public bool Discontinued { get; set; }
            /// <summary>
            /// Para quiz2
            public byte[] Picture { get; set; }
        }
    }
}
