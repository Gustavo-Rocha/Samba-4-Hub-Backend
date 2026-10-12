var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithDataVolume();

var db = postgres.AddDatabase("samba4hubdb");

builder.AddProject<Projects.Samba4Hub_Api>("samba4hub")
    .WithReference(db)
    .WaitFor(db);

builder.Build().Run();
