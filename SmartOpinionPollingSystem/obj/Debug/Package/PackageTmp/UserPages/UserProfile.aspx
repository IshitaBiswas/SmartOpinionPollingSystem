<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="SmartOpinionPollingSystem.UserPages.UserProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        #body
        {
            background-image: url("http://voshsoutheast.org/wp-content/uploads/2014/05/Background-Texture-Images4.jpg");
        }
        .whitelabel
        {
            width: 252px;
            height:25px;
            background-color : #00ccff;
            padding-top : 8px;
            display : inline-block
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    
    <p style="float:right;padding-right:15px">
            <asp:HyperLink runat="server" ID="UserDashBoard" ViewStateMode="Disabled" ForeColor="Blue">Return to User DashBoard</asp:HyperLink>
    </p><br /><br /><br />
    
    <hgroup class="title">
        <h1><%: Title %>User Details</h1>
    </hgroup>

                    <p class="validation-summary-errors">
                        <asp:Literal runat="server" ID="ErrorMessage" />
                    </p>

                    <fieldset>
                        <legend>View User Profile</legend>
                        <ul style="list-style-type: none;">
                            <li>
                                <asp:Label runat="server" ID="lblKeyFirstName" Font-Bold="True">First Name</asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label class="whitelabel" runat="server" ID="lblValueFirstName"></asp:Label>
                                &nbsp;
                                <asp:TextBox runat="server" ID="txtFirstName" Visible="false"/>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFirstName"
                                    CssClass="field-validation-error" ErrorMessage="The First name field is required." />
                            </li>

                            <br /><br />

                            <li>
                                <asp:Label runat="server" ID="lblKeyLastName" Font-Bold="True">Last Name</asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label class="whitelabel" runat="server" ID="lblValueLastName"></asp:Label>
                                &nbsp;&nbsp;
                                <asp:TextBox runat="server" ID="txtLastName" Visible="false"/>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtLastName"
                                    CssClass="field-validation-error" ErrorMessage="The Last Name  field is required." />
                            </li>

                            <br /><br />

                            <li>
                                <asp:Label runat="server" ID="lblKeyEmail" Font-Bold="True">Email address</asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label class="whitelabel" runat="server" ID="lblValueEmail">Email address</asp:Label>
                                &nbsp;
                                <asp:TextBox runat="server" ID="txtEmail" Visible="false"/>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmail"
                                    CssClass="field-validation-error" ErrorMessage="The email address field is required." />
                            </li>

                            <br /><br />

                            <li>
                                <asp:Label runat="server" ID="lblKeyPhoneNumber" Font-Bold="True">Phone Number</asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label class="whitelabel" runat="server" ID="lblValuePhoneNumber">Phone Number</asp:Label>
                                &nbsp;
                                <asp:TextBox runat="server" ID="txtPhoneNumber" Visible="false"/>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPhoneNumber"
                                    CssClass="field-validation-error" ErrorMessage="The Phone Number field is required." />
                            </li>

                            <br /><br />

                            <li>
                                <asp:Label runat="server" ID="lblKeyAge" Font-Bold="True">Age(in years)</asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label class="whitelabel" runat="server" ID="lblValueAge">Age(in years)</asp:Label>
                                &nbsp;
                                <asp:TextBox runat="server" ID="txtAge" Visible="false"/>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtAge"
                                    CssClass="field-validation-error" ErrorMessage="The Age field is required." />
                            </li>

                            <br /><br />

                            <li>
                                <asp:Label runat="server" ID="lblKeyOccupation" Font-Bold="True">Occupation</asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label class="whitelabel" runat="server" ID="lblValueOccupation">Occupation</asp:Label>
                                &nbsp;&nbsp;
                                <asp:TextBox runat="server" ID="txtOccupation" Visible="false"/>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtOccupation"
                                    CssClass="field-validation-error" ErrorMessage="The Occupation field is required." />
                            </li>

                            <br /><br />
                            
                            <li>
                            <asp:Label runat="server" ID="lblVotingCategory" Font-Bold="True">Voting Category(s) </asp:Label>
                            
                                <%--<asp:RequiredFieldValidator runat="server" ControlToValidate="lstUserVotingCategory" CssClass="field-validation-error" 
                                    ErrorMessage="Voting Category is required." />--%>
                                <table style="display:inline">
                                    <tr>
                                        <td>
                                            <asp:ListBox class="whitelabel" ID="lstUserVotingCategory" runat="server" Height="152px" Width="131px" SelectionMode="Multiple"></asp:ListBox>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnRight" runat="server" Text=">" width="30px" height="30px" OnClick="btnRight_Click" Visible ="false" /><br />
                                            <asp:Button ID="btnDoubleRight" runat="server" Text=">>" width="30px" height="30px" OnClick="btnDoubleRight_Click" Visible ="false"/><br />
                                            <asp:Button ID="btnLeft" runat="server" Text="<" width="30px" height="30px" OnClick="btnLeft_Click" Visible ="false"/><br />
                                            <asp:Button ID="btnDoubleLeft" runat="server" Text="<<" width="30px" height="30px" OnClick="btnDoubleLeft_Click" Visible ="false"/><br />
                                        </td>
                                        <td>
                                            <asp:ListBox class="whitelabel" ID="lstAllVotingCategory" runat="server" Height="152px" Width="131px" SelectionMode="Multiple" Visible ="false"></asp:ListBox>
                                        </td>
                                    </tr>
                                </table>
                            </li>

                            <br />
                            <%--<li>
                                <asp:Label runat="server" ID="Label1">User ID</asp:Label>
                                <asp:Label runat="server" ID="Label2">User ID</asp:Label>
                                <asp:TextBox runat="server" ID="TextBox1" Visible="false"/>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtUserID"
                                    CssClass="field-validation-error" ErrorMessage="The User ID field is required." />
                            </li>--%>
                            <%--<li>
                                <asp:Label runat="server" AssociatedControlID="txtFirstName">First Name</asp:Label>
                                <asp:TextBox runat="server" ID="txtFirstName" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFirstName"
                                    CssClass="field-validation-error" ErrorMessage="The First name field is required." />
                            </li>
                             <li>
                                <asp:Label runat="server" AssociatedControlID="txtLastName">Last Name</asp:Label>
                                <asp:TextBox runat="server" ID="txtLastName" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtLastName"
                                    CssClass="field-validation-error" ErrorMessage="The Last Name  field is required." />
                            </li>
                            <li>
                                <asp:Label runat="server" AssociatedControlID="txtEmail">Email address</asp:Label>
                                <asp:TextBox runat="server" ID="txtEmail" TextMode="Email" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmail"
                                    CssClass="field-validation-error" ErrorMessage="The email address field is required." />
                            </li>
                            <li>
                                <asp:Label runat="server" AssociatedControlID="txtPhoneNumber">Phone Number</asp:Label>
                                <asp:TextBox runat="server" ID="txtPhoneNumber" TextMode="Phone" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPhoneNumber"
                                    CssClass="field-validation-error" ErrorMessage="The Phone Number field is required." />
                            </li>
                            <li>
                                <asp:Label runat="server" AssociatedControlID="txtAge">Age(in years)</asp:Label>
                                <asp:TextBox runat="server" ID="txtAge"  />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtAge"
                                    CssClass="field-validation-error" ErrorMessage="The Age field is required." />
                            </li>
                            <li>
                                <asp:Label runat="server" AssociatedControlID="txtOccupation">Occupation</asp:Label>
                                <asp:TextBox runat="server" ID="txtOccupation"  />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtOccupation"
                                    CssClass="field-validation-error" ErrorMessage="The Occupation field is required." />
                            </li>
                            <li>
                              <asp:Label runat="server" AssociatedControlID="GenderRadioButtonList">Select Gender</asp:Label>
                              <asp:RadioButtonList ID="GenderRadioButtonList" runat="server" 
                                RepeatDirection="Horizontal">
                                <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                                <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                              </asp:RadioButtonList>
                              <asp:RequiredFieldValidator runat="server" ControlToValidate="GenderRadioButtonList"
                                  CssClass="field-validation-error" ErrorMessage="The Occupation field is required." />
                            </li>
                             <li>
                                <asp:Label runat="server" AssociatedControlID="chkUserVotingCategory">User Voting Category</asp:Label>
                                   <asp:CheckBoxList ID="chkUserVotingCategory" runat="server" RepeatDirection="Horizontal"></asp:CheckBoxList>
                            </li>
                          
                            <li>
                                <asp:Label runat="server" AssociatedControlID="txtPassword">Password</asp:Label>
                                <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPassword"
                                    CssClass="field-validation-error" ErrorMessage="The password field is required." />
                            </li>
                            <li>
                                <asp:Label runat="server" AssociatedControlID="txtConfirmPassword">Confirm password</asp:Label>
                                <asp:TextBox runat="server" ID="txtConfirmPassword" TextMode="Password" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtConfirmPassword"
                                     CssClass="field-validation-error" Display="Dynamic" ErrorMessage="The confirm password field is required." />
                                <asp:CompareValidator runat="server" ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword"
                                     CssClass="field-validation-error" Display="Dynamic" ErrorMessage="The password and confirmation password do not match." />
                            </li>--%>
                                
                            </ul>
                        <%--<asp:Button runat="server" ID="btnCancel"  Text="Cancel" OnClick="btnCancel_Click"  />--%>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button runat="server" ID="btnEditProfile" Text="Edit Profile" OnClick="btnEditProfile_Click" Width="121px" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" Visible="false"/>
                        <asp:Button ID="btnSave" runat="server" Text="Save Changes" Visible="false" Width="133px" OnClick="btnSave_Click"/>
                    </fieldset>

</asp:Content>
