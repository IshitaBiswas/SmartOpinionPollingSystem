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
using System.Text;
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
                hlnkUserDashBoard.NavigateUrl = "UserDashBoard";
                hlnkPollingDetails.NavigateUrl = "UserPollingDetails";

                if (Request.QueryString["QuestionID"] != null)
                {
                    _questionID = Request.QueryString["QuestionID"];
                    PollingWindowEnum _pollingWindow = (PollingWindowEnum)Session["pollingWindow"];

                    hlnkPollingDetails.Text = "View " +  _pollingWindow.ToString() +" Polling Details";

                    UserVotingDetail votingRecord = _rptService
                                                    .GetUserVotingQuestionDetails(HttpContext.Current.User.Identity.Name, _pollingWindow)
                                                    .FirstOrDefault(r => r.QuestionID == Convert.ToInt32(_questionID));

                    if (votingRecord != null && _pollingWindow == PollingWindowEnum.Previous)
                    {
                        pnlPrevious.Visible = true;

                        StringBuilder sb = new StringBuilder();
                        var discussions = _rptService.GetQuestionDiscussions(votingRecord.QuestionID).ToList();

                        discussions.ForEach(d =>
                                        {
                                            sb.Append("<strong><font size='2' face='Verdana'>");
                                            sb.Append(d.UserFName  + " : ");
                                            sb.Append("</font></strong>");

                                            sb.Append("<font size='2' face='Verdana'>");
                                            sb.Append(d.DiscussionText + " ");
                                            sb.Append("</font>");
                                            sb.Append("<br/><font color='gray' size='1' >Posted on:  " + d.DateDiscussionCreated);
                                            sb.Append("</font><br/><br/>");

                                        });

                        lblPreviousDiscussion.Text = sb.ToString();

                        lblOrgName.Text = votingRecord.OrgName;
                        lblQuestion.Text = votingRecord.QuestionText;
                        lblCategoryDescription.Text = string.Join(",", votingRecord.AllCategories);
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
                    else if (votingRecord != null && _pollingWindow == PollingWindowEnum.Current)
                    {
                                        
                        pnlCurrent.Visible = true;

                        StringBuilder sb = new StringBuilder();
                        var discussions = _rptService.GetQuestionDiscussions(votingRecord.QuestionID).ToList();

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

                        lblCurrentDiscussion.Text = sb.ToString();

                        if (!pnlToCastVote.Visible)
                        {
                            pnlVoteCasted.Visible = true;
                        }
                        pnlGraphVoteCast.Visible = true; //This will only be set to true post casting of the vote
                        pnlCurrentUserComments.Visible = true;

                        lblCurrentCategoryDescription.Text = string.Join(",", votingRecord.AllCategories); 
                        lblCurrentOrgName.Text = votingRecord.OrgName;
                        lblCurrentQuestion.Text = votingRecord.QuestionText;
                        if (votingRecord.B_UserVote == "Yes")
                            pnlVotedYesImg.Visible = true;
                        else if (votingRecord.B_UserVote == "No")
                            pnlVotedNoImg.Visible = true;
                        GetPieChartData(); 

                    }
                    else if (votingRecord == null && _pollingWindow == PollingWindowEnum.Current)
                    {
                        //Retrive the Pending Polling Queue for the user
                        IEnumerable<UserVotingDetail> pendingVotingRecords = _rptService.GetPendingPollingQueue(HttpContext.Current.User.Identity.Name);
                        UserVotingDetail pendingVotingRecord = pendingVotingRecords.FirstOrDefault(r => r.QuestionID == Convert.ToInt32(_questionID));

                        pnlCurrent.Visible = true;

                        StringBuilder sb = new StringBuilder();
                        var discussions = _rptService.GetQuestionDiscussions(Convert.ToInt32(_questionID)).ToList();

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

                        pnlCurrentDiscussion.Height = 150;
                        lblCurrentDiscussion.Text = sb.ToString();
                        pnlGraphVoteCast.Visible = false;
                        pnlCurrentUserComments.Visible = true;

                        pnlToCastVote.Visible = true;
                        lblCurrentOrgName.Text = pendingVotingRecord.OrgName; 
                        lblCurrentQuestion.Text = pendingVotingRecord.QuestionText;

                        lblCurrentCategoryDescription.Text = String.Join(" , ", _rptService.GetVotingCategorybyQuestionID(Convert.ToInt32(_questionID)));
                       
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

        protected void btnSaveUserComments_Click(object sender, EventArgs e)
        {
            Discussion dis = new Discussion
            {
                UserID = HttpContext.Current.User.Identity.Name,
                QuestionID = Convert.ToInt32(_questionID),
                DiscussionText = txtUserComments.Text

            };

            _rptService.SaveQuestionDiscussion(dis);

            txtUserComments.Text = "";

            StringBuilder sb = new StringBuilder();
            var discussions = _rptService.GetQuestionDiscussions(Convert.ToInt32(_questionID)).ToList();

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

            lblCurrentDiscussion.Text = sb.ToString();

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
           PostVotingControlSetting("thumbsup");
           
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

            PostVotingControlSetting("thumbsdown");
           
        }


        protected void PostVotingControlSetting(string imgclicked)
        {
            pnlGraphVoteCast.Visible = true;
            GetPieChartData(); //Fill the chart

            imgThumsDown.Enabled = false;
            imgThumsUp.Enabled = false;

            if (imgclicked.Equals("thumbsup"))
            {
                imgThumsUp.Width = 150;
                imgThumsUp.Height = 150;
                imgThumsUp.ImageUrl = "http://cdn.mysitemyway.com/etc-mysitemyway/icons/legacy-previews/icons/3d-glossy-green-orbs-icons-business/103478-3d-glossy-green-orb-icon-business-thumbs-up1.png";
               
            }
            else if (imgclicked.Equals("thumbsdown"))
            {
                imgThumsDown.ImageUrl = "http://blog.2createawebsite.com/wp-content/uploads/2014/09/thumbsDown.png";
                imgThumsDown.Width = 150;
                imgThumsDown.Height = 150;
                
            }

            lblUserVote.Text = "You have successfully casted your vote!!!! Thank you";
            lblUserVote.BackColor = System.Drawing.Color.Aqua;
        }



        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static object[] GetPieChartData()
        {

            var userVotingQuestion = _rptService.GetUserVotingQuestionDetails(HttpContext.Current.User.Identity.Name, PollingWindowEnum.All)
                                       .FirstOrDefault(r => r.QuestionID == Convert.ToInt32(_questionID));

            if (userVotingQuestion != null)
            {

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
            else return  new object[0];


        }

        
    }
}