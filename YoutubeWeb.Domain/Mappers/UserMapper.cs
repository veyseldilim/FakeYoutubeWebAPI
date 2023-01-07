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
    public class UserMapper : IUserMapper
    {
        private readonly ICommentMapper _commentMapper;
        private readonly IPostMapper _postMapper;

        public UserMapper(IPostMapper postMapper, ICommentMapper commentMapper)
        {
            this._postMapper = postMapper;
            this._commentMapper = commentMapper;
        }

        public User Map(AddUserRequest userRequest)
        {
            if(userRequest == null)
            {
                return null;
            }

            var user = new User() 
            {
                Name= userRequest.Name,
            };

            return user;
        }

        public User Map(EditUserRequest userRequest)
        {
            if(userRequest == null)
            {
                return null;
            }

            var user = new User()
            {
                Id = userRequest.Id,
                Name = userRequest.Name,
            };

            return user;
        }

        public UserResponse Map(User user)
        {
            if(user == null)
            {
                return null;
            }

            var userResponse = new UserResponse()
            {
                Id = user.Id,
                Name = user.Name,
                Comments = _commentMapper.Map(user.UserComments),
                Posts = _postMapper.Map(user.Posts)
            };

            return userResponse;
        }
    }
}
