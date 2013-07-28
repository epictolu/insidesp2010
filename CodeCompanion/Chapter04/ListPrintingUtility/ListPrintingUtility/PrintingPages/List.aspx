<%@ Page Language="C#" %>

<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
  Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages"
  Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd" >
<html>

<head>
  <title>List Printing Utility</title>
  <sharepoint:csslink id="core_css" runat="server" alternate="false" />
  <sharepoint:scriptlink id="core_js" language="javascript" name="core.js" runat="server" />
  <sharepoint:scriptlink id="sp" language="javascript" name="sp.js" runat="server"
    loadafterui="true" localizable="false" />
  <script type="text/javascript" src="List.js"></script>
  <link href="ListStyles.css" rel="stylesheet" type="text/css" />
  <link href="ListStylesPrinting.css" rel="stylesheet" type="text/css" media="print" />
  <script type="text/javascript" language="javascript">

    function OnItem(obj) { }
    function OnChildItem(obj) { }
  
  </script>
</head>

<body onload="initPage();">
  <form id="Form1" runat="server">
  <asp:scriptmanager id="ScriptManager" runat="server" />

 
  <div id="printBody">

    <div id="toolbar">
      <a class="ms-linkitem" href="javascript:window.print();">
        print&nbsp;<img src="/_layouts/images/hprint.png" alt="Print" style="border: none" />
      </a>
    </div>

    <div id="listTitle" >&nbsp;</div>
    
    <div id="listTable" >
      <img id="imgGears" alt="x" src="_layouts/images/GEARS_AN.GIF" />
    </div>
  
  </div>
  
  <div id="tray">
    <sharepoint:formdigest runat="server" />
  </div>
  </form>
</body>
</html>
