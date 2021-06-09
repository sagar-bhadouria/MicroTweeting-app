using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroblogServer.Shared.DTO
{
    public class TweetDTO
    {
        public string Message { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid TweetID { get; set; }
        public bool IsAuthor { get; set; }
        public bool IsLiked { get; set; }
    }
}
