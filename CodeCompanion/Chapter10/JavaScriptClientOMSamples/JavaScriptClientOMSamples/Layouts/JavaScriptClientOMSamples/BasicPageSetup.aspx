﻿<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BasicPageSetup.aspx.cs" Inherits="JavaScriptClientOMSamples.Layouts.JavaScriptClientOMSamples.BasicPageSetup" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
<!--
Basic setup for the JavaScript Client OM requires the following, which exist already
1. A reference to SP.js through a Script Link as follows
<SharePoint:ScriptLink ID="SPScriptLink" runat="server" LoadAfterUI="true" Localizable="false" Name="sp.js" />
2. Registration of Microsoft.SharePoint.WebControls
Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c"
3. A form digest control, if the code will update a site or list, which is in Default.master
<SharePoint:FormDigest runat="server" />
-->
    <script type="text/javascript">

        var site;
        var listCollection;

        function doClientModelOps() {

            //Get Context
            var ctx = new SP.ClientContext();

            //Get site
            site = ctx.get_web();
            ctx.load(site);

            //Get lists
            listCollection = site.get_lists();
            ctx.load(listCollection, 'Include(Title,Id,Fields.Include(Title,Description))');

            ctx.executeQueryAsync(success, failure);

        }

        function success() {

            //Show titles
            var html = "<h1>Results</h1>";
            html += "<p>Site Title: ";
            html += site.get_title();
            html += "</p>";
            html += "<p>Number of lists on site: ";
            html += listCollection.get_count();
            html += "</p>";
            html += "<ol>";

            //Show names of lists
            var listEnumerator = listCollection.getEnumerator();
            while (listEnumerator.moveNext()) {
                html += "<li>";
                html += listEnumerator.get_current().get_title();
                html += "</li>";
            }

            html += "</ol>";

            //Update div
            $get("displayDiv").innerHTML = html;

        }

        function failure() {
            $get("displayDiv").innerHTML = "<p>Failure!</p>";
        }

    </script>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <script type="text/javascript">
        //Waits until the SP.js script is loaded and then calls our function
        var e = ExecuteOrDelayUntilScriptLoaded(doClientModelOps, "sp.js");
    </script>
    <div id="displayDiv">
    </div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
JavaScript Client OM Page Setup
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
JavaScript Client OM Page Setup
</asp:Content>
