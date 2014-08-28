<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="geetest.aspx.vb" Inherits="Geetest_VBSDK.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server" method="post" style="margin-top:100px;">
    <div>
        <table>
       <tr>
          <td>用户名</td><td><input type="text"/></td>
       </tr>
       <tr>
          <td>密码</td><td><input type="password"/></td>
       </tr>
       <tr>
          <td colspan="2"><script type="text/javascript" src="http://api.geetest.com/get.php?gt=eec109e39008039b1f59dc812b55988d"></script></td>
       </tr>
       <tr>
          <td>
              
              <asp:Button ID="Button1" runat="server" Text="Login" />
              
          </td>
       </tr>
    </table>
    </div>
    </form>
</body>
</html>
