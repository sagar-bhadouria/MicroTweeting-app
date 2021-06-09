using System;
using System.ComponentModel.DataAnnotations;

namespace MicroblogServer.Entity
{ 
public   class LikeTweet
    {
        [Key]
        public Guid Id { get; set; }
        public Guid TweetId { get; set; }

        public Guid UserId { get; set; }
    }
}
