using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.UserCode;
using Microsoft.SharePoint.Administration;

namespace FullTrustProxy
{
    public class FullTrustProxyFeatureReceiver : SPFeatureReceiver
    {
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            var userCodeService = SPUserCodeService.Local;

            if (userCodeService == null)
                throw new Exception("User Code Service not running.");

            var assemblyName = this.GetType().Assembly.FullName;
            var proxyOperationType = new SPProxyOperationType(assemblyName, typeof(GetWebApplications).FullName);

            userCodeService.ProxyOperationTypes.Add(proxyOperationType);
            userCodeService.Update();
        }

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            var userCodeService = SPUserCodeService.Local;

            if (userCodeService == null)
                throw new Exception("User Code Service not running.");

            var assemblyName = this.GetType().Assembly.FullName;
            var proxyOperationType = new SPProxyOperationType(assemblyName, typeof(GetWebApplications).FullName);

            userCodeService.ProxyOperationTypes.Remove(proxyOperationType);
            userCodeService.Update();
        }
    }
}
