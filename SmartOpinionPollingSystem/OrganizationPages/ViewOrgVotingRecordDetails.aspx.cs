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
using System.Text;
using System.IO;
using System.Web.Script.Serialization;

namespace SmartOpinionPollingSystem.OrganizationPages
{
    public partial class ViewOrgVotingRecordDetails : System.Web.UI.Page
    {
            private static IReportingService _rptService = new ReportingService();
            private static string _questionID;
            IUserInfoServices _iuiservices;


            public ViewOrgVotingRecordDetails()
            {
                _rptService = new ReportingService();
                _iuiservices = new UserInfoServices();
            }

            protected void Page_Load(object sender, EventArgs e)
            {

                if (Request.IsAuthenticated)
                {
                    hlnkOrgDashBoard.NavigateUrl = "OrgDashBoard";
                    hlnkPollingDetails.NavigateUrl = "OrgPollingDetails";

                    if (Request.QueryString["QuestionID"] != null)
                    {
                        _questionID = Request.QueryString["QuestionID"];
                        PollingWindowEnum _pollingWindow = (PollingWindowEnum)Session["pollingWindow"];

                        hlnkPollingDetails.Text = "View " + _pollingWindow.ToString() + " Polling Details";

                        VotingQuestionDetail votingRecord = _rptService
                                                        .GetVotingQuestionDetails(HttpContext.Current.User.Identity.Name, _pollingWindow)
                                                        .FirstOrDefault(r => r.QuestionID == Convert.ToInt32(_questionID));

                        if (votingRecord != null && _pollingWindow == PollingWindowEnum.Previous)
                        {
                            pnlPrevious.Visible = true;

                            lblPreviousDiscussion.Text = getDiscussions();

                            lblQuestion.Text = votingRecord.QuestionText;
                            lblCategoryDescription.Text = votingRecord.CategoryDescription;
                            lblVotingStartDate.Text = votingRecord.VotingStartDate.ToShortDateString();
                            lblVotingEndDate.Text = votingRecord.VotingEndDate.ToShortDateString();
                            lblVotedYes.Text = votingRecord.VotedYes.ToString();
                            lblVotedNo.Text = votingRecord.VotedNo.ToString();
                            lblWinner.Text = votingRecord.VotedYes > votingRecord.VotedNo ? "Users voted in favour of the Question"
                                                                                          : (votingRecord.VotedYes < votingRecord.VotedNo
                                                                                                             ? "Users voted against the favour of the Question"
                                                                                                             : "It was a neutral vote");


                        }
                        else if (votingRecord != null && _pollingWindow == PollingWindowEnum.Current)
                        {
                            pnlCurrent.Visible = true;
                            lblCurrentCategoryDescription.Text = votingRecord.CategoryDescription;
                            lblCurrentQuestion.Text = votingRecord.QuestionText;
                            lblCurrentStartDate.Text = votingRecord.VotingStartDate.ToShortDateString();
                            lblCurrentEndDate.Text = votingRecord.VotingEndDate.ToShortDateString();

                            lblCurrentDiscussion.Text =  getDiscussions();

                        }
                        else if (_pollingWindow == PollingWindowEnum.Future)
                        {
                            pnlFuture.Visible = true;
                            pnlFuturemsg.Visible = true;
                            lblFuturemsg.Text = "The polling window has not started for this question!!!";
                        }

                    }

                }
            }

            private string getDiscussions()
            {

                StringBuilder sb = new StringBuilder();
                var discussions = _rptService.GetQuestionDiscussions(Convert.ToInt32(_questionID)).ToList();

                if (discussions != null && discussions.Any())
                {
                    discussions.ForEach(d =>
                    {
                        sb.Append("<strong><font size='2' face='Verdana'>");
                        sb.Append(d.UserFName + " : ");
                        sb.Append("</font></strong>");

                        sb.Append("<font size='2' face='Verdana'>");
                        sb.Append(d.DiscussionText + " ");
                        sb.Append("</font>");
                        sb.Append("<br/><font color='gray' size='1' >Posted on:  " + d.DateDiscussionCreated);
                        sb.Append("</font><br/><br/>");

                    });
                }

                return sb.ToString();

                
            }
          


            [WebMethod]
            [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
            public static object[] GetPieChartData()
            {

                var votingQuestion = _rptService.GetVotingQuestionDetails(HttpContext.Current.User.Identity.Name, PollingWindowEnum.All)
                                           .FirstOrDefault(r => r.QuestionID == Convert.ToInt32(_questionID));

                if (votingQuestion != null)
                {

                    List<VotingResultContainer> data = new List<VotingResultContainer>()
                                                 { new VotingResultContainer()
                                                                {
                                                                    VotingOption = "Yes",
                                                                    VotingCount = votingQuestion.VotedYes
                                                                },
                                                              new VotingResultContainer()
                                                                {
                                                                    VotingOption = "No",
                                                                    VotingCount = votingQuestion.VotedNo
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
                else return new object[0];


            }
        
    }
}