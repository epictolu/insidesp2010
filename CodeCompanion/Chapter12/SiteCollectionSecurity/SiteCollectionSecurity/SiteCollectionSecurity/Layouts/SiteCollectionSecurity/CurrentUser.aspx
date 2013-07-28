<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CurrentUser.aspx.cs" Inherits="SiteCollectionSecurity.Layouts.CurrentUser" DynamicMasterPageFile="~masterurl/default.master" %>


<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
	    <link href="styles.css" rel="stylesheet" type="text/css"/>
</asp:Content>


<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
  <h2>Current User Info</h2>
  <asp:Label ID="lblPageStatus" runat="server" Visible="false" />
  
  <h4>From Windows Security Context</h4>
  <asp:GridView ID="grdCurrentUserInfoWindows" runat="server" AutoGenerateColumns="true" CssClass="gridTable"  >
    <HeaderStyle CssClass="gridHeaderRow" />
    <RowStyle CssClass="gridRow" />
    <AlternatingRowStyle CssClass="gridAlternatingRow" />
  </asp:GridView>
  
  <h4>From ASP.NET Security Context</h4>
  <asp:GridView ID="grdCurrentUserInfoAspnet" runat="server" AutoGenerateColumns="true" CssClass="gridTable"  >
    <HeaderStyle CssClass="gridHeaderRow" />
    <RowStyle CssClass="gridRow" />
    <AlternatingRowStyle CssClass="gridAlternatingRow" />
  </asp:GridView>
  
  <h4>From SharePoint Security Context</h4>
  <asp:GridView ID="grdCurrentUserInfoSharePoint" runat="server" AutoGenerateColumns="true" CssClass="gridTable"  >
    <HeaderStyle CssClass="gridHeaderRow" />
    <RowStyle CssClass="gridRow" />
    <AlternatingRowStyle CssClass="gridAlternatingRow" />
  </asp:GridView>
  
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
  Current User Info from Available Security Context
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
  Current User Info from Available Security Context
</asp:Content>
