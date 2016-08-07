using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using SOP.Common;
using SOP.Common.Model;
using SOP.Data.Interfaces;


namespace SOP.Data
{
  public   class ReportingAccessor : IReportingAccessor
    {

      public IEnumerable<OrgCategoryBreakup> GetOrgCategoryBreakup()
      {
          using (var _db = new SOPDbDataContext())
          {
              return _db.tblOrganizations
                  .GroupBy(o => o.OrgCategory)
                  .Select(g => new OrgCategoryBreakup { 
                                          OrgCategory =  g.Key,
                                          NumberOfOrganizations = g.Count() 
                        })
                  .OrderBy(x => x.OrgCategory).ToArray();
          }

      }


      public IEnumerable<VotingQuestionDetail> GetVotingQuestionDetails(string OrgID, PollingWindowEnum pwEnum) // enum
      {
          using (var _db = new SOPDbDataContext())
          {
              IEnumerable<VotingQuestionDetail> vqds;
              switch (pwEnum)
              {
                  case PollingWindowEnum.Previous:
                      vqds = _db.tblVotingQuestionDetails
                             .Where(q => q.OrgID == OrgID && q.VotingEndDate < DateTime.Now)
                             .Select(GetVotingQuestionDetailFromEntityExpression())
                         .ToArray();
                     break;
                  case PollingWindowEnum.Current:
                  vqds =    _db.tblVotingQuestionDetails
                          .Where(q => q.OrgID == OrgID &&  q.VotingStartDate <= DateTime.Now &&  q.VotingEndDate >= DateTime.Now )
                          .Select(GetVotingQuestionDetailFromEntityExpression())
                          .ToArray();
                      break;
                  case PollingWindowEnum.Future:
                  vqds =    _db.tblVotingQuestionDetails
                          .Where(q => q.OrgID == OrgID && q.VotingStartDate > DateTime.Now )
                          .Select(GetVotingQuestionDetailFromEntityExpression())
                          .ToArray();
                      break;
                  case PollingWindowEnum.All:
                  vqds =    _db.tblVotingQuestionDetails
                          .Where(q => q.OrgID == OrgID)
                          .Select(GetVotingQuestionDetailFromEntityExpression())
                          .ToArray();
                      break;
                  default:
                      vqds = new List<VotingQuestionDetail>();
                      break;
              }
              return vqds;
          }
      }



          private static Expression<Func<tblVotingQuestionDetail, VotingQuestionDetail>> GetVotingQuestionDetailFromEntityExpression()
          {
            return  s => new  VotingQuestionDetail
                                  {
                                      OrgID = s.OrgID,
                                      QuestionID = s.QuestionID,
                                      QuestionText = s.QuestionText,
                                      VotedYes = s.VotedYes,
                                      VotedNo = s.VotedNo,
                                      VotingQuestionCategoryID = s.VotingQuestionCategoryID,
                                      VotingStartDate = s.VotingStartDate,
                                      VotingEndDate = s.VotingEndDate
                                  };
          }

      }
}
