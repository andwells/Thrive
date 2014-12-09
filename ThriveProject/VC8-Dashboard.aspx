<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="VC8-Dashboard.aspx.cs" Inherits="vc2Dashboard" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">

    <table>
        <tr>
            <td>
                <asp:Label ID="lbl_consumed" runat="server" Text="Calories Consumed" Width="49%" ></asp:Label>
                <asp:Label ID="lbl_burned" runat="server" Text="Calories Burned" Width="50%"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lbl_weight" runat="server" Text="Weight: "></asp:Label>
                <asp:TextBox ID="tb_weight" runat="server" Text="180"  Width="40"></asp:TextBox>
                
                <asp:Button ID ="b_weightChange" runat="server" Text="+" Height="25" Width="25"></asp:Button>
                <asp:Button ID ="b_weightChange2" runat="server" Text="-" Height="25" Width="25"></asp:Button>
                 
                <asp:Label ID="lbl_water" runat="server" Text="Water: "></asp:Label>
                <asp:TextBox ID="tb_water" runat="server" Text="24" Width="40"></asp:TextBox>

                <asp:Button ID ="b_waterChange" runat="server" Text="+"></asp:Button>
                <asp:Button ID ="b_waterChange2" runat="server" Text="-"></asp:Button>

                <asp:Label ID="lbl_sleep" runat="server" Text="Sleep: "></asp:Label>
                <asp:TextBox ID="tb_sleep" runat="server" Text="7" Width="40"></asp:TextBox>

                <asp:Button ID ="b_sleepChange" runat="server" Text="+"></asp:Button>
                <asp:Button ID ="b_sleepChange2" runat="server" Text="-"></asp:Button>

            </td>
        </tr>
        <tr>
            <td>
                 <asp:Button ID="btn_daily" runat="server" Text="Update"/>
            </td>
        </tr>
    </table>

    <br />
<asp:Chart ID="Chart1" runat="server" Width="600px" >
    <Series>
        
        <asp:Series Name="Food" Color="DarkRed" BorderWidth="3" ChartType="Line" LegendText="Foodz" MarkerBorderColor="White" Legend="Legend1" XValueType="DateTime"></asp:Series>
        <asp:Series Name="Exercise" ChartArea="ChartArea1" Color="LawnGreen" BorderWidth="4" ChartType="Spline" Legend="Legend1" XValueType="DateTime"></asp:Series>
        <asp:Series BorderWidth="3" ChartArea="ChartArea1" Enabled="False" Legend="Legend1" Name="Water" XValueType="DateTime"></asp:Series>
        <asp:Series ChartArea="ChartArea1" ChartType="Area" Color="Fuchsia" Enabled="False" Legend="Legend1" Name="Weight" XValueType="DateTime"></asp:Series>
    
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

    <br />
    <asp:DropDownList ID="ddlChoice" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlChoice_SelectedIndexChanged">
        <asp:ListItem>Food &amp; Exercise</asp:ListItem>
        <asp:ListItem>Water Consumption</asp:ListItem>
        <asp:ListItem Selected="True">Weight</asp:ListItem>
    </asp:DropDownList>
    <br />

    <asp:Button ID="bDay" runat="server" OnClick="bDay_Click" Text="View Day" />
    <asp:Button ID="bWeek" runat="server" OnClick="bWeek_Click" Text="View Week" />
    <asp:Button ID="bMonth" runat="server" Text="View Month" onClick="bMonth_Click"/>

    <br />
    <br />
    <asp:Label ID="lstartDate" runat="server" BackColor="White" BorderStyle="Solid" Text="Start Date"></asp:Label>
    <br />

    <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged" Height="25px">
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
    <br />
<br />

</asp:Content>
