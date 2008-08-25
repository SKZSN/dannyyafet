<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="URLTextBox" runat="server" BorderColor="White" Style="z-index: 100;
            left: 386px; position: absolute; top: 207px" TabIndex="1" Width="206px" ForeColor="Black" MaxLength="100" Rows="1" ToolTip="Enter a URL's Name to be parsed" Height="15px">http://</asp:TextBox>
        <asp:Button ID="parseButton" runat="server" Style="z-index: 102; left: 443px; position: absolute;
            top: 231px" TabIndex="2" Text="Parse URL" ToolTip="Parsing a URL given for images and texts" Height="24px" Width="98px" OnClick="parseButton_Click" />
    </div>
    </form>
</body>
</html>
