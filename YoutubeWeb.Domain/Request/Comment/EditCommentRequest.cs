using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeWeb.Domain.Entities;

namespace YoutubeWeb.Domain.Request.Comment
{
    public class EditCommentRequest
    {
        public Guid Id { get; set; }

        public string Body { get; set; }

        
    }
}
