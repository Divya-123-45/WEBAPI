using RestaurantOrderManagement.API.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantOrderManagement.API.Model
{
    public class OrderModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }  //Primary Key
        public string CustomerName { get; set; }
        public int Quantity { get; set; }
        
        public int TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        [ForeignKey("MenuItemId")]
        public int MenuItemId { get; set; }   //Foreign key
        
    }
}
