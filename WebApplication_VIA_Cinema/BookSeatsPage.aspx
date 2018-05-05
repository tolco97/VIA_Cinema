<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BookSeatsPage.aspx.cs" Inherits="WebApplication_VIA_Cinema.BookSeatsPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Book Seats</title>
</head>
<body>
<form id="form1" runat="server">
    <div>
        <asp:Label ID="projectionMovieNameLbl" runat="server" Text="Label"></asp:Label>
    </div>
    <asp:Label ID="projectionStartTimeLbl" runat="server" Text="Label"></asp:Label>
    <asp:CheckBoxList ID="seatNumCheckBoxList" runat="server">
    </asp:CheckBoxList>
    <p>
        &nbsp;
    </p>
    <p>
        <asp:Button ID="bookSeatsButton" runat="server" OnClick="BookSeatsButtonOnClick" Text="Book Seats"/>
    </p>
</form>
</body>
</html>