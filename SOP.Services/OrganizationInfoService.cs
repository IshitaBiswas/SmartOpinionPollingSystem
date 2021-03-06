﻿using System;
using System.Data;
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

            if (_odAccessor.DoesOrgWebsiteExist(org))
                throw new ApplicationException("An Organization with this website is already registered in the system.");

            //Business Valoidation...End
            
            
            _odAccessor.AddOrganization(org);

        }

        public void AddOrgPollingQuestionDetails(VotingQuestionDetail orgqstndetail)
        {
            _odAccessor.AddOrgPollingQuestionDetails(orgqstndetail);
        }

        public DataSet GetOrgPollingQuestionCategories(String orgID)
        {
            return _odAccessor.GetOrgPollingQuestionCategories(orgID);
        }

        public Organization GetOrganization(string orgID)
        {
            return _odAccessor.GetOrganization(orgID);
        }
        public void EditOrgProfile(Organization org)
        {
            _odAccessor.EditOrgProfile(org);
        }

        public bool OrgLogin(Organization org)
        {
             return _odAccessor.OrgLogin(org);
            
        }
        
    }
}
