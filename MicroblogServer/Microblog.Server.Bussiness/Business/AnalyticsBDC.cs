using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroblogServer.Entity.DAC;
using MicroblogServer.Shared.DTO;

namespace Microblog.Server.Bussiness
{
   
   public class AnalyticsBDC
    {
        private UserDAC UserDAC;
        private TweetDAC TweetDAC;
        public AnalyticsBDC()
        {
            UserDAC = new UserDAC();
            TweetDAC = new TweetDAC();
        }

        public AnalyticsDTO GetAnalysis() {
            AnalyticsDTO Analytics = new AnalyticsDTO();
            Analytics.MostTweetsBy = UserDAC.MostTweetBy();
            Analytics.MostTrending = TweetDAC.MostTrending();
            Analytics.MostLiked = TweetDAC.MostLiked();
            Analytics.TotalTweetsToday = TweetDAC.TotalTweetsToday();
            return Analytics;
        }
    }
}
