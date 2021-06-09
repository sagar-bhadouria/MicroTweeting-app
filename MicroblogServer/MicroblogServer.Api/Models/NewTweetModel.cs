using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace MicroblogServer.Api.Models
{
    public class NewTweetModel
    {
        [Required]
        public string UserID { get; set; }

        [Required]
        [MaxLength(240)]
        public string Message { get; set; }

        
        public string TweetId { get; set; }
       
    }
}