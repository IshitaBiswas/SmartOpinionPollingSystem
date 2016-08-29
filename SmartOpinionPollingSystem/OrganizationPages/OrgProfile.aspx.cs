using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SOP.Services;
using SOP.Services.Interfaces;
using SOP.Common.Model;

namespace SmartOpinionPollingSystem.OrganizationPages
{
    public partial class OrgProfile : System.Web.UI.Page
    {
        IOrganizationInfoService _iOrgServices;
        IGenericServices _iGenServices;
        private static IEnumerable<VotingCategoryDesc> _orgVotingCategories;
        private static IEnumerable<VotingCategoryDesc> _allVotingCategories;

        public OrgProfile()
        {
            _iOrgServices = new OrganizationInfoService();
            _iGenServices = new GenericServices();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                OrgDashBoard.NavigateUrl = "OrgDashBoard";
                if (!Page.IsPostBack)
                {
                    Organization org = _iOrgServices.GetOrganization(HttpContext.Current.User.Identity.Name);

                    lblValueOrgName.Text = org.OrgName;
                    lblValueOrgWebsite.Text = org.OrgWebsite;
                    lblValueOrgCategory.Text = org.OrgCategory;

                    txtOrgName.Text = lblValueOrgName.Text;
                    txtOrgWebsite.Text = lblValueOrgWebsite.Text;
                    txtOrgCategory.Text = lblValueOrgCategory.Text;

                    lstOrgVotingCategory.DataSource = org.OrgVotingCategoryDescriptions;
                    lstOrgVotingCategory.DataTextField = "CategoryDescription";
                    lstOrgVotingCategory.DataValueField = "VotingCategoryID";
                    lstOrgVotingCategory.DataBind();


                    IEnumerable<VotingCategoryDesc> dataAll = _iGenServices.GetVotingCategories()
                                                        .Where(a => !org.OrgVotingCategoryDescriptions
                                                                     .Contains(a, new VotingCategoryDescComparer()));

                    lstAllVotingCategory.DataSource = dataAll;
                    lstAllVotingCategory.DataTextField = "CategoryDescription";
                    lstAllVotingCategory.DataValueField = "VotingCategoryID";
                    lstAllVotingCategory.DataBind();


                    //Set the static fields
                    _orgVotingCategories = org.OrgVotingCategoryDescriptions;
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

            lstOrgVotingCategory.DataSource = _orgVotingCategories;
            lstOrgVotingCategory.DataTextField = "CategoryDescription";
            lstOrgVotingCategory.DataValueField = "VotingCategoryID";
            lstOrgVotingCategory.DataBind();

            lstAllVotingCategory.DataSource = _allVotingCategories;
            lstAllVotingCategory.DataTextField = "CategoryDescription";
            lstAllVotingCategory.DataValueField = "VotingCategoryID";
            lstAllVotingCategory.DataBind();
        }


        private void SetControlVisibility(bool visibility)
        {
            lblValueOrgName.Visible = visibility;
            lblValueOrgWebsite.Visible = visibility;
            lblValueOrgCategory.Visible = visibility;

            txtOrgName.Visible = !visibility;
            txtOrgWebsite.Visible = !visibility;
            txtOrgCategory.Visible = !visibility;

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
                    lstOrgVotingCategory.Items.Add(lstAllVotingCategory.Items[i]);
                    lstAllVotingCategory.Items.Remove(lstAllVotingCategory.Items[i]);
                }
            }
        }

        protected void btnDoubleRight_Click(object sender, EventArgs e)
        {
            while (lstOrgVotingCategory.Items.Count != 0)
            {
                for (int i = 0; i < lstOrgVotingCategory.Items.Count; i++)
                {
                    lstAllVotingCategory.Items.Add(lstOrgVotingCategory.Items[i]);
                    lstOrgVotingCategory.Items.Remove(lstOrgVotingCategory.Items[i]);
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


                    if (!lstOrgVotingCategory.Items.Contains(li))
                    {
                        lstOrgVotingCategory.Items.Add(li);
                    }
                    lstAllVotingCategory.Items.Remove(li);
                }
                lstOrgVotingCategory.SelectedIndex = -1;
            }


        }

        protected void btnRight_Click(object sender, EventArgs e)
        {
            List<VotingCategoryDesc> arraylist1 = new List<VotingCategoryDesc>();

            if (lstOrgVotingCategory.SelectedIndex >= 0)
            {
                for (int i = 0; i < lstOrgVotingCategory.Items.Count; i++)
                {
                    if (lstOrgVotingCategory.Items[i].Selected)
                    {

                        VotingCategoryDesc d = new VotingCategoryDesc
                        {
                            VotingCategoryID = Convert.ToInt32(lstOrgVotingCategory.Items[i].Value),
                            CategoryDescription = lstOrgVotingCategory.Items[i].Text
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
                    lstOrgVotingCategory.Items.Remove(li);
                }
                lstAllVotingCategory.SelectedIndex = -1;
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Organization org = new Organization()
                {
                    OrgID = HttpContext.Current.User.Identity.Name,
                    OrgName = txtOrgName.Text,
                    OrgCategory = txtOrgCategory.Text,
                    OrgWebsite = txtOrgWebsite.Text,
                };

                foreach (ListItem li in lstOrgVotingCategory.Items)
                {

                    // Add the listitem text to the list
                    org.OrgVotingCategoryIDs.Add(Convert.ToInt32(li.Value));

                }

                _iOrgServices.EditOrgProfile(org);

                Response.Redirect("~/OrganizationPages/OrgProfile.aspx");

            }
            catch (ApplicationException aex)
            {
                ErrorMessage.Text = aex.Message;
            }
            catch (Exception ex)
            {
                ErrorMessage.Text = "Cannot Save updated information of organization";
            }



        }
    }
}