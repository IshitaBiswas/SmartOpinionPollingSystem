using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using SOP.Services;
using Newtonsoft.Json;
using SOP.Services.Interfaces;
using SOP.Common;
using SOP.Common.Model;

namespace SmartOpinionPollingSystem.OrganizationPages
{
    public partial class OrgReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                hlnkOrgDashBoard.NavigateUrl = "OrgDashBoard";

            }
        }



    }




}