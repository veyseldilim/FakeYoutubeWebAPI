using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using YoutubeWeb.Domain.Request.User;
using YoutubeWeb.Domain.Services;

namespace YoutubeWebAPI.Filters
{
    public class UserExistsAttribute : TypeFilterAttribute
    {
        public UserExistsAttribute() : base(typeof
        (UserExistsFilterImpl))
        {

        }

        public class UserExistsFilterImpl : IAsyncActionFilter
        {
            private readonly IUserService _userService;
            public UserExistsFilterImpl(IUserService userService)
            {
                _userService = userService;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context,
            ActionExecutionDelegate next)
            {
                if (!(context.ActionArguments["id"] is Guid id))
                {
                    context.Result = new BadRequestResult();
                    return;
                }
                var result = await _userService.GetUserById(new GetUserRequest { Id = id });


                if (result == null)
                {
                    context.Result = new 
                            NotFoundObjectResult($"User with id { id } does not exist.");
                    return;
                }
                await next();
            }
        }
    }
}
