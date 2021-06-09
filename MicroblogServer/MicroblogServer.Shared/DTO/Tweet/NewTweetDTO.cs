using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroblogServer.Shared.DTO
{
   public  class NewTweetDTO
    {
        public Guid UserID { get; set; }
        public string Message { get; set; }
        public Guid TweetId { get; set; }
    }
}
