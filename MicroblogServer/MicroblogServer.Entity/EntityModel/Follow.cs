using System;
using System.ComponentModel.DataAnnotations;

namespace MicroblogServer.Entity
{
  public  class Follow
    {

        public Guid Id { get; set; }

        public Guid FollowerUserId { get; set; }

        public Guid FollowedUserId { get; set; }
    }
}
