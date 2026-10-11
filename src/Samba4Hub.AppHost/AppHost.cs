var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Samba4Hub_Api>("samba4hub");

builder.Build().Run();
