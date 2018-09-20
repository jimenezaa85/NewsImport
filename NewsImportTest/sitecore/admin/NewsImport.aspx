<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewsImport.aspx.cs" Inherits="NewsImportTest.sitecore.admin.NewsImport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Button ID="btnLoadXml" runat="server" Text="btnLoad" OnClick="btnLoadXml_Click" />
        <br /><br />
        <asp:Button ID="btnLocal" runat="server" Text="File Service local" OnClick="btnLocal_Click" />
        <div>
        </div>
        <br />
        <br />
        <div class="container" style="width:50%">
            <div style="float:left">
                <asp:Label ID="lblStartDate" runat="server" Text="Start Date:"></asp:Label>
                <asp:Calendar ID="startDate" runat="server"></asp:Calendar>
            </div>

            <div style="float: right">
                <asp:Label ID="lblEndDate" runat="server" Text="End Date:"></asp:Label>
                <asp:Calendar ID="endDate" runat="server"></asp:Calendar>
            </div>
            <div style="clear:both"></div>
            <asp:Label ID="lblLog" runat="server" Text="Welcome..."></asp:Label>
        </div>
    </form>
</body>
</html>
