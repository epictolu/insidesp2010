<%@ Assembly Name="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" MasterPageFile="~masterurl/default.master" Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage" %>
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=14.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <div id="PageBody">
        <div id="RightColumn">
            <h2>Wingtip Sales Team</h2>
            <WebPartPages:WebPartZone ID="TopRight" runat="server" Title="Top Right Web Part Zone" FrameType="TitleBarOnly" />
        </div>
        <div id="LeftColumn">
            <WebPartPages:WebPartZone ID="Main" runat="server" Title="Main Web Part Zone" FrameType="TitleBarOnly" />
        </div>
    </div>
</asp:Content>