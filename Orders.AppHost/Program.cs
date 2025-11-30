using Microsoft.Extensions.Configuration;
using Projects;


var builder = DistributedApplication.CreateBuilder(args);






//var password = builder.AddParameter("msSql-password", "mssql123",secret: true);

//var userMsSql = builder.AddParameter("sql-username", "mssql", secret: true);

var sql = builder.AddSqlServer("mssql").WithDataVolume();

var mongo = builder.AddMongoDB("mongo")
    .WithDataVolume()
    .WithContainerRuntimeArgs("--restart", "on-failure"); // restart policy

var deliveryDb = mongo.AddDatabase("deliveryDb");

var ordersDb = sql.AddDatabase("ordersDb");

var catalogDb = sql.AddDatabase("catalogDb");

var deliveryService = builder.AddProject<Projects.Delivery_Api>("delivery-api")
    .WithReference(deliveryDb)
    .WaitFor(deliveryDb);


var catalogService = builder.AddProject<Projects.Catalog_Api>("catalog-api")
    .WithReference(catalogDb)
    .WaitFor(catalogDb);

var ordersService = builder.AddProject<Projects.Orders_Api>("orders-api")
    .WithReference(ordersDb)
    .WaitFor(ordersDb);


builder.AddProject<Projects.Gateway_Api>("gateway-api");

builder.AddProject<Projects.Aggreagator_Api>("aggreagator-api")
    .WithReference(deliveryService)
    .WithReference(catalogService)
    .WithReference(ordersService)
    .WaitFor(deliveryService)
    .WaitFor(catalogService)
    .WaitFor(ordersService);

builder.Build().Run();
