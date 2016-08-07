using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOP.Services.Interfaces;
using SOP.Data;
using SOP.Data.Interfaces;
using SOP.Common;
using SOP.Common.Model;

namespace SOP.Services
{   
    public class ReportingService : IReportingService
    {
        IReportingAccessor _rptAcessor;
        public ReportingService()
        {
            _rptAcessor = new ReportingAccessor();
        }
        public IEnumerable<OrgCategoryBreakup> GetOrgCategoryBreakup()
         {
             return _rptAcessor.GetOrgCategoryBreakup();
         }

        public IEnumerable<VotingQuestionDetail> GetVotingQuestionDetails(string OrgID, PollingWindowEnum pwEnum) // enum
        {
              return _rptAcessor.GetVotingQuestionDetails(OrgID,pwEnum);
        }

    }
}
