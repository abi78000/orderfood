public class BillDetail
{
    public int Id { get; set; }
    public string? CustomerName { get; set; }
    public string? PaymentMethod { get; set; }
    public string? ItemName { get; set; }
    public string? ItemCode { get; set; }
    public string? Description { get; set; }
    public decimal SalesRate { get; set; }
    public decimal GstPercentage { get; set; }
    public int Quantity { get; set; }
    public decimal ItemTotal { get; set; }
    public decimal GstAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime CreatedAt { get; set; }
}
