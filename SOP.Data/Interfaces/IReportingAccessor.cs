﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOP.Common;
using SOP.Common.Model;

namespace SOP.Data.Interfaces
{
  public  interface IReportingAccessor
    {
      IEnumerable<OrgCategoryBreakup> GetOrgCategoryBreakup();
      IEnumerable<UserVotingCategoryBreakup> GetUserVotingCategoryBreakup();
      IEnumerable<VotingQuestionDetail> GetVotingQuestionDetails(string OrgID, PollingWindowEnum pwEnum);
      IEnumerable<UserVotingDetail> GetUserVotingQuestionDetails(string userID, PollingWindowEnum pwEnum);
    }
}
