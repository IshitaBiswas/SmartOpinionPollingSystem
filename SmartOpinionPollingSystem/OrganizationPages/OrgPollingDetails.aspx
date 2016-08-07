<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrgPollingDetails.aspx.cs" Inherits="SmartOpinionPollingSystem.OrganizationPages.OrgPollingDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <link href="../Content/themes/base/jquery-ui.css" rel="stylesheet" />
    <link href="../Scripts/JqGrid/css/ui.jqgrid.css" rel="stylesheet" />
    <script src="../Scripts/JqGrid/js/jquery-1.7.2.min.js"></script>
    <script src="../Scripts/JqGrid/js/jquery.jqGrid.min.js"></script>
    <script src="../Scripts/JqGrid/js/i18n/grid.locale-en.js"></script>

        <%--JQGrid--%>
     <script type="text/javascript">

         //function returnHyperLink(cellValue, options, rowdata, action) {
         //    return "<a href='./test.do?custId=" + options.rowId + "' >View</a>";
         //}

         $(function () {
             $("#dataGrid").jqGrid({
                 url: 'OrgPollingDetails.aspx/GetPollingDetails',  //WebMethod
                 datatype: 'json',
                 data: "{}",
                 mtype: 'POST',
                 ajaxGridOptions: { contentType: 'application/json; charset=utf-8' },
                 serializeGridData: function (postData) {
                     return JSON.stringify(postData);
                 },
                 jsonReader: { repeatitems: false, root: "d.rows", page: "d.page", total: "d.total", records: "d.records" },
                 loadonce: true,
                 colNames: ['QuestionID', 'VotingQuestionCategoryID', 'OrgID', 'QuestionText','VotedYes', 'VotedNo', 'VotingStartDate', 'VotingEndDate' ],
                 colModel: [
                                 { name: 'QuestionID', index: 'QuestionID', editable: false },
                                 { name: 'VotingQuestionCategoryID', index: 'VotingQuestionCategoryID', editable: false },
                                 { name: 'OrgID', index: 'OrgID', editable: false },
                                 { name: 'QuestionText', index: 'QuestionText', editable: false },
                                 { name: 'VotedYes', index: 'VotedYes', editable: false },
                                 { name: 'VotedNo', index: 'VotedNo', editable: false },
                                 { name: 'VotingStartDate', index: 'VotingStartDate', editable: false },
                                 { name: 'VotingEndDate', index: 'VotingEndDate', editable: false },
                                 //{ name: 'c_myname', index: 'OrgCategory', width: 60, editable: false, formatter: returnHyperLink }//added custom formatter function 
                 ],
                 pager: jQuery('#pager'),
                 rowNum: 10,
                 rowList: [10, 20, 30, 40],
                 height: 'auto',
                 pager: '#pager',
                 viewrecords: true,
                 caption: 'Dash Summary',
                 sortorder: 'asc',
                 emptyrecords: 'No records to display',
                 gridview: true,
                 autowidth: true,
                 multiselect: false,
                 altrows: true,
                 hoverrows: true
             }).navGrid(pager, { edit: true, add: false, del: false, search: false });
         });
    </script>  




   <table id="dataGrid">
    </table>
    <div id="pager" class="scroll" style="text-align: center;">
    </div>

</asp:Content>
