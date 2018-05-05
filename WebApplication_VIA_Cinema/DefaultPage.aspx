<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DefaultPage.aspx.cs" Inherits="WebApplication_VIA_Cinema.Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Button ID="loginButton" runat="server" Height="54px" OnClick="LoginButtonOnClick" Text="Login" Width="116px"/>

    <asp:Button ID="logoutButton" runat="server" Height="54px" Text="Logout" Width="111px" OnClick="LogoutOnClick"/>

    <asp:Button ID="allMoviesButton" runat="server" Height="54px" Text="All Movies" Width="111px" OnClick="AllMoviesButtonOnClick"/>

    <asp:Button ID="createAccountButton" runat="server" Height="54px" Text="Register Account" Width="158px" OnClick="RegisterAccountOnClick"/>

    <asp:Label ID="isLoggedInLabel" runat="server" Text="You are not logged in!" ForeColor="red"></asp:Label>

</asp:Content>