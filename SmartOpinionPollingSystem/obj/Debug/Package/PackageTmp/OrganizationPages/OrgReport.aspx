<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrgReport.aspx.cs" Inherits="SmartOpinionPollingSystem.OrganizationPages.OrgReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <style>
         #body
        {
            background-image: url("http://www.myfreetextures.com/wp-content/uploads/2011/06/illust18.jpg");
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
<link href="../Content/themes/base/jquery-ui.css" rel="stylesheet" />
<link rel="stylesheet" href="http://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.6.3/css/font-awesome.min.css">
<script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>

    <script>
        var chartData; // globar variable for hold chart data
        google.load("visualization", "1", { packages: ["corechart"] });

        // Here we will fill chartData

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
                google.setOnLoadCallback(drawPieChart);
                drawPieChart();
            });

            $.ajax({
                url: "/UserPages/UserDashboard.aspx/GetChartData",
                data: "",
                dataType: "json",
                type: "POST",
                contentType: "application/json; chartset=utf-8",
                success: function (data) {
                    chartData = data.d;
                },
                error: function () {
                    alert("Error loading data! Please try again!!!!.");
                }
            }).done(function () {
                // after complete loading data
                google.setOnLoadCallback(drawColumnChart);
                drawColumnChart();
            });



        });

        function drawPieChart() {
            var data = google.visualization.arrayToDataTable(chartData);

            var options = {
                title: " Registered Organization Categories - Breakup",
                pointSize: 5,
                pieHole: 0.2,
                //is3D: true
            };

            var pieChart = new google.visualization.PieChart(document.getElementById('chart_div'));
            pieChart.draw(data, options);

        }

        function drawColumnChart() {
            var data = google.visualization.arrayToDataTable(chartData);

            var options = {
                title: " Registered Users Voting Categories - Breakup",
                pointSize: 5,
                is3D: true
            };

            var columnChart = new google.visualization.ColumnChart(document.getElementById('chart_divColumnChart'));
            columnChart.draw(data, options);

        }
    </script>

     <p style="float:right;padding-right:15px">
            <asp:HyperLink runat="server" ID="hlnkOrgDashBoard" ViewStateMode="Disabled" ForeColor="Blue" Text="Organization DashBoard"></asp:HyperLink> 
     </p>
      <br /><br />
    <h2>All Organizations Report -Data Analytics</h2><br />

    <table>
        <tr>
            <td> <div id="chart_div" style="width: 400px; height: 400px; text-align:center">
                    <%-- Here Chart Will Load --%>
                </div>
            </td>
            <td>
                <div id="chart_divColumnChart" style="width: 550px; height: 400px; text-align:center">
                    <%-- Here Chart Will Load --%>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
