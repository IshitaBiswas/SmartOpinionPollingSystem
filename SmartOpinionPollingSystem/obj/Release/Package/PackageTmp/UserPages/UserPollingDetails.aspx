<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserPollingDetails.aspx.cs" Inherits="SmartOpinionPollingSystem.UserPages.UserPollingDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <style>

        #body
        {
            background-image: url("http://voshsoutheast.org/wp-content/uploads/2014/05/Background-Texture-Images4.jpg");
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">



    <link href="../Scripts/NewJQGrid/Content/themes/base/jquery-ui.css" rel="stylesheet" />
<%--    <link href="../Scripts/NewJQGrid/Content/jquery.jqGrid/ui.jqgrid.css" rel="stylesheet" />
    <script src="../Scripts/NewJQGrid/Scripts/jquery-1.9.1.min.js"></script>
    <script src="../Scripts/NewJQGrid/Scripts/jquery-ui-1.10.4.min.js"></script>
    <script src="../Scripts/NewJQGrid/Scripts/i18n/grid.locale-en.js"></script>
    <script src="../Scripts/NewJQGrid/Scripts/jquery.jqGrid.min.js"></script>--%>


    <link rel="stylesheet" type="text/css" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.15/themes/redmond/jquery-ui.css" />
    <link rel="stylesheet" type="text/css" href="http://www.ok-soft-gmbh.com/jqGrid/jquery.jqGrid-4.1.2/css/ui.jqgrid.css" />
    <link rel="stylesheet" type="text/css" href="http://www.ok-soft-gmbh.com/jqGrid/jquery.jqGrid-4.1.2/plugin/ui.multiselect.css" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.6.2/jquery.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.15/jquery-ui.min.js"></script>
    <script type="text/javascript" src="http://www.ok-soft-gmbh.com/jqGrid/jquery.jqGrid-4.1.2/plugin/ui.multiselect.js"></script>
    <script type="text/javascript" src="http://www.ok-soft-gmbh.com/jqGrid/jquery.jqGrid-4.1.2/js/i18n/grid.locale-en.js"></script>
    <script type="text/javascript" src="http://www.ok-soft-gmbh.com/jqGrid/jquery.jqGrid-4.1.2/js/jquery.jqGrid.src.js"></script>



        <%--JQGrid--%>
     <script type="text/javascript">

         function returnHyperLink(cellValue, options, rowdata, action) {
             return "<a href='ViewUserVotingRecordDetails.aspx?QuestionID=" + options.rowId + "' >View Details</a>";
         }

         $(function () {
             $("#dataGrid").jqGrid({
                 url: 'UserPollingDetails.aspx/GetUserPollingDetails',  //WebMethod
                 datatype: 'json',
                 data: "{}",
                 mtype: 'POST',
                 ajaxGridOptions: { contentType: 'application/json; charset=utf-8' },
                 serializeGridData: function (postData) {
                     return JSON.stringify(postData);
                 },
                 jsonReader: { repeatitems: false, root: "d.rows", page: "d.page", total: "d.total", records: "d.records" },
                 loadonce: true,
                 colNames: [ 'QID#', 'CategoryDescription', 'Organization',  'QuestionText', 'UserVote' , 'DateVoteCasted',  'CountOfYes', 'CountOfNo', 'VotingStartDate', 'VotingEndDate'],
                 colModel: [
                                 { name: 'QuestionID', index: 'QuestionID', width: 180, editable: false,  key: true, formatter: returnHyperLink },//added custom formatter function 
                                 { name: 'CategoryDescription', index: 'CategoryDescription', width: 180, editable: true },
                                 { name: 'OrgName', index: 'OrgName', width: 180, editable: false },
                                 { name: 'QuestionText', index: 'QuestionText', width: 180, editable: true },
                                 { name: 'B_UserVote', index: 'B_UserVote', width: 100, editable: false },
                                 { name: 'DtVoteCasted', index: 'DtVoteCasted', width: 100, editable: false, formatter: 'date' },
                                 { name: 'VotedYes', index: 'VotedYes', width: 180, editable: false },
                                 { name: 'VotedNo', index: 'VotedNo', width: 180, editable: false },
                                 { name: 'VotingStartDate', index: 'VotingStartDate', width: 180, editable: false, formatter: 'date' },
                                 { name: 'VotingEndDate', index: 'VotingEndDate', width: 180, editable: false, formatter: 'date' },
                 ],
                 pager: jQuery('#pager'),
                 //  rowNum: 5,
                 rowList: [5, 10, 15, 20],
                 height: 'auto',
                 pager: '#pager',
                 viewrecords: true,
                 caption: 'User Voting Summary',
                 sortorder: 'asc',
                 emptyrecords: 'No records to display',
                 gridview: true,
                 autowidth: true,
                 multiselect: false,
                 altrows: true,
                 hoverrows: true,
             }).navGrid('#pager', { edit: false, add: false, del: false, search: true, refresh: true });
         });

         //

         $(function () {
             $("#dataGridPendingVotes").jqGrid({
                 url: 'UserPollingDetails.aspx/GetPendingPollingQueue',  //WebMethod
                 datatype: 'json',
                 data: "{}",
                 mtype: 'POST',
                 ajaxGridOptions: { contentType: 'application/json; charset=utf-8' },
                 serializeGridData: function (postData) {
                     return JSON.stringify(postData);
                 },
                 jsonReader: { repeatitems: false, root: "d.rows", page: "d.page", total: "d.total", records: "d.records" },
                 loadonce: true,
                 colNames: ['QID#', 'Organization', 'QuestionText', 'VotingStartDate', 'VotingEndDate'],
                 colModel: [
                                 { name: 'QuestionID', index: 'QuestionID', width: 180, editable: false, key: true, formatter: returnHyperLink },//added custom formatter function 
                                 { name: 'OrgName', index: 'OrgName', width: 180, editable: false },
                                 { name: 'QuestionText', index: 'QuestionText', width: 180, editable: true },
                                 { name: 'VotingStartDate', index: 'VotingStartDate', width: 180, editable: false, formatter: 'date' },
                                 { name: 'VotingEndDate', index: 'VotingEndDate', width: 180, editable: false, formatter: 'date' },
                 ],
                 pager: jQuery('#pagerPendingVotes'),
                 //  rowNum: 5,
                 rowList: [5, 10, 15, 20],
                 height: 'auto',
                 pager: '#pagerPendingVotes',
                 viewrecords: true,
                 caption: 'Polling Queue - Waiting for user vote',
                 sortorder: 'asc',
                 emptyrecords: 'No records to display',
                 gridview: true,
                 autowidth: true,
                 multiselect: false,
                 altrows: true,
                 hoverrows: true,
             }).navGrid('#pagerPendingVotes', { edit: false, add: false, del: false, search: true, refresh: true });
         });



    </script>  

    

    <asp:Panel runat ="server" ID="pnlAlreadyVotedRecord">
           <p style="float:right;padding-right:15px">
            <asp:HyperLink runat="server" ID="UserDashBoard" ViewStateMode="Disabled" ForeColor="Blue">Return to User DashBoard</asp:HyperLink>
         </p><br /><br /><br />

   <table id="dataGrid">
    </table>
    <div id="pager" class="scroll" style="text-align: center;">
    </div>
    </asp:Panel>
    <br/>
    <br/>
    <asp:Panel runat ="server" ID="pnlPendingVotingRecord" Visible="false">
    <table id="dataGridPendingVotes">
    </table>
    <div id="pagerPendingVotes" class="scroll" style="text-align: center;">
    </div>
    </asp:Panel>

   
   

</asp:Content>
