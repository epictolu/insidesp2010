using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.UserCode;
using Microsoft.SharePoint.Administration;

namespace FullTrustProxy
{
    public class GetWebApplications : SPProxyOperation
    {
        public override object Execute(SPProxyOperationArgs args)
        {
            var webApplications = new List<string>();
            var webService = SPFarm.Local.Servers.GetValue<SPWebService>();

            foreach (var webApplication in webService.WebApplications)
                webApplications.Add(webApplication.DisplayName);

            return webApplications;
        }
    }
}
