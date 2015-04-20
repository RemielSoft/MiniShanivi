<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileDownload.aspx.cs" Inherits="MiniERP.Quotation.Parts.FileDownload"
    Title="DownloadedFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DownLoadFile</title>
    <script type="text/javascript" language="javascript">
        function myOpen() {
            window.open("FileDownload.aspx", "mywindow", "location=no,titlebar=yes,status=yes,scrollbars=yes,menubar=yes,toolbar=no,directories=yes,resizable=yes,copyhistory=yes,width=300,height=500 ");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <%--    <asp:Image ID="img_notFound" runat="server" ImageUrl="~/Images/FileNotFound.jpg" Visible="false"/>--%>
    </div>
    </form>
</body>
</html>
