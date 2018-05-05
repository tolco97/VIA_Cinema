<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="WebApplication_VIA_Cinema.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
</head>
<body>
<form id="form1" runat="server">
    <div>
    </div>
    <p>
        <asp:Label ID="emailLabel" runat="server" Text="Email"></asp:Label>
    </p>
    <p>
        <asp:TextBox ID="emailTextField" runat="server"></asp:TextBox>
    </p>
    <p>
        &nbsp;<asp:Label ID="passwordLabel" runat="server" Text="Password"></asp:Label>
    </p>
    <p style="height: 37px">
        &nbsp;<asp:TextBox runat="server" TextMode="Password" ID="passwordTextField"></asp:TextBox>
    </p>
    <p style="height: 37px">
        <asp:Button ID="loginButton" runat="server" OnClick="LoginButtonOnClick" Text="Login"/>
    </p>
</form>
</body>
</html>