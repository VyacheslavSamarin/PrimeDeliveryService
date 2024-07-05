namespace dev.Models
{
    public class UpdateOrderStatusRequest
    {
        public int OrderId { get; set; }
        public string OrderStatus { get; set; }
    }
}
