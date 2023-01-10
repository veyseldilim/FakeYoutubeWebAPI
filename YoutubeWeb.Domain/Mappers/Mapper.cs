using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeWeb.Domain.Entities;
using YoutubeWeb.Domain.Request.Comment;
using YoutubeWeb.Domain.Request.Post;
using YoutubeWeb.Domain.Request.User;
using YoutubeWeb.Domain.Response;

namespace YoutubeWeb.Domain.Mappers
{
    public class Mapper : IMapper
    {


        public Comment Map(AddCommentRequest commentRequest)
        {
            if (commentRequest == null)
            {
                return null;
            }

            var comment = new Comment()
            {
                Body = commentRequest.Body,
                UserId = commentRequest.UserId,
                PostId = commentRequest.PostId,

            };

            return comment;
        }

        public Comment Map(EditCommentRequest commentRequest)
        {
            if (commentRequest == null)
            {
                return null;
            }

            var comment = new Comment()
            {
                Id = commentRequest.Id,
                Body = commentRequest.Body,
            };

            return comment;
        }

        public CommentResponse Map(Comment comment)
        {
            if (comment == null)
            {
                return null;
            }

            var commentResponse = new CommentResponse()
            {
                Id = comment.Id,
                Body = comment.Body,
                UserId = comment.UserId,
                PostId = comment.PostId,
               
            };

            return commentResponse;

        }

        public IEnumerable<CommentResponse> Map(IEnumerable<Comment> comments)
        {
            if (comments == null)
            {
                return null;
            }


            List<CommentResponse> commentResponses = new List<CommentResponse>();



            foreach (var comment in comments)
            {
                var commentResponse = Map(comment);
                commentResponses.Add(commentResponse);
            }

            return commentResponses;
        }


        public User Map(AddUserRequest userRequest)
        {
            if (userRequest == null)
            {
                return null;
            }

            var user = new User()
            {
                Name = userRequest.Name,
            };

            return user;
        }

        public User Map(EditUserRequest userRequest)
        {
            if (userRequest == null)
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
            if (user == null)
            {
                return null;
            }

            var userResponse = new UserResponse()
            {
                Id = user.Id,
                Name = user.Name,
                Comments = Map(user.UserComments),
                Posts = Map(user.Posts)
            };

            return userResponse;
        }



        public Post Map(AddPostRequest postRequest)
        {
            if (postRequest == null)
            {
                return null;
            }

            var post = new Post()
            {
                Title = postRequest.Title,
                Body = postRequest.Body,
                UserId = postRequest.UserId,
            };

            return post;
        }

        public Post Map(EditPostRequest postRequest)
        {
            if (postRequest == null)
            {
                return null;
            }

            var post = new Post()
            {
                Id = postRequest.Id,
                Title = postRequest.Title,
                Body = postRequest.Body
            };


            return post;

        }

        public PostResponse Map(Post post)
        {
            if (post == null)
            {
                return null;
            }

            var postResponse = new PostResponse()
            {
                Id = post.Id,
                Title = post.Title,
                Body = post.Body,
                UserId = post.UserId,
                Comments = Map(post.PostComments)


            };

            return postResponse;

        }

        public IEnumerable<PostResponse> Map(IEnumerable<Post> posts)
        {
            if (posts == null)
            {
                return null;
            }


            List<PostResponse> postResponses = new List<PostResponse>();

            foreach (var post in posts)
            {
                var postResponse = Map(post);
                postResponses.Add(postResponse);
            }

            return postResponses;
        }
    }
}
