﻿using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;

namespace Branding101.ChildSiteInit
{
    /// <summary>
    /// Web Events
    /// </summary>
    public class ChildSiteInit : SPWebEventReceiver
    {
        public override void WebProvisioned(SPWebEventProperties properties)
        {
            base.WebProvisioned(properties);

            var childSite = properties.Web;
            var topSite = childSite.Site.RootWeb;

            childSite.MasterUrl = topSite.MasterUrl;
            childSite.CustomMasterUrl = topSite.CustomMasterUrl;
            childSite.AlternateCssUrl = topSite.AlternateCssUrl;
            childSite.SiteLogoUrl = topSite.SiteLogoUrl;
            childSite.Update();
        }


    }
}