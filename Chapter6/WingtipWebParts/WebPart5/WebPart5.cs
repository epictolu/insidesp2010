using System;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using System.Net;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace WingtipWebParts.WebPart5
{
    [ToolboxItemAttribute(false)]
    public class WebPart5 : WebPart
    {
        protected override void RenderContents(HtmlTextWriter writer)
        {
            var urlRSS = "http://feeds.feedburner.com/AndrewConnell";

            var request = WebRequest.CreateDefault(new Uri(urlRSS));
            var response = request.GetResponse();
            var responseStream = response.GetResponseStream();

            var xsltContent = Properties.Resources.RssFeedToHtml;

            var transform = new XslCompiledTransform();
            var xslt = XmlReader.Create(new StringReader(xsltContent));
            transform.Load(xslt);

            using (var reader = new XmlTextReader(responseStream))
            {
                var results = new XmlTextWriter(writer.InnerWriter);
                transform.Transform(reader, results);
            }
        }
    }
}
