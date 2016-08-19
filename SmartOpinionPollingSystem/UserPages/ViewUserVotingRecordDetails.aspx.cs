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
    public partial class ViewUserVotingRecordDetails : System.Web.UI.Page
    {
        private static IReportingService  _rptService = new ReportingService();
        private static string _questionID;
        IUserInfoServices _iuiservices;


        public ViewUserVotingRecordDetails()
        {
            _rptService = new ReportingService();
            _iuiservices = new UserInfoServices();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.IsAuthenticated)
            {
                if (Request.QueryString["QuestionID"] != null)
                {
                    _questionID = Request.QueryString["QuestionID"];
                    PollingWindowEnum _pollingWindow = (PollingWindowEnum)Session["pollingWindow"];

                    IEnumerable<UserVotingDetail> userVotingDetails = _rptService.GetUserVotingQuestionDetails(HttpContext.Current.User.Identity.Name, _pollingWindow);
                    UserVotingDetail votingRecord = userVotingDetails.FirstOrDefault(r => r.QuestionID == Convert.ToInt32(_questionID));

                    if (userVotingDetails.Any() && _pollingWindow == PollingWindowEnum.Previous)
                    {
                        pnlPrevious.Visible = true;

                        lblOrgName.Text = votingRecord.OrgName;
                        lblQuestion.Text = votingRecord.QuestionText;
                        lblCategoryDescription.Text = votingRecord.CategoryDescription;
                        lblB_UserVote.Text = votingRecord.B_UserVote;
                        lblVotingStartDate.Text = votingRecord.VotingStartDate.ToShortDateString();
                        lblVotingEndDate.Text = votingRecord.VotingEndDate.ToShortDateString();
                        lblVotedYes.Text = votingRecord.VotedYes.ToString();
                        lblVotedNo.Text = votingRecord.VotedNo.ToString();
                        lblWinner.Text = votingRecord.VotedYes > votingRecord.VotedNo ? "Users voted in favour of the Question"
                                                                                      : (votingRecord.VotedYes < votingRecord.VotedNo
                                                                                                         ? "Users voted against the favour of the Question"
                                                                                                         : "It was a neutral vote");

                        GetPieChartData(); //Fill the chart

                    }
                    else  if (userVotingDetails.Any() && _pollingWindow == PollingWindowEnum.Current)
                    {
                                        
                        pnlCurrent.Visible = true;

                        pnlVoteCasted.Visible = true;
                        pnlGraphVoteCast.Visible = true; //This will only be set to true post casting of the vote

                        lblCurrentCategoryDescription.Text = votingRecord.CategoryDescription; 
                        lblCurrentOrgName.Text = votingRecord.OrgName;
                        lblCurrentQuestion.Text = votingRecord.QuestionText;
                        if (votingRecord.B_UserVote == "Yes")
                            pnlVotedYesImg.Visible = true;
                        else if (votingRecord.B_UserVote == "No")
                            pnlVotedYesImg.Visible = true;
                        GetPieChartData(); 

                    }
                    else
                    {
                        pnlMessage.Visible = true;
                        lblMesage.Text = "No votes were casted for this question!!!";
                    }

                }

            }
        }


      

        protected void imgThumsUp_Click(object sender, ImageClickEventArgs e)
        {

            UserVotingDetail vd = new UserVotingDetail()
            {
                UserID = HttpContext.Current.User.Identity.Name,
                QuestionID = Convert.ToInt32(_questionID),
                B_UserVote = "true"
            };

           _iuiservices.SaveUserVote(vd);

           pnlGraphVoteCast.Visible = false;
            GetPieChartData(); //Fill the chart
        }

        protected void imgThumsDown_Click(object sender, ImageClickEventArgs e)
        {
            UserVotingDetail vd = new UserVotingDetail()
            {
                UserID = HttpContext.Current.User.Identity.Name,
                QuestionID = Convert.ToInt32(_questionID),
                B_UserVote = "false"
            };

            _iuiservices.SaveUserVote(vd);

            pnlGraphVoteCast.Visible = false;
            GetPieChartData(); //Fill the chart
        }



        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object[] GetPieChartData()
        {

            var userVotingQuestion = _rptService.GetUserVotingQuestionDetails(HttpContext.Current.User.Identity.Name, PollingWindowEnum.All)
                                       .FirstOrDefault(r => r.QuestionID == Convert.ToInt32(_questionID));

            List<VotingResultContainer> data = new List<VotingResultContainer>()
                                                 { new VotingResultContainer()
                                                                {
                                                                    VotingOption = "Yes",
                                                                    VotingCount = userVotingQuestion.VotedYes
                                                                },
                                                              new VotingResultContainer()
                                                                {
                                                                    VotingOption = "No",
                                                                    VotingCount = userVotingQuestion.VotedNo
                                                                }
                                                };



            var chartData = new object[data.Count + 1];
            chartData[0] = new object[]{
                "Voting Option",
                "Count"
            };
            int j = 0;
            foreach (var i in data)
            {
                j++;
                chartData[j] = new object[] { i.VotingOption.ToString(), i.VotingCount };
            }

            return chartData;
        }


    }
}