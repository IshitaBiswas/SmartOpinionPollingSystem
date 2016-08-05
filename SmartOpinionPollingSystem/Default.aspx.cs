using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SOP.Services.Interfaces;
using SOP.Services;

namespace SmartOpinionPollingSystem
{
    public partial class _Default : Page
    {

        IPollingStatusService ips;

        public _Default()
        {
           ips = new PollingStatusService();

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            gvUsers.DataSource = ips.GetRegisteredUsers();
            gvUsers.DataBind();
            
        }
    }
}