using Application.Abstractions;
using Application.Posts.Commands;
using DataAccess.Repositories;
using DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalAPI.Abstraction;

namespace MinimalAPI.Extensions
{
    public static class MinimalApiExtensions
    {
        public static void RegisterServices(this WebApplicationBuilder builder)
        {

            builder.Services.AddControllers();

            //builder.Services.AddDbContext<SocialDbContext>(options =>
            //    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

            var cs = builder.Configuration.GetConnectionString("DbConnection");
            builder.Services.AddDbContext<SocialDbContext>(opt => opt.UseSqlServer(cs));
            builder.Services.AddScoped<IPostRepository, PostRepository>();
            builder.Services.AddMediatR(typeof(CreatePost));

        }
        public static void RegisterEndpointDefinitions(this WebApplication app)
        {
            var endpointDefinitions = typeof(Program).Assembly
                .GetTypes().Where(t => t.IsAssignableTo(typeof(IEndpointDefitnion))
                && !t.IsAbstract && !t.IsInterface)
                .Select(Activator.CreateInstance)
                .Cast<IEndpointDefitnion>();
            foreach (var endpointDef in endpointDefinitions )
            {
                endpointDef.RegisterEndpoints(app);
            }
                

        }
    }
}