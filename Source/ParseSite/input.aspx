<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="input.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Parsing Website WebApp</title>
</head>
<body>
    <form id="InputForm" runat="server">
    <div>
        &nbsp;&nbsp;
        <div style="left: 348px; width: 273px; position: absolute; top: 272px; height: 38px">
            &nbsp;<asp:TextBox ID="InputTB" runat="server" Width="253px"></asp:TextBox>&nbsp;<br />
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            <asp:Button ID="ParseBtn" runat="server" Text="Parse Website" OnClick="ParseBtn_Click" Width="109px" /></div>
    </div>
    </form>
</body>
</html>
