public class MenuItem
{
    public int Id { get; set; }
    public string ItemCode { get; set; } = string.Empty;
    public string ItemName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal GSTPercentage { get; set; }
    public decimal MRP { get; set; }
    public decimal SalesRate { get; set; }
    public byte[]? ImageData { get; set; }
    public bool IsAvailable { get; set; }

    public int CategoryId { get; set; }
    public MenuCategory? Category { get; set; }
}
