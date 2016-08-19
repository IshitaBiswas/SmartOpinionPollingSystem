<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrgPollingDetails.aspx.cs" Inherits="SmartOpinionPollingSystem.OrganizationPages.OrgPollingDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">


 <%--   <link href="../Content/themes/base/jquery-ui.css" rel="stylesheet" />
    <link href="../Scripts/JqGrid/css/ui.jqgrid.css" rel="stylesheet" />
    <script src="../Scripts/JqGrid/js/jquery-1.7.2.min.js"></script>
    <script src="../Scripts/JqGrid/js/jquery.jqGrid.min.js"></script>
    <script src="../Scripts/JqGrid/js/i18n/grid.locale-en.js"></script>--%>

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
                 colNames: ['QID', 'CategoryDescription', 'QuestionText', 'VotedYes', 'VotedNo', 'VotingStartDate', 'VotingEndDate'],
                 colModel: [
                                 { name: 'QuestionID', index: 'QuestionID', width: 60, editable: false },
                                 { name: 'CategoryDescription', index: 'CategoryDescription', width: 180, editable: true },
                                 //{ name: 'OrgID', index: 'OrgID',  width: 60, editable: false },
                                 { name: 'QuestionText', index: 'QuestionText', width: 180, editable: true },
                                 { name: 'VotedYes', index: 'VotedYes', width: 60, editable: false },
                                 { name: 'VotedNo', index: 'VotedNo', width: 60, editable: false },
                                 { name: 'VotingStartDate', index: 'VotingStartDate', width: 180, editable: true, formatter: 'date' },
                                 { name: 'VotingEndDate', index: 'VotingEndDate', width: 180, editable: true, formatter: 'date' },
                                 //{ name: 'c_myname', index: 'OrgCategory', width: 60, editable: false, formatter: returnHyperLink }//added custom formatter function 
                 ],
                 pager: jQuery('#pager'),
                 //  rowNum: 5,
                 rowList: [5, 10, 15, 20],
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
                 hoverrows: true,
             }).navGrid('#pager', { edit: false, add: false, del: false, search: true, refresh: true });
         });


    </script>  




   <table id="dataGrid">
    </table>
    <div id="pager" class="scroll" style="text-align: center;">
    </div>


    </br>
    <div>
         <p>
            <asp:HyperLink runat="server" ID="OrgDashBoard" ViewStateMode="Disabled">Organization DashBoard</asp:HyperLink>
            Return back to Dashboard
        </p>


    </div>

</asp:Content>
