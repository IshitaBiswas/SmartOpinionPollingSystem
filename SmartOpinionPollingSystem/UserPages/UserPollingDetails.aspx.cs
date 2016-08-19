using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using SOP.Common.Model;
using SOP.Services.Interfaces;
using SOP.Services;
using System.Linq;
using System.Security.Principal;
using SOP.Common;
using System.IO;
using System.Web.Script.Serialization;

namespace SmartOpinionPollingSystem.UserPages
{
    public partial class UserPollingDetails : System.Web.UI.Page
    {
        static IReportingService _rptService;
        static PollingWindowEnum _pollingWindow;

        public UserPollingDetails()
        {

            _rptService = new ReportingService();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                UserDashBoard.NavigateUrl = "UserDashBoard";


                if (Request.QueryString["pollingwindow"] != null)
                {
                    var qString = Request.QueryString["pollingwindow"];
                    _pollingWindow = qString == "previous"
                        ? PollingWindowEnum.Previous
                        : (qString == "current"
                             ? PollingWindowEnum.Current
                             : (qString == "future" ? PollingWindowEnum.Future : PollingWindowEnum.All));

                    Session["pollingWindow"] = _pollingWindow; //Save to session
                }


                var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
                if (!String.IsNullOrEmpty(returnUrl))
                {
                    UserDashBoard.NavigateUrl += "?ReturnUrl=" + returnUrl;
                }

            }

        }



        [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static JqGridData GetUserPollingDetails(int page, int rows, string sidx, string sord, bool _search)
        {
            IEnumerable<UserVotingDetail> data = _rptService.GetUserVotingQuestionDetails(HttpContext.Current.User.Identity.Name, _pollingWindow);
            int recordsCount = data.ToList().Count;
            int startIndex = (page - 1) * rows;
            int endIndex = (startIndex + rows < recordsCount) ? startIndex + rows : recordsCount;
            List<UserVotingDetail> gridRows = new List<UserVotingDetail>(rows);
            for (int i = startIndex; i < endIndex; i++)
                gridRows.Add(data.ToArray()[i]);

            return new JqGridData()
            {
                total = (recordsCount + rows - 1) / rows,
                page = page,
                records = recordsCount,
                rows = gridRows
            };
        }



        //JqGrid related container classes
        public class JqGridData
        {
            public int total { get; set; }
            public int page { get; set; }
            public int records { get; set; }
            public List<UserVotingDetail> rows { get; set; }
        }

        public class jqGridSearchFilterItem
        {
            public string field { get; set; }
            public string op { get; set; }
            public string data { get; set; }
        }
        public class jqGridSearchFilter
        {
            public string groupOp { get; set; }
            public List<jqGridSearchFilterItem> rules { get; set; }
        }

    }
}