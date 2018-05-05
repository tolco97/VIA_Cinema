<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PaymentPage.aspx.cs" Inherits="WebApplication_VIA_Cinema.PaymentPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Payment Form</title>
</head>
<body>
<form id="form1" runat="server">
    <div>
        <asp:Label ID="creditCardNumberLbl" runat="server" Text="Enter Credit Card Number"></asp:Label>
    </div>
    <asp:TextBox ID="creditCardTextBox" runat="server" TextMode="Password"></asp:TextBox>
    <p>
        <asp:Label ID="pinLabel" runat="server" Text="Enter Credit Card Pin"></asp:Label>
    </p>
    <asp:TextBox ID="pinTextBox"
                 TextMode="Password" runat="server">
    </asp:TextBox>
    <p>
        <asp:Button ID="payButton" runat="server" OnClick="PayButtonOnClick" Text="Pay" />
    </p>
</form>
</body>
</html>