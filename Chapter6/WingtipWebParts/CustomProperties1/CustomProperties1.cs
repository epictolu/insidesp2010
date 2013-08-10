using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace WingtipWebParts.CustomProperties1
{
    [ToolboxItemAttribute(false)]
    public class CustomProperties1 : WebPart
    {
        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        WebDisplayName("User Greeting"),
        WebDescription("Description for User Greeting"),
        Category("Wingtip Web Parts")]
        public string UserGreeting { get; set; }

        [Personalizable(PersonalizationScope.User),
        WebBrowsable(true),
        WebDisplayName("Text Font Size"),
        WebDescription("Description for Text Font Size"),
        Category("Wingtip Web Parts")]
        public string TextFontSize { get; set; }

        [Personalizable(PersonalizationScope.User),
        WebBrowsable(true),
        WebDisplayName("Text Font Color"),
        WebDescription("Description for Text Font Color"),
        Category("Wingtip Web Parts")]
        public string TextFontColor { get; set; }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            writer.Write("Check out my Web Part properties");
        }
    }
}
