using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SOP.Common.Model;
using SOP.Services.Interfaces;
using SOP.Services;
using System.Web.Security;
using SOP.Common;

namespace SmartOpinionPollingSystem.Account
{
    
    public partial class Login : Page
    {
        IOrganizationInfoService _orgInfoService;
        IGenericServices _iGenService;

        public Login()
        {
            _orgInfoService = new OrganizationInfoService();
            _iGenService = new GenericServices();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            UserRegisterHyperLink.NavigateUrl = "UserRegistration";
            OrgRegisterHyperLink.NavigateUrl = "OrgRegistration";
          

            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            if (!String.IsNullOrEmpty(returnUrl))
            {
                UserRegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
                OrgRegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            }
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            try
            {
                GenericLogin genlogin = new GenericLogin()
                {
                    LoginID = txtID.Text.Trim(),
                    Password = Password.Text.Trim()
                };

                if (_iGenService.ValidateLogin(genlogin) == LoginTypeEnum.Organization) 
                {
                    FormsAuthentication.SetAuthCookie(genlogin.LoginID, RememberMe.Checked);
                    Response.Redirect("~/OrganizationPages/OrgDashboard.aspx");
                }
                else if (_iGenService.ValidateLogin(genlogin) == LoginTypeEnum.User)
                {
                    FormsAuthentication.SetAuthCookie(genlogin.LoginID, RememberMe.Checked);
                    Response.Redirect("~/UserPages/UserDashboard.aspx");
                }

                else if (_iGenService.ValidateLogin(genlogin) == LoginTypeEnum.Anonymous)
                    FailureText.Text = "Invalid credentials. Please try again.";
            }
            
            catch (Exception ex)
            {
                FailureText.Text = "Sorry. Unable to login";
            }
            
        }
    }
}