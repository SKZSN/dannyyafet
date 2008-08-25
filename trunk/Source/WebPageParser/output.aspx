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
            left: 34px; position: absolute; top: 38px" Width="346px" Font-Size="Large"></asp:Label>
        <asp:TextBox ID="URLTextBox" runat="server" BorderColor="White" ForeColor="Black"
            Height="17px" MaxLength="100" Rows="1" Style="z-index: 101; left: 34px; position: absolute;
            top: 80px" TabIndex="1" ToolTip="Enter a URL's Name to be parsed" Width="206px">http://</asp:TextBox>
        <asp:Button ID="parseButton" runat="server" Height="24px" OnClick="parseButton_Click"
            Style="z-index: 102; left: 256px; position: absolute; top: 79px" TabIndex="2"
            Text="Parse URL" ToolTip="Parsing a URL given for images and texts" Width="98px" />
        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" Style="z-index: 103; left: 34px;
            position: absolute; top: 121px" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="Gainsboro" />
            <Columns>
                <asp:BoundField DataField="webPage" HeaderText="webPage" SortExpression="webPage" />
                <asp:BoundField DataField="element" HeaderText="element" SortExpression="element" />
                <asp:BoundField DataField="sub" HeaderText="sub" SortExpression="sub" />
                <asp:BoundField DataField="type" HeaderText="type" SortExpression="type" />
                <asp:BoundField DataField="tag" HeaderText="tag" SortExpression="tag" />
                <asp:BoundField DataField="loc" HeaderText="loc" ReadOnly="True" SortExpression="loc" />
                <asp:ImageField DataImageUrlField="pictureUrl" HeaderText="Display">
                </asp:ImageField>
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConflictDetection="CompareAllValues"
            ConnectionString="<%$ ConnectionStrings:ConnectionString %>" DeleteCommand="DELETE FROM [imageTable] WHERE [loc] = @original_loc AND [webPage] = @original_webPage AND [element] = @original_element AND [sub] = @original_sub AND [type] = @original_type AND [tag] = @original_tag AND [pictureUrl] = @original_pictureUrl"
            InsertCommand="INSERT INTO [imageTable] ([webPage], [element], [sub], [type], [tag], [loc], [pictureUrl]) VALUES (@webPage, @element, @sub, @type, @tag, @loc, @pictureUrl)"
            OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT [webPage], [element], [sub], [type], [tag], [loc], [pictureUrl] FROM [imageTable]"
            UpdateCommand="UPDATE [imageTable] SET [webPage] = @webPage, [element] = @element, [sub] = @sub, [type] = @type, [tag] = @tag, [pictureUrl] = @pictureUrl WHERE [loc] = @original_loc AND [webPage] = @original_webPage AND [element] = @original_element AND [sub] = @original_sub AND [type] = @original_type AND [tag] = @original_tag AND [pictureUrl] = @original_pictureUrl">
            <DeleteParameters>
                <asp:Parameter Name="original_loc" Type="Int32" />
                <asp:Parameter Name="original_webPage" Type="String" />
                <asp:Parameter Name="original_element" Type="Int32" />
                <asp:Parameter Name="original_sub" Type="String" />
                <asp:Parameter Name="original_type" Type="String" />
                <asp:Parameter Name="original_tag" Type="String" />
                <asp:Parameter Name="original_pictureUrl" Type="String" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="webPage" Type="String" />
                <asp:Parameter Name="element" Type="Int32" />
                <asp:Parameter Name="sub" Type="String" />
                <asp:Parameter Name="type" Type="String" />
                <asp:Parameter Name="tag" Type="String" />
                <asp:Parameter Name="pictureUrl" Type="String" />
                <asp:Parameter Name="original_loc" Type="Int32" />
                <asp:Parameter Name="original_webPage" Type="String" />
                <asp:Parameter Name="original_element" Type="Int32" />
                <asp:Parameter Name="original_sub" Type="String" />
                <asp:Parameter Name="original_type" Type="String" />
                <asp:Parameter Name="original_tag" Type="String" />
                <asp:Parameter Name="original_pictureUrl" Type="String" />
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="webPage" Type="String" />
                <asp:Parameter Name="element" Type="Int32" />
                <asp:Parameter Name="sub" Type="String" />
                <asp:Parameter Name="type" Type="String" />
                <asp:Parameter Name="tag" Type="String" />
                <asp:Parameter Name="loc" Type="Int32" />
                <asp:Parameter Name="pictureUrl" Type="String" />
            </InsertParameters>
        </asp:SqlDataSource>
    </div>
    </form>
</body>
</html>
