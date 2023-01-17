using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using YoutubeWeb.Domain.Request.Post;
using YoutubeWeb.Domain.Request.User;
using YoutubeWeb.Domain.Services;

namespace YoutubeWebAPI.Filters
{
    public class PostExistsAttribute : TypeFilterAttribute
    {
        public PostExistsAttribute() : base(typeof
       (PostExistsFilterImpl))
        {

        }

        public class PostExistsFilterImpl : IAsyncActionFilter
        {
            private readonly IPostService _postService;
            public PostExistsFilterImpl(IPostService postService)
            {
                _postService = postService;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context,
            ActionExecutionDelegate next)
            {
                if (!(context.ActionArguments["id"] is Guid id))
                {
                    context.Result = new BadRequestResult();
                    return;
                }
                var result = await _postService.GetPostById(new GetPostRequest { Id = id });


                if (result == null)
                {
                    context.Result = new
                            NotFoundObjectResult($"User with id {id} does not exist.");
                    return;
                }
                await next();
            }
        }
    }
}
