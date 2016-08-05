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


namespace SmartOpinionPollingSystem.OrganizationPages
{
    public partial class OrgDashboard : System.Web.UI.Page
    {

       static IReportingService _rptService;


        public OrgDashboard()
        {
            _rptService = new ReportingService();

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                GetChartData();

                dgTest.DataSource = _rptService.GetOrgCategoryBreakup().ToList();
                dgTest.DataBind();
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
                chartData[j] = new object[] {i.OrgCategory, i.NumberOfOrganizations };
            }

            return chartData;
        }
    

    }
}