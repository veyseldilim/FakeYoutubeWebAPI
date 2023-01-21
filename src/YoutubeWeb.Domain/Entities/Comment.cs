using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeWeb.Domain.Entities
{
    public class Comment
    {
        public Guid Id { get; set; }

        public string? Body { get; set; }

        public Guid? UserId { get; set; }

        public User? User { get; set; }

        public Guid? PostId { get; set; }

        public Post? Post { get; set; }

    }
}
