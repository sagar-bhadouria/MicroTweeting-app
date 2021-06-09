using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroblogServer.Shared.DTO;
using MicroblogServer.Entity.DAC;
namespace Microblog.Server.Bussiness.Business
{
    public class TweetTagBDC
    {
        TweetTagDAC tweetTagDAC;
        public TweetTagBDC()
        {
            tweetTagDAC = new TweetTagDAC();
        }
        public bool CreateNewTags(NewTweetDTO newtweetdto)
        {
            string[] result = newtweetdto.Message.Split(' ');
            List<string> tagElements = new List<string>();
            foreach (string s in result)
            {

                if (s.Contains('#'))
                {
                    tagElements.Add(s);
                }
            }

            bool res = tweetTagDAC.AddTags(tagElements, newtweetdto.TweetId);
            return res;
         }
    }
}
