using Microsoft.EntityFrameworkCore;
using ProductService.Kafka;
using ProductService.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Add service entity framework
var connectionString = builder.Configuration.GetConnectionString("ProductDbService");
builder.Services.AddDbContext<ProductDbServiceContext>(options =>
    options
        .UseLazyLoadingProxies(false)
        .UseSqlServer(connectionString));
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

//Chạy background service lắng nghe sự thay đổi topic trên kafka
// builder.Services.AddHostedService<KafkaConsumer>();


builder.WebHost.UseUrls("http://*:80");

var app = builder.Build();
// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();
app.UseCors("allow_all");
app.MapControllers();
app.UseHttpsRedirection();
app.Urls.Add("http://*:80");


app.Run();

