using OrderService.Models;
using Microsoft.EntityFrameworkCore;
using OrderService.Kafka;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("OrderDbService");
builder.Services.AddDbContext<OrderDbServiceContext>(options =>
    options.UseSqlServer(connectionString));
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Add middleware controller
builder.Services.AddControllers();
//bật cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("allow_all", builder =>
    {
            builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
//Khởi tạo producerKafka
builder.Services.AddScoped<IKafkaProducer,KafkaProducer>();



var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("allow_all");
app.MapControllers();


app.UseHttpsRedirection();

app.Run();
