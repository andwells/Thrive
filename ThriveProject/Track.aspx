<%@ Page Title="Track" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Track.aspx.cs" Inherits="Track" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <title>Track</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <script type="text/javascript">
        function showhide(x) {
            var section = document.getElementById(x);
            if (section.style.visibility == "hidden") {
                section.style.visibility = "visible";
            }
            else {
                section.style.visibility = "hidden";
            }
        }
    </script>
    <hgroup class="title">
        <h1><%: Title %></h1>
    </hgroup>
    <asp:SqlDataSource ID="sqlDSAccess" runat="server" ConnectionString='<%$ ConnectionStrings:USDAFoods %>' ProviderName='<%$ ConnectionStrings:USDAFoods.ProviderName %>' SelectCommand="SELECT [NDB_No] AS FoodId, [Shrt_Desc] AS Name, [GmWt_Desc1], [Energ_Kcal] AS Calories FROM [ABBREV] WHERE ([Shrt_Desc] LIKE '%' + ? + '%') ORDER BY [Shrt_Desc]" DataSourceMode="DataSet">
        <SelectParameters>
            <asp:ControlParameter ControlID="tbFood" PropertyName="Text" Name="Shrt_Desc" Type="String"></asp:ControlParameter>
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="sqlDSLocal" runat="server" ConnectionString='<%$ ConnectionStrings:DefaultConnection %>' DeleteCommand="DeleteFood" InsertCommand="UpdateFood" SelectCommand="QueryFoods" UpdateCommand="UpdateFood" SelectCommandType="StoredProcedure" DeleteCommandType="StoredProcedure" InsertCommandType="StoredProcedure" UpdateCommandType="StoredProcedure" DataSourceMode="DataSet">
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
        <asp:Menu ID="trackMenu" Width="500px" runat="server" Orientation="Horizontal" StaticEnableDefaultPopOutImage="False" OnMenuItemClick="Menu1_MenuItemClick">
            <Items>
                <asp:MenuItem Text="Track Food" Value="0">
                </asp:MenuItem>
                <asp:MenuItem Text="Track Exercise" Value="1"></asp:MenuItem>
                <asp:MenuItem Text="Track Water" Value="2"></asp:MenuItem>
                <asp:MenuItem Text="Track Sleep" Value="3"></asp:MenuItem>
            </Items>
        </asp:Menu>
        <br />
        <br />
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
            <asp:View ID="Tab1" runat="server">
                <asp:DropDownList ID="ddlSearchType" runat="server">
                    <asp:ListItem Value="name">By Name</asp:ListItem>
                    <asp:ListItem Value="type">By Type</asp:ListItem>
                    <asp:ListItem Value="category">By Category</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="tbFood" runat="server"></asp:TextBox>
                <asp:Button ID="btnSearchFood" runat="server" Text="Search" OnClick="btnSearchFood_Click" />
                <br />
                <asp:Label ID="lblError" runat="server" Visible="false"></asp:Label><asp:LinkButton ID="lbtnCreate" runat="server" visble="true" Text="Create New Food?" OnClientClick="showhide(createFood)"></asp:LinkButton>
                <br />
                <section id="createFood" style="visibility:hidden">
                    <h3>Create Food</h3>
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label><asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><asp:Label ID="Label2" runat="server" Text="Label"></asp:Label><asp:TextBox ID="TextBox2" runat="server"></asp:TextBox><asp:Label ID="Label3" runat="server" Text="Label"></asp:Label><asp:TextBox ID="TextBox3" runat="server"></asp:TextBox><asp:Button ID="btnCreateFood" runat="server" Text="Create"/>
                </section>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gvResults" runat="server" AutoGenerateSelectButton="true" AllowPaging="true" OnPageIndexChanging="gvResults_PageIndexChanging" OnSelectedIndexChanged="gvResults_SelectedIndexChanged" OnRowCreated="gvResults_RowCreated"></asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
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

