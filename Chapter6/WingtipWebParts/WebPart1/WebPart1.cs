using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace WingtipWebParts.WebPart1
{
    [ToolboxItemAttribute(false)]
    public class WebPart1 : WebPart
    {
        protected override void RenderContents(HtmlTextWriter writer)
        {
            writer.AddStyleAttribute(HtmlTextWriterStyle.Color, "Blue");
            writer.AddStyleAttribute(HtmlTextWriterStyle.FontSize, "18px");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.Write("Hello World");
            writer.RenderEndTag();
        }
    }
}
