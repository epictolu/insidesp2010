using System;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Xml.Serialization;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Workflow;

namespace WingtipApprovalWorkflows.Features.MainSite {

  [Guid("bf685208-c534-40cd-89c4-21abac2ef2a8")]
  public class MainSiteEventReceiver : SPFeatureReceiver {

    public override void FeatureActivated(SPFeatureReceiverProperties properties) {

      SPSite siteCollection = (SPSite)properties.Feature.Parent;
      SPWeb site = siteCollection.RootWeb;

      // obtain referecnes to lists
      SPList targetList = site.Lists["Product Proposals"];
      SPList taskList = site.Lists["Wingtip Workflow Tasks"];
      SPList workflowHistoryList = site.Lists["Wingtip Workflow History"];

      taskList.UseFormsForDisplay = false;
      taskList.Update();

      // obtain reference to workflow template
      Guid WorkflowTemplateId = new Guid("b06c5f7d-7b06-46b0-a07d-ccb2504153f5");
      SPWorkflowTemplate WorkflowTemplate = site.WorkflowTemplates[WorkflowTemplateId];

      // create user-friendly name for workflow association
      string WorkflowAssociationName = "Wingtip Product Approval";

      // create workflow association
      SPWorkflowAssociation WorkflowAssociation =
        SPWorkflowAssociation.CreateListAssociation(
                                WorkflowTemplate,
                                WorkflowAssociationName,
                                taskList,
                                workflowHistoryList);

      // configure workflow association and add to WorkflowAssociations collection
      WorkflowAssociation.Description = "Used to begin the product proposal approval process";
      WorkflowAssociation.AllowManual = true;
      WorkflowAssociation.AutoStartCreate = false;
      WorkflowAssociation.AutoStartChange = false;

      // add sample workflow association data
      ProductApprovalWorkflowData wfData = new ProductApprovalWorkflowData();
      wfData.Approver = @"WINGTIP\Administrator";
      wfData.ApprovalScope = "Internal";
      wfData.Instructions = @"Please review this product proposal";

      using (MemoryStream stream = new MemoryStream()) {
        XmlSerializer serializer = new XmlSerializer(typeof(ProductApprovalWorkflowData));
        serializer.Serialize(stream, wfData);
        stream.Position = 0;
        byte[] bytes = new byte[stream.Length];
        stream.Read(bytes, 0, bytes.Length);
        WorkflowAssociation.AssociationData = Encoding.UTF8.GetString(bytes);
      }

      targetList.WorkflowAssociations.Add(WorkflowAssociation);

    }

    public override void FeatureDeactivating(SPFeatureReceiverProperties properties) {
      
      SPSite siteCollection = (SPSite)properties.Feature.Parent;
      SPWeb site = siteCollection.RootWeb;

      // remove association
      try {
        SPList ProductProposalsList = site.Lists["Product Proposals"];
        Guid WorkflowTemplateId = new Guid("b06c5f7d-7b06-46b0-a07d-ccb2504153f5");
        SPWorkflowAssociation wfa = ProductProposalsList.WorkflowAssociations.GetAssociationByBaseID(WorkflowTemplateId);
        ProductProposalsList.WorkflowAssociations.Remove(wfa.Id);
      }
      catch { }
    }
  }
}
