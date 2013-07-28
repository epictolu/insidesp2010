using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Collections;
using System.Drawing;
using System.Reflection;
using System.Workflow.ComponentModel.Compiler;
using System.Workflow.ComponentModel.Serialization;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Runtime;
using System.Workflow.Activities;
using System.Workflow.Activities.Rules;

namespace WingtipApprovalWorkflows.ProductApproval {
  public sealed partial class ProductApproval {
    #region Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    [System.Diagnostics.DebuggerNonUserCode]
    private void InitializeComponent() {
      this.CanModifyActivities = true;
      System.Workflow.ComponentModel.ActivityBind activitybind1 = new System.Workflow.ComponentModel.ActivityBind();
      System.Workflow.ComponentModel.ActivityBind activitybind2 = new System.Workflow.ComponentModel.ActivityBind();
      System.Workflow.ComponentModel.ActivityBind activitybind3 = new System.Workflow.ComponentModel.ActivityBind();
      System.Workflow.Runtime.CorrelationToken correlationtoken1 = new System.Workflow.Runtime.CorrelationToken();
      System.Workflow.ComponentModel.ActivityBind activitybind4 = new System.Workflow.ComponentModel.ActivityBind();
      System.Workflow.ComponentModel.ActivityBind activitybind5 = new System.Workflow.ComponentModel.ActivityBind();
      System.Workflow.ComponentModel.ActivityBind activitybind6 = new System.Workflow.ComponentModel.ActivityBind();
      System.Workflow.ComponentModel.ActivityBind activitybind7 = new System.Workflow.ComponentModel.ActivityBind();
      System.Workflow.ComponentModel.ActivityBind activitybind8 = new System.Workflow.ComponentModel.ActivityBind();
      System.Workflow.ComponentModel.ActivityBind activitybind9 = new System.Workflow.ComponentModel.ActivityBind();
      System.Workflow.ComponentModel.ActivityBind activitybind10 = new System.Workflow.ComponentModel.ActivityBind();
      System.Workflow.ComponentModel.ActivityBind activitybind11 = new System.Workflow.ComponentModel.ActivityBind();
      System.Workflow.ComponentModel.ActivityBind activitybind12 = new System.Workflow.ComponentModel.ActivityBind();
      System.Workflow.ComponentModel.ActivityBind activitybind13 = new System.Workflow.ComponentModel.ActivityBind();
      System.Workflow.ComponentModel.ActivityBind activitybind14 = new System.Workflow.ComponentModel.ActivityBind();
      System.Workflow.ComponentModel.ActivityBind activitybind15 = new System.Workflow.ComponentModel.ActivityBind();
      System.Workflow.ComponentModel.ActivityBind activitybind16 = new System.Workflow.ComponentModel.ActivityBind();
      System.Workflow.ComponentModel.ActivityBind activitybind17 = new System.Workflow.ComponentModel.ActivityBind();
      System.Workflow.ComponentModel.ActivityBind activitybind18 = new System.Workflow.ComponentModel.ActivityBind();
      System.Workflow.ComponentModel.ActivityBind activitybind20 = new System.Workflow.ComponentModel.ActivityBind();
      System.Workflow.Runtime.CorrelationToken correlationtoken2 = new System.Workflow.Runtime.CorrelationToken();
      System.Workflow.ComponentModel.ActivityBind activitybind19 = new System.Workflow.ComponentModel.ActivityBind();
      this.cancellationHandlerActivity1 = new System.Workflow.ComponentModel.CancellationHandlerActivity();
      this.logTaskComplete = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
      this.completeTask = new Microsoft.SharePoint.WorkflowActions.CompleteTask();
      this.logTaskUpdated = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
      this.onTaskChanged = new Microsoft.SharePoint.WorkflowActions.OnTaskChanged();
      this.logTaskCreated = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
      this.createApprovalTask = new Microsoft.SharePoint.WorkflowActions.CreateTask();
      this.logActivated = new Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity();
      this.onWFActivated = new Microsoft.SharePoint.WorkflowActions.OnWorkflowActivated();
      // 
      // cancellationHandlerActivity1
      // 
      this.cancellationHandlerActivity1.Enabled = false;
      this.cancellationHandlerActivity1.Name = "cancellationHandlerActivity1";
      // 
      // logTaskComplete
      // 
      this.logTaskComplete.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
      this.logTaskComplete.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
      activitybind1.Name = "ProductApproval";
      activitybind1.Path = "HistoryDescription";
      activitybind2.Name = "ProductApproval";
      activitybind2.Path = "HistoryOutcome";
      this.logTaskComplete.Name = "logTaskComplete";
      this.logTaskComplete.OtherData = "";
      activitybind3.Name = "ProductApproval";
      activitybind3.Path = "workflowProperties.OriginatorUser.ID";
      this.logTaskComplete.MethodInvoking += new System.EventHandler(this.logTaskComplete_MethodInvoking);
      this.logTaskComplete.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind1)));
      this.logTaskComplete.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind2)));
      this.logTaskComplete.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.UserIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind3)));
      // 
      // completeTask
      // 
      correlationtoken1.Name = "taskToken";
      correlationtoken1.OwnerActivityName = "ProductApproval";
      this.completeTask.CorrelationToken = correlationtoken1;
      this.completeTask.Name = "completeTask";
      activitybind4.Name = "ProductApproval";
      activitybind4.Path = "TaskId";
      activitybind5.Name = "ProductApproval";
      activitybind5.Path = "TaskOutcome";
      this.completeTask.MethodInvoking += new System.EventHandler(this.completeTask_MethodInvoking);
      this.completeTask.SetBinding(Microsoft.SharePoint.WorkflowActions.CompleteTask.TaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind4)));
      this.completeTask.SetBinding(Microsoft.SharePoint.WorkflowActions.CompleteTask.TaskOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind5)));
      // 
      // logTaskUpdated
      // 
      this.logTaskUpdated.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
      this.logTaskUpdated.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
      activitybind6.Name = "ProductApproval";
      activitybind6.Path = "HistoryDescription";
      activitybind7.Name = "ProductApproval";
      activitybind7.Path = "HistoryOutcome";
      this.logTaskUpdated.Name = "logTaskUpdated";
      this.logTaskUpdated.OtherData = "";
      activitybind8.Name = "ProductApproval";
      activitybind8.Path = "workflowProperties.OriginatorUser.ID";
      this.logTaskUpdated.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind6)));
      this.logTaskUpdated.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind7)));
      this.logTaskUpdated.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.UserIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind8)));
      // 
      // onTaskChanged
      // 
      activitybind9.Name = "ProductApproval";
      activitybind9.Path = "TaskAfterProperties";
      this.onTaskChanged.BeforeProperties = null;
      this.onTaskChanged.CorrelationToken = correlationtoken1;
      this.onTaskChanged.Executor = null;
      this.onTaskChanged.Name = "onTaskChanged";
      activitybind10.Name = "ProductApproval";
      activitybind10.Path = "TaskId";
      this.onTaskChanged.Invoked += new System.EventHandler<System.Workflow.Activities.ExternalDataEventArgs>(this.onTaskChanged_Invoked);
      this.onTaskChanged.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.TaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind10)));
      this.onTaskChanged.SetBinding(Microsoft.SharePoint.WorkflowActions.OnTaskChanged.AfterPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind9)));
      // 
      // logTaskCreated
      // 
      this.logTaskCreated.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
      this.logTaskCreated.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
      activitybind11.Name = "ProductApproval";
      activitybind11.Path = "HistoryDescription";
      activitybind12.Name = "ProductApproval";
      activitybind12.Path = "HistoryOutcome";
      this.logTaskCreated.Name = "logTaskCreated";
      this.logTaskCreated.OtherData = "";
      activitybind13.Name = "ProductApproval";
      activitybind13.Path = "workflowProperties.OriginatorUser.ID";
      this.logTaskCreated.MethodInvoking += new System.EventHandler(this.logTaskCreated_MethodInvoking);
      this.logTaskCreated.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind11)));
      this.logTaskCreated.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind12)));
      this.logTaskCreated.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.UserIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind13)));
      // 
      // createApprovalTask
      // 
      this.createApprovalTask.CorrelationToken = correlationtoken1;
      this.createApprovalTask.ListItemId = -1;
      this.createApprovalTask.Name = "createApprovalTask";
      this.createApprovalTask.SpecialPermissions = null;
      activitybind14.Name = "ProductApproval";
      activitybind14.Path = "TaskId";
      activitybind15.Name = "ProductApproval";
      activitybind15.Path = "TaskProperties";
      this.createApprovalTask.MethodInvoking += new System.EventHandler(this.createTask1_MethodInvoking);
      this.createApprovalTask.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTask.TaskIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind14)));
      this.createApprovalTask.SetBinding(Microsoft.SharePoint.WorkflowActions.CreateTask.TaskPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind15)));
      // 
      // logActivated
      // 
      this.logActivated.Duration = System.TimeSpan.Parse("-10675199.02:48:05.4775808");
      this.logActivated.EventId = Microsoft.SharePoint.Workflow.SPWorkflowHistoryEventType.WorkflowComment;
      activitybind16.Name = "ProductApproval";
      activitybind16.Path = "HistoryDescription";
      activitybind17.Name = "ProductApproval";
      activitybind17.Path = "HistoryOutcome";
      this.logActivated.Name = "logActivated";
      this.logActivated.OtherData = "";
      activitybind18.Name = "ProductApproval";
      activitybind18.Path = "workflowProperties.OriginatorUser.ID";
      this.logActivated.MethodInvoking += new System.EventHandler(this.logActivated_MethodInvoking);
      this.logActivated.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryDescriptionProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind16)));
      this.logActivated.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.HistoryOutcomeProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind17)));
      this.logActivated.SetBinding(Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity.UserIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind18)));
      activitybind20.Name = "ProductApproval";
      activitybind20.Path = "workflowId";
      // 
      // onWFActivated
      // 
      correlationtoken2.Name = "workflowToken";
      correlationtoken2.OwnerActivityName = "ProductApproval";
      this.onWFActivated.CorrelationToken = correlationtoken2;
      this.onWFActivated.EventName = "OnWorkflowActivated";
      this.onWFActivated.Name = "onWFActivated";
      activitybind19.Name = "ProductApproval";
      activitybind19.Path = "workflowProperties";
      this.onWFActivated.Invoked += new System.EventHandler<System.Workflow.Activities.ExternalDataEventArgs>(this.onWorkflowActivated1_Invoked);
      this.onWFActivated.SetBinding(Microsoft.SharePoint.WorkflowActions.OnWorkflowActivated.WorkflowIdProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind20)));
      this.onWFActivated.SetBinding(Microsoft.SharePoint.WorkflowActions.OnWorkflowActivated.WorkflowPropertiesProperty, ((System.Workflow.ComponentModel.ActivityBind)(activitybind19)));
      // 
      // ProductApproval
      // 
      this.Activities.Add(this.onWFActivated);
      this.Activities.Add(this.logActivated);
      this.Activities.Add(this.createApprovalTask);
      this.Activities.Add(this.logTaskCreated);
      this.Activities.Add(this.onTaskChanged);
      this.Activities.Add(this.logTaskUpdated);
      this.Activities.Add(this.completeTask);
      this.Activities.Add(this.logTaskComplete);
      this.Activities.Add(this.cancellationHandlerActivity1);
      this.Name = "ProductApproval";
      this.CanModifyActivities = false;

    }

    #endregion

    private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logTaskComplete;

    private Microsoft.SharePoint.WorkflowActions.CompleteTask completeTask;

    private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logTaskUpdated;

    private Microsoft.SharePoint.WorkflowActions.OnTaskChanged onTaskChanged;

    private Microsoft.SharePoint.WorkflowActions.CreateTask createApprovalTask;

    private CancellationHandlerActivity cancellationHandlerActivity1;

    private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logTaskCreated;

    private Microsoft.SharePoint.WorkflowActions.LogToHistoryListActivity logActivated;

    private Microsoft.SharePoint.WorkflowActions.OnWorkflowActivated onWFActivated;






































  }
}
