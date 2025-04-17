

var builder = DistributedApplication.CreateBuilder(args);

// var IdentityApi = builder.AddProject<Projects.IdentityService>("identity-api");
// var OrderApi = builder.AddProject<Projects.OrderService>("order-api");
var ProductApi = builder.AddProject<Projects.ProductService>("product-api");
var Payment = builder.AddProject<Projects.PaymentService>("payment-api");
// var BlazorApp = builder.AddProject<Projects.BlazorWebApp>("blazor-web-app");

builder.AddProject<Projects.IdentityService>("identity-api")
// .WithReference(IdentityApi)
// .WithReference(OrderApi)
.WithReference(ProductApi)
.WithReference(Payment);

builder.Build().Run();

