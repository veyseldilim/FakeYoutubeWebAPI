using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeWeb.Domain.Request.User
{
    public class EditUserRequest
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        
    }
}
