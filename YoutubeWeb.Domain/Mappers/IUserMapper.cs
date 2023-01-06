using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeWeb.Domain.Entities;
using YoutubeWeb.Domain.Request.User;
using YoutubeWeb.Domain.Response;

namespace YoutubeWeb.Domain.Mappers
{
    public interface IUserMapper
    {
        User Map(AddUserRequest userRequest);

        User Map(EditUserRequest userRequest);

        UserResponse Map(User user);



    }
}
