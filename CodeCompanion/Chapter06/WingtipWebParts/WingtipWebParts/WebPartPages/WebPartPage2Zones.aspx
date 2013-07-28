﻿<%@ Assembly Name="Microsoft.SharePoint,Version=14.0.0.0,Culture=neutral,PublicKeyToken=71e9bce111e9429c" %> 
         
<%@ Page language="C#" MasterPageFile="~masterurl/default.master" 
         Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage"  %> 
         
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" 
             Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<asp:Content ContentPlaceHolderId="PlaceHolderAdditionalPageHead" runat="server">
    <link href="styles/styles.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ContentPlaceHolderId="PlaceHolderMain" runat="server">

  <div id="Main" >
    <table style="Width:100%" >
      <tr>
        <td style="Width:50%;vertical-align:top" >
          <WebPartPages:WebPartZone 
            ID="Left" runat="server" 
            Title="Main Web Part Zone"
            FrameType="TitleBarOnly" />
        </td>
        <td style="Width:50%;vertical-align:top" >
          <WebPartPages:WebPartZone 
            ID="Right" runat="server" 
            Title="Right Web Part Zone"
            FrameType="TitleBarOnly" />
        </td>
      <tr>
    </table>
  </div>

</asp:Content>