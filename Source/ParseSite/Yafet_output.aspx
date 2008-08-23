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
            left: 281px; position: absolute; top: 114px" Width="346px" Font-Size="Large"></asp:Label>
        <asp:TextBox ID="URLTextBox" runat="server" BorderColor="White" ForeColor="Black"
            Height="17px" MaxLength="100" Rows="1" Style="z-index: 101; left: 281px; position: absolute;
            top: 542px" TabIndex="1" ToolTip="Enter a URL's Name to be parsed" Width="206px">http://</asp:TextBox>
        <asp:Button ID="parseButton" runat="server" Height="24px" OnClick="parseButton_Click"
            Style="z-index: 102; left: 511px; position: absolute; top: 541px" TabIndex="2"
            Text="Parse URL" ToolTip="Parsing a URL given for images and texts" Width="98px" />
        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" Style="z-index: 104; left: 328px;
            position: absolute; top: 210px" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="Gainsboro" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
