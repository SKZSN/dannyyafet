<%@ Page Language="C#" AutoEventWireup="true" CodeFile="output.aspx.cs" Inherits="output" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Height="24px" Style="z-index: 100;
            left: 326px; position: absolute; top: 177px" Width="346px" Font-Size="Large"></asp:Label>
        <asp:TextBox ID="URLTextBox" runat="server" BorderColor="White" ForeColor="Black"
            Height="17px" MaxLength="100" Rows="1" Style="z-index: 101; left: 326px; position: absolute;
            top: 230px" TabIndex="1" ToolTip="Enter a URL's Name to be parsed" Width="206px">http://</asp:TextBox>
        <asp:Button ID="parseButton" runat="server" Height="24px" OnClick="parseButton_Click"
            Style="z-index: 102; left: 549px; position: absolute; top: 229px" TabIndex="2"
            Text="Parse URL" ToolTip="Parsing a URL given for images and texts" Width="98px" />
    </div>
    </form>
</body>
</html>
