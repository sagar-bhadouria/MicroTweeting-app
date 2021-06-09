using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroblogServer.Shared.DTO;
using MicroblogServer.Entity.EntityModel;
namespace MicroblogServer.Entity.DAC
{
    public class UserDAC
    {
        #region RegisterUser
        async public Task<UserDTO> RegisterUser(UserDTO userDTO)
        {
            try
            {
                using (MicroBlogDBContext dbContext = new MicroBlogDBContext())
                {
                    User user = new User();
                    user.Id = Guid.NewGuid();
                    user.FirstName = userDTO.FirstName;
                    user.LastName = userDTO.LastName;
                    user.Email = userDTO.Email;
                    user.Image = userDTO.Image;
                    user.PhoneNumber = userDTO.PhoneNumber;
                    user.Country = userDTO.Country;
                    user.Password = userDTO.Password;
                    dbContext.Users.Add(user);
                    await dbContext.SaveChangesAsync();
                    userDTO.Id = user.Id;
                    return userDTO;
                }
            }
            catch (Exception error)
            {
                throw error;
            }

        }
        #endregion

        #region LoginUser
        public UserDTO LoginUser(string email)
        {
            try
            {
                using (MicroBlogDBContext dBContext = new MicroBlogDBContext())
                {
                    User user = dBContext.Users.FirstOrDefault(u => u.Email == email);
                    if (user != null)
                    {
                        UserDTO userDTO = new UserDTO();
                        userDTO.Email = user.Email;
                        userDTO.Password = user.Password;
                        userDTO.Id = user.Id;
                        return userDTO;
                    }

                    return null;
                }
            }
            catch (Exception error)
            {
                throw error;
            }
        }
        #endregion

        #region GetUserInfoById
        async public Task<UserDTO> GetUserInfoByID(UserDTO userDTO)
        {

            try
            {
                using (MicroBlogDBContext dBContext = new MicroBlogDBContext())
                {
                    User user = await dBContext.Users.FindAsync(userDTO.Id);
                    userDTO = new UserDTO();
                    userDTO.Id = user.Id;
                    userDTO.FirstName = user.FirstName;
                    userDTO.LastName = user.LastName;
                    userDTO.PhoneNumber = user.PhoneNumber;
                    userDTO.Country = user.Country;
                    userDTO.Email = user.Email;
                    userDTO.Image = user.Image;
                    return userDTO;
                }

            }
            catch (Exception error)
            {
                throw error;
            }
        }
        #endregion


        #region EmailExist
        public bool EmailExist(string email)
        {
            try
            {
                using (MicroBlogDBContext dbContext = new MicroBlogDBContext())
                {
                    if (dbContext.Users.Where(u => u.Email == email).Any())
                    {
                        return true;
                    }

                    return false;
                }

            }
            catch (Exception error)
            {
                throw error;
            }

        }
        #endregion

        #region Follow
        public bool Follow(FollowDTO followDTO)
        {
            try
            {
                using (MicroBlogDBContext dbContext = new MicroBlogDBContext())
                {
                    Follow follow1 = dbContext.Follows.Where(dFollow => dFollow.FollowedUserId == followDTO.UserToFollowId).FirstOrDefault();
                    if (follow1 != null && follow1.FollowerUserId == followDTO.UserId)
                    {
                        return false;
                    }
                    else
                    {
                        Follow follow = new Follow();
                        follow.Id = Guid.NewGuid();
                        follow.FollowerUserId = followDTO.UserId;
                        follow.FollowedUserId = followDTO.UserToFollowId;
                        dbContext.Follows.Add(follow);
                        dbContext.SaveChanges();
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

        #region Unfollow
        public bool UnFollow(FollowDTO followDTO)
        {
            try
            {
                using (MicroBlogDBContext dBContext = new MicroBlogDBContext())
                {
                    Follow unFollow = dBContext.Follows.Where(dfollow => dfollow.FollowedUserId == followDTO.UserToFollowId).FirstOrDefault();
                    dBContext.Follows.Remove(unFollow);
                    dBContext.SaveChanges();
                    return true;
                }

            }
            catch (Exception error)
            {

                throw error;
            }
        } 
        #endregion

        #region GetAllFollowing
        public IList<UserBasicDTO> GetAllFollowing(Guid userId)
        {
            IList<UserBasicDTO> FollowingList = new List<UserBasicDTO>();
            UserBasicDTO following;
            User user; ;
            try
            {
                using (MicroBlogDBContext dBContext = new MicroBlogDBContext())
                {
                    IEnumerable<Follow> followingUser = dBContext.Follows.Where(follow => follow.FollowerUserId == userId);
                    foreach (var item in followingUser)
                    {

                        following = new UserBasicDTO();
                        user = new User();
                        Follow Following = dBContext.Follows.Where(follow => follow.FollowedUserId == item.FollowedUserId).FirstOrDefault();
                        user = dBContext.Users.Where(dUser => dUser.Id == Following.FollowedUserId).FirstOrDefault();
                        following.Email = user.Email;
                        following.ID = user.Id;
                        following.FirstName = user.FirstName;
                        following.LastName = user.LastName;
                        following.Image = user.Image;

                        FollowingList.Add(following);
                    }

                    return FollowingList;
                }


            }
            catch (Exception error)
            {

                throw error;
            }
        }
        #endregion

        #region GetAllFollowers
        public IList<UserBasicDTO> GetAllFollowers(Guid userId)
        {
            IList<UserBasicDTO> FollowersList = new List<UserBasicDTO>();
            UserBasicDTO followers;
            User user;
            try
            {
                using (MicroBlogDBContext dBContext = new MicroBlogDBContext())
                {
                    IEnumerable<Follow> followeduser = dBContext.Follows.Where(followed => followed.FollowedUserId == userId);
                    foreach (var item in followeduser)
                    {
                        followers = new UserBasicDTO();
                        user = new User();
                        Follow Followers = dBContext.Follows.Where(followed => followed.FollowerUserId == item.FollowerUserId).FirstOrDefault();

                        user = dBContext.Users.Where(dUser => dUser.Id == Followers.FollowerUserId).FirstOrDefault();
                        followers.Email = user.Email;
                        followers.FirstName = user.FirstName;
                        followers.LastName = user.LastName;
                        followers.Image = user.Image;

                        FollowersList.Add(followers);
                    }
                    return FollowersList;
                }

            }
            catch (Exception error)
            {

                throw error;
            }

        }
        #endregion

        public string MostTweetBy() {
            try
            {
                string retValue = null;
                using (MicroBlogDBContext dBContext = new MicroBlogDBContext())
                {
                    Guid maxId = dBContext.Tweets.GroupBy(tweet => tweet.UserId).OrderByDescending(tweet => tweet.Count()).FirstOrDefault().Key;
                    if (maxId == default(Guid))
                        return retValue;
                    User user = dBContext.Users.Where(dUser => dUser.Id == maxId).FirstOrDefault();
                    if (user == null)
                        return retValue;
                    
                    return retValue= user.FirstName + " " + user.LastName;
                }

            }
            catch (Exception error)
            {

                throw error;
            }
        }
    }
}
