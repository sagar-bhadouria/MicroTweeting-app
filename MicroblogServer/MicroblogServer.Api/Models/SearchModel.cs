using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicroblogServer.Api.Models
{
    public class SearchModel
    {
        public string SearchString { get; set; }
        public string UserId { get; set; }
    }
}