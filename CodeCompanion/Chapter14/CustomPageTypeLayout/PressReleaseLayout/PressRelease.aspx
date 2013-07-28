<%@ Page language="C#" Inherits="Microsoft.SharePoint.Publishing.PublishingLayoutPage, Microsoft.SharePoint.Publishing, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" Namespace="Microsoft.SharePoint.WebControls" TagPrefix="SharePointWebControls" %>
<%@ Register Assembly="Microsoft.SharePoint.Publishing, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" Namespace="Microsoft.SharePoint.Publishing.WebControls" TagPrefix="PublishingWebControls" %>

<asp:Content runat="server" contentplaceholderid="PlaceHolderPageTitle">
  <SharePointWebControls:FieldValue id="PageTitle" FieldName="Title" runat="server"/>
</asp:Content>

<asp:Content runat="server" contentplaceholderid="PlaceHolderPageTitleInTitleArea">
  <SharePointWebControls:TextField runat="server" id="TitleField" FieldName="Title"/>
</asp:Content>

<asp:Content runat="server" contentplaceholderid="PlaceHolderMain">
  <SharePointWebControls:TextField ID="TextField1" FieldName="PRByLine" runat="server"/>
  <br />
  <PublishingWebControls:RichHtmlField ID="RichHtmlField1" FieldName="PRBody" runat="server"/>
</asp:Content>