using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Drawing;
using System.Web.UI.HtmlControls;

namespace WingtipWebParts.FontConnectionProvider.cs
{
    [ToolboxItemAttribute(false)]
    public class FontConnectionProvider : WebPart, IFontProvider
    {
        [Personalizable, WebBrowsable(true), WebDescription("Text Font Size"), WebDisplayName("Text Font Size")]
        public int TextFontSize { get; set; }

        [Personalizable, WebBrowsable(true), WebDescription("Text Font Color"), WebDisplayName("Text Font Color")]
        public string TextFontColor { get; set; }

        public FontUnit FontSize
        {
            get { return new FontUnit(TextFontSize); }
        }

        public Color FontColor
        {
            get { return Color.FromName(TextFontColor); }
        }

        protected override void CreateChildControls()
        {
            var sizeLabel = new Label { Text = string.Format("Font Size: {0}", FontSize.ToString()) };
            var colorLabel = new Label { Text = string.Format("Font Color: {0}", FontColor.ToString()) };

            Controls.Add(sizeLabel);
            Controls.Add(new HtmlGenericControl("br"));
            Controls.Add(colorLabel);
        }

        [ConnectionProvider("Font Formatting", AllowsMultipleConnections = true)]
        public IFontProvider FontProviderConnectionPoint()
        {
            return this;
        }
    }
}
