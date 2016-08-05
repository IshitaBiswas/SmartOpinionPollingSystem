using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOP.Common.Model;

namespace SOP.Data.Interfaces
{
    public interface IOrgDetailsAccessor
    {
         void AddOrganization(Organization org);
         bool DoesOrgExist(string OrgID);

         bool OrgLogin(Organization org);

    }
}
