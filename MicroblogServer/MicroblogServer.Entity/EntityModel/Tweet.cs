using System;
using System.ComponentModel.DataAnnotations;

namespace MicroblogServer.Entity
{
  public  class Tweet
    {
        [Key]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Message { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
