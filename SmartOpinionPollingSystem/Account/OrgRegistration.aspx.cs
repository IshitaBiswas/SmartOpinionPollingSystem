using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SOP.Common.Model;
using SOP.Services;
using SOP.Services.Interfaces;
using System.Web.Security;
using SOP.Common;
namespace SmartOpinionPollingSystem.Account
{
    public partial class OrgRegistration : System.Web.UI.Page
    {
        
        IOrganizationInfoService _orgInfoService;
        IGenericServices _igenServices;

        public OrgRegistration()
        {
            _orgInfoService = new OrganizationInfoService();
            _igenServices = new GenericServices();

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                chkOrgVotingCategory.DataSource = _igenServices.ChooseOrgVotingCategories();
                chkOrgVotingCategory.DataTextField = "CategoryDescription";
                chkOrgVotingCategory.DataValueField = "VotingCategoryID";
                chkOrgVotingCategory.DataBind();
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void RegisterButton_Click(object sender, EventArgs e)
        {
            try
            {
                Organization org = new Organization()
                {
                    OrgID = txtOrgID.Text.Trim(),
                    OrgName = txtOrganizationName.Text.Trim(),
                    OrgCategory = ddlOrgCategory.SelectedValue.Trim(),
                    OrgWebsite = txtOrgWebSite.Text.Trim(),
                    OrgRegPassword = txtOrgPassword.Text.Trim()
                };

                foreach (ListItem li in chkOrgVotingCategory.Items)
                {
                    if (li.Selected)
                    {
                        org.OrgVotingCategoryIDs.Add(Convert.ToInt32(li.Value));
                    }
                }

                _orgInfoService.AddOrgDetails(org);

                Session["LoginType"] = LoginTypeEnum.Organization;

                FormsAuthentication.SetAuthCookie(org.OrgID, createPersistentCookie: false);
                Response.Redirect("~/OrganizationPages/OrgDashboard.aspx");
            }
            catch (ApplicationException aex)
            {
                ErrorMessage.Text = aex.Message;
            }
            catch (Exception ex)
            {
                ErrorMessage.Text = "Registration  Failed";
            }
            

        }
    }
}