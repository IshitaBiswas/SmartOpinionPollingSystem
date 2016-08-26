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
       IEnumerable<UserVotingCategoryBreakup> GetUserVotingCategoryBreakup();
       IEnumerable<VotingQuestionDetail> GetVotingQuestionDetails(string OrgID, PollingWindowEnum pwEnum);
       IEnumerable<UserVotingDetail> GetUserVotingQuestionDetails(string userID, PollingWindowEnum pwEnum);
       IEnumerable<UserVotingDetail> GetPendingPollingQueue(string userID, PollingWindowEnum pwEnum = PollingWindowEnum.Current);
       IEnumerable<string> GetVotingCategorybyQuestionID(int questionID);
       IEnumerable<Discussion> GetQuestionDiscussions(int questionID);
       void SaveQuestionDiscussion(Discussion discussion);
    }
}
