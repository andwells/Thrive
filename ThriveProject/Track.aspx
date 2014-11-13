<%@ Page Title="Track" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Track.aspx.cs" Inherits="Food" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <title>Track</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <hgroup class="title">
        <h1><%: Title %></h1>
    </hgroup>
    <asp:SqlDataSource ID="sqlDSAccess" runat="server" ConnectionString='<%$ ConnectionStrings:USDAFoods %>' ProviderName='<%$ ConnectionStrings:USDAFoods.ProviderName %>' SelectCommand="SELECT [NDB_No], [Shrt_Desc], [GmWt_Desc1], [Energ_Kcal] FROM [ABBREV] WHERE ([Shrt_Desc] LIKE '%' + ? + '%') ORDER BY [Shrt_Desc]" DataSourceMode="DataReader">
        <SelectParameters>
            <asp:ControlParameter ControlID="TextBox1" PropertyName="Text" Name="Shrt_Desc" Type="String"></asp:ControlParameter>
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlDSLocal" runat="server" ConnectionString='<%$ ConnectionStrings:DefaultConnection %>' DeleteCommand="DeleteFood" InsertCommand="UpdateFood" SelectCommand="QueryFoods" UpdateCommand="UpdateFood" SelectCommandType="StoredProcedure" DeleteCommandType="StoredProcedure" InsertCommandType="StoredProcedure" UpdateCommandType="StoredProcedure">
        <DeleteParameters>
            <asp:Parameter Name="FoodId" Type="Int32"></asp:Parameter>
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="UserId" Type="Object"></asp:Parameter>
            <asp:Parameter Name="Name" Type="String"></asp:Parameter>
            <asp:Parameter Name="Calories" Type="Int32"></asp:Parameter>
            <asp:Parameter Name="IsRestaurant" Type="Boolean"></asp:Parameter>
            <asp:Parameter Name="Categories" Type="String"></asp:Parameter>
        </InsertParameters>
        <SelectParameters>
            <asp:Parameter Name="Name" Type="String"></asp:Parameter>
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="FoodId" Type="Int32"></asp:Parameter>
            <asp:Parameter Name="UserId" Type="Object"></asp:Parameter>
            <asp:Parameter Name="Name" Type="String"></asp:Parameter>
            <asp:Parameter Name="Calories" Type="Int32"></asp:Parameter>
            <asp:Parameter Name="IsRestaurant" Type="Boolean"></asp:Parameter>
            <asp:Parameter Name="Categories" Type="String"></asp:Parameter>
        </UpdateParameters>
    </asp:SqlDataSource>
    <section id="track">
        <asp:Menu ID="trackMenu" Width="300px" runat="server" Orientation="Horizontal" StaticEnableDefaultPopOutImage="False" OnMenuItemClick="Menu1_MenuItemClick">
            <Items>
                <asp:MenuItem Text="Track Food" Value="0">
                </asp:MenuItem>
                <asp:MenuItem Text="Track Exercise" Value="1"></asp:MenuItem>
                <asp:MenuItem Text="Track Water" Value="2"></asp:MenuItem>
                <asp:MenuItem Text="Track Sleep" Value="3"></asp:MenuItem>
            </Items>
        </asp:Menu>
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
            <asp:View ID="Tab1" runat="server">
                <asp:DropDownList ID="ddlSearchType" runat="server">
                    <asp:ListItem Value="By Name">By Name</asp:ListItem>
                    <asp:ListItem>By Type</asp:ListItem>
                    <asp:ListItem>By Category</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="tbFood" runat="server"></asp:TextBox>
                

                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                <asp:Button ID="btnSearchFood" runat="server" Text="Button1" />
            </asp:View>
            <asp:View ID="Tab2" runat="server">
                <asp:TextBox ID="tbExercise" runat="server"></asp:TextBox>
                <asp:Button ID="btnSearchExercise" runat="server" Text="Button2" />
            </asp:View>
            <asp:View ID="Tab3" runat="server">
                <asp:TextBox ID="waterCount" runat="server"></asp:TextBox>
                
                <asp:Button ID="btnWaterSave" runat="server" Text="Button3" />
            </asp:View>
            <asp:View ID="Tab4" runat="server">
                <asp:TextBox ID="tbSleep" runat="server"></asp:TextBox>
                <asp:Button ID="Button4" runat="server" Text="Button3" />
            </asp:View>
        </asp:MultiView>
    </section>
</asp:Content>

