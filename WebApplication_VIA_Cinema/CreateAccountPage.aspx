<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateAccountPage.aspx.cs" Inherits="WebApplication_VIA_Cinema.CreateAccountPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Create new Account</title>
    <style type="text/css">
        #form1 { height: 427px; }
    </style>
</head>
<body>
<form id="form1" runat="server">
    <div>
        <asp:Label ID="emailLabel" runat="server" Text="Enter Email"></asp:Label>
    </div>
    <div>
        <asp:TextBox ID="emailTextBox" runat="server"></asp:TextBox>
    </div>
    <p style="width: 165px">
        <asp:Label ID="passwordLabel" runat="server" Text="Enter Password"></asp:Label>
    </p>
    <div>
        <asp:TextBox ID="passwordTextBox" runat="server" TextMode="Password" style="margin-top: 0px"></asp:TextBox>
    </div>
    <div>
        <asp:Label ID="firstNameLabel" runat="server" Text="Enter First name"></asp:Label>
    </div>
    <div>
        <asp:TextBox ID="firstNameTextBox" runat="server"></asp:TextBox>
    </div>
    <div style="width: 165px">
        <asp:Label ID="lastNameLabel" runat="server" Text="Enter Last name"></asp:Label>
    </div>
    <div>
        <asp:TextBox ID="lastNameTextBox" runat="server"></asp:TextBox>
    </div>
    <div>
        <asp:Label ID="birthdayLabel" runat="server" Text="Enter Birthday in format: {day/month/year}"></asp:Label>
    </div>
    <div>
        <asp:TextBox ID="birthdayTextBox" runat="server"></asp:TextBox>
    </div>
    <div>
        <asp:Button ID="createAccountButton" runat="server" Text="Create Account" OnClick="CreateAccountButtonOnClick"/>
    </div>
    <div>
        <asp:Label ID="statusLabel" runat="server" Text=" "></asp:Label>
    </div>
</form>
</body>
</html>