using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOP.Common.Model;
using SOP.Common;
using System.Data;

namespace SOP.Data.Interfaces
{
    public interface IOrgDetailsAccessor
    {
         void AddOrganization(Organization org);
         bool DoesOrgExist(string OrgID);
         DataSet GetOrgPollingQuestionCategories(String orgID);
         void AddOrgPollingQuestionDetails(VotingQuestionDetail orgqstndetail);
         Organization GetOrganization(string orgID);
         void EditOrgProfile(Organization org);

         bool OrgLogin(Organization org);

    }
}
