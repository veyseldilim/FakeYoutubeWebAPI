﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoutubeWeb.Domain.Response
{
    public class UserResponse
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public IEnumerable<PostResponse> Posts { get; set; }

        public IEnumerable<CommentResponse> Comments { get; set; }
    }
}
