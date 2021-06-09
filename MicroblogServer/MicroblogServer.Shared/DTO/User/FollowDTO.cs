using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroblogServer.Shared.DTO
{
    public class FollowDTO
    {
        public Guid UserId { get; set; }
        public Guid UserToFollowId { get; set; }
    }
}
