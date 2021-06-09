using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Microblog.Server.Bussiness;
using MicroblogServer.Shared.DTO;
namespace MicroblogServer.Api.Controllers
{
    public class AnalyticsController : ApiController
    {
        private AnalyticsBDC AnalyticsBDC;
        public AnalyticsController()
        {
            AnalyticsBDC = new AnalyticsBDC();
        }

        [HttpGet]
        [Route("api/analytics")]
        public IHttpActionResult Analytics() {
            try
            {
                AnalyticsDTO analytics = AnalyticsBDC.GetAnalysis();
                return Ok(new { Analytics = analytics });

            }
            catch (Exception error)
            {

                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError ,error.Message));
            }
        }
    }
}
