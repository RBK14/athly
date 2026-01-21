var builder = DistributedApplication.CreateBuilder(args);

var sqlServer = builder.AddSqlServer("sqlserver");

var searchDb = sqlServer.AddDatabase("sportevents-db");

var rabbitMq = builder.AddRabbitMQ("eventbus");

// Services
builder.AddProject<Projects.Athly_SportEvents_API>("sportevents-api")
       .WithReference(searchDb);
//.WithReference(rabbitMq);

builder.AddProject<Projects.Athly_SportEvents_Worker>("sportevents-worker")
       .WithReference(searchDb);
//       .WithReference(rabbit);

builder.Build().Run();