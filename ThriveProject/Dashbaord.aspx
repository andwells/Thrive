<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Dashboard" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">

<asp:Chart ID="Chart1" runat="server">
    <Series>
        <asp:Series Name="Food" Color="DarkRed" BorderWidth="3" ChartType="Line" LegendText="Foodz" MarkerBorderColor="White"></asp:Series>
        <asp:Series Name="Exercise" ChartArea="ChartArea1" Color="LawnGreen" BorderWidth="4" ChartType="Spline"></asp:Series>
    </Series>
    <ChartAreas>
        <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
    </ChartAreas>
</asp:Chart>

</asp:Content>

