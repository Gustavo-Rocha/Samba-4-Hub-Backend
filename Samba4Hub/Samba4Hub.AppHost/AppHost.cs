var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Samba4Hub>("samba4hub");

builder.Build().Run();
