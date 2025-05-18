using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading.Tasks;

namespace FoodOrderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MenuItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET api/menuitems
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _context.MenuItems
                .Include(m => m.Category)
                .Select(m => new
                {
                    m.Id,
                    m.ItemCode,
                    m.ItemName,
                    m.Description,
                    m.GSTPercentage,
                    m.MRP,
                    m.SalesRate,
                    m.IsAvailable,
                    CategoryName = m.Category!.Name,
                    ImageBase64 = m.ImageData != null ? Convert.ToBase64String(m.ImageData) : null
                })
                .ToListAsync();

            return Ok(items);
        }

        // POST api/menuitems
        [HttpPost]
        public async Task<IActionResult> Add([FromForm] MenuItemDto itemDto)
        {
            // Validate CategoryId exists in MenuCategories table
            var category = await _context.MenuCategories.FindAsync(itemDto.CategoryId);
            if (category == null)
            {
                return BadRequest(new { message = $"Category with ID {itemDto.CategoryId} does not exist." });
            }

            byte[]? imageData = null;
            if (itemDto.Image != null)
            {
                using var ms = new MemoryStream();
                await itemDto.Image.CopyToAsync(ms);
                imageData = ms.ToArray();
            }

            var item = new MenuItem
            {
                ItemCode = itemDto.ItemCode,
                ItemName = itemDto.ItemName,
                Description = itemDto.Description,
                GSTPercentage = itemDto.GSTPercentage,
                MRP = itemDto.MRP,
                SalesRate = itemDto.SalesRate,
                CategoryId = itemDto.CategoryId,
                IsAvailable = itemDto.IsAvailable,
                ImageData = imageData
            };

            _context.MenuItems.Add(item);
            await _context.SaveChangesAsync();
            return Ok(new { item.Id });
        }
    }
}

public class MenuItemDto
{
    public string? ItemCode { get; set; }
    public string? ItemName { get; set; }
    public string? Description { get; set; }
    public decimal GSTPercentage { get; set; }
    public decimal MRP { get; set; }
    public decimal SalesRate { get; set; }
    public int CategoryId { get; set; }
    public bool IsAvailable { get; set; } = true;
    public IFormFile? Image { get; set; }
}
