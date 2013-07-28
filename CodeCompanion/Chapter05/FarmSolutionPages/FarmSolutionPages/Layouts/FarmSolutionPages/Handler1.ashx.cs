using System;
using System.Linq;
using System.Text;
using System.Web;
using Microsoft.SharePoint;
using System.Xml;
using System.Xml.Linq;

namespace FarmSolutionPages {

  public class Handler1 : IHttpHandler {

    public void ProcessRequest(HttpContext context) {

      // gather info
      SPSite siteCollection = SPContext.Current.Site;
      SPWeb topLevelSite = siteCollection.RootWeb;

      var xDec = new XDeclaration("1.0", "utf-8", "yes");
      var xDoc = new XDocument(xDec, new XElement("SiteCollection"));

      xDoc.Root.Add(new XAttribute("URL", siteCollection.Url));


      foreach (SPWeb site in siteCollection.AllWebs) {
        xDoc.Root.Add(
          new XElement("Site",
            new XAttribute("Title", site.Title), 
            new XAttribute("URL", site.Url)));
      }

      context.Response.Clear();
      context.Response.ContentType = "text/xml";      
      context.Response.Write(xDoc.Declaration);
      context.Response.Write(xDoc);
      context.Response.End();

    }

    public bool IsReusable {
      get {
        return false;
      }
    }

  }


}
