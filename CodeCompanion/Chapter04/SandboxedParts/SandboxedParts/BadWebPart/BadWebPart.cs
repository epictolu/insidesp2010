﻿using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace SandboxedParts.BadWebPart
{
    [ToolboxItemAttribute(false)]
    public class BadWebPart : WebPart
    {
        protected override void CreateChildControls()
        {
        }
    }
}
