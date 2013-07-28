using System;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace HelloLinqPart.VisualPart
{
    public class LinqSources : WebPart
    {
        protected const string _ascxPath = @"~/_CONTROLTEMPLATES/HelloLinqPart_LinqSources/LinqSourcesUserControl.ascx";
        Label messages = default(Label);

        public LinqSources()
        {
        }

        protected override void CreateChildControls()
        {
            try
            {
                messages = new Label();
                messages.Text = "Use this web part to perform LINQ queries";
                Controls.Add(messages);
                Control control = this.Page.LoadControl(_ascxPath);
                Controls.Add(control);
            }
            catch (Exception x)
            {
                messages.Text = x.Message;
            }
            finally
            {
                base.CreateChildControls();
            }
        }

    }
}
