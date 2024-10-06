using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RestaurantOrderManagement.API.Data
{
    public class MenuItemModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MenuItemId { get; set; }

        [Required(ErrorMessage ="Menu Item Name is required")]
        [StringLength(50)]
        public string MName { get; set; }

        [Required(ErrorMessage = "price is required")]
        [Range(10,1000)]
        public int Price { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [StringLength(30)]
        public string Category { get; set; }

        [Required(ErrorMessage = "ItemAvailability is required")]
        [StringLength(20)]

        public string ItemAvailability { get; set; }
    }
}
