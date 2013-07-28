<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LinqSourcesUserControl.ascx.cs" Inherits="HelloLinqPart.VisualPart.LinqSourcesUserControl" %>
<%@ Register assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI" tagprefix="asp" %>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:Table ID="Table1" runat="server" CellPadding="5" CellSpacing="5" BorderWidth="0">
            <asp:TableRow ID="TableRow1" runat="server">
                <asp:TableCell ID="TableCell11" runat="server" VerticalAlign="Top">
                    <asp:LinkButton ID="showFiles" runat="server" OnClick="showFiles_OnClick">File Query</asp:LinkButton><br />
                    <asp:LinkButton ID="showProcesses" runat="server" OnClick="showProcesses_OnClick">Process Query</asp:LinkButton><br />
                    <asp:LinkButton ID="showArray" runat="server" OnClick="showArray_OnClick">Array Query</asp:LinkButton><br />
                    <asp:LinkButton ID="showXML" runat="server" OnClick="showXML_OnClick">XML Query</asp:LinkButton><br />
                    <asp:LinkButton ID="showSQL" runat="server" OnClick="showSQL_OnClick">SQL Query</asp:LinkButton><br />
                    <asp:LinkButton ID="showObjects" runat="server" OnClick="showObjects_OnClick">Collection Query</asp:LinkButton><br />
                </asp:TableCell>
                <asp:TableCell ID="TableCell21" runat="server">
                    <asp:ListBox ID="results" runat="server" Rows="10" Width="200px" />
                </asp:TableCell>    
            </asp:TableRow>
        </asp:Table>
    </ContentTemplate>
</asp:UpdatePanel>

