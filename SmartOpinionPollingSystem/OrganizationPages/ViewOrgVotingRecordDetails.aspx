<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewOrgVotingRecordDetails.aspx.cs" Inherits="SmartOpinionPollingSystem.OrganizationPages.ViewOrgVotingRecordDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
<link href="../Content/themes/base/jquery-ui.css" rel="stylesheet" />
<link rel="stylesheet" href="http://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.6.3/css/font-awesome.min.css">

    <link href="../Content/style15.css" rel="stylesheet" />

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

        tr:hover {
            background-color: #f5f5f5;
        }

        .auto-style1 {
            
        }
        article
        {
            width:auto;
        }
        .auto-style2 {
            width: 351px;
        }
        .auto-style3 {
            width: 60px;
        }
        .votingpanelwhite
        {
            background-color: white;
        }
         #body
        {
            background-image: url("http://www.myfreetextures.com/wp-content/uploads/2011/06/illust18.jpg");
        }
    </style>
    <script>
        var chartData; // globar variable for hold chart data
        google.load("visualization", "1", { packages: ["corechart"] });

        // Here We will fill chartData

        $(document).ready(function () {
            $.ajax({
                url: "ViewOrgVotingRecordDetails.aspx/GetPieChartData",
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
                title: " Voting Results -Yes Vs No",
                pointSize: 5,
                is3D: true
            };

            if ('<%=Session["pollingWindow"]%>' == 'Previous') {

                var pieChart = new google.visualization.PieChart(document.getElementById('chart_div'));
            }
           else if ('<%=Session["pollingWindow"]%>' == 'Current') {

                var pieChart = new google.visualization.PieChart(document.getElementById('chart_div1'));
            }

            pieChart.draw(data, options);

        }
    </script>







      <asp:Panel runat="server" ID="pnlMessage" Visible="false">
            <asp:Label ID="lblMesage" runat="server"></asp:Label><br>
      </asp:Panel>

    <asp:Panel runat="server" ID="pnlPrevious" Visible="false">

        <h1 class="main_header" style="text-align:center">Previous Polling Summary</h1>

        <div class="container">
            <section class="main_section">
                <article>
                    <header>
                        <hgroup>
                            <h2>Previous Polling Summary Highlight</h2>
                        </hgroup>
                    </header>
                    <table>
                        <tr>
                            <td>
                                <b>Question:</b>
                            </td>
                            <td>
                                <asp:Label ID="lblQuestion" runat="server"></asp:Label><br>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Target Audience Domain:</b>
                            </td>
                            <td>
                                <asp:Label ID="lblCategoryDescription" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                           <td>
                                <b>Voting StartDate:</b>
                            </td>
                           <td>
                                <asp:Label ID="lblVotingStartDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Voting EndDate:</b>

                            </td>

                           <td>
                                <asp:Label ID="lblVotingEndDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </article>
                <article>
                    <header>
                        <hgroup>
                            <h2>Polling Result</h2>
                        </hgroup>
                    </header>
                    <table>
                        <tr>
                           <td>
                                <b>Number of votes in favour:</b>
                            </td>
                            <td>
                                <asp:Label ID="lblVotedYes" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                           <td>
                                <b>Number of votes against:</b>
                            </td>
                            <td>
                                <asp:Label ID="lblVotedNo" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                           <td>
                                <b>Who own:</b>

                            </td>
                            <td>
                                <asp:Label ID="lblWinner" runat="server"></asp:Label>
                            </td>
                        </tr>

                    </table>
                </article>
            </section>
            <aside class="main_aside">
                <figure>
                    <div id="chart_div" style="width: 500px; height: 400px; text-align: center">
                        <%-- Here Chart Will Load --%>
                    </div>
                </figure>
                <br>

                <blockquote>Visualize Result!!</blockquote>
            </aside>

        </div>

    </asp:Panel>
    <asp:Panel runat="server" ID="pnlCurrent" Visible="false">

        <h1 class="main_header" style="text-align: center">Ongoing Polling Summary</h1>

        <div class="flex-container">
            <div class="flex-item1">
                <header>
                    <hgroup>
                        <h2>Ongoing Polling Summary Highlight</h2>
                    </hgroup>
                </header>
                <table>
                    <tr>
                        <td>
                            <b>Target Audience Domain:</b>
                        </td>
                        <td>
                            <asp:Label ID="lblCurrentCategoryDescription" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <b>Question:</b>
                        </td>
                        <td>
                            <asp:Label ID="lblCurrentQuestion" runat="server"></asp:Label><br>
                        </td>
                    </tr>
                     <tr>
                           <td>
                                <b>Voting StartDate:</b>
                            </td>
                           <td>
                                <asp:Label ID="lblCurrentStartDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Voting EndDate:</b>

                            </td>

                           <td>
                                <asp:Label ID="lblCurrentEndDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                </table>

            </div>


            <div class="flex-item2">

                <div id="wrapper">
                    <asp:Panel class="wrapperasideleft" runat="server" ID="pnlGraphVoteCast">
                        <%--<div class="wrapperasideleft">--%>
                        <figure>
                            <div id="chart_div1" style="width: 400px; height: 400px; text-align: center">
                            </div>
                        </figure>
                        <br>

                        <blockquote>Visualize Result!!</blockquote>
                        <%--</div>--%>
                    </asp:Panel>

                    <div class="wrapperasideright">
                        <header>
                            <hgroup>
                                <h2>Discussion Board</h2>
                            </hgroup>
                        </header>
                        <table>
                            <tr>
                                <td>
                                    <b>Category Description:</b>
                                </td>
                                <td>
                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>Organization conducting the poll:</b>
                                </td>
                                <td>
                                    <asp:Label ID="Label2" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <b>Question:</b>
                                </td>
                                <td>
                                    <asp:Label ID="Label3" runat="server"></asp:Label><br>
                                </td>
                            </tr>
                        </table>
                    </div>

                </div>

            </div>
        </div>
        
     </asp:Panel>

    <asp:Panel runat="server" ID="pnlFuture" Visible="false">
        <h1 id="main_header2" style="text-align: center">Future Polling</h1>
    </asp:Panel>


</asp:Content>
