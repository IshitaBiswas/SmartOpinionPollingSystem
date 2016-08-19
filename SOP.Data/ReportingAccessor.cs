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

      public IEnumerable<UserVotingCategoryBreakup> GetUserVotingCategoryBreakup()
      {
          using (var _db = new SOPDbDataContext())
          {
              return _db.tblUserVotingCategories
                  .GroupBy(o => o.UserVotingCategoryID)
                  .Select(g => new UserVotingCategoryBreakup
                  {
                      VotingCategoryID = g.Key,
                      NumberOfUsers = g.Count(),
                      CategoryDescription  = _db.tblVotingCategoryDescs.FirstOrDefault(v => v.VotingCategoryID == g.Key ).CategoryDescription
                  })
                  .OrderBy(x => x.VotingCategoryID)
                  .ToArray();
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
                          .Join(_db.tblOrgQuestionTargetAudiences,
                          qd => qd.QuestionID,
                          ta => ta.QuestionID,
                          (qd,ta) => new { qd, ta})
                          .Where(z => z.ta.OrgID == OrgID && z.qd.VotingEndDate < DateTime.Now)
                          .Select(z=> new  VotingQuestionDetail
                                  {  
                                      OrgID =  z.ta.OrgID,
                                      QuestionID = z.ta.QuestionID,
                                      QuestionText = z.qd.QuestionText,
                                      VotedYes = z.qd.VotedYes,
                                      VotedNo = z.qd.VotedNo,
                                      VotingQuestionCategoryID = z.ta.VotingQuestionCategoryID,
                                      VotingStartDate = z.qd.VotingStartDate,
                                      VotingEndDate = z.qd.VotingEndDate,
                                      CategoryDescription =  GetVotingQuestionCategory(z.ta.VotingQuestionCategoryID)
                                  })
                         .ToArray();
                     break;
                  case PollingWindowEnum.Current:
                  vqds =    _db.tblVotingQuestionDetails
                      .Join(_db.tblOrgQuestionTargetAudiences,
                          qd => qd.QuestionID,
                          ta => ta.QuestionID,
                          (qd, ta) => new { qd, ta })
                         .Where(z => z.ta.OrgID == OrgID && z.qd.VotingStartDate <= DateTime.Now && z.qd.VotingEndDate >= DateTime.Now)
                           .Select(z => new VotingQuestionDetail
                           {
                               OrgID = z.ta.OrgID,
                               QuestionID = z.ta.QuestionID,
                               QuestionText = z.qd.QuestionText,
                               VotedYes = z.qd.VotedYes,
                               VotedNo = z.qd.VotedNo,
                               VotingQuestionCategoryID = z.ta.VotingQuestionCategoryID,
                               VotingStartDate = z.qd.VotingStartDate,
                               VotingEndDate = z.qd.VotingEndDate,
                               CategoryDescription = GetVotingQuestionCategory(z.ta.VotingQuestionCategoryID)
                           })
                         .ToArray();
                      break;
                  case PollingWindowEnum.Future:
                  vqds =    _db.tblVotingQuestionDetails
                      .Join(_db.tblOrgQuestionTargetAudiences,
                          qd => qd.QuestionID,
                          ta => ta.QuestionID,
                          (qd, ta) => new { qd, ta })
                         .Where(z => z.ta.OrgID == OrgID && z.qd.VotingStartDate > DateTime.Now)
                          .Select(z => new VotingQuestionDetail
                          {
                              OrgID = z.ta.OrgID,
                              QuestionID = z.ta.QuestionID,
                              QuestionText = z.qd.QuestionText,
                              VotedYes = z.qd.VotedYes,
                              VotedNo = z.qd.VotedNo,
                              VotingQuestionCategoryID = z.ta.VotingQuestionCategoryID,
                              VotingStartDate = z.qd.VotingStartDate,
                              VotingEndDate = z.qd.VotingEndDate,
                              CategoryDescription = GetVotingQuestionCategory(z.ta.VotingQuestionCategoryID)
                          })
                         .ToArray();
                      break;
                  case PollingWindowEnum.All:
                  vqds =    _db.tblVotingQuestionDetails
                        .Join(_db.tblOrgQuestionTargetAudiences,
                          qd => qd.QuestionID,
                          ta => ta.QuestionID,
                          (qd, ta) => new { qd, ta })
                         .Where(z => z.ta.OrgID == OrgID)
                          .Select(z => new VotingQuestionDetail
                          {
                              OrgID = z.ta.OrgID,
                              QuestionID = z.ta.QuestionID,
                              QuestionText = z.qd.QuestionText,
                              VotedYes = z.qd.VotedYes,
                              VotedNo = z.qd.VotedNo,
                              VotingQuestionCategoryID = z.ta.VotingQuestionCategoryID,
                              VotingStartDate = z.qd.VotingStartDate,
                              VotingEndDate = z.qd.VotingEndDate,
                              CategoryDescription = GetVotingQuestionCategory(z.ta.VotingQuestionCategoryID)
                          })
                         .ToArray();
                      break;
                  default:
                      vqds = new List<VotingQuestionDetail>();
                      break;
              }
              return vqds;
          }
      }



          //private static Expression<Func<Object, VotingQuestionDetail>> GetVotingQuestionDetailFromEntityExpression(tblVotingQuestionDetail s, tblOrgQuestionTargetAudience t)
          //{
          //  return  o => new  VotingQuestionDetail
          //                        {  
          //                            OrgID =  t.OrgID,
          //                            QuestionID = t.QuestionID,
          //                            QuestionText = s.QuestionText,
          //                            VotedYes = s.VotedYes,
          //                            VotedNo = s.VotedNo,
          //                            VotingQuestionCategoryID = t.VotingQuestionCategoryID,
          //                            VotingStartDate = s.VotingStartDate,
          //                            VotingEndDate = s.VotingEndDate,
          //                            CategoryDescription =  GetVotingQuestionCategory(t.VotingQuestionCategoryID)
          //                        };
          //}


          private static string GetVotingQuestionCategory(int votingQuestionCategoryID)
          {
              using (var _db = new SOPDbDataContext())
              {
                  return _db.tblVotingCategoryDescs
                      .FirstOrDefault(v => v.VotingCategoryID == votingQuestionCategoryID).CategoryDescription;
              }
          }



          private  tblOrganization GetOrgDetails(string orgID)
          {
              using (var _db = new SOPDbDataContext())
              {
                  return _db.tblOrganizations
                      .FirstOrDefault(v => v.OrgID == orgID);
              }
          }

      //wrong///////////////////////////////////////
          public IEnumerable<UserVotingDetail> GetUserVotingQuestionDetails(string userID, PollingWindowEnum pwEnum) // enum
          {
              using (var _db = new SOPDbDataContext())
              {

                  IEnumerable<UserVotingDetail> vqds;
                  switch (pwEnum)
                  {
                      case PollingWindowEnum.Previous:
                          vqds = _db.tblVotingQuestionDetails
                              .Join(_db.tblUserVotingDetails,
                              qd => qd.QuestionID,
                              uv => uv.QuestionID,
                              (qd, uv) => new { qd, uv })
                              .Where(z => z.uv.UserID == userID && z.qd.VotingEndDate < DateTime.Now)
                              .Select(z => new UserVotingDetail
                              {
                                  OrgName =  GetOrgDetails(z.qd.tblOrgQuestionTargetAudiences.FirstOrDefault(a => a.QuestionID == z.uv.QuestionID).OrgID).OrgName,
                                  UserID = z.uv.UserID,
                                  QuestionID = z.uv.QuestionID,
                                  QuestionText = z.qd.QuestionText,
                                  VotedYes = z.qd.VotedYes,
                                  VotedNo = z.qd.VotedNo,
                                  VotingStartDate = z.qd.VotingStartDate, 
                                  VotingEndDate = z.qd.VotingEndDate,
                                  B_UserVote = z.uv.B_UserVote ? "Yes": "No",
                                  DtVoteCasted = z.uv.DtVoteCasted,
                                  CategoryDescription = GetVotingQuestionCategory( z.qd.tblOrgQuestionTargetAudiences
                                                                                    .FirstOrDefault(a => a.QuestionID == z.uv.QuestionID)
                                                                                    .VotingQuestionCategoryID)
                              })
                             .ToArray();
                          break;
                      case PollingWindowEnum.Current:
                          vqds = _db.tblVotingQuestionDetails
                              .Join(_db.tblUserVotingDetails,
                              qd => qd.QuestionID,
                              uv => uv.QuestionID,
                              (qd, uv) => new { qd, uv })
                               .Where(z => z.uv.UserID == userID && z.qd.VotingStartDate <= DateTime.Now && z.qd.VotingEndDate >= DateTime.Now)
                              .Select(z => new UserVotingDetail
                              {
                                  OrgName = GetOrgDetails(z.qd.tblOrgQuestionTargetAudiences.FirstOrDefault(a => a.QuestionID == z.uv.QuestionID).OrgID).OrgName,
                                  UserID = z.uv.UserID,
                                  QuestionID = z.uv.QuestionID,
                                  QuestionText = z.qd.QuestionText,
                                  VotedYes = z.qd.VotedYes,
                                  VotedNo = z.qd.VotedNo,
                                  VotingStartDate = z.qd.VotingStartDate,
                                  VotingEndDate = z.qd.VotingEndDate,
                                  B_UserVote = z.uv.B_UserVote ? "Yes": "No",
                                  DtVoteCasted = z.uv.DtVoteCasted,
                                  CategoryDescription = GetVotingQuestionCategory(z.qd.tblOrgQuestionTargetAudiences
                                                                                .FirstOrDefault(a => a.QuestionID == z.uv.QuestionID)
                                                                                .VotingQuestionCategoryID)
                              })
                             .ToArray();
                          break;
                      case PollingWindowEnum.Future:
                          vqds = _db.tblVotingQuestionDetails
                               .Join(_db.tblUserVotingDetails,
                               qd => qd.QuestionID,
                               uv => uv.QuestionID,
                               (qd, uv) => new { qd, uv })
                                .Where(z => z.uv.UserID == userID && z.qd.VotingStartDate > DateTime.Now)
                               .Select(z => new UserVotingDetail
                               {
                                   OrgName = GetOrgDetails(z.qd.tblOrgQuestionTargetAudiences.FirstOrDefault(a => a.QuestionID == z.uv.QuestionID).OrgID).OrgName,
                                   UserID = z.uv.UserID,
                                   QuestionID = z.uv.QuestionID,
                                   QuestionText = z.qd.QuestionText,
                                   VotedYes = z.qd.VotedYes,
                                   VotedNo = z.qd.VotedNo,
                                   VotingStartDate = z.qd.VotingStartDate,
                                   VotingEndDate = z.qd.VotingEndDate,
                                   B_UserVote = z.uv.B_UserVote ? "Yes" : "No",
                                   DtVoteCasted = z.uv.DtVoteCasted,
                                   CategoryDescription = GetVotingQuestionCategory(z.qd.tblOrgQuestionTargetAudiences
                                                                                .FirstOrDefault(a => a.QuestionID == z.uv.QuestionID)
                                                                                .VotingQuestionCategoryID)
                               })
                              .ToArray();
                          break;
                      case PollingWindowEnum.All:
                          vqds = _db.tblVotingQuestionDetails
                               .Join(_db.tblUserVotingDetails,
                               qd => qd.QuestionID,
                               uv => uv.QuestionID,
                               (qd, uv) => new { qd, uv })
                                .Where(z => z.uv.UserID == userID)
                               .Select(z => new UserVotingDetail
                               {
                                   OrgName = GetOrgDetails(z.qd.tblOrgQuestionTargetAudiences.FirstOrDefault(a => a.QuestionID == z.uv.QuestionID).OrgID).OrgName,
                                   UserID = z.uv.UserID,
                                   QuestionID = z.uv.QuestionID,
                                   QuestionText = z.qd.QuestionText,
                                   VotedYes = z.qd.VotedYes,
                                   VotedNo = z.qd.VotedNo,
                                   VotingStartDate = z.qd.VotingStartDate,
                                   VotingEndDate = z.qd.VotingEndDate,
                                   B_UserVote = z.uv.B_UserVote ? "Yes" : "No",
                                   DtVoteCasted = z.uv.DtVoteCasted,
                                   CategoryDescription = GetVotingQuestionCategory(z.qd.tblOrgQuestionTargetAudiences
                                                                                .FirstOrDefault(a => a.QuestionID == z.uv.QuestionID)
                                                                                .VotingQuestionCategoryID)
                               })
                              .ToArray();
                          break;
                      default:
                          vqds = new List<UserVotingDetail>();
                          break;
                  }
                  return vqds;
              }
            
          }


      }
}
