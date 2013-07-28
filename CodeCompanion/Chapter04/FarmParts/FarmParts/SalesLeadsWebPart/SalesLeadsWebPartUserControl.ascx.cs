using System;
using System.IO;
using System.Net;
using System.Xml;
using System.Xml.Xsl;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace FarmParts.SalesLeadsWebPart {
  public partial class SalesLeadsWebPartUserControl : UserControl {
    
    protected override void OnPreRender(EventArgs e) {
      string url = "http://intranet.wingtip.com/_vti_bin/ListData.svc/SalesLeads";
      HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
      request.Credentials = CredentialCache.DefaultNetworkCredentials;
      WebResponse response = request.GetResponse();
      StreamReader reader = new StreamReader(response.GetResponseStream());
      //XmlDocument xdoc = new XmlDocument();
      //xdoc.Load(reader);

      xmlSalesLeads.DocumentContent = reader.ReadToEnd();
      xmlSalesLeads.TransformSource = @"/_layouts/FarmParts/FeedTransform.xsl";
    }
    
  }
}
