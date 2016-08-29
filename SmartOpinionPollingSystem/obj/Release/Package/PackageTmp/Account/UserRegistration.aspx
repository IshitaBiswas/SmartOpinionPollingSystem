<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserRegistration.aspx.cs" Inherits="SmartOpinionPollingSystem.Account.UserRegister" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <style>
        #body
        {
            background-image: url("http://voshsoutheast.org/wp-content/uploads/2014/05/Background-Texture-Images4.jpg");
        }
    </style>
    <hgroup class="title">
        <h1><%: Title %>.</h1>
        <h2>Use the form below to create a new account.</h2>
    </hgroup>

                    <p class="validation-summary-errors">
                        <asp:Literal runat="server" ID="ErrorMessage" />
                    </p>

                    <fieldset>
                        <legend>User Registration Form</legend>
                        <ul style="list-style-type: none;">
                            <li>
                                <asp:Label runat="server" AssociatedControlID="txtUserID">User ID</asp:Label>
                                <asp:TextBox runat="server" ID="txtUserID" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtUserID"
                                    CssClass="field-validation-error" ErrorMessage="The User ID field is required." />
                            </li>
                            <li>
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
                            </li>
                        </ul>
                        <asp:Button runat="server" OnClick="RegisterButton_Click" Text="Register" />
                        <asp:Button runat="server" ID="btnCancel"  Text="Cancel" OnClick="btnCancel_Click"  />
                    </fieldset>
 
</asp:Content>