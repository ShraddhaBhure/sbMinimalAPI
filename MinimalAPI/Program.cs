
using MinimalAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.RegisterServices();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.RegisterEndpointDefinitions();


app.Run();
