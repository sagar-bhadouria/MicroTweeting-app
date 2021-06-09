using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroblogServer.Entity.DAC;
using MicroblogServer.Shared.DTO;

namespace Microblog.Server.Bussiness.Business
{
    public class TweetBDC
    {
        private TweetDAC TweetDAC;
        private TweetTagDAC TweetTagDAC;
        private TweetTagBDC TweetTagBDC;
        public TweetBDC()
        {
            TweetDAC = new TweetDAC();
            TweetTagDAC = new TweetTagDAC();
            TweetTagBDC = new TweetTagBDC();
        }
        #region CreateNewTweet 
        public async Task<NewTweetDTO> CreateNewTweet(NewTweetDTO newTweetDTO)
        {
            NewTweetDTO TweetDTO = await TweetDAC.CreateNewTweet(newTweetDTO);
            if (TweetDTO != null)
            {
                bool result = TweetTagBDC.CreateNewTags(TweetDTO);
            }

            return TweetDTO;
        }
        #endregion

        #region GetAllTweets
        public IList<TweetDTO> GetAllTweets(Guid id)
        {
            IList<TweetDTO> Tweets = TweetDAC.GetAllTweets(id);
            
            return Tweets;
        }
        #endregion

        #region DeleteTweet
        public bool DeleteTweet(Guid userId, Guid tweetId)
        {
            return TweetDAC.DeleteTweet(userId, tweetId);
        }
        #endregion

        #region UpdateTweet 
        public bool UpdateTweet(NewTweetDTO newTweetDTO)
        {
            try
            {
                Guid Result = TweetDAC.UpdateTweet(newTweetDTO);
                TweetTagDAC.DeleteTag(Result);
                TweetTagBDC.CreateNewTags(newTweetDTO);
                return true;
            }
            catch (Exception error)
            {

                throw error;
            }
        } 
        #endregion

        #region LikeTweet
        public bool LikeTweet(Guid UserId, Guid TweetId)
        {
            try
            {
                bool result = TweetDAC.LikeTweet(UserId, TweetId);
                return result;
            }
            catch (Exception error)
            {

                throw error;
            }

        }
        #endregion

        #region DisLike Tweet
        public bool DisLikeTweet(Guid UserId, Guid TweetId)
        {
            try
            {
                bool result = TweetDAC.DisLikeTweet(UserId, TweetId);
                return result;
            }
            catch (Exception error)
            {

                throw error;
            }
        } 
        #endregion
    }
}
