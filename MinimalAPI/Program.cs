using Application.Abstractions;
using Application.Posts.Commands;
using Application.Posts.Queries;
using DataAccess;
using DataAccess.Repositories;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<SocialDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddMediatR(typeof(CreatePost));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

//adding mideator in minimal api
app.MapGet("/api/posts/{id}", async (IMediator mediator, int id) =>
{//method injection in minimal api

    var getPost = new GetPostById { PostId = id };
    var post = await mediator.Send(getPost);
    return Results.Ok();
}).WithName("GetPostById");//Specific Metadata- provides information , here to get specific location after getting api data

app.MapPost("/api/posts",async (IMediator mediator,Post post) =>
{
    var createPost = new CreatePost { PostContent = post.Content };
    var createdPost = await mediator.Send(createPost);
    return Results.CreatedAtRoute("GetPostById", new {createdPost.Id},createdPost);

});

app.MapGet("/api/posts", async (IMediator mediator) =>
{
    var getCommand = new GetAllPosts();
    var posts = await mediator.Send(getCommand);
    return Results.Ok(posts);

});

app.MapPut("/api/posts/{id}", async (IMediator mediator, Post post, int id) =>
{
    var updatePost = new UpdatePost { PostId = id, PostContent = post.Content };
    var updatedPost = await mediator.Send(updatePost);
    return Results.Ok(updatePost);

});

app.MapDelete("/api/posts/{id}", async (IMediator mediator, int id) =>
{
    var deletePost = new DeletePost { PostId = id};
    await mediator.Send(deletePost);
    return Results.NoContent();
    
});



app.UseAuthorization();

app.MapControllers();

app.Run();
