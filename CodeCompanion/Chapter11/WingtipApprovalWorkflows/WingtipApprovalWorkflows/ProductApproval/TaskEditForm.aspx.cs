using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.Workflow;

namespace WingtipApprovalWorkflows.Layouts.WingtipApprovalWorkflows {
  public partial class TaskEditForm : LayoutsPageBase {

    protected override void OnInit(EventArgs e) {
      btnApprove.Click += new EventHandler(btnApprove_Click);
      btnReject.Click += new EventHandler(btnReject_Click);
      btnCancel.Click += new EventHandler(btnCancel_Click);
    }

    protected string ListId;
    protected SPList TaskList;
    protected SPListItem TaskItem;
    protected Guid WorkflowInstanceId;
    protected SPWorkflow WorkflowInstance;
    protected SPList ItemList;
    protected SPListItem Item;
    protected SPWorkflowTask Task;

    protected string TaskData;
    protected string TaskName;
    protected SPWorkflowAssociation WorkflowAssociation;


    protected override void OnLoad(EventArgs e) {

      ListId = Request.QueryString["List"];
      TaskList = Web.Lists[new Guid(ListId)];
      TaskItem = TaskList.GetItemById(Convert.ToInt32(Request.Params["ID"]));
      WorkflowInstanceId = new Guid((string)TaskItem["WorkflowInstanceID"]);
      WorkflowInstance = new SPWorkflow(Web, WorkflowInstanceId);
      Task = WorkflowInstance.Tasks[0];
      ItemList = WorkflowInstance.ParentList;
      Item = ItemList.GetItemById(WorkflowInstance.ItemId);

      WorkflowAssociation = ItemList.WorkflowAssociations[WorkflowInstance.AssociationId];

      TaskName = TaskItem["Title"].ToString();

      string UrlToItem = Web.Site.MakeFullUrl(ItemList.RootFolder.Url +
                                              @"\DispForm.aspx?ID=" +
                                              Item.ID.ToString());
      string ItemName;
      if (Item.File != null) {
        ItemName = Item.File.Name;
      }
      else {
        ItemName = Item.Title;
      }

      lnkItem.Text = ItemName;
      lnkItem.NavigateUrl = UrlToItem;
      lblListName.Text = ItemList.Title;
      lblSiteUrl.Text = Web.Url;

      
    }

    protected void btnApprove_Click(object sender, EventArgs e) {
      Hashtable taskHash = new Hashtable();
      taskHash["TaskStatus"] = "Completed";
      taskHash["TaskOutcome"] = "Approved";
      taskHash["Outcome"] = "Approved";
      taskHash["ApproverComments"] = txtComments.Text;
      SPWorkflowTask.AlterTask(TaskItem, taskHash, true);

      if (Request["IsDlg"] == "1") { 
        Context.Response.Write("<script type='text/javascript'>window.frameElement.commitPopup();</script>"); 
        Context.Response.Flush(); 
        Context.Response.End(); } 
      else { 
        SPUtility.Redirect(ItemList.DefaultViewUrl, SPRedirectFlags.UseSource, HttpContext.Current); 
      }
    }

    protected void btnReject_Click(object sender, EventArgs e) {
      Hashtable taskHash = new Hashtable();
      taskHash["TaskStatus"] = "Completed";
      taskHash["TaskOutcome"] = "Rejected";
      taskHash["Outcome"] = "Rejected";
      taskHash["ApproverComments"] = txtComments.Text;
      SPWorkflowTask.AlterTask(TaskItem, taskHash, true);


      if (Request["IsDlg"] == "1") { 
        Context.Response.Write("<script type='text/javascript'>window.frameElement.commitPopup();</script>"); 
        Context.Response.Flush(); 
        Context.Response.End(); } 
      else { 
        SPUtility.Redirect(ItemList.DefaultViewUrl, SPRedirectFlags.UseSource, HttpContext.Current); 
      }

    }

    protected void btnCancel_Click(object sender, EventArgs e) {
      
      if (Request["IsDlg"] == "1") { 
        Context.Response.Write("<script type='text/javascript'>window.frameElement.commitPopup();</script>"); 
        Context.Response.Flush(); 
        Context.Response.End(); } 
      else { 
        SPUtility.Redirect(ItemList.DefaultViewUrl, SPRedirectFlags.UseSource, HttpContext.Current); 
      }

    }

    protected override void OnPreRender(EventArgs e) {
      if (TaskItem["Description"] != null && TaskItem["Description"].ToString() != string.Empty) {
        lblInstructions.Text = TaskItem["Description"].ToString();
      }
      else {
        lblInstructions.Text = "No instructions were provided for this task.";
      }

    }

  }
}
