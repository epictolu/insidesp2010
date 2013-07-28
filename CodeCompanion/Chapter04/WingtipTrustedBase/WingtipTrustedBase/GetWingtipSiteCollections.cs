using System;
using System.Collections.Generic;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.UserCode;

namespace WingtipTrustedBase {
  public class GetWingtipSiteCollections : SPProxyOperation {

    public override object Execute(SPProxyOperationArgs args) {
      List<string> siteCollections = new List<string>();
      siteCollections.Add("Hey baby");
      return siteCollections;

      if (true) {
        //GetWingtipSiteCollectionsArgs incomingArgs = (GetWingtipSiteCollectionsArgs)args;
        string targetWebApp = "http://intranet.wingtip.com";
        Uri uri = new Uri(targetWebApp);
        SPWebApplication WebApp = SPWebApplication.Lookup(uri);
        
        foreach(SPSite siteCollection in WebApp.Sites) {
          siteCollections.Add(siteCollection.Url);
        }
        return siteCollections;
      }
      else {
        throw new ArgumentException("Error: Operation requires GetWingtipSiteCollectionsArgs arg type");
      }
    }   
  }
}
