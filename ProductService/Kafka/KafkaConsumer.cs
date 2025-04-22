using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProductService.Models;

namespace ProductService.Kafka
{
    public class KafkaConsumer : BackgroundService
    {
        IServiceScopeFactory scopeFactory;

        public KafkaConsumer(IServiceScopeFactory sf){
            scopeFactory = sf;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run(() =>
            {
                _ = ComsumeAsync("order-topic", stoppingToken);
            }, stoppingToken);
        }

        public async Task ComsumeAsync(string topic, CancellationToken stoppingToken)
        {
            var config = new ConsumerConfig
            {
                GroupId = "order-group-1",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = false
            };

            using var consumer = new ConsumerBuilder<string, string>(config).Build();
            consumer.Subscribe(topic);

            while (!stoppingToken.IsCancellationRequested)
            {
                var consumeResult = consumer.Consume(stoppingToken);
                using var scope =  scopeFactory.CreateScope();
                //Tạo context trong consumer
                var _context = scope.ServiceProvider.GetRequiredService<ProductDbServiceContext>();
                try
                {
                    string orderId = consumeResult.Key; //orderid
                    var messageValue = consumeResult.Message.Value;
                    // 👇 Change 'YourModel' to your actual model class
                    var data = JsonSerializer.Deserialize<OrderItem>(messageValue);
                    //handle xử lý database 
                    await _context.Database.BeginTransactionAsync();
                    var prod = _context.Products.SingleOrDefault(pro => pro.Id == data.ProductId);
                    if(prod != null) {
                        prod.Stock -= data.Quantity;
                    }
                    await _context.SaveChangesAsync();
                    await _context.Database.CommitTransactionAsync();
                    //done message của consumer
                    consumer.Commit(consumeResult);
                }
                catch (Exception ex)
                {
                    //log db hoặc log file với các mess lỗi
                    await _context.Database.RollbackTransactionAsync();

                }


            }
        }
    }
}


public partial class OrderItem
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

}




