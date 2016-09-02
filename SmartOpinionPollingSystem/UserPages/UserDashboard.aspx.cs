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


namespace SmartOpinionPollingSystem.UserPages
{
    public partial class UserDashboard : System.Web.UI.Page
    {
        static IReportingService _rptService;


        public UserDashboard()
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
               // GetChartData();
            }

        }
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object[] GetChartData()
        {
            List<UserVotingCategoryBreakup> data = new List<UserVotingCategoryBreakup>();
            data = _rptService.GetUserVotingCategoryBreakup().ToList();

            var chartData = new object[data.Count + 1];
            chartData[0] = new object[]{
                "Voting Category",
                "Registerd Users Count"

            };
            int j = 0;
            foreach (var i in data)
            {
                j++;
                chartData[j] = new object[] { i.CategoryDescription.ToString(), i.NumberOfUsers };
            }

            return chartData;
        }



        protected void btnCurrentPollingQuestions_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UserPages/UserPollingDetails.aspx?pollingwindow=current");
        }

        protected void btnPrevPollingQuestions_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UserPages/UserPollingDetails.aspx?pollingwindow=previous");
        }

        protected void btnFuturePollingQuestions_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UserPages/UserPollingDetails.aspx?pollingwindow=future");
        }

        
    }
}