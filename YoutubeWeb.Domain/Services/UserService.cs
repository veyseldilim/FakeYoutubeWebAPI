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
        private readonly ICommentRepository _commentRepository;
        private readonly ICommentMapper _commentMapper;
        private readonly IPostMapper _postMapper;
        private readonly IPostRepository _postRepository;


        public UserService(IItemRepository<User> userRepository,
            IUserMapper userMapper,
            ICommentRepository commentRepository,
            ICommentMapper commentMapper,
            IPostMapper postMapper,
            IPostRepository postRepository)
        {
            _userRepository = userRepository;
            _userMapper = userMapper;
            _commentRepository = commentRepository;
            _commentMapper = commentMapper;
            _postMapper = postMapper;
            _postRepository = postRepository;
        }

        public async Task<IEnumerable<UserResponse>> GetAllUsers()
        {
            var users = await _userRepository.GetAsync();

            return users.Select(x => _userMapper.Map(x));
        }

        public async Task<UserResponse> GetUserById(GetUserRequest userRequest)
        {
            if (userRequest == null) throw new ArgumentNullException();

            var result = await _userRepository.GetById(userRequest.Id);

            return _userMapper.Map(result);
        }



        public async Task<UserResponse> AddUser(AddUserRequest userRequest)
        {
            var user = _userMapper.Map(userRequest);

            var result =  _userRepository.Add(user);

            await _userRepository.UnitOfWork.SaveChangesAsync();

            return _userMapper.Map(result);
        }

        public async Task<UserResponse> EditUser(EditUserRequest userRequest)
        {
            var existingRecord = await _userRepository.GetById(userRequest.Id);

            if(existingRecord == null)
            {
                throw new ArgumentException($"Entity with {userRequest.Id} is not present");
            }

            var entity = _userMapper.Map(userRequest);
            var result = _userRepository.Update(entity);

            await _userRepository.UnitOfWork.SaveChangesAsync();

            return _userMapper.Map(result);
        }

       

        public async Task<IEnumerable<CommentResponse>> GetUserComments(GetUserRequest userRequest)
        {
            if(userRequest?.Id == null) throw new ArgumentNullException();

            var result = await _commentRepository.GetCommentsByPostId(userRequest.Id);

            return result.Select(x => _commentMapper.Map(x));
        }

        public async Task<IEnumerable<PostResponse>> GetUserPosts(GetUserRequest userRequest)
        {
            if(userRequest?.Id == null) throw new ArgumentNullException();

            var result = await _postRepository.GetPostsByUserId(userRequest.Id);

            return result.Select(x => _postMapper.Map(x));
        }
    }
}
