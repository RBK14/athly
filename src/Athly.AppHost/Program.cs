var builder = DistributedApplication.CreateBuilder(args);

var sqlServer = builder.AddSqlServer("sqlserver");

var sportEventsDb = sqlServer.AddDatabase("sport-events-db");

var rabbitMq = builder.AddRabbitMQ("eventbus");

// Services
builder.AddProject<Projects.Athly_SportEvents_API>("sport-events-api")
       .WithReference(sportEventsDb);
       //.WithReference(rabbitMq);

//builder.AddProject<Projects.Athly_SportEvents_Worker>("SportEventsWorker")
//       .WithReference(sportEventsDb)
//       .WithReference(rabbit);

builder.Build().Run();