using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Membership.OpenAuth;
using SOP.Common;
using SOP.Common.Model;
using SOP.Services.Interfaces;
using SOP.Services;


namespace SmartOpinionPollingSystem.Account
{
    public partial class UserRegister : Page
    {
        IUserInfoServices _iuiservices;
        IGenericServices _igenServices;
        public UserRegister()
        {
            _iuiservices = new UserInfoServices();
            _igenServices = new GenericServices();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                chkUserVotingCategory.DataSource = _igenServices.GetVotingCategories();
                chkUserVotingCategory.DataTextField = "CategoryDescription";
                chkUserVotingCategory.DataValueField = "VotingCategoryID";
                chkUserVotingCategory.DataBind();
            }
               
        }

        protected void RegisterButton_Click(object sender, EventArgs e)
        {
            try
            {
                User user = new User()
                {
                    UserID = txtUserID.Text,
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    Email = txtEmail.Text,
                    Age = Convert.ToInt32(txtAge.Text),
                    Gender = GenderRadioButtonList.SelectedItem.Value,
                    Occupation = txtOccupation.Text,
                    Phoneno = txtPhoneNumber.Text,
                    Password = txtPassword.Text
                };

                foreach (ListItem li in chkUserVotingCategory.Items)
                {
                    // If the listitem is selected
                    if (li.Selected)
                    {
                        // Add the listitem text to the list
                        user.UserVotingCategoryIDs.Add(Convert.ToInt32(li.Value));

                    }
                }

                _iuiservices.RegisterUser(user);

                FormsAuthentication.SetAuthCookie(user.UserID, createPersistentCookie: false);
                Response.Redirect("~/UserPages/UserDashboard.aspx");
                
            }
            catch (ApplicationException aex)
            {
                ErrorMessage.Text = aex.Message;
            }
            catch(Exception ex)
            {
                ErrorMessage.Text = "Cannot Register new User.";
            }
            

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}