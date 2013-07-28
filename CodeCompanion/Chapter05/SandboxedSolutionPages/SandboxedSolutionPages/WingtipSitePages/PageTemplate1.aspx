<%@ Page MasterPageFile="~masterurl/default.master" %> 
      
<asp:Content ContentPlaceHolderId="PlaceHolderAdditionalPageHead" runat="server">
    <link href="styles.css" rel="stylesheet" type="text/css"/>
</asp:Content>      
         
<asp:Content ContentPlaceHolderId="PlaceHolderMain" runat="server">

  <div id="PageBanner" >
    <img src="images/WingtipLogo.gif" alt="Wingtip Logo" />
    <span id="PageBannerText" >Wingtip Developers Page</span>
  </div>
	
  <div id="PageBody" >  
    <div id="RightColumn" >
      <h3>Wingtip Company Founders</h3>
      <img src="images/TheEarlyDays.jpg" alt="Back when we were young" />
      <p>Are you seriously telling us that we need to upgrade our current hardware just to install SharePoint Server 2010?</p>
    </div>

    <div id="LeftColumn" >
      <h2>Developer Links</h2>
      <ul>
        <li><a href="http://blogs.msdn.com/b/sharepoint/">Microsoft SharePoint Team Blog</a></li>
        <li><a href="http://msdn.microsoft.com/sharepoint/default.aspx">MSDN SharePoint Developer Center</a></li>
    </ul>
    </div>
    
    
  </div>
	
</asp:Content>