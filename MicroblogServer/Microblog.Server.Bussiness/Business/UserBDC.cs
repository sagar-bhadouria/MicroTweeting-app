using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroblogServer.Entity.DAC;
using MicroblogServer.Shared.DTO;
using MicroblogServer.Shared.PasswordHash;

namespace Microblog.Server.Bussiness.Business
{
    public class UserBDC
    {
        private UserDAC UserDAC;

        public UserBDC()
        {
            this.UserDAC = new UserDAC();
        }

        #region Register User 
        public async Task<UserDTO> RegisterUser(UserDTO userInput)
        {
            try
            {
                if (!UserDAC.EmailExist(userInput.Email))
                {
                    userInput.Password = HashedPassword.HashPassword(userInput.Password);
                    userInput = await UserDAC.RegisterUser(userInput);
                    return userInput;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception error)
            {
                throw error;
            }

        }
        #endregion

        #region Login User
        public async Task<UserDTO> LoginUser(UserLoginDTO userLoginDTO)
        {
            try
            {
                UserDTO userDTO = UserDAC.LoginUser(userLoginDTO.Email);
                if (userDTO != null && HashedPassword.ValidatePassword(userLoginDTO.Password, userDTO.Password))
                {
                    return await UserDAC.GetUserInfoByID(userDTO);
                }
                return null;
            }
            catch (Exception error)
            {
                throw error;
            }

        }
        #endregion

        #region Unfollow
        public bool UnFollow(FollowDTO followdto)
        {
            try
            {
                bool result = UserDAC.UnFollow(followdto);
                return true;
            }
            catch (Exception error)
            {

                throw error;
            }

        }
        #endregion
       
        #region Follow
        public bool Follow(FollowDTO followdto)
        {
            try
            {
                bool result = UserDAC.Follow(followdto);
                return result;
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
            try
            {
                IList<UserBasicDTO> following = UserDAC.GetAllFollowing(userId);
                return following;
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
            try
            {
                IList<UserBasicDTO> followers = UserDAC.GetAllFollowers(userId);
                return followers;
            }
            catch (Exception error)
            {

                throw error;
            }
        } 
        #endregion
    }
}
