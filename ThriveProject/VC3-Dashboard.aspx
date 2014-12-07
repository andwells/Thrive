<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="vc2Dashboard.aspx.cs" Inherits="vc2Dashboard" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">

<!--
<add name="ChartImageHandler" preCondition="integratedMode" verb="GET,HEAD,POST" path="ChartImg.axd" type="System.Web.UI.DataVisualization.Charting.ChartHttpHandler, System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" />
-->
<!--
            added xvaluetype, to test shortdate(); under asp:Series 3 lines down
-->
<asp:Chart ID="Chart1" runat="server" Width="600px" >
    <Series>
        
        <asp:Series Name="Food" Color="DarkRed" BorderWidth="3" ChartType="Line" LegendText="Foodz" MarkerBorderColor="White" Legend="Legend1" XValueType="DateTime"></asp:Series>
        <asp:Series Name="Exercise" ChartArea="ChartArea1" Color="LawnGreen" BorderWidth="4" ChartType="Spline" Legend="Legend1" XValueType="DateTime"></asp:Series>
    </Series>
    <ChartAreas>
        <asp:ChartArea Name="ChartArea1">
        </asp:ChartArea>
    </ChartAreas>
    <Legends>
        <asp:Legend Name="Legend1" Font="Microsoft YaHei UI, 8.25pt" IsTextAutoFit="False">
        </asp:Legend>
    </Legends>
</asp:Chart>

    <asp:Button ID="bDay" runat="server" OnClick="bDay_Click" Text="View Day" />
    <asp:Button ID="bWeek" runat="server" OnClick="bWeek_Click" Text="View Week" />
    <asp:Button ID="bMonth" runat="server" Text="View Month" onClick="bMonth_Click"/>

    <br />
    <br />
    <asp:Label ID="lstartDate" runat="server" BackColor="White" BorderStyle="Solid" Text="Start Date"></asp:Label>
    <br />

    <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged" style="height: 22px">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlDay" runat="server">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True">
    </asp:DropDownList>

    <br />
    <br />
    <br />
    <asp:Label ID="lEndDate" runat="server" BorderStyle="Solid" Text="End Date"></asp:Label>
    <br />
    <asp:DropDownList ID="ddlEndYear" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEndYear_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlEndMonth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEndMonth_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:DropDownList ID="ddlEndDay" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEndDay_SelectedIndexChanged">
    </asp:DropDownList>

<br />
    <br />
    <asp:Button ID="bViewRange" runat="server" OnClick="bViewRange_Click" Text="View Range" />
    <br />
<br />

</asp:Content>
