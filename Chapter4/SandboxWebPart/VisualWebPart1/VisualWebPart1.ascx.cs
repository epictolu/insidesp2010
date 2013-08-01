using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint.Utilities;
using System.Collections.Generic;

namespace SandboxWebPart.VisualWebPart1
{
    [ToolboxItemAttribute(false)]
    public partial class VisualWebPart1 : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling using
        // the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public VisualWebPart1()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected override void RenderContents(System.Web.UI.HtmlTextWriter writer)
        {
            base.RenderContents(writer);

            var operationAssemblyName = "FullTrustProxy, Version=1.0.0.0, Culture=neutral, PublicKeyToken=adfe4e9f5594a2b3";
            var operationTypeName = "FullTrustProxy.GetWebApplications";

            var webApplications = SPUtility.ExecuteRegisteredProxyOperation(operationAssemblyName, operationTypeName, null) as IList<string>;

            foreach (var webApplication in webApplications)
                writer.Write(webApplication + "<br />");
        }
    }
}
