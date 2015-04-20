<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="MiniERP.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Styles/Login.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <img alt="full screen background image" src="Images/login.jpg" id="full-screen-background-image" />
    <div class="wrap">
        <div id="content">
            <div id="main">
                <div class="full_w">
                    <form id="Form2" runat="server">
                    <asp:Login ID="loginControl" runat="server" LoginButtonStyle-CssClass="button" TextBoxStyle-CssClass="text"
                        TitleText="" onauthenticate="loginControl_Authenticate">
                        <checkboxstyle cssclass="chkbox" />
                    </asp:Login>
                   <%-- <div class="DivChangePass" style=">
                        <asp:LinkButton ID="lnkChangePass" runat="server" CssClass="ChangePass" 
                            onclick="lnkChangePass_Click">Change Password</asp:LinkButton>
                    </div>--%>
                    </form>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
