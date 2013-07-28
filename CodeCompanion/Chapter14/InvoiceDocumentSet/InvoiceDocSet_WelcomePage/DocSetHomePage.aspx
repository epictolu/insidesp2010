<%@ Page language="C#" MasterPageFile="~masterurl/default.master" AutoEventWireup="true" Inherits="InvoiceDocumentSet.WelcomePage.DocSetHomePage, $SharePoint.Project.AssemblyFullName$" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> <%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> <%@ Import Namespace="Microsoft.SharePoint" %> <%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="OfficeServer" Namespace="Microsoft.Office.Server.WebControls" Assembly="Microsoft.Office.DocumentManagement, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<asp:Content ContentPlaceHolderId="PlaceHolderPageTitle" runat="server">
	<SharePoint:EncodedLiteral runat="server" text="<%$Resources:dlcdm, DocSetHomepage_Title%>" EncodeMethod='HtmlEncode'/>
</asp:Content>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
	<span id="idParentListName">&#160;</span>
	<span id="idMiddleSection">
		<span id="idTitleSeparator">
			<SharePoint:ClusteredDirectionalSeparatorArrow runat="server" />
		</span>
		<span id="idParentFolderName">&#160;</span>
	</span>
	<SharePoint:ClusteredDirectionalSeparatorArrow runat="server" />
	<span id="idDocsetName">&#160;</span>
</asp:Content>
<asp:Content ContentPlaceholderID="PlaceHolderMain" runat="server">
	<OfficeServer:DocSetWelcomePageControl runat="server" ID="idDocSet" />
	<table width="100%">
		<tr>
			<td width="15%" valign="top">
				<WebPartPages:WebPartZone runat="server" PartChromeType="None" id="WebPartZone_TopLeft" LayoutOrientation="vertical" AllowPersonalization="false" AllowCustomization="true">
					<ZoneTemplate></ZoneTemplate>
				</WebPartPages:WebPartZone>
                <div style="font-weight:bold; font-size:12pt; color:Maroon; text-align:center;">
                    <asp:Label id="DiscountMessage" runat="server" />
                </div>
			</td>
			<td width="85%" valign="top">
				<WebPartPages:WebPartZone runat="server" PartChromeType="None" id="WebPartZone_Top" LayoutOrientation="vertical" AllowPersonalization="false" AllowCustomization="true">
					<ZoneTemplate></ZoneTemplate>
				</WebPartPages:WebPartZone>
			</td>
		</tr>
	</table>
	<table width="100%">
		<tr>
			<td>
				<WebPartPages:WebPartZone runat="server" PartChromeType="None" id="WebPartZone_CenterMain" LayoutOrientation="vertical" AllowPersonalization="false" AllowCustomization="true">
					<ZoneTemplate></ZoneTemplate>
				</WebPartPages:WebPartZone>
			</td>
		</tr>
	</table>
	<table width="100%">
		<tr>
			<td>
				<WebPartPages:WebPartZone runat="server" PartChromeType="None" id="WebPartZone_Bottom" LayoutOrientation="vertical" AllowPersonalization="false" AllowCustomization="true">
					<ZoneTemplate></ZoneTemplate>
				</WebPartPages:WebPartZone>
			</td>
		</tr>
	</table>
</asp:Content>