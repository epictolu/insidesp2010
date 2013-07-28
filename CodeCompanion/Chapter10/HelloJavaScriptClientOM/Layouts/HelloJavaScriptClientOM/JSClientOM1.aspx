<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JSClientOM1.aspx.cs" Inherits="HelloJavaScriptClientOM.Layouts.HelloJavaScriptClientOM.JSClientOM1" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" contentplaceholderid="PlaceHolderAdditionalPageHead" runat="server">
       
       <SharePoint:ScriptLink ID="SPScriptLink" runat="server" LoadAfterUI="true" Localizable="false" Name="sp.js" />
       <script src="js/jquery-1.3.2.min.js" type="text/javascript"></script>
       <script src="js/jquery-ui-1.7.1.custom.min.js" type="text/javascript"></script>
       
       <script language="javascript" type="text/javascript">

           function createList() {

               SP.UI.Status.removeAllStatus();
               SP.UI.Notify.addNotification("Starting...");

               var context = new SP.ClientContext();

               var listCI = new SP.ListCreationInformation();

               listCI.set_title(document.getElementById("newListName").value);
               listCI.set_description("Created by the JavaScript Client OM");
               listCI.set_templateType(SP.ListTemplateType.announcements);
               listCI.set_quickLaunchOption(SP.QuickLaunchOptions.on);

               var list = context.get_web().get_lists().add(listCI);

               var itemCI = new SP.ListItemCreationInformation();
               var listItem1 = list.addItem(itemCI);

               listItem1.parseAndSetFieldValue("Title", "New List Created!");
               listItem1.parseAndSetFieldValue("Body", "Your new list is was created by the JavaScript Client OM");
               listItem1.update();

               context.executeQueryAsync(success,failure);

           }

           function success() {
               SP.UI.Status.addStatus("Client Object Model:", "A list was successfully created.");
           }

           function failure() {
               SP.UI.Notify.addNotification("Client Object Model:", "An error occurred during list creation.");
           }
           
        </script>
</asp:Content>

<asp:Content ID="Main" contentplaceholderid="PlaceHolderMain" runat="server">
    <input type="text" id="newListName" value="My Announcements" />
    <input type='button' onclick='createList()' value='Create a new Announcements List' />
</asp:Content>

<asp:Content ID="PageTitle" contentplaceholderid="PlaceHolderPageTitle" runat="server">
JavaScript Client OM
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" contentplaceholderid="PlaceHolderPageTitleInTitleArea" runat="server" >
JavaScript Client OM
</asp:Content>
