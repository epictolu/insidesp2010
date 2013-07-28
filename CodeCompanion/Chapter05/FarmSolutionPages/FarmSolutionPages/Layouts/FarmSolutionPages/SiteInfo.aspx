<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SiteInfo.aspx.cs" Inherits="FarmSolutionPages.SiteInfo" DynamicMasterPageFile="~masterurl/default.master" %>


<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
  Wingtip Site Info
</asp:Content>

<asp:Content ID="Content1" contentplaceholderid="PlaceHolderPageTitleInTitleArea" runat="server">
	<a href="/_layouts/settings.aspx">Site Settings</a>&#32;
  <SharePoint:ClusteredDirectionalSeparatorArrow ID="arrow1" runat="server" />&#32;
  Wingtip Site Info
</asp:Content>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
  <style type="text/css">
    #MSO_ContentTable{ padding-left:10px; }    
    .gridTable {margin-left: 16px;font-size: 10pt;min-width: 480px;font-size:8pt;width:680px;}
    .gridTable, .gridHeaderRow th, .gridRow td, .gridAlternatingRow td {border: 1px solid black;}
    .gridHeaderRow th {background-color: Black;color: White;text-align: left;padding: 2px;padding-right: 16px;}
    .gridRow td {padding: 4px;}
    .gridAlternatingRow td {padding: 4px;background-color: #EEEEEE;}
    hr {margin-top: 20px;border-width: 2px;border-color: #DDDDDD;}
    .DisplayTable {padding: 4px;margin-left: 16px;border: solid 2px black;background-color: #DDDDDD;}
    .HeaderCell {vertical-align: top;color: White;background-color: black;padding: 4px;}
    .BodyCell {vertical-align: top;background-color: White;padding: 4px;border: solid 1 black;empty-cells: show;}
  </style>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">

  <h1>Wingtip Site Info</h1>    
  
  <div>
    <asp:Button ID="cmdReturn" runat="server" Text="Return to Site Settings" />
  </div>


  <div style="margin-bottom:8px;">
   <h2>Site Lists</h2>
    <asp:GridView ID="grdLists" runat="server" AutoGenerateColumns="true" CssClass="gridTable" >
      <HeaderStyle CssClass="gridHeaderRow" />
      <RowStyle CssClass="gridRow" />
      <AlternatingRowStyle CssClass="gridAlternatingRow" />
    </asp:GridView>
  </div>

  <h2>Hidden Lists</h2>
  <div id="divSystemLists" style="margin-bottom:8px;">
    <asp:GridView ID="grdSystemLists" runat="server" AutoGenerateColumns="true" CssClass="gridTable" >
      <HeaderStyle CssClass="gridHeaderRow" />
      <RowStyle CssClass="gridRow" />
      <AlternatingRowStyle CssClass="gridAlternatingRow" />
    </asp:GridView>
  </div>

</asp:Content>

<asp:Content ID="TitleBreadcrumb" contentplaceholderid="PlaceHolderTitleBreadcrumb" runat="server">

		<SharePoint:ListSiteMapPath ID="ListSiteMapPath1"
			runat="server"
			SiteMapProviders="SPSiteMapProvider,SPXmlContentMapProvider"
			RenderCurrentNodeAsLink="false"
			PathSeparator=""
			CssClass="s4-breadcrumb"
			NodeStyle-CssClass="s4-breadcrumbNode"
			CurrentNodeStyle-CssClass="s4-breadcrumbCurrentNode"
			RootNodeStyle-CssClass="s4-breadcrumbRootNode"
			HideInteriorRootNodes="true"
			SkipLinkText="" />    

</asp:Content>