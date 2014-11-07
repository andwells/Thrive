<%@ Page Title="Foods" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Food.aspx.cs" Inherits="Food" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <title>Foods</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <hgroup class="title">
        <h1><%: Title %></h1>
    </hgroup>
    <section id="searchFoods">
        <asp:Menu ID="Menu1" Width="300px" runat="server" Orientation="Horizontal" StaticEnableDefaultPopOutImage="False" OnMenuItemClick="Menu1_MenuItemClick">
            <Items>
                <asp:MenuItem Text="Track Food" Value="0">
                </asp:MenuItem>
                <asp:MenuItem Text="Track 2" Value="1"></asp:MenuItem>
                <asp:MenuItem Text="Track 3" Value="2"></asp:MenuItem>
            </Items>
        </asp:Menu>
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
            <asp:View ID="Tab1" runat="server">
                <asp:Button ID="Button1" runat="server" Text="Button1" />
            </asp:View>
            <asp:View ID="Tab2" runat="server">
                <asp:Button ID="Button2" runat="server" Text="Button2" />
            </asp:View>
            <asp:View ID="Tab3" runat="server">
                <asp:Button ID="Button3" runat="server" Text="Button3" />
            </asp:View>
        </asp:MultiView>
    </section>
</asp:Content>

