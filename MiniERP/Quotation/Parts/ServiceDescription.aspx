<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ServiceDescription.aspx.cs"
    Inherits="MiniERP.Quotation.Parts.ServiceDescription" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Admin/Parts/ServiceDetail.ascx" TagName="serviceDetail" TagPrefix="uc_sd" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Service Details</title>
    <link href="../../Styles/layout.css" rel="stylesheet" type="text/css" />
    <%--<link href="../Styles/MessageBox.css" rel="stylesheet" type="text/css"/>--%>
    <!--[if lt IE 9]>
	<link rel="stylesheet" href="css/ie.css" type="text/css" media="screen" />
	<script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
	<![endif]-->
    <script src="../../Scripts/jquery-1.5.2.min.js" type="text/javascript"></script>
    <script src="../../Scripts/hideshow.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.tablesorter.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../Scripts/jquery.equalHeight.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".tablesorter").tablesorter();
        }
	);
        $(document).ready(function () {

            //When page loads...
            $(".tab_content").hide(); //Hide all content
            $("ul.tabs li:first").addClass("active").show(); //Activate first tab
            $(".tab_content:first").show(); //Show first tab content

            //On Click Event
            $("ul.tabs li").click(function () {

                $("ul.tabs li").removeClass("active"); //Remove any "active" class
                $(this).addClass("active"); //Add "active" class to selected tab
                $(".tab_content").hide(); //Hide all tab content

                var activeTab = $(this).find("a").attr("href"); //Find the href attribute value to identify the active tab + content
                $(activeTab).fadeIn(); //Fade in the active ID content
                return false;
            });

        });
    </script>
    <script type="text/javascript">
        $(function () {
            $('.column').equalHeight();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <uc_sd:serviceDetail ID="uc_ServiceDetail" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
