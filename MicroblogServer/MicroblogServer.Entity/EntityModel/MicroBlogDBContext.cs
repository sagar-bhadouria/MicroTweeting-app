using System.Data.Entity;


namespace MicroblogServer.Entity.EntityModel
{
    public class MicroBlogDBContext: DbContext
    {
        public MicroBlogDBContext(): base("MicroBlogDB") { 
        
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Tweet> Tweets { get; set; }

        public DbSet<Follow> Follows { get; set; }

        public DbSet<TweetTag> TweetTags { get; set; }
        public DbSet<LikeTweet> LikeTweets { get; set; }
    }
}
