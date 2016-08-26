<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrgProfile.aspx.cs" Inherits="SmartOpinionPollingSystem.OrganizationPages.OrgProfile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        #body
        {
            background-image: url("http://www.myfreetextures.com/wp-content/uploads/2011/06/illust18.jpg");
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
    <hgroup class="title">
        <h1><%: Title %>Organization Profile</h1>
    </hgroup>

                    <p class="validation-summary-errors">
                        <asp:Literal runat="server" ID="ErrorMessage" />
                    </p>

                    <fieldset>
                        <legend>View Organization Profile</legend>
                        <ul style="list-style-type: none;">
                            <li>
                                <asp:Label runat="server" ID="lblKeyOrgName" Font-Bold="True">Organization Name</asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label class="whitelabel" runat="server" ID="lblValueOrgName"></asp:Label>
                                <asp:TextBox runat="server" ID="txtOrgName" Visible="false"/>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtOrgName"
                                    CssClass="field-validation-error" ErrorMessage="Organization Name is required." />
                            </li>

                            <br /><br />

                            <li>
                                <asp:Label runat="server" ID="lblKeyOrgWebsite" Font-Bold="True">Organization WebSite</asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label class="whitelabel" runat="server" ID="lblValueOrgWebsite"></asp:Label>
                                <asp:TextBox runat="server" ID="txtOrgWebsite" Visible="false"/>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtOrgWebsite"
                                    CssClass="field-validation-error" ErrorMessage="Organization Website is required." />
                            </li>

                            <br /><br />

                            <li>
                                <asp:Label runat="server" ID="lblKeyOrgCategory" Font-Bold="True">Organization Category</asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label class="whitelabel" runat="server" ID="lblValueOrgCategory"></asp:Label>
                                <asp:TextBox runat="server" ID="txtOrgCategory" Visible="false"/>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtOrgCategory"
                                    CssClass="field-validation-error" ErrorMessage="Organization Category is required." />
                            </li>

                            <br /><br />

                            
                            <li>
                            <asp:Label runat="server" ID="lblOrgVotingCategory" Font-Bold="True">Target Audience Domain </asp:Label>
                            
                                &nbsp;
                            
                                <%--<asp:RequiredFieldValidator runat="server" ControlToValidate="lstUserVotingCategory" CssClass="field-validation-error" 
                                    ErrorMessage="Voting Category is required." />--%>
                                <table style="display:inline">
                                    <tr>
                                        <td>
                                            <asp:ListBox class="whitelabel" ID="lstOrgVotingCategory" runat="server" Height="152px" Width="131px" SelectionMode="Multiple"></asp:ListBox>
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
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button runat="server" ID="btnEditProfile" Text="Edit Profile" OnClick="btnEditProfile_Click" Width="121px" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" Visible="false"/>
                        <asp:Button ID="btnSave" runat="server" Text="Save Changes" Visible="false" Width="133px" OnClick="btnSave_Click"/>
                    </fieldset>
                        </ul>

</asp:Content>
