using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeWeb.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public  ICollection<Comment>? UserComments { get; set; }    

        public  ICollection<Post>? Posts { get; set; }
    }
}


