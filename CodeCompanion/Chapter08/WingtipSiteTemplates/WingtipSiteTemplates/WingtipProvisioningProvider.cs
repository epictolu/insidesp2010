using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace WingtipSiteTemplates {

  public class WingtipProvisioningProvider : SPWebProvisioningProvider {

    public override void Provision(SPWebProvisioningProperties props) {

      // provision standard team site
      if (props.Data.Equals("StandardTeamSite")) {
        // apply template using a configuration for Blank site
        props.Web.ApplyWebTemplate("STS#1");
        // elevate privileges before programming against site
        SPSecurity.RunWithElevatedPrivileges(delegate() {
          using (SPSite siteCollection = new SPSite(props.Web.Site.ID)) {
            using (SPWeb site = siteCollection.OpenWeb(props.Web.ID)) {
              // activate features and initilize site properties
              site.Features.Add(new Guid("00BFEA71-D8FE-4FEC-8DAD-01C19A6E4053"));
              site.Title = "Wingtip Team Site";
              site.Description = "This site was created on " + DateTime.Now.ToShortDateString();
              site.Update();
            }
          }
        });
      }

      // provision Wingtip sales site
      if (props.Data.Equals("SalesSite")) {
        // apply template using a configuration for Blank site
        props.Web.ApplyWebTemplate("STS#1");
        // elevate privileges before programming against site
        SPSecurity.RunWithElevatedPrivileges(delegate() {
          using (SPSite siteCollection = new SPSite(props.Web.Site.ID)) {
            using (SPWeb site = siteCollection.OpenWeb(props.Web.ID)) {
              // activate features and initilize site properties
              //site.Features.Add(new Guid("00BFEA71-D8FE-4FEC-8DAD-01C19A6E4053"));
              site.Title = "Wingtip Sales Site";
              site.Description = "This site was created on " + DateTime.Now.ToShortDateString();
              site.Update();
            }
          }
        });

      } 
    }

  }
}
