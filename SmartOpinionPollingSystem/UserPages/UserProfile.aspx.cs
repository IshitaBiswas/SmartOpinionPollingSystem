using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;
using SOP.Services.Interfaces;
using SOP.Services;
using SOP.Common.Model;

namespace SmartOpinionPollingSystem.UserPages
{

    public partial class UserProfile : System.Web.UI.Page
    {
        IUserInfoServices _iUserServices;
        IGenericServices _iGenServices;
        private static IEnumerable<VotingCategoryDesc> _userVotingCategories;
        private static IEnumerable<VotingCategoryDesc> _allVotingCategories;

        public UserProfile()
        {
            _iUserServices = new UserInfoServices();
            _iGenServices = new GenericServices();
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.IsAuthenticated)
            {
                UserDashBoard.NavigateUrl = "UserDashBoard";

                if (!Page.IsPostBack)
                {
                    User user = _iUserServices.GetUser(HttpContext.Current.User.Identity.Name);

                    lblValueFirstName.Text = user.FirstName;
                    lblValueLastName.Text = user.LastName;
                    lblValueAge.Text = Convert.ToString(user.Age);
                    lblValueEmail.Text = user.Email;
                    lblValuePhoneNumber.Text = user.Phoneno;
                    lblValueOccupation.Text = user.Occupation;

                    txtFirstName.Text = lblValueFirstName.Text;
                    txtLastName.Text = lblValueLastName.Text;
                    txtEmail.Text = lblValueEmail.Text;
                    txtAge.Text = lblValueAge.Text;
                    txtOccupation.Text = lblValueOccupation.Text;
                    txtPhoneNumber.Text = lblValuePhoneNumber.Text;

                    lstUserVotingCategory.DataSource = user.UserVotingCategoryDescriptions; 
                    lstUserVotingCategory.DataTextField = "CategoryDescription";
                    lstUserVotingCategory.DataValueField = "VotingCategoryID";
                    lstUserVotingCategory.DataBind();


                    IEnumerable<VotingCategoryDesc> dataAll = _iGenServices.GetVotingCategories()
                                                        .Where(a => !user.UserVotingCategoryDescriptions
                                                                     .Contains(a, new VotingCategoryDescComparer()));

                    lstAllVotingCategory.DataSource = dataAll;
                    lstAllVotingCategory.DataTextField = "CategoryDescription";
                    lstAllVotingCategory.DataValueField = "VotingCategoryID";
                    lstAllVotingCategory.DataBind();


                    //Set the static fields
                    _userVotingCategories = user.UserVotingCategoryDescriptions;
                    _allVotingCategories = dataAll;



                }
            }

        }

        protected void btnEditProfile_Click(object sender, EventArgs e)
        {
            SetControlVisibility(false);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            SetControlVisibility(true);

            lstUserVotingCategory.DataSource = _userVotingCategories;
            lstUserVotingCategory.DataTextField = "CategoryDescription";
            lstUserVotingCategory.DataValueField = "VotingCategoryID";
            lstUserVotingCategory.DataBind();

            lstAllVotingCategory.DataSource = _allVotingCategories;
            lstAllVotingCategory.DataTextField = "CategoryDescription";
            lstAllVotingCategory.DataValueField = "VotingCategoryID";
            lstAllVotingCategory.DataBind();
        }


        private void SetControlVisibility(bool visibility)
        {
            lblValueFirstName.Visible = visibility;
            lblValueLastName.Visible = visibility;
            lblValueEmail.Visible = visibility;
            lblValueAge.Visible = visibility;
            lblValueOccupation.Visible = visibility;
            lblValuePhoneNumber.Visible = visibility;
            btnRight.Visible = !visibility;

            txtFirstName.Visible = !visibility;
            txtLastName.Visible = !visibility;
            txtEmail.Visible = !visibility;
            txtAge.Visible = !visibility;
            txtOccupation.Visible = !visibility;
            txtPhoneNumber.Visible = !visibility;

            btnRight.Visible = !visibility;
            btnDoubleRight.Visible = !visibility;
            btnLeft.Visible = !visibility;
            btnDoubleLeft.Visible = !visibility;
            lstAllVotingCategory.Visible = !visibility;

            btnEditProfile.Visible = visibility;
            btnCancel.Visible = !visibility;
            btnSave.Visible = !visibility;
        }

        protected void btnDoubleLeft_Click(object sender, EventArgs e)
        {
            while (lstAllVotingCategory.Items.Count != 0)
            {
                for (int i = 0; i < lstAllVotingCategory.Items.Count; i++)
                {
                    lstUserVotingCategory.Items.Add(lstAllVotingCategory.Items[i]);
                    lstAllVotingCategory.Items.Remove(lstAllVotingCategory.Items[i]);
                }
            }
        }

        protected void btnDoubleRight_Click(object sender, EventArgs e)
        {
            while (lstUserVotingCategory.Items.Count != 0)
            {
                for (int i = 0; i < lstUserVotingCategory.Items.Count; i++)
                {
                    lstAllVotingCategory.Items.Add(lstUserVotingCategory.Items[i]);
                    lstUserVotingCategory.Items.Remove(lstUserVotingCategory.Items[i]);
                }
            }
        }

        protected void btnLeft_Click(object sender, EventArgs e)
        {

            List<VotingCategoryDesc> arraylist1 = new List<VotingCategoryDesc>();

            if (lstAllVotingCategory.SelectedIndex >= 0)
            {
                for (int i = 0; i < lstAllVotingCategory.Items.Count; i++)
                {
                    if (lstAllVotingCategory.Items[i].Selected)
                    {

                        VotingCategoryDesc d = new VotingCategoryDesc
                        {
                            VotingCategoryID = Convert.ToInt32(lstAllVotingCategory.Items[i].Value),
                            CategoryDescription = lstAllVotingCategory.Items[i].Text
                        };


                        if (!arraylist1.Contains(d))
                        {
                            arraylist1.Add(d);
                        }
                    }
                }
                for (int i = 0; i < arraylist1.Count; i++)
                {
                    VotingCategoryDesc d = arraylist1[i];
                    ListItem li = new ListItem
                    {
                        Text = d.CategoryDescription,
                        Value = Convert.ToString(d.VotingCategoryID)
                    };


                    if (!lstUserVotingCategory.Items.Contains(li))
                    {
                        lstUserVotingCategory.Items.Add(li);
                    }
                    lstAllVotingCategory.Items.Remove(li);
                }
                lstUserVotingCategory.SelectedIndex = -1;
            }


        }

        protected void btnRight_Click(object sender, EventArgs e)
        {
            List<VotingCategoryDesc> arraylist1 = new List<VotingCategoryDesc>();

            if (lstUserVotingCategory.SelectedIndex >= 0)
            {
                for (int i = 0; i < lstUserVotingCategory.Items.Count; i++)
                {
                    if (lstUserVotingCategory.Items[i].Selected)
                    {

                        VotingCategoryDesc d = new VotingCategoryDesc
                        {
                            VotingCategoryID = Convert.ToInt32( lstUserVotingCategory.Items[i].Value),
                            CategoryDescription = lstUserVotingCategory.Items[i].Text
                        };


                        if (!arraylist1.Contains(d))
                        {
                            arraylist1.Add(d);
                        }
                    }
                }
                for (int i = 0; i < arraylist1.Count; i++)
                {
                  VotingCategoryDesc d = arraylist1[i];
                  ListItem li = new ListItem
                  {
                      Text = d.CategoryDescription,
                      Value = Convert.ToString(d.VotingCategoryID)
                  };

                   
                    if (!lstAllVotingCategory.Items.Contains(li))
                    {
                        lstAllVotingCategory.Items.Add(li);
                    }
                    lstUserVotingCategory.Items.Remove(li);
                }
                lstAllVotingCategory.SelectedIndex = -1;
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                User user = new User()
                {
                    UserID = HttpContext.Current.User.Identity.Name,
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    Email = txtEmail.Text,
                    Age = Convert.ToInt32(txtAge.Text),
                    Occupation = txtOccupation.Text,
                    Phoneno = txtPhoneNumber.Text,
                };

                foreach (ListItem li in lstUserVotingCategory.Items)
                {
                    
                        // Add the listitem text to the list
                        user.UserVotingCategoryIDs.Add(Convert.ToInt32(li.Value));

                }

                _iUserServices.EditUserProfile(user);

                Response.Redirect("~/UserPages/UserProfile.aspx");

            }
            catch (ApplicationException aex)
            {
                ErrorMessage.Text = aex.Message;
            }
            catch (Exception ex)
            {
                ErrorMessage.Text = "Cannot Save updated information of user.";
            }
            
          
                
        }

    }
}