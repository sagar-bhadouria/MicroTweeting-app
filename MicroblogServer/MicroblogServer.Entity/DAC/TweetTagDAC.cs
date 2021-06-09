using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using MicroblogServer.Entity.EntityModel;
namespace MicroblogServer.Entity.DAC
{
   public class TweetTagDAC
    {
        MicroBlogDBContext dbContext;
        public TweetTagDAC()
        {
            dbContext = new MicroBlogDBContext();
        }
        public bool AddTags(List<string> tags, Guid tweetId)
        {
            foreach (string tag in tags)
            {
                TweetTag NewTag = new TweetTag();
                NewTag.Id = Guid.NewGuid();
                NewTag.TweetId = tweetId;
                NewTag.TagName = tag;
                NewTag.SearchCount = 0;
                dbContext.TweetTags.Add(NewTag);
                dbContext.SaveChanges();

            }

            return true;
        }

        public bool DeleteTag(Guid tweetId) {
            IList<TweetTag> tagList = dbContext.TweetTags.Where(tag => tag.TweetId == tweetId).ToList();
            if (tagList.Count > 0)
            {
                foreach (var tag in tagList)
                {
                    dbContext.Entry(tag).State = EntityState.Deleted;
                    dbContext.SaveChanges();
                }

                return true;
            }
            else {
                return false;
            }
        }
    }
}
