namespace ECommerce.DTOs
{
    public class UpdateOrderDto
    {
        public DateTime OrderDate { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
    }
}
