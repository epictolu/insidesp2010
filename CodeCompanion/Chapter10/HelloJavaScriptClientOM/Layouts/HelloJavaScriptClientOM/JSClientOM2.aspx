<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls"
    Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JSClientOM2.aspx.cs" Inherits="HelloJavaScriptClientOM.Layouts.HelloJavaScriptClientOM.JSClientOM2"
    DynamicMasterPageFile="~masterurl/default.master" %>

<asp:Content ID="PageHead" ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    
    <SharePoint:ScriptLink ID="SPScriptLink" runat="server" LoadAfterUI="true" Localizable="false"
        Name="sp.js">
    </SharePoint:ScriptLink>
    <link href="css/trontastic/jquery-ui-1.7.1.custom.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="js/jquery-ui-1.7.1.custom.min.js" type="text/javascript"></script>
    <link href="css/MyCSS.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">

        //Entry point for our code
        _spBodyOnLoadFunctionNames.push("Initialize");

        var site;
        var context;
        var lists;

        function Initialize() {

            context = SP.ClientContext.get_current();

            site = context.get_web();
            context.load(site);

            lists = site.get_lists();
            context.load(lists);

            context.executeQueryAsync(onQuerySucceeded, onQueryFailed);
        }

        function onQuerySucceeded(sender, args) {
            
            LoadListBox();
        }

        function onQueryFailed(sender, args) {
            alert('request failed ' + args.get_message() +
                    '\n' + args.get_stackTrace());
        }

        function LoadListBox() {
            //handle the change event
            $("#ListBox1").change(onSelectChange);

            var listEnumerator = lists.getEnumerator();

            //iterate though all of the items
            while (listEnumerator.moveNext()) {
                var listItem = listEnumerator.get_current();
                var listId = listItem.get_id().toString();
                var listTitle = listItem.get_title();

                //add the item to the ListBox using jQuery
                $("#ListBox1").append('<option id="' + listId + '" value="' + listTitle + '">' + listTitle + '</option>');
            }
        }

        function BuildTable(ListName) {

            var categoryId = "X" + $("Select#ListBox1").val();

            //add a new section if it doesn't exist
            if ($("#" + categoryId).length == 0) {
                if ($("#accordion").length == 0) {
                    alert('accordion not found2');
                }
                $("#accordion").append("<div><h3><a href='#'>" + ListName + "</a></h3><div id='" + categoryId + "'/></div>");
            }

            //Add the item
            $("#" + categoryId).append('<h2>' + ListName + '</h2>');

            //set the accordian style
            $("#accordion").accordion('destroy');
            $("#accordion").accordion({ header: "h3" });
        }

        function onSelectChange() {
            BuildTable($("#ListBox1 option:selected").text());
        }
    </script>

</asp:Content>
<asp:Content ID="Main" ContentPlaceHolderID="PlaceHolderMain" runat="server">

    <div id="MyCode">
        <select id="ListBox1" size="8" style="width: 176px; height: 128px"></select>
        <div id="accordion"></div>
    </div>

</asp:Content>

<asp:Content ID="PageTitle" ContentPlaceHolderID="PlaceHolderPageTitle" runat="server">
    JavaScript Client OM
</asp:Content>

<asp:Content ID="PageTitleInTitleArea" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea"
    runat="server">
    JavaScript Client OM
</asp:Content>
