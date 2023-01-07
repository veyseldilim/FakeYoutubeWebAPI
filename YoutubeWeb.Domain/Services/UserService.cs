using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeWeb.Domain.Entities;
using YoutubeWeb.Domain.Mappers;
using YoutubeWeb.Domain.Repositories;
using YoutubeWeb.Domain.Request.User;
using YoutubeWeb.Domain.Response;

namespace YoutubeWeb.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IItemRepository<User> _userRepository;
        private readonly IUserMapper _userMapper;


        public UserService(IItemRepository<User> userRepository, IUserMapper userMapper)
        {
            _userRepository = userRepository;
            _userMapper = userMapper;
        }

        public Task<UserResponse> AddUser(AddUserRequest userRequest)
        {
            throw new NotImplementedException();
        }

        public Task<UserResponse> EditUser(EditUserRequest userRequest)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserResponse>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Task<UserResponse> GetUserById(GetUserRequest userRequest)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CommentResponse>> GetUserComments(GetUserRequest userRequest)
        {
            throw new NotImplementedException();
        }
    }
}
