<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserControlDemo.aspx.cs" Inherits="WingtipControls.Layouts.WingtipControls.UserControlDemo" DynamicMasterPageFile="~masterurl/default.master" %>
<%@ Register TagPrefix="wtuc" TagName="WingtipUserControl" src="~/_controltemplates/WingtipControls/WingtipUserControl.ascx" %>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
  <wtuc:WingtipUserControl runat="server" />
</asp:Content>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
  <link href="/_layouts/WingtipControls/styles.css" rel="stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
  Wingtip User Control Demo
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
  Wingtip User Control Demo
</asp:Content>
