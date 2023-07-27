
using MinimalAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.RegisterServices();


var app = builder.Build();
// Configure the HTTP request pipeline.
app.Use(async(ctx, next)=>
{
	try
	{
		await next();
	}
	catch (Exception)
	{
        ctx.Response.StatusCode = 500;
		await ctx.Response.WriteAsync("An Error occurred");

	}
});



app.UseHttpsRedirection();

app.RegisterEndpointDefinitions();


app.Run();
