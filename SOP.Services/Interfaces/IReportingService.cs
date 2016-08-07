using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOP.Common.Model;
using SOP.Common;

namespace SOP.Services.Interfaces
{
   public interface IReportingService
    {
       IEnumerable<OrgCategoryBreakup> GetOrgCategoryBreakup();
       IEnumerable<VotingQuestionDetail> GetVotingQuestionDetails(string OrgID, PollingWindowEnum pwEnum);
      
    }
}
