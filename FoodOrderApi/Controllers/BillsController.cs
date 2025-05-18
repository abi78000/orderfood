using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class BillsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public BillsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("saveBill")]
    public async Task<IActionResult> SaveBill([FromBody] BillRequest billRequest)
    {
        var totalAmount = 0m;
        foreach (var item in billRequest.Items)
        {
            decimal gstAmount = item.SalesRate * item.GstPercentage / 100;
            decimal itemTotal = (item.SalesRate * item.Quantity) + gstAmount;
            totalAmount += itemTotal;

            var billDetail = new BillDetail
            {
                CustomerName = billRequest.CustomerName,
                PaymentMethod = billRequest.PaymentMethod,
                ItemName = item.ItemName,
                ItemCode = item.ItemCode,
                Description = item.Description,
                SalesRate = item.SalesRate,
                GstPercentage = item.GstPercentage,
                Quantity = item.Quantity,
                ItemTotal = itemTotal,
                GstAmount = gstAmount,
                TotalAmount = totalAmount,
                CreatedAt = DateTime.UtcNow // Ensure this is UTC
            };

            _context.BillDetails.Add(billDetail);
        }

        await _context.SaveChangesAsync();
        return Ok(new { TotalAmount = totalAmount });
    }
}

public class BillRequest
{
    public string? CustomerName { get; set; }
    public string? PaymentMethod { get; set; }
    public List<BillItemRequest>? Items { get; set; }
}

public class BillItemRequest
{
    public string? ItemName { get; set; }
    public string? ItemCode { get; set; }
    public string? Description { get; set; }
    public decimal SalesRate { get; set; }
    public decimal GstPercentage { get; set; }
    public int Quantity { get; set; }
}
