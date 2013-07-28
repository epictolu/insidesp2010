using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace WingtipCustomActions.Features.RegisterAuthorizedTypes {

    [Guid("532c29be-5641-4fdf-9364-666a5585e2a4")]
    public class RegisterAuthorizedTypesEventReceiver : SPFeatureReceiver {

        private SPWebConfigModification CreateModificationForAuthType() {
            string assemblyName = this.GetType().Assembly.FullName;
            string assemblyNamespace = "WingtipCustomActions";
            SPWebConfigModification mod = new SPWebConfigModification();
            mod.Owner = "";
            mod.Sequence = 0;
            mod.Type = SPWebConfigModification.SPWebConfigModificationType.EnsureChildNode;
            mod.Path = "configuration/System.Workflow.ComponentModel.WorkflowCompiler/authorizedTypes";

            mod.Name = "authorizedType[@Assembly='" + assemblyName + "']" +
                                     "[@Namespace='" + assemblyNamespace + "']" +
                                     "[@TypeName='*'][@Authorized='True']";

            mod.Value = "<authorizedType Assembly='" + assemblyName + "' " + 
                                        "Namespace='" + assemblyNamespace + "' " + 
                                        "TypeName='*' " + 
                                        "Authorized='True' />";
            return mod;
        }

        public override void FeatureActivated(SPFeatureReceiverProperties properties) {
            SPWebApplication webApp = (SPWebApplication)properties.Feature.Parent;
            SPWebConfigModification mod = CreateModificationForAuthType();
            webApp.WebConfigModifications.Add(mod);
            webApp.Update();
            webApp.WebService.ApplyWebConfigModifications();
        }

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties) {
            SPWebApplication webApp = (SPWebApplication)properties.Feature.Parent;
            SPWebConfigModification mod = CreateModificationForAuthType();
            webApp.WebConfigModifications.Remove(mod);
            webApp.Update();
            webApp.WebService.ApplyWebConfigModifications();
        }
    
    }
}
