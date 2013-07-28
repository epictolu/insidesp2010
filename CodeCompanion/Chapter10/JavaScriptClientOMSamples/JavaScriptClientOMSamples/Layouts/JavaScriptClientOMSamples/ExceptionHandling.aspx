<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExceptionHandling.aspx.cs" Inherits="JavaScriptClientOMSamples.Layouts.JavaScriptClientOMSamples.ExceptionHandling" DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <script type="text/javascript">

        var site;

        function goClientOM() {

            //Get Context
            var ctx = new SP.ClientContext();


            //Start Exception-Handling Scope
            var e = new SP.ExceptionHandlingScope(ctx);
            var s = e.startScope();

              //try
              var t = e.startTry();

                  var list1 = ctx.get_web().get_lists().getByTitle("My List");
                  ctx.load(list1);
                  list1.set_description("A new description");
                  list1.update();

              t.dispose();

              //catch
              var c = e.startCatch();

                  var listCI = new SP.ListCreationInformation();

                  listCI.set_title("My List");
                  listCI.set_templateType(SP.ListTemplateType.announcements);
                  listCI.set_quickLaunchOption(SP.QuickLaunchOptions.on);

                  var list = ctx.get_web().get_lists().add(listCI);

              c.dispose();

              //finally
              var f = e.startFinally();

                  var list2 = ctx.get_web().get_lists().getByTitle("My List");
                  ctx.load(list2);
                  list2.set_description("A new description");
                  list2.update();

              f.dispose();
            
            //End Exception-Handling Scope
            s.dispose();

            //Execute
            ctx.executeQueryAsync(success, failure);

        }

        function success() {
            alert("Success");
        }

        function failure() {
            alert("Failure!");
        }

    </script>
</asp:Content>

<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div id="displayDiv">
        <input type="button" value="Push Me!" onclick="javascript:goClientOM();" />
    </div>
</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
Application Page
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server" >
My Application Page
</asp:Content>
