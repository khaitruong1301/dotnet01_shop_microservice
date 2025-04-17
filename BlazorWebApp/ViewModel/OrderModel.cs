namespace BlazorWebApp.ViewModel
{

    public class OrderModel
    {
        public int UserId { get; set; } = 2;
   
        public long ProductId { get; set; }
        public int Quantity { get; set; } = 1;
        public double Price { get; set; }

    }


}