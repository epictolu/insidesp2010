<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
  Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateNewDocument.aspx.cs"
  Inherits="WingtipDocuments.Layouts.WingtipDocuments.CreateNewDocument" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
  <style type="text/css" >
    #MSO_ContentTable {
      padding: 8px;
    }
    #MainTable {
      border: 1px solid #CCCCCC;
      background-color: #EEEEEE;
      padding-bottom: 4px;
    }
    #MainTable td {
      vertical-align: top;
      padding: 2px;
    }
  </style>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">

  <table id="MainTable" >
    <tr>
      <td style="width:160px;" >
        Document Library:
      </td>
      <td>
        <asp:DropDownList ID="lstTargetLibrary" runat="server" />
      </td>
    </tr>
    <tr>
      <td>
        Document Name:
      </td>
      <td>
        <asp:TextBox ID="txtDocumentName" runat="server" Width="200px" />
        &nbsp;(do not include extention)
      </td>
    </tr>
    <tr>
      <td>
        Document Content:
      </td>
      <td>
        <asp:TextBox ID="txtDocumentContent" runat="server" Width="400px" 
                     TextMode="MultiLine" Columns="8" />
      </td>
    </tr>
    <tr>
      <td>
        Document Type:
      </td>
      <td>
        <asp:RadioButtonList ID="lstDocumentTypes" runat="server">
          <asp:ListItem Text="Text Document (*.txt)" Value="Text" Selected="True" />
          <asp:ListItem Text="XML Document (*.xml)" Value="Xml" />
          <asp:ListItem Text="Microsoft Word Document (*.docx)" Value="WordDocument" />
        </asp:RadioButtonList>
      </td>
    </tr>
    <tr>
      <td colspan="2" >
        <asp:Button ID="cmdCreateDocument" runat="server" Text="Create Document" />
      </td>      
    </tr>
  </table>
  
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
  Create New Document
</asp:Content>
<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea"
  runat="server">
  Create New Document
</asp:Content>
