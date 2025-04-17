namespace BlazorWebApp.ViewModel{
    public class ProductModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public long Stock { get; set; }
        public long CategoryId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public object[] ProductImages { get; set; }
    }
}


