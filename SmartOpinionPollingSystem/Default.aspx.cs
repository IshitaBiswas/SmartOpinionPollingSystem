using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SOP.Services.Interfaces;
using SOP.Services;
using SOP.Common;

namespace SmartOpinionPollingSystem
{
    public partial class _Default : Page
    {


        public _Default()
        {

        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.IsAuthenticated)
            {
                if (Session["LoginType"] != null)
                {
                    if (((LoginTypeEnum)Session["LoginType"]) == LoginTypeEnum.User)
                    {
                        Response.Redirect("~/UserPages/UserProfile");
                    }
                    else if (((LoginTypeEnum)Session["LoginType"]) == LoginTypeEnum.Organization)
                    {
                        Response.Redirect("~/OrganizationPages/OrgProfile");
                    }
                }              
            }

           // gvUsers.DataSource = ips.GetRegisteredUsers();
           // gvUsers.DataBind();
        }
    }
}