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
        static PollingWindowEnum _pollingWindow;
        static IReportingService _rptService;
        static IPollingStatusService _pollingStatusService;

        public UserPollingDetails()
        {

            _rptService = new ReportingService();
            _pollingStatusService = new PollingStatusService();
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

                    if(_pollingWindow ==PollingWindowEnum.Current )
                    {
                        pnlPendingVotingRecord.Visible = true;
                    }
                }


                var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
                if (!String.IsNullOrEmpty(returnUrl))
                {
                    UserDashBoard.NavigateUrl += "?ReturnUrl=" + returnUrl;
                }
            }

        }


        //'Pending Polling Queue - Waiting for user vote'--start

        [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static JqGridData GetPendingPollingQueue(int page, int rows, string sidx, string sord, bool _search)
        {
            IEnumerable<UserVotingDetail> data = _rptService.GetPendingPollingQueue(HttpContext.Current.User.Identity.Name);

            //Filter questions by Age and Gender
            if (data.Any(d => (d.MaxVotingAge ?? 0) > 0 && (d.MinVotingAge ?? 0) > 0) || data.Any(d => !(d.TargetAudienceGender.Trim().Equals("All"))))
            {
                User currentUser = _pollingStatusService.GetRegisteredUsers().First(u => u.UserID == HttpContext.Current.User.Identity.Name);
                data = data
                         .Where(d =>
                                 (((d.MaxVotingAge ?? 0) >= currentUser.Age && (d.MinVotingAge ?? 0) <= currentUser.Age)
                                          || ((d.MaxVotingAge ?? 0) == 0 && (d.MinVotingAge ?? 0) == 0))
                                 && (d.TargetAudienceGender.Trim().Equals("All") || (d.TargetAudienceGender.Trim().Equals(currentUser.Gender))));
            }


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

        //'Polling Queue - Waiting for user vote'--end



        //'User Voting Summary - Current Voting Window' -- start

        [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static JqGridData GetUserPollingDetails(int page, int rows, string sidx, string sord, bool _search)
        {
            IEnumerable<UserVotingDetail> data = null; 
            if (!(_pollingWindow == PollingWindowEnum.Future))
           {
                data = _rptService.GetUserVotingQuestionDetails(HttpContext.Current.User.Identity.Name, _pollingWindow);
           }
           else if(_pollingWindow == PollingWindowEnum.Future)
           {
               data = _rptService.GetPendingPollingQueue(HttpContext.Current.User.Identity.Name, PollingWindowEnum.Future);    
           }

           //Filter questions by Age and Gender
           if( data.Any(d => (d.MaxVotingAge??0) > 0 && (d.MinVotingAge??0) > 0) || data.Any(d => !(d.TargetAudienceGender.Trim().Equals("All"))))
           {
               User currentUser = _pollingStatusService.GetRegisteredUsers().First(u => u.UserID == HttpContext.Current.User.Identity.Name);
               data = data
                        .Where(d =>
                                (((d.MaxVotingAge ?? 0) >= currentUser.Age && (d.MinVotingAge ?? 0) <= currentUser.Age)
                                         || ((d.MaxVotingAge ?? 0) == 0 && (d.MinVotingAge ?? 0) == 0))
                                && (d.TargetAudienceGender.Trim().Equals("All") || (d.TargetAudienceGender.Trim().Equals(currentUser.Gender))));
           }


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



        //JqGrid container class
        public class JqGridData
        {
            public int total { get; set; }
            public int page { get; set; }
            public int records { get; set; }
            public List<UserVotingDetail> rows { get; set; }
        }

    }
}