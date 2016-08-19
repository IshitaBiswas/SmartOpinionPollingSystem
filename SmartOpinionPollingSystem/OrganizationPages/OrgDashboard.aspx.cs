using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using SOP.Common.Model;
using SOP.Services.Interfaces;
using SOP.Services;
using System.Linq;
using Newtonsoft.Json;
using SOP.Common;



namespace SmartOpinionPollingSystem.OrganizationPages
{
    public partial class OrgDashboard : System.Web.UI.Page
    {

        static IReportingService _rptService;
        static PollingWindowEnum _pollingWindow;


        public OrgDashboard()
        {
            _rptService = new ReportingService();
        }
        protected void Page_Preload(object sender, EventArgs e)
        {
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                GetChartData();
            }

        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object[] GetChartData()
        {
            List<OrgCategoryBreakup> data = new List<OrgCategoryBreakup>();
            data = _rptService.GetOrgCategoryBreakup().ToList();

            var chartData = new object[data.Count + 1];
            chartData[0] = new object[]{
                "Org Category",
                "Org Count"
            };
            int j = 0;
            foreach (var i in data)
            {
                j++;
                chartData[j] = new object[] { i.OrgCategory, i.NumberOfOrganizations };
            }

            return chartData;
        }







        protected void btnPrevPollingStatus_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/OrganizationPages/OrgPollingDetails.aspx?pollingwindow=previous");
        }

        protected void btnCurrentPollingStatus_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/OrganizationPages/OrgPollingDetails.aspx?pollingwindow=current");
        }

        protected void btnFuturePollingStatus_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/OrganizationPages/OrgPollingDetails.aspx?pollingwindow=future");
        }

        protected void btnAddQuestion_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/OrganizationPages/AddNewQuestion.aspx");
        }

    }
}