using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroblogServer.Entity.DAC;
using MicroblogServer.Shared.DTO;

namespace Microblog.Server.Bussiness.Business
{
   public class SearchBDC
    {
        private SearchDAC SearchDAC;
        public SearchBDC()
        {
            SearchDAC = new SearchDAC();
        }

        public IList<SearchDTO> SearchAllUsers(string searchString, Guid userId) {
            try
            {
                IList<SearchDTO> getAllResult = SearchDAC.GetAllUsers(searchString, userId);
                return getAllResult;

            }
            catch (Exception error)
            {

                throw error;
            }
        }

        public IList<SearchDTO> SearchAllHashTag(string searchString, Guid userId)
        {
            try
            {
                IList<SearchDTO> getAllResult = SearchDAC.GetAllHashTag(searchString, userId);
                return getAllResult;

            }
            catch (Exception error)
            {

                throw error;
            }
        }
    }
}
