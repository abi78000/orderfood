using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.EntityFrameworkCore;


[ApiController]
[Route("api/[controller]")]
public class BillDetailsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public BillDetailsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BillDetail>>> GetBillDetails()
    {
        var result = await _context.BillDetails
            .OrderByDescending(b => b.CreatedAt)
            .ToListAsync();

        return Ok(result);
    }
}
