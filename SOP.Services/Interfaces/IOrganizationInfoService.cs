using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOP.Common.Model;
using System.Data;

namespace SOP.Services.Interfaces
{
   public interface IOrganizationInfoService
    {
       void AddOrgDetails(Organization org);
       DataSet GetOrgPollingQuestionCategories(String orgID);
       void AddOrgPollingQuestionDetails(VotingQuestionDetail orgqstndetail);

       bool OrgLogin(Organization org);
       Organization GetOrganization(string orgID);
       void EditOrgProfile(Organization org);
    }
}
