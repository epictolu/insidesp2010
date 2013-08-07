using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Drawing;

namespace WingtipWebParts.WingtipWebParts.WebPart2
{
    [ToolboxItemAttribute(false)]
    public class WebPart2 : WebPart
    {
        protected Label label;

        protected override void CreateChildControls()
        {
            label = new Label();
            label.Text = "Hello Child Controls";
            label.Font.Size = new FontUnit(18);
            label.ForeColor = Color.Blue;

            Controls.Add(label);
        }
    }
}
