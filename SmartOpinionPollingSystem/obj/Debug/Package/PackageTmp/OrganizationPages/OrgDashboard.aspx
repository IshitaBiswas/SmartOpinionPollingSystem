<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrgDashboard.aspx.cs" Inherits="SmartOpinionPollingSystem.OrganizationPages.OrgDashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <style>
        table, th, td {
            border: 1px solid black;
            border-collapse: collapse;
        }

        th, td {
            padding: 5px;
            text-align: center;
            margin-left: 80px;
        }
        .auto-style1 {
            height: 185px;
        }

        #body
        {
            background-image: url("http://www.myfreetextures.com/wp-content/uploads/2011/06/illust18.jpg");
        }
    </style>

    </asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

        
<script type="text/javascript" src="https://www.google.com/jsapi"></script>
<link href="../Content/themes/base/jquery-ui.css" rel="stylesheet" />
<link rel="stylesheet" href="http://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.6.3/css/font-awesome.min.css">


    <script>
        var chartData; // globar variable for hold chart data
        google.load("visualization", "1", { packages: ["corechart"] });

        // Here We will fill chartData

        $(document).ready(function () {
            $.ajax({
                url: "OrgDashboard.aspx/GetChartData",
                data: "",   
                dataType: "json",
                type: "POST",
                contentType: "application/json; chartset=utf-8",
                success: function (data) {
                    chartData = data.d;
                },
                error: function () {
                    alert("Error loading data! Please try again.");
                }
            }).done(function () {
                // after complete loading data
                google.setOnLoadCallback(drawChart);
                drawChart();
            });
        });

        function drawChart() {
            var data = google.visualization.arrayToDataTable(chartData);

            var options = {
                title: " Registered Organization Categories - Breakup",
                pointSize: 5,
                is3D: true
            };

            var pieChart = new google.visualization.PieChart(document.getElementById('chart_div'));
            pieChart.draw(data, options);

        }
    </script>




    <table style="width: 100%">
        <caption><h2>Organization Dashboard</h2></caption>
        <tr>
            <th>Polling Action</th>
            <th>Chart</th>
        </tr>
        <tr>
            <td>
                 
                    <asp:Button ID="btnAddQuestion" runat="server" Text="Add a New Polling Question" Width="295px" OnClick="btnAddQuestion_Click"/>
                    <br />
                    <asp:Button ID="btnPrevPollingStatus" runat="server" Text="Check Previous Polling Status" Width="295px" OnClick="btnPrevPollingStatus_Click" />
                    <br />
                    <asp:Button ID="btnCurrentPollingStatus" runat="server" Text="Check Current Polling Status" Width="295px" OnClick="btnCurrentPollingStatus_Click"  />
                    <br />
                    <asp:Button ID="btnFuturePollingStatus" runat="server" Text="Check Future Polling Status" Width="295px" OnClick="btnFuturePollingStatus_Click" />
                    <asp:Button ID="btnViewOrgReport" runat="server" Text="View Report" Width="295px" OnClick="btnViewOrgReport_Click" />
            </td>
            <td>
                <div id="chart_div" style="width: 500px; height: 400px; text-align:center">
                    <%-- Here Chart Will Load --%>
                </div>
            </td>
         </tr>
    </table>
</asp:Content>
