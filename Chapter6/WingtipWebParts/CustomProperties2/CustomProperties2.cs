
using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace WingtipWebParts.CustomProperties2
{
    [ToolboxItemAttribute(false)]
    public class CustomProperties2 : WebPart
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
        public TextFontSizeEnum TextFontSize { get; set; }

        [Personalizable(PersonalizationScope.User),
        WebBrowsable(true),
        WebDisplayName("Text Font Color"),
        WebDescription("Description for Text Font Color"),
        Category("Wingtip Web Parts")]
        public TextFontColorEnum TextFontColor { get; set; }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            writer.Write("Check out my Web Part properties");
        }
    }

    public enum TextFontColorEnum
    {
        Purple,
        Green,
        Blue,
        Black
    }

    public enum TextFontSizeEnum
    {
        Fourteen = 14,
        Eighteen = 18,
        TwentyFour = 24,
        ThirtyTwo = 32
    }
}