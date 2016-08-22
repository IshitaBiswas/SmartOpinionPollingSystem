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
                    if (Request.QueryString["QuestionID"] != null)
                    {
                        _questionID = Request.QueryString["QuestionID"];
                        PollingWindowEnum _pollingWindow = (PollingWindowEnum)Session["pollingWindow"];

                        VotingQuestionDetail votingRecord = _rptService
                                                        .GetVotingQuestionDetails(HttpContext.Current.User.Identity.Name, _pollingWindow)
                                                        .FirstOrDefault(r => r.QuestionID == Convert.ToInt32(_questionID));

                        if (votingRecord != null && _pollingWindow == PollingWindowEnum.Previous)
                        {
                            pnlPrevious.Visible = true;

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

                        }
                        else
                        {
                            pnlMessage.Visible = true;
                            lblMesage.Text = "The polling window has not started for this question!!!";
                        }

                    }

                }
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