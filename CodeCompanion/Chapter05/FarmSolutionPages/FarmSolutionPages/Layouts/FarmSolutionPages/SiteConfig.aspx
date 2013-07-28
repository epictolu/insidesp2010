<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SiteConfig.aspx.cs" Inherits="FarmSolutionPages.Layouts.FarmSolutionPages.SiteConfig" DynamicMasterPageFile="~masterurl/default.master" %>
<%@ Register TagPrefix="wssuc" TagName="InputFormSection" Src="~/_controltemplates/InputFormSection.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="InputFormControl" Src="~/_controltemplates/InputFormControl.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="ButtonSection" Src="~/_controltemplates/ButtonSection.ascx" %>
<%@ Register TagPrefix="wssawc" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>


<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
  Wingtip Site Configuration
</asp:Content>


<asp:Content ID="PageTitleInTitleArea" contentplaceholderid="PlaceHolderPageTitleInTitleArea" runat="server">
	<a href="/_layouts/settings.aspx">Site Settings</a>&#32;
  <SharePoint:ClusteredDirectionalSeparatorArrow ID="arrow1" runat="server" />&#32;
  Wingtip Site Configuration
</asp:Content>

<asp:Content ID="AdditionalPageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
  <SharePoint:CssRegistration ID="CssRegistration1" Name="forms.css" runat="server"/>
  <SharePoint:CssRegistration ID="CssRegistration2" Name="layouts.css" runat="server" />	
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">

<table class="propertysheet" border="0" cellspacing="0" cellpadding="0" id="SiteConfigFormTable">

    <wssuc:InputFormSection Title="Site Title" Description="Type a title for your Web site. The title is displayed on each page in the site." runat="server">
      <template_inputformcontrols>
		    <wssuc:InputFormControl LabelText="Title:" runat="server">
				<Template_Control>
			    <wssawc:InputFormTextBox ID="txtSiteTitle" class="ms-input" Columns="40" Runat="server" MaxLength=255 />
				</Template_Control>
			</wssuc:InputFormControl>			
	   </template_inputformcontrols>
    </wssuc:InputFormSection>

    <wssuc:InputFormSection Title="Site Description" Description="Type a description for your Web site. The description is displayed on the home page." runat="server">
      <template_inputformcontrols>
		    <wssuc:InputFormControl LabelText="Description:" runat="server">
				<Template_Control>
			    <wssawc:InputFormTextBox ID="txtSiteDescription" class="ms-input"  Runat="server" TextMode="MultiLine" Columns="40" Rows="3"/>
				</Template_Control>
			</wssuc:InputFormControl>			
	  </template_inputformcontrols>
  </wssuc:InputFormSection>

  <wssuc:ButtonSection runat="server" ID="btnsSaveCancel" ShowStandardCancelButton="false" ShowSectionLine="true" TopButtons="true" >
		<Template_Buttons>
			<asp:PlaceHolder ID="cmdUpdateSitePlaceHolder" runat="server">
				<asp:Button ID="cmdUpdateSite" runat="server" Text="Update Site" CssClass="ms-ButtonHeightWidth" />
			</asp:PlaceHolder>
			<asp:PlaceHolder ID="cmdCancelPlaceHolder" runat="server">
		    <asp:Button ID="cmdCancel" runat="server" Text="Cancel" CssClass="ms-ButtonHeightWidth"  />		
			</asp:PlaceHolder>
		</Template_Buttons>
	</wssuc:ButtonSection>

  </table>

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