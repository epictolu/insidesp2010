using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Workflow;
using Microsoft.SharePoint.WorkflowActions;

namespace WingtipApprovalWorkflows.ProductApproval {
  public sealed partial class ProductApproval : SequentialWorkflowActivity {
    public ProductApproval() {
      InitializeComponent();
    }

    public Guid workflowId = default(System.Guid);
    public SPWorkflowActivationProperties workflowProperties = new SPWorkflowActivationProperties();


    // fields to store initiation data from initiation form 
    public string Approver = default(string);
    public string ApprovalScope = default(string);
    public string ApproverInstructions = default(string);

    private void onWorkflowActivated1_Invoked(object sender, ExternalDataEventArgs e) {
      // deserialize initiation data; 
      string InitiationData = workflowProperties.InitiationData;
      XmlSerializer serializer =
                    new XmlSerializer(typeof(ProductApprovalWorkflowData));
      XmlTextReader reader =
                    new XmlTextReader(new StringReader(InitiationData));
      ProductApprovalWorkflowData FormData =
             (ProductApprovalWorkflowData)serializer.Deserialize(reader);

      // assign form data values to workflow fields 
      Approver = FormData.Approver;
      ApprovalScope = FormData.ApprovalScope;
      ApproverInstructions = FormData.Instructions;
    }

    public String HistoryDescription = default(System.String);
    public String HistoryOutcome = default(System.String);

    private void logActivated_MethodInvoking(object sender, EventArgs e) {
      HistoryDescription = "Workflow data: " +
                           "Approver=" + Approver + "; " +
                           "ApprovalScope=" + ApprovalScope + ";";
      HistoryOutcome = "Workflow activated";
    }

    public Guid TaskId = default(System.Guid);
    public SPWorkflowTaskProperties TaskProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();
    string TaskStatus = "Pending Approval";
    string ApproverComments = "";

    private void createTask1_MethodInvoking(object sender, EventArgs e) {
      // generate new GUID used to initialize task correlation token 
      TaskId = Guid.NewGuid();

      // assign initial properties prior to task creation 
      TaskProperties.Title = "Review " + workflowProperties.Item.File.Name;
      TaskProperties.Description = "Please review then approve or reject this product proposal.";
      TaskProperties.AssignedTo = Approver;
      TaskProperties.PercentComplete = 0;
      TaskProperties.StartDate = DateTime.Today;
      TaskProperties.DueDate = DateTime.Today.AddDays(3);
      TaskProperties.ExtendedProperties["ApprovalScope"] = ApprovalScope;
      TaskProperties.ExtendedProperties["ApproverInstructions"] = ApproverInstructions;
      TaskProperties.ExtendedProperties["TaskStatus"] = TaskStatus;
    }

    private void logTaskCreated_MethodInvoking(object sender, EventArgs e) {
      HistoryDescription = "Task data: " +
                            "AssignedTo=" + Approver + "; " +
                            "TaskStatus=" + TaskProperties.ExtendedProperties["TaskStatus"].ToString() + "; " +
                            "TaskTitle=" + TaskProperties.Title + ";";
      HistoryOutcome = "Task created";

    }

    public SPWorkflowTaskProperties TaskAfterProperties = new Microsoft.SharePoint.Workflow.SPWorkflowTaskProperties();

    private void onTaskChanged_Invoked(object sender, ExternalDataEventArgs e) {
      HistoryOutcome = "Task updated";
      HistoryDescription = "TaskStatus: " +
                           TaskAfterProperties.ExtendedProperties["TaskStatus"].ToString() + "; " +
                           "ApproverComments: " +
                           TaskAfterProperties.ExtendedProperties["ApproverComments"].ToString();

    }

    public String TaskOutcome = default(System.String);

    private void completeTask_MethodInvoking(object sender, EventArgs e) {
      TaskOutcome = TaskAfterProperties.ExtendedProperties["TaskOutcome"].ToString();
    }

    private void logTaskComplete_MethodInvoking(object sender, EventArgs e) {
      HistoryDescription = "TaskOutcome: " +
                           TaskAfterProperties.ExtendedProperties["TaskOutcome"].ToString();

      HistoryOutcome = "Task Completed";
    }



    private void ProductApproval_Completed(object sender, EventArgs e) {

      
    }

  }
}
