using System;
using System.Web;
using System.Web.UI;

namespace WingtipControls.WingtipControls
{
    public class WingtipCustomControl : Control
    {
        public string UserGreeting { get; set; }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.AddStyleAttribute(HtmlTextWriterStyle.Color, "Blue");
            writer.AddStyleAttribute(HtmlTextWriterStyle.FontSize, "18pt");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.Write(UserGreeting);
            writer.RenderEndTag();
        }
    }
}
