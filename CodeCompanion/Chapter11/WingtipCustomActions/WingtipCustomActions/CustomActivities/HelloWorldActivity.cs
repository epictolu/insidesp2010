
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Workflow;
using System.Workflow.Activities;
using System.Workflow.ComponentModel;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Workflow;
using Microsoft.SharePoint.WorkflowActions;
using Microsoft.SharePoint.Administration;


namespace WingtipCustomActions {
  public class HelloWorldActivity : Activity {

    public static DependencyProperty __ContextProperty =
      DependencyProperty.Register("__Context", typeof(WorkflowContext), typeof(HelloWorldActivity));
       
    public static DependencyProperty UserIdProperty =
      DependencyProperty.Register("UserId", typeof(int), typeof(HelloWorldActivity));

    // Properties
    public WorkflowContext __Context {
      get { return (WorkflowContext)base.GetValue(__ContextProperty); }
      set { base.SetValue(__ContextProperty, value); }
    }
        
    public int UserId {
      get { return (int)base.GetValue(UserIdProperty); }
      set { base.SetValue(UserIdProperty, value); }
    }

 
    protected override ActivityExecutionStatus Execute(ActivityExecutionContext context) {

      ISharePointService SPService = (ISharePointService)context.GetService(typeof(ISharePointService));
      ITaskService TaskService = (ITaskService)context.GetService(typeof(ITaskService));
      IListItemService ListItemService = (IListItemService)context.GetService(typeof(IListItemService));
      
      SPService.LogToHistoryList(
        this.WorkflowInstanceId, 
        SPWorkflowHistoryEventType.WorkflowComment,
        this.UserId, 
        TimeSpan.MinValue,
        "your custom outcome",
        "your custom description", 
        "other custom data");


      return ActivityExecutionStatus.Closed;
    }
    
  }
}
