<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AllMoviesPage.aspx.cs" Inherits="WebApplication_VIA_Cinema.AllMovies" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Movies</title>
</head>
<body style="height: 475px; width: 1024px">
<form id="form1" runat="server">
    <div>
        <asp:Label ID="moviesLabel" runat="server" Text="Select a Movie:"></asp:Label>
        <asp:DropDownList ID="movieDropdownMenu" AutoPostBack="True" runat="server" Height="68px" OnSelectedIndexChanged="MovieDropdownSelectedIndexChanged" Width="1026px">
        </asp:DropDownList>
    </div>
    <p>
        <asp:Table ID="projectionTable" runat="server" Height="27px" Width="1019px">
        </asp:Table>
    </p>
    <p>
        &nbsp;
    </p>
</form>
</body>
</html>