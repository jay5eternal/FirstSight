<%@ Page Language="C#" AutoEventWireup="true" CodeFile="colortest.aspx.cs" Inherits="colortest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
<form id="form1" runat="server">
        <div style="margin-left: 50px; margin-top: 50px;">
            <table>
                <tr>
                   <td>
                        <asp:DropDownList ID="ddlMultiColor" 
                        OnSelectedIndexChanged="ddlMultiColor_OnSelectedIndexChanged"
                            runat="server" AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                  <td>
                       <div id="msgColor" runat="server">
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        </form>
    </body>
</html>
