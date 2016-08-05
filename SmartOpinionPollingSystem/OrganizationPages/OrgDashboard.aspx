<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrgDashboard.aspx.cs" Inherits="SmartOpinionPollingSystem.OrganizationPages.OrgDashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
    <%-- Here We need to write some js code for load google chart with database data --%>
    <script src="Scripts/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>


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
    </style>


    </asp:Content>




<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

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
                pointSize: 5
            };

            var pieChart = new google.visualization.PieChart(document.getElementById('chart_div'));
            pieChart.draw(data, options);

        }
    </script>


    <%--JQGrid--%>
    <script>



    </script>


    <link rel="stylesheet" href="http://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.6.3/css/font-awesome.min.css">


    <table style="width: 100%">
        <caption>Organization Dashboard</caption>
        <tr>
            <th>Polling Action</th>
            <th>Chart</th>
        </tr>
        <tr>
            <td>
                    <button ID="btnAddQuestion" runat="server" Text="Add a New Polling Question" Width="295px">
                        Add a New Polling Question <%--<i class="fa fa-plus"> --%>
                    </button>
                    <br />
                    <asp:Button ID="btnPrevPollingStatus" runat="server" Text="Check Previous Polling Status" Width="295px" />
                    <br />
                    <asp:Button ID="btnCurrentPollingStatus" runat="server" Text="Check Current Polling Status" Width="295px" />
                    <br />
                    <asp:Button ID="btnFuturePollingStatus" runat="server" Text="Check Future Polling Status" Width="295px" />
            </td>
            <td>
                <div id="chart_div" style="width: 500px; height: 400px; text-align:center">
                    <%-- Here Chart Will Load --%>
                </div>
            </td>
        </tr>

        <tr>
            <td colspan="2">
                <asp:DataGrid runat ="server" Width="1072px" ID="dgTest"></asp:DataGrid>
            </td>

        </tr>
    </table>



   <%-- <div id="chart_div" style="width:500px;height:400px">
        <%-- Here Chart Will Load 
    </div>--%>

</asp:Content>
