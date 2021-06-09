﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroblogServer.Shared.DTO
{
   public class SearchDTO
    {
        public string SearchString { get; set; }
        public string Message { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid TweetId { get; set; }
        public Guid UserId { get; set; }
        public string isLiked { get; set; }
        public bool isAuthor { get; set; }
        public bool isFollowed { get; set; }
    }
}
