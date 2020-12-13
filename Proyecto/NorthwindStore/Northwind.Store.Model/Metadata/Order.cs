using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Store.Model
{
    [ModelMetadataType(typeof(OrderMetadata))]
    public partial class Order : ModelBase
    {
        public class OrderMetadata
        {
            [Required(ErrorMessage = "Debe ingresar el código del cliente.")]
            [Display(Name = "CustomerId", Prompt = "Digite el código del cliente")]
            [StringLength(5)]
            public string CustomerId { get; set; }

            [NotMapped]
            public decimal OrderSubTotal { get; set; }

        }
    }
}
