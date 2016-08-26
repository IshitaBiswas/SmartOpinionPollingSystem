<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserDashboard.aspx.cs" Inherits="SmartOpinionPollingSystem.UserPages.UserDashboard" %>
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
        .auto-style2 {
            width: 270px;
        }
        #body
        {
            background-image: url("http://voshsoutheast.org/wp-content/uploads/2014/05/Background-Texture-Images4.jpg");
        }
    </style>

</asp:Content>
<%--<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>--%>
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
                url: "UserDashboard.aspx/GetChartData",
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
                title: " Registered Users Voting Categories - Breakup",
                pointSize: 5,
                is3D: true
            };

            var pieChart = new google.visualization.PieChart(document.getElementById('chart_div'));
            pieChart.draw(data, options);

        }
    </script>




    <table style="width: 100%">
        <caption>User Dashboard</caption>
        <tr>
            <th class="auto-style2">User Activities : Click one</th>
            <th>Chart</th>
        </tr>
        <tr>
            <td class="auto-style2">
                    <asp:Button ID="btnAddQuestion" runat="server" Text="View Current Polling Questions" Width="232px" OnClick="btnCurrentPollingQuestions_Click"/>
                    <br />
                    <asp:Button ID="btnPrevPollingStatus" runat="server" Text="View Pevious Polling Sumary" Width="227px" OnClick="btnPrevPollingQuestions_Click" />
                    <br />
                    <asp:Button ID="btnCurrentPollingStatus" runat="server" Text="View Future Polling Questions" Width="221px" OnClick="btnFuturePollingQuestions_Click"  />
                    <br />
            </td>

             <td style="text-align:center">
                <div id="chart_div" style="width: 500px; height: 400px; text-align:center">
                    <%-- Here Chart Will Load --%>
                </div>
            </td>
            </tr>
      
    </table>
</asp:Content>
