﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOP.Common.Model;

namespace SOP.Services.Interfaces
{
   public interface IOrganizationInfoService
    {
       void AddOrgDetails(Organization org);
       bool OrgLogin(Organization org);
    }
}
