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
        private readonly IUserRepository _userRepository;       
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;

        private readonly IMapper _mapper;


        public UserService(IUserRepository userRepository,       
            ICommentRepository commentRepository,      
            IPostRepository postRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;    
            _commentRepository = commentRepository;
            _postRepository = postRepository;

            _mapper = mapper;
        }

        public async Task<IEnumerable<UserResponse>> GetAllUsers()
        {
            var users = await _userRepository.GetAsync();

            return users.Select(x => _mapper.Map(x));
        }

        public async Task<UserResponse> GetUserById(GetUserRequest userRequest)
        {
            if (userRequest == null) throw new ArgumentNullException();

            var result = await _userRepository.GetById(userRequest.Id);

            return _mapper.Map(result);
        }



        public async Task<UserResponse> AddUser(AddUserRequest userRequest)
        {
            var user = _mapper.Map(userRequest);

            var result =  _userRepository.Add(user);

            await _userRepository.UnitOfWork.SaveChangesAsync();

            return _mapper.Map(result);
        }

        public async Task<UserResponse> EditUser(EditUserRequest userRequest)
        {
            var existingRecord = await _userRepository.GetById(userRequest.Id);

            if(existingRecord == null)
            {
                throw new ArgumentException($"Entity with {userRequest.Id} is not present");
            }

            existingRecord.Name = userRequest.Name;
            var result = _userRepository.Update(existingRecord);

            await _userRepository.UnitOfWork.SaveChangesAsync();

            return _mapper.Map(result);
        }

       

        public async Task<IEnumerable<CommentResponse>> GetUserComments(GetUserRequest userRequest)
        {
            if(userRequest?.Id == null) throw new ArgumentNullException();

            var result = await _commentRepository.GetCommentsByUserId(userRequest.Id);

            return result.Select(x => _mapper.Map(x));
        }

        public async Task<IEnumerable<PostResponse>> GetUserPosts(GetUserRequest userRequest)
        {
            if(userRequest?.Id == null) throw new ArgumentNullException();

            var result = await _postRepository.GetPostsByUserId(userRequest.Id);

            return result.Select(x => _mapper.Map(x));
        }
    }
}
