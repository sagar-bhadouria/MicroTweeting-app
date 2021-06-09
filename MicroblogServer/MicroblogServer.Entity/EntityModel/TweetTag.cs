using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroblogServer.Entity.EntityModel
{
    public class TweetTag
    {
        public Guid Id { get; set; }
        public Guid TweetId { get; set;  }

        public string TagName { get; set; }

        public int SearchCount { get; set; }
    }
}
