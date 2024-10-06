using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantOrderManagement.API.Data;

namespace RestaurantOrderManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
   
    public class MenuItemController : ControllerBase
    {
       public static List<MenuItemModel> menuItems = new List<MenuItemModel>()
        {
            new MenuItemModel
            {
                MenuItemId=1,
                MName="Dosa",
                Price=70,
                Category="South Indian",
                ItemAvailability="Available"
            },
            new MenuItemModel
            {
                MenuItemId=2,
                MName="Idli",
                Price=40,
                Category="South Indian",
                ItemAvailability="Available"

            }
        };


        //Get all MenuItems
        
        [HttpGet]
        public ActionResult<IEnumerable<MenuItemModel>> GetItems()
        {
            return Ok(menuItems);
        }


        //Get MenuItems by id
       
        [HttpGet("{id:int}")]
        public ActionResult<IEnumerable<MenuItemModel>> GetItemById(int id)
        {
            var item= menuItems.Where(c=> c.MenuItemId==id).FirstOrDefault();
            return Ok(item);
        }



        //Create new menu item
      
        [HttpPost]
        public ActionResult<MenuItemModel> Create(MenuItemModel newMenuItem)
        {
            newMenuItem.MenuItemId = menuItems.Max(c => c.MenuItemId) + 1;
            menuItems.Add(newMenuItem);
            return CreatedAtAction(nameof(GetItems),new { id = newMenuItem.MenuItemId }, newMenuItem);
        }

        //Update details of menu item
       
        [HttpPut("{id:int}")]
        public ActionResult<IEnumerable<MenuItemModel>> Update(int id,[FromBody] MenuItemModel newMenuItem)
        {
            if(newMenuItem == null || id!= newMenuItem.MenuItemId)
            {
                return BadRequest("No Such menu item");
            }
            var existingMenuItem=menuItems.FirstOrDefault(c=>c.MenuItemId==id);
            existingMenuItem.MenuItemId= newMenuItem.MenuItemId;
            existingMenuItem.MName= newMenuItem.MName;
            existingMenuItem.Price= newMenuItem.Price;
            existingMenuItem.Category= newMenuItem.Category;
            existingMenuItem.ItemAvailability= newMenuItem.ItemAvailability;
            
            return Ok(newMenuItem);
        }

       
        //Delete item from menu
        
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var item = menuItems.Where(c=>c.MenuItemId==id).FirstOrDefault();
            if (item == null)
            {
                return BadRequest("No item found");
            }
            menuItems.Remove(item);
            return Ok();
        }

        //Serach the details of menu item by its id or name
        [HttpGet("SERACH")]
        public async Task<IActionResult> Search([FromQuery] string itemName)
        {
            if (string.IsNullOrWhiteSpace(itemName))
            {
                return BadRequest("Item name is required");
            }
            var item = menuItems.Where(c => c.MName.Contains(itemName, System.StringComparison.OrdinalIgnoreCase)).ToList();
            if (!item.Any())
            {
                return NotFound("No name found matching the search result");
            }
            return Ok(item);

        }
    }
}
