<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomControlDemo.aspx.cs" Inherits="WingtipControls.Layouts.WingtipControls.CustomControlDemo" DynamicMasterPageFile="~masterurl/default.master" %>

<%@ Register Tagprefix="WingtipControls" 
             Namespace="WingtipControls" 
             Assembly="WingtipControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=ab9c89e9a3adf1f6" %>


<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">

  <WingtipControls:WingtipCustomControl 
    Id="WingtipControl1"
    UserGreeting="Hello SPWorld" 
    runat="server" />

</asp:Content>


<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
  <link href="/_layouts/WingtipControls/styles.css" rel="stylesheet" type="text/css" />
</asp:Content>


<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
  Wingtip Custom Control Demo
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
  Wingtip Custom Control Demo
</asp:Content>
