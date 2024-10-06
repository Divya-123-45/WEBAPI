using Microsoft.EntityFrameworkCore;
using RestaurantOrderManagement.API.Model;

namespace RestaurantOrderManagement.API.Data
{
    public class RestaurantDbContext : DbContext
    {
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> restaurantDbContext) :base(restaurantDbContext)
        {

        }
        public DbSet<MenuItemModel> MenuItems { get; set; }
        public DbSet<OrderModel> orderModel { get; set; }
    }
}
