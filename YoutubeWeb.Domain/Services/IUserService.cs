using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeWeb.Domain.Request.User;
using YoutubeWeb.Domain.Response;

namespace YoutubeWeb.Domain.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponse>> GetAllUsers();

        Task<UserResponse> GetUserById(GetUserRequest userRequest);

        Task<UserResponse> AddUser(AddUserRequest userRequest);

        Task<UserResponse> EditUser(EditUserRequest userRequest);

        Task<IEnumerable<CommentResponse>> GetUserComments(GetUserRequest userRequest);


    }
}
