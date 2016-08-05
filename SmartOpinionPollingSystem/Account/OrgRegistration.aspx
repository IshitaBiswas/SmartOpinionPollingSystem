<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrgRegistration.aspx.cs" Inherits="SmartOpinionPollingSystem.Account.OrgRegistration" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Title %> Organization user  - Use the form below to create a new account.</h1>
    </hgroup>

                    <p class="validation-summary-errors">
                        <asp:Literal runat="server" ID="ErrorMessage" />
                    </p>

                    <fieldset>
                        <legend>Organization Registration Form</legend>
                        <ul style="list-style-type: none;">
                             <li>
                                <asp:Label runat="server" AssociatedControlID="txtOrgID">Org ID</asp:Label>
                                <asp:TextBox runat="server" ID="txtOrgID" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtOrgID"
                                    CssClass="field-validation-error" ErrorMessage="The OrgID field is required." />
                            </li>
                            <li>
                                <asp:Label runat="server" AssociatedControlID="txtOrganizationName">Organization Name</asp:Label>
                                <asp:TextBox runat="server" ID="txtOrganizationName" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtOrganizationName"
                                    CssClass="field-validation-error" ErrorMessage="The Organization Name field is required." />
                            </li>
                            <li>
                                <asp:Label runat="server" AssociatedControlID="txtOrgWebSite">Organization WebSite</asp:Label>
                                <asp:TextBox runat="server" ID="txtOrgWebSite"  />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtOrgWebSite"
                                    CssClass="field-validation-error" ErrorMessage="The Organization Website field is required." />
                            </li>
                            <li>
                                <asp:Label runat="server" AssociatedControlID="txtOrgCategory">Organization Category</asp:Label>
                                <asp:TextBox runat="server" ID="txtOrgCategory"  />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtOrgCategory"
                                    CssClass="field-validation-error" ErrorMessage="The Organization CAtegory field is required." />
                            </li>
                             <li>
                                <asp:Label runat="server" AssociatedControlID="chkOrgVotingCategory">Organization Voting Category</asp:Label>
                                   <asp:CheckBoxList ID="chkOrgVotingCategory" runat="server" RepeatDirection="Horizontal"></asp:CheckBoxList>
                            </li>

                            <li>
                                <asp:Label runat="server" AssociatedControlID="txtOrgPassword">Organization Password</asp:Label>
                                <asp:TextBox runat="server" ID="txtOrgPassword" TextMode="Password" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtOrgPassword"
                                    CssClass="field-validation-error" ErrorMessage="The password field is required." />
                            </li>
                            <li>
                                <asp:Label runat="server" AssociatedControlID="txtConfirmOrgPassword">Confirm password</asp:Label>
                                <asp:TextBox runat="server" ID="txtConfirmOrgPassword" TextMode="Password" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtConfirmOrgPassword"
                                     CssClass="field-validation-error" Display="Dynamic" ErrorMessage="The confirm password field is required." />
                                <asp:CompareValidator runat="server" ControlToCompare="txtOrgPassword" ControlToValidate="txtConfirmOrgPassword"
                                     CssClass="field-validation-error" Display="Dynamic" ErrorMessage="The password and confirmation password do not match." />
                            </li>
                        </ul>
                        <asp:Button runat="server" OnClick="RegisterButton_Click" Text="Register" />
                        <asp:Button runat="server" ID="btnCancel"  Text="Cancel Org Registration" OnClick="btnCancel_Click"  />
                    </fieldset>
 
</asp:Content>