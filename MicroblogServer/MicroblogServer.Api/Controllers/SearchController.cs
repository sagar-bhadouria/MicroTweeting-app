using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microblog.Server.Bussiness.Business;
using MicroblogServer.Api.Models;
using MicroblogServer.Shared.DTO;
using MicroblogServer.Shared.Constants;

namespace MicroblogServer.Api.Controllers
{
    public class SearchController : ApiController
    {
        private SearchBDC SearchBDC;
        public SearchController()
        {
            SearchBDC = new SearchBDC();
        }

        #region SearchUser
        [HttpGet]
        [Route("api/user/searchUser/{userId}/{searchString}")]
        public IHttpActionResult SearchUser(string userId , string searchString)
        {
            try
            {
                SearchDTO searchDTO = new SearchDTO();
                searchDTO.UserId = Guid.Parse(userId);
                searchDTO.SearchString = searchString;
                IList<SearchDTO> AllResults = SearchBDC.SearchAllUsers(searchDTO.SearchString, searchDTO.UserId);
                if (AllResults == null)
                {
                    Status status = new Status();
                    status.StatusCode = Constant.Ex103;
                    status.Message = Constant.NotFound;
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, status));
                }
                return Ok(new { Result = AllResults });
            }
            catch (Exception error)
            {

                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, error.Message));
            }

        }
        #endregion

        #region Searchhash Tag
        [HttpGet]
        [Route("api/user/search-hashtag")]
        public IHttpActionResult SearchTag([FromBody] SearchModel SearchQuery)
        {
            try
            {
                SearchDTO searchDTO = new SearchDTO();
                searchDTO.UserId = Guid.Parse(SearchQuery.UserId);
                searchDTO.SearchString = SearchQuery.SearchString;
                IList<SearchDTO> AllResults = SearchBDC.SearchAllHashTag(searchDTO.SearchString, searchDTO.UserId);
                if (AllResults == null)
                {
                    Status status = new Status();
                    status.StatusCode = Constant.Ex103;
                    status.Message = Constant.NotFound;
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.Forbidden, status));
                }

                return Ok(new { Result = AllResults });
            }
            catch (Exception error)
            {

                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, error.Message));
            }

        } 
        #endregion
    }
}
