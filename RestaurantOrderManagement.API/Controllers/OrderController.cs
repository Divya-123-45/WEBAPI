using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantOrderManagement.API.Data;
using RestaurantOrderManagement.API.Model;
namespace RestaurantOrderManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private static List<OrderModel> order = new List<OrderModel>()
        {
            new OrderModel
            {
                OrderId=1,
                CustomerName="Divya",
                Quantity=1,
                TotalPrice=70,
                OrderDate=DateTime.Now,
                MenuItemId=1
            },
            new OrderModel
            {
                OrderId=2,
                CustomerName="Shree",
                Quantity=2,
                TotalPrice=80,
                OrderDate=DateTime.Now,
                MenuItemId=2
            }
        };

        //Get the details of all orders
        [HttpGet]
        public ActionResult<IEnumerable<OrderModel>> GetOrders()
        {
            return Ok(order);
        }


        //Get the details of all orders by id
        [HttpGet("{OrderId:int}")]
        public ActionResult<IEnumerable<OrderModel>> GetOrderById(int OrderId)
        {
            var orders = order.Where(c=>c.OrderId == OrderId).FirstOrDefault();
            if(orders == null)
            {
                return BadRequest("No Customers Found");
            }
            return Ok(orders);
        }

        [HttpPost]
        public ActionResult<OrderModel> Create(OrderModel newOrder)
        {
            //newOrder.OrderId= order.Max(c=>c.OrderId)+1;
            newOrder.MenuItemId = order.Max(c => c.MenuItemId) + 1;
            order.Add(newOrder);
            return CreatedAtAction(nameof(GetOrders), new { id = newOrder.OrderId }, newOrder);
        }


        //Updating the order details
        [HttpPut("{id:int}")]
        public ActionResult<IEnumerable<MenuItemModel>> Update(int id, [FromBody] OrderModel orderModel)
        {
            if (orderModel == null || id != orderModel.OrderId)
            {
                return BadRequest("No Such order item");
            }
            var existingOrderItem = order.FirstOrDefault(c => c.OrderId == id);
            existingOrderItem.OrderId = orderModel.OrderId;
            existingOrderItem.CustomerName = orderModel.CustomerName;
            existingOrderItem.Quantity = orderModel.Quantity;
            existingOrderItem.TotalPrice = orderModel.TotalPrice;
            existingOrderItem.OrderDate = orderModel.OrderDate;
            existingOrderItem.MenuItemId = orderModel.MenuItemId;

            return Ok(orderModel);
        }



        //Deleting the orders

        [HttpDelete("{OrderId:int}")]
        public ActionResult<IEnumerable<OrderModel>> Delete(int OrderId)
        {
            var orders = order.Where(c => c.OrderId == OrderId).FirstOrDefault();
            if (orders == null)
            {
                return BadRequest("No Orders Found");
            }
            //var food = MenuItemController.menuItems.Where(c=>c.MenuItemId == OrderId).FirstOrDefault();
            //string foodname = food.MName;
            order.Remove(orders);
            return Ok($"Order {OrderId} is deleted Successfully");
        }


        //Search by customer name
        [HttpGet("SERACH")]
        public async Task<IActionResult> Search([FromQuery] string customerName)
        {


            if (string.IsNullOrWhiteSpace(customerName))
            {
                return BadRequest("Customer name is required");
            }
            var orders = order.Where(c => c.CustomerName.Contains(customerName, System.StringComparison.OrdinalIgnoreCase)).ToList();
            if (!orders.Any())
            {
                return NotFound("No name found matching the search result");
            }
            return Ok(orders);


        }

    }
}
