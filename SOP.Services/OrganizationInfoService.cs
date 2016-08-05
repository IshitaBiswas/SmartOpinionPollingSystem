using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOP.Services.Interfaces;
using SOP.Data;
using SOP.Data.Interfaces;
using SOP.Common.Model;

namespace SOP.Services
{
    public class OrganizationInfoService : IOrganizationInfoService
    {
        
        IOrgDetailsAccessor _odAccessor;

        public OrganizationInfoService()
        {
            _odAccessor = new OrgDetailsAccessor();

        }
        public void AddOrgDetails(Organization org)
        {
            //Business Valoidation...Start
            if (_odAccessor.DoesOrgExist(org.OrgID))
                throw new ApplicationException("An Organization with this ID is already registered.");

            //Business Valoidation...End
            
            
            _odAccessor.AddOrganization(org);

        }

        public bool OrgLogin(Organization org)
        {
             return _odAccessor.OrgLogin(org);
            
        }
        
    }
}
