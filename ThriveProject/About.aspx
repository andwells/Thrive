<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="About.aspx.cs" Inherits="About" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Title %>.</h1>
        <h2>Your app description page.</h2>
    </hgroup>

    <article>
        <p>        
            Obesity is rampant in America and many people desire a healthy weight. However, some people struggle to achieve their weight management goals. Our vision at Thrive Technologies is to help people realize a healthy weight management is achievable through the aid of our software. Our software will provide simple tools for weight management standards, such as caloric intake and exercise to assist the user in achieving their personal weight management objectives. These tools facilitate the process of tracking their progress to insure success in reaching these goals. The focal points of our software will be on informing users of their recorded data through visual representations, motivating them by showing that their ambitions are achievable, and tracking progress as they work towards their goals.
        </p>

        <p>        
            Use this area to provide additional information.
        </p>

        <p>        
            Use this area to provide additional information.
        </p>
    </article>

    <aside>
        <h3>Aside Title</h3>
        <p>        
            Use this area to provide additional information.
        </p>
        <ul>
            <li><a runat="server" href="~/">Home</a></li>
            <li><a runat="server" href="~/About">About</a></li>
            <li><a runat="server" href="~/Contact">Contact</a></li>
        </ul>
    </aside>
</asp:Content>
