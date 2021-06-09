using System;
using AutoMapper;
using System.Threading.Tasks;
using System.Text;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web;
using Newtonsoft.Json;
using System.Web.Http.Cors;
using System.Web.Http;
using System.Net;
using System.Net.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using MicroblogServer.Api.Models;
using MicroblogServer.Shared.DTO;
using Microblog.Server.Bussiness.Business;
using MicroblogServer.Shared.Constants;
namespace MicroblogServer.Api.Controllers
{
   // [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserController : ApiController
    {
        private UserBDC UserBDC;
        IMapper UserMapper;


        public UserController()
        {
            UserBDC = new UserBDC();

            var userMappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<UserRegisterModel, UserDTO>();
                config.CreateMap<LoginModel, UserLoginDTO>();
            });
            UserMapper = new Mapper(userMappingConfig);
        }

        #region Register user API
        [AllowAnonymous]
        [HttpPost]
        [Route("api/user/register")]
        public async Task<IHttpActionResult> Register([FromBody] UserRegisterModel user)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.Forbidden, ModelState));
                }

                UserDTO userDTO = UserMapper.Map<UserRegisterModel, UserDTO>(user);
                UserDTO newUser = await UserBDC.RegisterUser(userDTO);
                if (newUser == null)
                {
                    Status status = new Status();
                    status.StatusCode = Constant.Ex101;
                    status.Message = Constant.UserExist;

                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, status));
                }
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, newUser));

            }
            catch (Exception e)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, JsonConvert.SerializeObject(e.Message)));
            }
        }
        #endregion

        #region User Login API
        [HttpPost]
        [Route("api/user/login")]
        public async Task<IHttpActionResult> LoginUser([FromBody] LoginModel user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.Forbidden, ModelState));
                }

                UserLoginDTO userLoginDTO = UserMapper.Map<LoginModel, UserLoginDTO>(user);
                UserDTO loginUser = await UserBDC.LoginUser(userLoginDTO);
                if (loginUser == null)
                {
                    Status status = new Status();
                    status.StatusCode = Constant.Ex102;
                    status.Message = Constant.InvalidUser;
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, status));
                }
                var response = Request.CreateResponse(HttpStatusCode.OK, loginUser);

                return ResponseMessage(response);

            }
            catch (Exception error)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, JsonConvert.SerializeObject(error.Message)));
            }
        }
        #endregion

        #region User FOllow API
        [HttpPost]
        [Route("api/user/follow")]
        public IHttpActionResult Follow(FollowModel followModel)
        {

            try
            {
                FollowDTO followDTO = new FollowDTO();
                followDTO.UserId = Guid.Parse(followModel.UserId);
                followDTO.UserToFollowId = Guid.Parse(followModel.UserToFollowId);
                bool result = UserBDC.Follow(followDTO);
                return Ok(new { Action = result });
            }
            catch (Exception error)
            {

                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, error.Message));
            }

        }
        #endregion

        #region User Unfollow API
        [HttpPost]
        [Route("api/user/unfollow")]
        public IHttpActionResult Unfollow(FollowModel followModel)
        {
            try
            {
                FollowDTO followDTO = new FollowDTO();
                followDTO.UserId = Guid.Parse(followModel.UserId);
                followDTO.UserToFollowId = Guid.Parse(followModel.UserToFollowId);
                bool result = UserBDC.UnFollow(followDTO);
                return Ok(new { Action = result });
            }
            catch (Exception error)
            {

                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, error.Message));
            }

        } 
        #endregion

        #region Get Following API
        [HttpGet]
        [Route("api/user/following/{userId}")]
        public IHttpActionResult Following(string userId)
        {
            try
            {
                Guid UserId = Guid.Parse(userId);
                IList<UserBasicDTO> following = UserBDC.GetAllFollowing(UserId);

                return Ok(new { Following = following, FollowingCount = following.Count });
            }
            catch (Exception error)
            {

                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, error.Message));
            }

        }
        #endregion

        #region Get Followers API
        [HttpGet]
        [Route("api/user/followers/{userId}")]
        public IHttpActionResult Followers(string userId)
        {
            try
            {
                Guid UserId = Guid.Parse(userId);
                IList<UserBasicDTO> followers = UserBDC.GetAllFollowers(UserId);
                return Ok(new { Followers = followers, FollowersCount = followers.Count });
            }
            catch (Exception error)
            {

                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, error.Message));
            }

        } 
        #endregion

        #region JWT Token
        private string CreateJWT(UserDTO user)
        {
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("This is my secret"));
            var Claims = new Claim[] {
               new Claim(ClaimTypes.Email , user.Email ),
               new Claim(ClaimTypes.NameIdentifier , user.Id.ToString())
            };

            var SigningCredential = new SigningCredentials(
                Key, SecurityAlgorithms.HmacSha256Signature);

            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(Claims),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = SigningCredential
            };

            var TokenHandler = new JwtSecurityTokenHandler();
            var Token = TokenHandler.CreateToken(TokenDescriptor);

            return TokenHandler.WriteToken(Token);
        } 
        #endregion
    }
}