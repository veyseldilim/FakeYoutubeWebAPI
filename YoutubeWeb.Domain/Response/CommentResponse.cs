using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeWeb.Domain.Entities;

namespace YoutubeWeb.Domain.Response
{
    public class CommentResponse
    {
        public Guid Id { get; set; }

        public string? Body { get; set; }

        public Guid? UserId { get; set; }


        public Guid? PostId { get; set; }

       
    }
}
