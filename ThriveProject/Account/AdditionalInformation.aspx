<%@ Page Title="Additional Information" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AdditionalInformation.aspx.cs" Inherits="Account_AdditionalInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    
    <hgroup class="title">
        <h1><%: Title %>.</h1>
    </hgroup>
    <section>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Wizard ID="wzrdAdditional" runat="server" Width="530px" ActiveStepIndex="0" OnFinishButtonClick="wzrdAdditional_FinishButtonClick" OnNextButtonClick="wzrdAdditional_NextButtonClick">
                    <WizardSteps>
                        <asp:WizardStep runat="server" Title="Step 1">
                             Height<br />
                                <asp:ListBox ID="lbFeet" runat="server" Height="22px" Rows="1">
                                    <asp:ListItem>1</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                    <asp:ListItem>3</asp:ListItem>
                                    <asp:ListItem>4</asp:ListItem>
                                    <asp:ListItem>5</asp:ListItem>
                                    <asp:ListItem>6</asp:ListItem>
                                    <asp:ListItem>7</asp:ListItem>
                                    <asp:ListItem>8</asp:ListItem>
                                    <asp:ListItem>9</asp:ListItem>
                                    <asp:ListItem>10</asp:ListItem>
                                </asp:ListBox>
                                &nbsp;ft.&nbsp;
                                <asp:ListBox ID="lbInches" runat="server" Height="22px" Rows="1">
                                    <asp:ListItem>1</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                    <asp:ListItem>3</asp:ListItem>
                                    <asp:ListItem>4</asp:ListItem>
                                    <asp:ListItem>5</asp:ListItem>
                                    <asp:ListItem>6</asp:ListItem>
                                    <asp:ListItem>8</asp:ListItem>
                                    <asp:ListItem>7</asp:ListItem>
                                    <asp:ListItem>9</asp:ListItem>
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>11</asp:ListItem>
                                </asp:ListBox>
                                &nbsp;inches<br />
                                <br />
                                <br />
                                Weight<br />
                                <asp:TextBox ID="tbWeight" runat="server" Height="16px" Width="91px"></asp:TextBox>
                                <br />
                                <br />
                                Age<br />
                                <asp:TextBox ID="tbAge" runat="server" Width="92px"></asp:TextBox>
                                <br />
                                <br />
                                Gender<br />
                                <asp:DropDownList ID="ddlGender" runat="server">
                                    <asp:ListItem>Select Your Gender</asp:ListItem>
                                    <asp:ListItem>Male</asp:ListItem>
                                    <asp:ListItem>Female</asp:ListItem>
                                </asp:DropDownList>
                        </asp:WizardStep>
                        <asp:WizardStep runat="server" title="Step 2">
                            <asp:RadioButtonList ID="rblActivityLevel" runat="server">
                                    <asp:ListItem Value="sedentary">Sedentary (e.g. bedridden)</asp:ListItem>
                                    <asp:ListItem Value="light">Light (e.g. office worker)</asp:ListItem>
                                    <asp:ListItem Value="medium">Medium (e.g. professional cleaner)</asp:ListItem>
                                    <asp:ListItem Value="heavy">Heavy (e.g. construction worker)</asp:ListItem>
                            </asp:RadioButtonList>
                        </asp:WizardStep>
                        <asp:WizardStep runat="server" title="Step 3">
                            <asp:RaidioButtonList ID="rblWeightGoal" runat="server">
                                <asp:ListItem Value="0">I want to lose weight</asp:ListItem>
                                <asp:ListItem Value="1">I want to gain weight</asp:ListItem>
                                <asp:ListItem Value="2">I want to maintain my weight</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:TextBox ID="tbSleep" runat="server" Width="92px"></asp:TextBox>
                        </asp:WizardStep>
                        <asp:WizardStep runat="server" title="Step 4">
                            <asp:RaidioButtonList ID="rblHydrationTracking" runat="server">
                                <asp:ListItem Value="1">I want to track hydration</asp:ListItem>
                                <asp:ListItem Value="0">I do not want to track hydration</asp:ListItem>
                            </asp:RadioButtonList>
                            <<asp:TextBox ID="tbHydration" runat="server" Width="92px"></asp:TextBox>
                        </asp:WizardStep>
                        <asp:WizardStep runat="server" title="Step 5">
                            <asp:RaidioButtonList ID="rblSleepTracking" runat="server">
                                <asp:ListItem Value="1">I want to track sleep</asp:ListItem>
                                <asp:ListItem Value="0">I do not want to track sleep</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:TextBox ID="tbSleep" runat="server" Width="92px"></asp:TextBox>
                        </asp:WizardStep>
                        
                    </WizardSteps>
                </asp:Wizard>
                
            </ContentTemplate>
        </asp:UpdatePanel>
    </section>
</asp:Content>

