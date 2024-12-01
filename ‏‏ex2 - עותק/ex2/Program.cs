using ex1.Repositories;
using ex1.Services;
using ex2.Repositories;
using ex2.Services;
using ex2.Services.Logger;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TasksApi.Services.Logger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(); // This registers controller services

builder.Services.AddScoped<ITasksRepository, TasksRepository>();
builder.Services.AddScoped<ITasksService, TasksService>();

builder.Services.AddScoped<IRepositoryWithAdo, RepositoryWithAdo>();
builder.Services.AddScoped<IServiceWithAdo, ServiceWithAdo>();

builder.Services.AddScoped<ILoggerService, ConsoleLoggerService>();

builder.Services.AddScoped<DBLoggerService>();

builder.Services.AddScoped<FileLoggerService>(provider =>
    new FileLoggerService("E:\\Web-Api\\hw-text1.txt")
);

builder.Services.AddScoped<TasksApi.Services.Logger.LoggerFactory>();

builder.Services.AddDbContext<TasksdbContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);


// Add Swagger services to the container.
builder.Services.AddEndpointsApiExplorer(); // For exposing endpoints
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Books API",
        Description = "A simple example ASP.NET Core API to manage books",
        Contact = new OpenApiContact
        {
            Name = "Your Name",
            Email = "your.email@example.com",
            Url = new Uri("https://yourwebsite.com"),
        }
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    // Enable middleware to serve generated Swagger as a JSON endpoint.
    app.UseSwagger();

    // Enable middleware to serve Swagger UI (HTML, JS, CSS, etc.)
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Books API V1");
        c.RoutePrefix = string.Empty; // Serve Swagger UI at the app's root
    });
}


app.UseRouting(); // Adds routing to the request pipeline

app.MapControllers(); // This maps attribute-based routing controllers like [Route("api/[controller]")]

app.MapGet("/", () => "Hello World!");

app.Run();

