

using Domain.Models;


namespace MinimalAPI.Filters
{
    public class PostValidationFilters : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context,
            EndpointFilterDelegate next)
        {

            var post = context.GetArgument<Post>(1);//1 or 0 is index number for Post in defination create update
            if (string.IsNullOrEmpty(post.Content)) 
                return await Task.FromResult(Results.BadRequest("Post not Valid"));
           
            return await next(context);
        }
    }

}
