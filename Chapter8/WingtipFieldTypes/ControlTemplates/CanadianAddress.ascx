<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CanadianAddress.ascx.cs" Inherits="WingtipFieldTypes.ControlTemplates.CanadianAddress" %>

<SharePoint:RenderingTemplate ID="CanadianAddressRenderingTemplate" runat="server">
    <Template>
        <table class="ms-authoringcontrols">
            <tr>
                <td>Street:</td>
                <td><asp:TextBox ID="txtStreet" runat="server" Width="328px" /></td>
            </tr>
            <tr>
                <td>City:</td>
                <td><asp:TextBox ID="txtCity" runat="server" Width="328px" /></td>
            </tr>
            <tr>
                <td>State:</td>
                <td><asp:TextBox ID="txtState" runat="server" Width="120px" MaxLength="2" /></td>
            </tr>
            <tr>
                <td>Zip Code:</td>
                <td><asp:TextBox ID="txtZipCode" runat="server" Width="120px" MaxLength="6"/></td>
            </tr>
        </table>
    </Template>
</SharePoint:RenderingTemplate>