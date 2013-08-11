using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Net;
using System.IO;
using System.Xml;
using System.Xml.Xsl;

namespace WingtipWebParts.AsyncRssReader
{
    [ToolboxItemAttribute(false)]
    public class AsyncRssReader : WebPart
    {
        [Personalizable(PersonalizationScope.User),
         WebBrowsable(true),
         WebDisplayName("RSS Source URL:"),
         WebDescription("Provides address for RSS feed"),
         Category("Wingtip Web Parts")]
        public string RssSourceUrl { get; set; }

        WebRequest request;
        Stream response;
        bool requestTimedOut;

        protected override void OnPreRender(EventArgs e)
        {
            if (WebPartManager.DisplayMode.AllowPageDesign)
                return;

            RssSourceUrl = RssSourceUrl ?? "http://feeds.feedburner.com/AndrewConnell";

            request = WebRequest.CreateDefault(new Uri(RssSourceUrl));
            var task1 = new PageAsyncTask(Task1Begin, Task1End, Task1Timeout, null);
            Page.AsyncTimeout = new TimeSpan(0, 0, 10);
            Page.RegisterAsyncTask(task1);
        }

        IAsyncResult Task1Begin(object sender, EventArgs e, AsyncCallback callback, object state)
        {
            return request.BeginGetResponse(callback, state);
        }

        void Task1End(IAsyncResult result)
        {
            response = request.EndGetResponse(result).GetResponseStream();
        }

        void Task1Timeout(IAsyncResult result)
        {
            requestTimedOut = true;
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            if (WebPartManager.DisplayMode.AllowPageDesign)
            {
                writer.Write("No RSS Reading while in design mode");
                return;
            }

            if (requestTimedOut || response == null)
            {
                writer.Write("Request for RSS feed timed out");
                return;
            }

            var transform = new XslCompiledTransform();
            var xsltRssfeedToHtml = Properties.Resources.RssFeedToHtml;
            var xslt = XmlReader.Create(new StringReader(xsltRssfeedToHtml));

            transform.Load(xslt);
            var reader = new XmlTextReader(response);

            var results = new XmlTextWriter(writer.InnerWriter);
            transform.Transform(reader, results);
            reader.Close();
        }
    }
}
