<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Fundamentals.aspx.cs" Inherits="JavaScriptClientOMBasics.Layouts.JavaScriptClientOMBasics.Fundamentals"
    DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <script type="text/javascript">

        var siteCollection;
        var site;
        var listCollection;

        function doClientModelOps() {

            //Get Context
            var ctx = new SP.ClientContext();
            siteCollection = ctx.get_site();

            //Try to read property uninitialized
            try {
                alert(siteCollection.get_url());
            }
            catch (x) {
                alert(x.description);
            }

            //Try again
            ctx.load(siteCollection, "Url", "Id");
            ctx.executeQueryAsync(success1, failure);

        }

        function success1() {
            alert(siteCollection.get_url());
            alert(siteCollection.get_id());

            var ctx = new SP.ClientContext();
            site = ctx.get_web();
            ctx.load(site);
            listCollection = site.get_lists();
            ctx.load(listCollection, 'Include(Title,Id,Fields.Include(Title,Description))');
            ctx.executeQueryAsync(success2, failure);
        }

        function success2() {
            alert(site.get_title());
            alert(listCollection.get_count());

            var listEnumerator = listCollection.getEnumerator();
            while (listEnumerator.moveNext()) {
                alert(listEnumerator.get_current().get_fields().get_count());
            }
        }

        function failure() {
            alert("Failure!");
        }

    </script>
</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <script type="text/javascript">
        var e = ExecuteOrDelayUntilScriptLoaded(doClientModelOps, "sp.js");
    </script>
    <div id="displayDiv">
    </div>
</asp:Content>
<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    Fundamentals
</asp:Content>
<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea"
    runat="server">
    Fundamentals
</asp:Content>
