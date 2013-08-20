using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WingtipProvisioningProvider
{
    // This project gives an error about security validation ... 
    public class WingtipProvisioningProvider : SPWebProvisioningProvider
    {
        public override void Provision(SPWebProvisioningProperties props)
        {
            props.Web.ApplyWebTemplate("STS#1");

            SPSecurity.RunWithElevatedPrivileges(() => 
                {
                    using (var siteCollection = new SPSite(props.Web.Site.ID))
                    {
                        using (var site = siteCollection.OpenWeb(props.Web.ID))
                        {
                            site.Title = "Provisioned using Provisioning Provider";
                            site.Update();
                        }
                    }
                });
        }
    }
}
