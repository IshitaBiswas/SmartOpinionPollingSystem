using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SOP.Services;
using SOP.Services.Interfaces;
using SOP.Common.Model;
using System.Web.Security;

namespace SmartOpinionPollingSystem.OrganizationPages
{
    public partial class AddNewQuestion : System.Web.UI.Page
    {
        IGenericServices _iGenServices;
        IOrganizationInfoService _iOrgServices;

        public AddNewQuestion()
        {
            _iGenServices = new GenericServices();
            _iOrgServices = new OrganizationInfoService();

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            hlnkOrgDashBoard.NavigateUrl = "OrgDashBoard";

            if(!IsPostBack)
            {
                lstTargetAudience.DataSource = _iOrgServices.GetOrgPollingQuestionCategories(HttpContext.Current.User.Identity.Name);
                lstTargetAudience.DataTextField =  "CategoryDescription";
                lstTargetAudience.DataValueField = "OrgVotingCategoryID";
                lstTargetAudience.DataBind();
            }
        }

        protected void Submit(object sender, EventArgs e)
        {
            try
            {
                VotingQuestionDetail orgqstndetail = new VotingQuestionDetail()
                {
                    OrgID = HttpContext.Current.User.Identity.Name,
                    QuestionText = txtQuestiontext.Text.Trim(),
                    //VotingStartDate = Convert.ToDateTime(txtPollingStartDate.Value.Trim()),
                    //VotingEndDate = Convert.ToDateTime(txtPollingEndDate.Value.Trim()),
                    VotingStartDate = DateTime.Parse(txtPollingStartDate.Value),
                    VotingEndDate = DateTime.Parse(txtPollingEndDate.Value),
                    VotedYes = 0,
                    VotedNo = 0,
                    MinVotingAge = txtTargetAudienceMinAge.Text.Trim() == "" ? (int?)null : Convert.ToInt32(txtTargetAudienceMinAge.Text.Trim()),
                    MaxVotingAge = txtTargetAudienceMaxAge.Text.Trim() == "" ? (int?)null : Convert.ToInt32(txtTargetAudienceMaxAge.Text.Trim())
                     
                };

                if (ddlTargetAudienceGender.SelectedValue != "-1")
                {
                    orgqstndetail.TargetAudienceGender = ddlTargetAudienceGender.SelectedItem.Value;
                }

                foreach (ListItem li in lstTargetAudience.Items)
                {
                    if (li.Selected)
                    {
                        orgqstndetail.OrgAddVotingCategoryIDs.Add(Convert.ToInt32(li.Value));
                    }
                }

                _iOrgServices.AddOrgPollingQuestionDetails(orgqstndetail);

                if(orgqstndetail.VotingStartDate > DateTime.Now)
                {
                    Response.Redirect("~/OrganizationPages/OrgPollingDetails.aspx?pollingwindow=future");

                }
                else if (orgqstndetail.VotingStartDate.ToShortDateString() == DateTime.Now.ToShortDateString())
                {
                    Response.Redirect("~/OrganizationPages/OrgPollingDetails.aspx?pollingwindow=current");
                }

            }
            catch (ApplicationException aex)
            {
               // ErrorMessage.Text = aex.Message;
                throw aex;
            }
            catch (Exception ex)
            {
              //  ErrorMessage.Text = "Registration  Failed";
                throw ex;
            }
        }

        protected void Reset(object sender, EventArgs e)
        {
            txtQuestiontext.Text = String.Empty;
            txtPollingStartDate.Value = String.Empty;
            txtPollingEndDate.Value = String.Empty;
            txtTargetAudienceMinAge.Text = String.Empty;
            txtTargetAudienceMaxAge.Text = String.Empty;
            ddlTargetAudienceGender.SelectedIndex = 0;
            lstTargetAudience.ClearSelection();


        }

        protected void Cancel(object sender, EventArgs e)
        {
            Response.Redirect("~/OrganizationPages/OrgDashboard.aspx");
        }
    }
}