using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Serialization;
using System.Web;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.Workflow;

namespace WingtipApprovalWorkflows.ProductApproval {
  public partial class WorkflowInitiationForm : LayoutsPageBase {
    protected void Page_Load(object sender, EventArgs e) {
      InitializeParams();

      // Optionally, add code here to pre-populate your form fields.
      SPWorkflowAssociation association =
     this.workflowList.WorkflowAssociations[new Guid(this.associationGuid)];

      XmlSerializer serializer = new XmlSerializer(typeof(ProductApprovalWorkflowData));
      XmlTextReader reader = new XmlTextReader(new StringReader(association.AssociationData));
      ProductApprovalWorkflowData wfData = (ProductApprovalWorkflowData)serializer.Deserialize(reader);

      pickerApprover.CommaSeparatedAccounts = wfData.Approver;

      if (wfData.ApprovalScope.Equals("Internal")) {
        radInternalApproval.Checked = true;
      }
      else {
        radExternalApproval.Checked = true;
      }
      txtInstructions.Text = wfData.Instructions;
    }

    // This method is called when the user clicks the button to start the workflow.
    private string GetInitiationData() {
      ProductApprovalWorkflowData wfData = new ProductApprovalWorkflowData();

      PickerEntity ApproverEntity = (PickerEntity)pickerApprover.Entities[0];
      wfData.Approver = ApproverEntity.Key;

      if (radInternalApproval.Checked) {
        wfData.ApprovalScope = "Internal";
      }
      else {
        wfData.ApprovalScope = "External";
      }
      wfData.Instructions = txtInstructions.Text;
      
      using (MemoryStream stream = new MemoryStream()) {
        XmlSerializer serializer = new XmlSerializer(typeof(ProductApprovalWorkflowData));
        serializer.Serialize(stream, wfData);
        stream.Position = 0;
        byte[] bytes = new byte[stream.Length];
        stream.Read(bytes, 0, bytes.Length);
        string WorkflowData = Encoding.UTF8.GetString(bytes);
        return WorkflowData;
      }

    }

    protected void StartWorkflow_Click(object sender, EventArgs e) {
      // Optionally, add code here to perform additional steps before starting your workflow
      try {
        HandleStartWorkflow();
      }
      catch (Exception ex) {
        SPUtility.TransferToErrorPage("Error Starting Workflow" + ex.Message);
      }
    }

    protected void Cancel_Click(object sender, EventArgs e) {
      SPUtility.Redirect("Workflow.aspx", SPRedirectFlags.RelativeToLayoutsPage, HttpContext.Current, Page.ClientQueryString);
    }

    #region Workflow Initiation Code - Typically, the following code should not be changed

    private string associationGuid;
    private SPList workflowList;
    private SPListItem workflowListItem;

    private void InitializeParams() {
      try {
        this.associationGuid = Request.Params["TemplateID"];

        // Parameters 'List' and 'ID' will be null for site workflows.
        if (!String.IsNullOrEmpty(Request.Params["List"]) && !String.IsNullOrEmpty(Request.Params["ID"])) {
          this.workflowList = this.Web.Lists[new Guid(Request.Params["List"])];
          this.workflowListItem = this.workflowList.GetItemById(Convert.ToInt32(Request.Params["ID"]));
        }
      }
      catch (Exception) {
        SPUtility.TransferToErrorPage(SPHttpUtility.UrlKeyValueEncode("Failed to read Request Parameters"));
      }
    }

    private void HandleStartWorkflow() {
      if (this.workflowList != null && this.workflowListItem != null) {
        StartListWorkflow();
      }
      else {
        StartSiteWorkflow();
      }
    }

    private void StartSiteWorkflow() {
      SPWorkflowAssociation association = this.Web.WorkflowAssociations[new Guid(this.associationGuid)];
      this.Web.Site.WorkflowManager.StartWorkflow((object)null, association, GetInitiationData(), SPWorkflowRunOptions.Synchronous);
      SPUtility.Redirect(this.Web.Url, SPRedirectFlags.UseSource, HttpContext.Current);
    }

    private void StartListWorkflow() {
      SPWorkflowAssociation association = this.workflowList.WorkflowAssociations[new Guid(this.associationGuid)];
      this.Web.Site.WorkflowManager.StartWorkflow(workflowListItem, association, GetInitiationData());
      SPUtility.Redirect(this.workflowList.DefaultViewUrl, SPRedirectFlags.UseSource, HttpContext.Current);
    }
    #endregion
  }
}