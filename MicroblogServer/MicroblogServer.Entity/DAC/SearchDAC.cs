using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroblogServer.Shared.DTO;
using MicroblogServer.Entity.EntityModel;
namespace MicroblogServer.Entity.DAC
{
    public class SearchDAC
    {

        private TweetDAC TweetDAC;
        public SearchDAC()
        {
            TweetDAC = new TweetDAC();
        }
        public IList<SearchDTO> GetAllUsers(string searchString, Guid userId)
        {

            if (searchString != null && searchString != "")
            {
                try
                {
                    IList<SearchDTO> resultList = new List<SearchDTO>();
                    SearchDTO getAllUsers;
                    using (MicroBlogDBContext dBContext = new MicroBlogDBContext())
                    {
                        IList<User> user = dBContext.Users.Where(dUser => (dUser.FirstName.Contains(searchString)
                                                                           || dUser.Email.Contains(searchString)
                                                                           || dUser.LastName.Contains(searchString))
                                                                           && (dUser.Id != userId)).ToList();
                        if (user.Count > 0)
                        {
                            foreach (var item in user)
                            {
                                getAllUsers = new SearchDTO();
                                getAllUsers.Image = item.Image;
                                getAllUsers.LastName = item.LastName;
                                getAllUsers.FirstName = item.FirstName;
                                getAllUsers.Email = item.Email;
                                getAllUsers.UserId = item.Id;

                                Follow follow = dBContext.Follows.Where(dFollow => (dFollow.FollowerUserId == userId)
                                                                        && (dFollow.FollowedUserId == getAllUsers.UserId))
                                                                        .FirstOrDefault();
                                if (follow != null)
                                {
                                    getAllUsers.isFollowed = true;
                                }
                                else
                                    getAllUsers.isFollowed = false;

                                resultList.Add(getAllUsers);
                            }

                            return resultList;

                        }
                        else
                            return null;


                    }

                }
                catch (Exception error)
                {

                    throw error;
                }

            }
            else
                return null;
        }

        public IList<SearchDTO> GetAllHashTag(string searchString, Guid userId) {
            if (searchString != null && searchString == "")
            {
                IList<SearchDTO> resultList = new List<SearchDTO>();
                SearchDTO getAllTags;
                try
                {
                    using (MicroBlogDBContext dBContext = new MicroBlogDBContext())
                    {
                        IList<TweetTag> tag = dBContext.TweetTags.Where(dTag => dTag.TagName.Contains(searchString)).ToList();
                        if (tag.Count > 0) {
                            foreach (var item in tag)
                            {
                                getAllTags = new SearchDTO();
                                TweetDAC.UpdateSearchCount(item);
                                IList<Tweet> tweets = dBContext.Tweets.Where(dTweet => dTweet.Id == item.TweetId).ToList();
                                foreach (var tweet in tweets)
                                {
                                    User user = dBContext.Users.Where(dUser => dUser.Id == tweet.UserId).FirstOrDefault();
                                    getAllTags.Message = tweet.Message;
                                    getAllTags.CreatedAt = tweet.CreatedAt;
                                    getAllTags.UserName = user.FirstName + user.LastName;
                                }
                                resultList.Add(getAllTags);
                            }
                            return resultList;
                        } else
                            return null;
                    }
                }
                catch (Exception error)
                {

                    throw error;
                }

            }
            else {
                return null;
            }
        
        }

    }
}
