using System;
using System.Collections;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.UserCode;
using Microsoft.SharePoint.Utilities;


namespace WingtipSandboxedActions {


  public class CreateDocWorkspaceAction {
    public Hashtable CreateDocWorkspace(SPUserCodeWorkflowContext context) {
      Hashtable actionResults = new Hashtable();
      actionResults["Outcome"] = string.Empty;
      SPWeb workspace = null;
      try {
        using (SPSite site = new SPSite(context.CurrentWebUrl)) {
          using (SPWeb web = site.OpenWeb()) {
            try {
              workspace = web.Webs[SPEncode.UrlEncodeAsUrl(context.ItemName)];
              if (!workspace.Exists) {
                web.Webs.Add(SPEncode.UrlEncodeAsUrl(context.ItemName), context.ItemName, "Document workspace for collaborating on " + context.ItemName, web.Language, "STS#2", false, false);
              }
              web.Lists.GetList(context.ListId, true).GetItemById(context.ItemId).CopyTo(workspace.Url + "/" + "Shared Documents/" + context.ItemName);
            }
            finally {
              if (null != workspace) {
                workspace.Dispose();
              }
            }
          }
        }
        actionResults["Outcome"] = "Success";
      }
      catch (Exception ex) {
        actionResults["Outcome"] = ex.ToString();
      }
      return (actionResults);
    }

  }
}
