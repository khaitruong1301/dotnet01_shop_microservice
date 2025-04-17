namespace OrderService.ViewModel
{

    public class OrderRequest
    {
        public int UserId { get; set; } = 1;
        public int ProductId { get; set; }
        public int Quantity { get; set; } = 1;
        public decimal Price { get; set; }
    }


}