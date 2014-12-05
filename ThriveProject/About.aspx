<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Contact.aspx.cs" Inherits="Contact" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Title %>.</h1>
        <h2>Contact Thrive</h2>
    </hgroup>

    <section class="contact">
        <header>
            <h3>Phone:</h3>
        </header>
        <p>
            <span class="label">Main:</span>
            <span>918.290.1330</span>
        </p>
        <p>
            <span class="label">After Hours:</span>
            <span>229.237.2660</span>
        </p>
    </section>

    <section class="contact">
        <header>
            <h3>Email:</h3>
        </header>
        <p>
            <span class="label">Support:</span>
            <span><a href="mailto:wladams@valdosta.com">wladams@valdosta.com</a></span>
        </p>
        <p>
            <span class="label">Marketing:</span>
            <span><a href="mailto:drgully@valdosta.com">drgully@valdosta.com</a></span>
        </p>
        <p>
            <span class="label">General:</span>
            <span><a href="mailto:andwells@valdosta.com">andwells@valdosta.com</a></span>
        </p>
    </section>

    <section class="contact">
        <header>
            <h3>Address:</h3>
        </header>
        <p>
            One Microsoft Way<br />
            Redmond, WA 98052-6399
        </p>
    </section>
</asp:Content>
