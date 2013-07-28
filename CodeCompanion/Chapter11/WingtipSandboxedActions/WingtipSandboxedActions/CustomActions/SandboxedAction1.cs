using System;
using System.Collections;
using Microsoft.SharePoint;
using Microsoft.SharePoint.UserCode;
using Microsoft.SharePoint.Utilities;

namespace WingtipSandboxedActions {

public class SandboxedAction1 {
  public Hashtable ActionMethod1(SPUserCodeWorkflowContext context) {
  
    // perform work for action 
    using (SPSite siteCollection = new SPSite(context.CurrentWebUrl)) {      
      SPWeb web = siteCollection.OpenWeb();
      SPList list  = web.Lists.GetList(context.ListId,true);
      SPListItem item = list.GetItemById(context.ItemId);
      // perform work required by action       
    }

    // return outcome status using Hashtable
    Hashtable actionResults = new Hashtable();
    actionResults["Outcome"] = "Success";
    return actionResults;
   
  }

}
}
