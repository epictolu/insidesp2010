<%@ Page MasterPageFile="~masterurl/default.master" %> 
      

<asp:Content ContentPlaceHolderId="PlaceHolderAdditionalPageHead" runat="server">
    <link href="styles.css" rel="stylesheet" type="text/css"/>
</asp:Content>       
         
<asp:Content ContentPlaceHolderId="PlaceHolderMain" runat="server">
	
   <div id="PageBanner" >
    <img src="images/WingtipLogo.gif" alt="Wingtip Logo" />
    <span id="PageBannerText" >Silverlight Demo</span>
  </div>
	
  <div id="PageBody" >  
    
  <object id="SilverlightApp1" width="800" height="300"
          data="data:application/x-silverlight-2," 
          type="application/x-silverlight-2" >

    <param name="source" value="SilverlightApp1.xap"/>

    <!-- Display installation image. -->
    <a href="http://go.microsoft.com/fwlink/?LinkID=149156&v=4.0.50401.0" 
       style="text-decoration: none;">
       <img src="http://go.microsoft.com/fwlink/?LinkId=161376" 
            alt="Get Microsoft Silverlight" 
            style="border-style: none"/>
    </a>

</object>
  
  </div>

  
</asp:Content>