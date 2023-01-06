using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeWeb.Domain.Entities;

namespace YoutubeWeb.Domain.Response
{
    public class PostResponse
    {
        public Guid Id { get; set; }

        public string? Title { get; set; }

        public string? Body { get; set; }
        public Guid? UserId { get; set; }

        public UserResponse? User { get; set; }

        
    }
}
