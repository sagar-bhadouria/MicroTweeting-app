using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using Microblog.Server.Bussiness.Business;
using MicroblogServer.Api.Models;
using MicroblogServer.Shared.DTO;
namespace MicroblogServer.Api.Controllers
{
    public class TweetController : ApiController
    {
        private TweetBDC TweetBDC;

        public TweetController()
        {
            TweetBDC = new TweetBDC();
        }

        #region NewTweet API 
        [Route("api/user/new-tweet")]
        public async Task<IHttpActionResult> NewTweet([FromBody] NewTweetModel newTweetModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.Forbidden, ModelState));
                }

                NewTweetDTO newTweetDTO = new NewTweetDTO();
                newTweetDTO.UserID = Guid.Parse(newTweetModel.UserID);
                newTweetDTO.Message = newTweetModel.Message;
                newTweetDTO = await TweetBDC.CreateNewTweet(newTweetDTO);
                return Ok(new { Tweet = newTweetDTO });

            }
            catch (Exception error)
            {

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.Forbidden, error.Message));
            }


        }
        #endregion

        #region GetAllTweets API
        [HttpGet]
        [Route("api/user/tweets/{userId}")]
        public IHttpActionResult GetAll(string userId)
        {
            try
            {
                Guid UserId = Guid.Parse(userId);
                IList<TweetDTO> AllTweets = TweetBDC.GetAllTweets(UserId);
                return Ok(new { Tweets = AllTweets });
            }
            catch (Exception error)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.Forbidden, error.Message));
            }


        }
        #endregion

        #region Delete Tweet API
        [HttpDelete]
        [Route("api/user/delete-tweet/{userId}/{tweetId}")]
        public IHttpActionResult DeleteTweet(string userId, string tweetId)
        {

            
            try
            {
                Guid UserId = Guid.Parse(userId.Trim());
                Guid TweetId = Guid.Parse(tweetId.Trim());

                bool deleted = TweetBDC.DeleteTweet(UserId, TweetId);
                return Ok(new { action = deleted });
            }
            catch (Exception error)
            {


                return ResponseMessage(Request.CreateResponse(HttpStatusCode.Forbidden, error.Message));
            }

        }
        #endregion

        #region Update Tweet API
        [HttpPut]
        [Route("api/user/update-tweet")]
        public IHttpActionResult UpdateTweet([FromBody] NewTweetModel model)
        {

            try
            {
                NewTweetDTO newTweetDTO = new NewTweetDTO();
                newTweetDTO.UserID = Guid.Parse(model.UserID);
                newTweetDTO.TweetId = Guid.Parse(model.TweetId);
                newTweetDTO.Message = model.Message;
                bool result = TweetBDC.UpdateTweet(newTweetDTO);
                return Ok(new { Action = result });
            }
            catch (Exception error)
            {

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.Forbidden, error.Message));
            }

        } 
        #endregion

        #region LikeTweet API
        [HttpPost]
        [Route("api/user/like/{userId}/{tweetId}")]
        public IHttpActionResult Like(string userId, string tweetId)
        {
            try
            {
                Guid UserId = Guid.Parse(userId);
                Guid TweetId = Guid.Parse(tweetId);
                bool result = TweetBDC.LikeTweet(UserId, TweetId);
                return Ok(new { Action = result });
            }
            catch (Exception error)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.Forbidden, error.Message));

            }
        } 
        #endregion

        #region Dislike tweet API
        [HttpPost]
        [Route("api/user/dislike/{userId}/{tweetId}")]
        public IHttpActionResult DisLike(string userId, string tweetId)
        {
            try
            {
                Guid UserId = Guid.Parse(userId);
                Guid TweetId = Guid.Parse(tweetId);
                bool result = TweetBDC.DisLikeTweet(UserId, TweetId);
                return Ok(new { Action = result });
            }
            catch (Exception error)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.Forbidden, error.Message));

            }
        } 
        #endregion
    }
}
