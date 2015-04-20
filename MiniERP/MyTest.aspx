<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyTest.aspx.cs" Inherits="MiniERP.MyTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
   <%-- <p id="upload-area">
        <input id="File1" type="file" runat="server" />
    </p>
    <input id="AddFile" type="button" value="Attach More File" onclick="addFileUploadBox()" />
    <p>
        <asp:Button ID="btnSubmit" runat="server" Text="Upload Now" OnClick="btnSubmit_Click" /></p>--%>
    <span id="Span1" runat="server" />
    <script type="text/javascript">
        function addFileUploadBox() {
            if (!document.getElementById || !document.createElement)
                return false;

            var uploadArea = document.getElementById("upload-area");

            if (!uploadArea)
                return;

            var newLine = document.createElement("br");
            uploadArea.appendChild(newLine);

            var newUploadBox = document.createElement("input");

            // Set up the new input for file uploads 
            newUploadBox.type = "file";
          //  newUploadBox.size = "60";

            // The new box needs a name and an ID 
            if (!addFileUploadBox.lastAssignedId)
                addFileUploadBox.lastAssignedId = 100;

            newUploadBox.setAttribute("id", "dynamic" + addFileUploadBox.lastAssignedId);
            newUploadBox.setAttribute("name", "dynamic:" + addFileUploadBox.lastAssignedId);
            uploadArea.appendChild(newUploadBox);
            addFileUploadBox.lastAssignedId++;
        } 
    </script>

    <div>
    <asp:FileUpload ID="FileUpld" runat="server" ondatabinding="FileUpld_DataBinding" />
    <asp:Button ID="btnUpload" runat="server"  Text="Upload" onclick="btnUpload_Click" />
    </div>
    </form>
</body>
</html>
