using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using WingtipWebParts.FontConnectionProvider;
using System.Web.UI.HtmlControls;

namespace WingtipWebParts.FontConnectionConsumer.cs
{
    [ToolboxItemAttribute(false)]
    public class FontConnectionConsumer : WebPart
    {
        IFontProvider FontProvider { get; set; }

        [ConnectionConsumer("Font Formatting", AllowsMultipleConnections=false)]
        public void FontProviderConnectionPoint(IFontProvider provider)
        {
            FontProvider = provider;
        }

        protected override void CreateChildControls()
        {
            if (FontProvider == null)
                return;

            var fontSize = new Label { Text = string.Format("Font Size: {0}", FontProvider.FontSize) };
            var fontColor = new Label { Text = string.Format("Font Color: {0}", FontProvider.FontColor) };

            Controls.Add(fontSize);
            Controls.Add(new HtmlGenericControl("br"));
            Controls.Add(fontColor);
        }
    }
}
