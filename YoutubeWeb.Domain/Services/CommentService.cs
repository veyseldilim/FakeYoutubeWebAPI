using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeWeb.Domain.Entities;
using YoutubeWeb.Domain.Mappers;
using YoutubeWeb.Domain.Repositories;
using YoutubeWeb.Domain.Request.Comment;
using YoutubeWeb.Domain.Response;

namespace YoutubeWeb.Domain.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;


        public CommentService(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CommentResponse>> GetAllComments()
        {
            var comments = await _commentRepository.GetAsync();

            return comments.Select(x => _mapper.Map(x));


        }

        public async Task<CommentResponse> GetCommentById(GetCommentRequest commentRequest)
        {
            if(commentRequest?.Id == null)
            {
                throw new ArgumentNullException();
            }

            var comment = await _commentRepository.GetById(commentRequest.Id);

            return _mapper.Map(comment);
        }


        public async Task<CommentResponse> AddComment(AddCommentRequest commentRequest)
        {
            var comment = _mapper.Map(commentRequest);
            var result = _commentRepository.Add(comment);

            await _commentRepository.UnitOfWork.SaveChangesAsync();

            return _mapper.Map(result);

        }

        public async Task<CommentResponse> EditComment(EditCommentRequest commentRequest)
        {
            var existingRecord = _commentRepository.GetById(commentRequest.Id);

            if(existingRecord == null)
            {
                throw new ArgumentException($"Entity with {commentRequest.Id} is not present");
            }

            var entity = _mapper.Map(commentRequest);
            var result = _commentRepository.Update(entity);

            await _commentRepository.UnitOfWork.SaveChangesAsync();


            return _mapper.Map(result);


        }

        
    }
}
