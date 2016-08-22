<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddNewQuestion.aspx.cs" Inherits="SmartOpinionPollingSystem.OrganizationPages.AddNewQuestion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">


<link rel="stylesheet" href="//code.jquery.com/ui/1.12.0/themes/base/jquery-ui.css">
<link rel="stylesheet" href="/resources/demos/style.css">
<script src="https://code.jquery.com/jquery-1.12.4.js"></script>
<script src="https://code.jquery.com/ui/1.12.0/jquery-ui.js"></script>


<script type="text/javascript">

    $(function () {
        $("#<%= txtPollingEndDate.ClientID  %>").datepicker({
            dateFormat: 'mm/dd/yy',
            changeMonth: true,
            changeYear: true,
            minDate: new Date()
        }).datepicker("setDate", new Date());
        $("#<%= txtPollingStartDate.ClientID  %>").datepicker({
            dateFormat: 'mm/dd/yy',
            changeMonth: true,
            changeYear: true,
            onSelect: function () {
                $("#<%= txtPollingEndDate.ClientID  %>").datepicker('option', 'minDate', $("#<%= txtPollingStartDate.ClientID  %>").datepicker("getDate"));
            }
        }).datepicker("setDate", new Date());
    });
</script>

<style>
table {
    border-collapse: collapse;
    width: 100%;
}

th, td {
    padding: 8px;
    text-align: left;
    border-bottom: 1px solid #ddd;
}

tr:hover{background-color:#f5f5f5}
    .auto-style1 {
        width: 391px;
    }
tr .age{

          }
    
</style>

<h2>Add A New Polling Question</h2>
<p>Participating organizations need to fill up this form to post a question for polling.</p>

    <table>
        <tr>
            <td class="auto-style1">
                <asp:Label AssociatedControlID="lstTargetAudience" runat="server" Text="Select Target Audience"></asp:Label>
            </td>
            <td>&nbsp;&nbsp;&nbsp;
                <asp:ListBox ID="lstTargetAudience" runat="server" Height="52px" Width="131px" SelectionMode="Multiple"></asp:ListBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="lstTargetAudience" CssClass="field-validation-error" ErrorMessage="Target Audience is required." />
            </td>
        </tr>
        <tr>
            <td class="auto-style1">
                <asp:Label AssociatedControlID="txtQuestiontext" runat="server" Text="Question Text"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtQuestiontext" runat="server" Height="70px" TextMode="MultiLine" Width="458px"></asp:TextBox>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtQuestiontext" CssClass="field-validation-error" ErrorMessage="Question Text is required." />
            </td>
        </tr>
        <tr>
            <td class="auto-style1">
                <label for="txtPollingStartDate">Polling Start Date </label>
            </td>
            <td>
                <input type="text" ID="txtPollingStartDate" runat="server" Width="131px">
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPollingStartDate" CssClass="field-validation-error" ErrorMessage="Polling Start Date is required." />
            </td>
        </tr>
        <tr>
            <td class="auto-style1">
                <label for="txtPollingEndDate">Polling End Date </label>
            </td>
            <td>
                <input type="text" id="txtPollingEndDate" runat="server">
                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPollingEndDate" CssClass="field-validation-error" ErrorMessage="Polling End Date is required." />
            </td>
        </tr>
        <tr class ="age">
            <td class="auto-style1" rowspan ="2">
                <label for="txtPollingEndDate">Add Age for Target Voting Audience (optional) </label>
            </td>
            <td class="auto-style1" >
                <asp:Label AssociatedControlID="txtTargetAudienceMinAge" runat="server" Text="Minimum Age" Width="148px"></asp:Label>
                <asp:TextBox ID="txtTargetAudienceMaxAge" runat="server" Width="50px"></asp:TextBox>
                
            </td>
        </tr>
        <tr class ="age">
            <td>
                <asp:Label AssociatedControlID="txtTargetAudienceMinAge" runat="server" Text="Maximum Age" ID="Label1" Width="137px"></asp:Label>
                <asp:TextBox ID="txtTargetAudienceMinAge" runat="server" Width="51px"></asp:TextBox>
            </td>
        </tr>
        
           
        <tr>
            <td class="auto-style1">
                <asp:Label AssociatedControlID="ddlTargetAudienceGender" runat="server" Text="Target Audience Gender (if applicable)"></asp:Label>
            </td>
            <td>&nbsp;&nbsp;&nbsp;
                <asp:DropDownList ID="ddlTargetAudienceGender" runat="server" Width="131px">
                    <asp:ListItem Value="All" Selected="True">All</asp:ListItem>
                    <asp:ListItem Value="Male">Male</asp:ListItem>
                    <asp:ListItem Value="Female">Female</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlTargetAudienceGender" CssClass="field-validation-error" ErrorMessage="Gender is required." />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align:center">
                <br />
                <br />
                <asp:Button Text="Submit" runat="server" OnClick="Submit" />
                <asp:Button Text="Reset" runat="server" OnClick="Reset" />
                <asp:Button Text="Cancel" runat="server" OnClick="Cancel" />
            </td>
        </tr>
    </table>

</asp:Content>
