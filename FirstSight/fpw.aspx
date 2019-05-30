<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fpw.aspx.cs" Inherits="fpw" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="Button1" runat="server" Text="Button" onmousedown="mouseDown()" onmouseup="mouseUp()"/>
            
            <asp:Label ID="Label1" runat="server" Text="Label" onmousedown="mouseDown()" onmouseup="mouseUp()"></asp:Label>
        </div>
    </form>

    <script>
        var last = 0;
        var now = 0;

        function mouseDown() {
            last = new Date();
        }

        function mouseUp() {
            now = new Date();
            if (now - last > 2 * 1000) {
                alert('ZZZZ');
            }
        }
    </script>

</body>
</html>
