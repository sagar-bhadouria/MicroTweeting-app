using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroblogServer.Shared.DTO;
using MicroblogServer.Entity.EntityModel;
using System.Data.Entity;
namespace MicroblogServer.Entity.DAC
{

    public class TweetDAC
    {
        TweetTagDAC TweetTagDAC;
        public TweetDAC()
        {
            TweetTagDAC = new TweetTagDAC();
        }

        #region CreateNewTweet DAC
        public async Task<NewTweetDTO> CreateNewTweet(NewTweetDTO newTweetDTO)
        {
            try
            {
                using (MicroBlogDBContext dbContext = new MicroBlogDBContext())
                {
                    Tweet newTweet = new Tweet();
                    newTweet.Id = Guid.NewGuid();
                    newTweet.Message = newTweetDTO.Message;
                    newTweet.UserId = newTweetDTO.UserID;
                    newTweet.CreatedAt = DateTime.Now;
                    dbContext.Tweets.Add(newTweet);
                    await dbContext.SaveChangesAsync();

                    newTweetDTO.TweetId = newTweet.Id;
                    return newTweetDTO;
                }
            }
            catch (Exception error) { throw error; }
        }
        #endregion

        #region GetAllTweet DAC
        public IList<TweetDTO> GetAllTweets(Guid id)
        {
            IList<TweetDTO> tweetList = new List<TweetDTO>();
            IList<TweetDTO> tweetList1 = new List<TweetDTO>();
            using (MicroBlogDBContext dBContext = new MicroBlogDBContext())
            {
                tweetList = (from u in dBContext.Follows.Where(ds => ds.FollowerUserId == id)
                             join uf in dBContext.Follows on u.FollowedUserId equals uf.FollowedUserId
                             join t in dBContext.Tweets on uf.FollowedUserId equals t.UserId
                             join user in dBContext.Users on t.UserId equals user.Id

                             orderby t.CreatedAt descending
                             select new TweetDTO()
                             {
                                 Message = t.Message,
                                 CreatedAt = t.CreatedAt,
                                 UserName = user.FirstName + " " + user.LastName,
                                 IsAuthor = false,
                                 TweetID = t.Id
                             }).Distinct().ToList();

                foreach (var iterate in tweetList)
                {
                    LikeTweet likeTweet = dBContext.LikeTweets.Where(x => (x.UserId == id) && (x.TweetId == iterate.TweetID)).FirstOrDefault();
                    if (likeTweet != null)
                    {
                        iterate.IsLiked = true;
                    }
                    else
                    {
                        iterate.IsLiked = false;
                    }
                }

                tweetList1 = (from u in dBContext.Users.Where(tr => tr.Id == id)
                              join t in dBContext.Tweets on u.Id equals t.UserId
                              orderby t.CreatedAt descending
                              select new TweetDTO()
                              {
                                  Message = t.Message,
                                  CreatedAt = t.CreatedAt,
                                  UserName = u.FirstName + " " + u.LastName,
                                  IsAuthor = true,
                                  //IsLiked = false,
                                  TweetID = t.Id
                              }).ToList();
                foreach (var item in tweetList1)
                {
                    LikeTweet likeTweet = dBContext.LikeTweets.Where(x => (x.UserId == id) && (x.TweetId == item.TweetID)).FirstOrDefault();
                    if (likeTweet != null)
                    {
                        item.IsLiked = true;
                    }
                    else
                    {
                        item.IsLiked = false;
                    }
                }
                tweetList = tweetList.Concat(tweetList1).OrderByDescending(tweet => tweet.CreatedAt).ToList();
                return tweetList;
            }

        }
        #endregion

        #region UpdateTweet
        public Guid UpdateTweet(NewTweetDTO updatedTweet)
        {
            try
            {
                using (MicroBlogDBContext dBContext = new MicroBlogDBContext())
                {
                    Tweet tweet = dBContext.Tweets.Where(dTweet => dTweet.Id == updatedTweet.TweetId).FirstOrDefault();
                    tweet.Message = updatedTweet.Message;
                    tweet.CreatedAt = DateTime.Now;
                    dBContext.SaveChanges();
                }
                return updatedTweet.TweetId;
            }
            catch (Exception error)
            {

                throw error;
            }
        }
        #endregion

        #region DeleteTweetDAC
        public bool DeleteTweet(Guid userId, Guid tweetId)
        {
            using (MicroBlogDBContext dBContext = new MicroBlogDBContext())
            {
                Tweet tweet = dBContext.Tweets.Where(dTweet => dTweet.Id == tweetId && dTweet.UserId == userId).FirstOrDefault();
                if (tweet != null)
                {
                    TweetTagDAC.DeleteTag(tweet.Id);
                    dBContext.Entry(tweet).State = EntityState.Deleted;
                    dBContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        #endregion

        public void UpdateSearchCount(TweetTag item)
        {
            try
            {
                using (MicroBlogDBContext dBContext = new MicroBlogDBContext())
                {
                    TweetTag updateTag = dBContext.TweetTags.Where(ds => ds.Id == item.Id).FirstOrDefault();


                    updateTag.SearchCount++;

                    dBContext.SaveChanges();

                }
            }
            catch (Exception error)
            {

                throw error;
            }

        }

        #region LikeTweet DAC
        public bool LikeTweet(Guid userId, Guid tweetId)
        {
            try
            {
                using (MicroBlogDBContext dBContext = new MicroBlogDBContext())
                {
                    LikeTweet likeTweet1 = dBContext.LikeTweets.Where(dLikeTweet => dLikeTweet.UserId == userId && dLikeTweet.TweetId == tweetId).FirstOrDefault();
                    if (likeTweet1 != null)
                    {
                        return false;
                    }
                    else
                    {
                        LikeTweet likeTweet = new LikeTweet();
                        likeTweet.Id = Guid.NewGuid();
                        likeTweet.TweetId = tweetId;
                        likeTweet.UserId = userId;
                        dBContext.LikeTweets.Add(likeTweet);
                        dBContext.SaveChanges();
                        return true;
                    }
                }
            }
            catch (Exception error)
            {

                throw error;
            }
        }
        #endregion

        #region DisLike Tweet DAC
        public bool DisLikeTweet(Guid userId, Guid tweetId)
        {
            try
            {
                using (MicroBlogDBContext dBContext = new MicroBlogDBContext())
                {
                    LikeTweet tweet = dBContext.LikeTweets.Where(dLikeTweet => dLikeTweet.UserId == userId && dLikeTweet.TweetId == tweetId).FirstOrDefault(); ;
                    if (tweet != null)
                    {
                        dBContext.LikeTweets.Remove(tweet);
                        dBContext.SaveChanges();
                        return true;
                    }
                    else
                        return false;
                }
            }
            catch (Exception error)
            {

                throw error;
            }
        }
        #endregion

        public string MostTrending() {
            try
            {
                string retValue = null;
                using (MicroBlogDBContext dBContext = new MicroBlogDBContext())
                {
                    IEnumerable<TweetTag> tagByName = dBContext.TweetTags.OrderByDescending(t => t.SearchCount).ThenByDescending(t => t.TagName);
                    if (tagByName == null)
                        return retValue;
                    return retValue = tagByName.ElementAt(0).TagName;
                }

            }
            catch (Exception error)
            {

                throw error;
            }
        }
        public string MostLiked()
        {
            string retValue = null;
            try
            {
                using (MicroBlogDBContext dBContext = new MicroBlogDBContext())
                {
                    Guid maxId = dBContext.LikeTweets.GroupBy(dLikeTweet => dLikeTweet.TweetId).OrderByDescending(dLikeTweet => dLikeTweet.Count()).FirstOrDefault().Key;
                    if (maxId == default(Guid))
                        return retValue;
                    Tweet tweet = dBContext.Tweets.Where(dTweet => dTweet.Id == maxId).FirstOrDefault();

                    if (tweet == null)
                        return retValue;

                    return retValue =tweet.Message; 
                }

            }
            catch (Exception error)
            {

                throw error;
            }
        }
        public int TotalTweetsToday()
        {
            
            try
            {
                using (MicroBlogDBContext dBContext = new MicroBlogDBContext())
                {
                    DateTime systemDate = DateTime.Today;
                    int count = dBContext.Tweets.Count(tweet => DbFunctions.TruncateTime(tweet.CreatedAt) == DateTime.Today);
                    
                    return count;
                }

            }
            catch (Exception error)
            {

                throw error;
            }
        }
    }
}
