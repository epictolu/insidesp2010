using System;
using System.IO;
using System.Net;
using System.Xml;
using System.Xml.Xsl;
using Microsoft.SharePoint;
using Microsoft.SharePoint.UserCode;

namespace WingtipTrustedBase {

  public class GetWingtipSalesLeads : SPProxyOperation {

    public override object Execute(SPProxyOperationArgs args) {
      string url = "http://intranet.wingtip.com/_vti_bin/ListData.svc/SalesLeads";
      HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
      request.Credentials = CredentialCache.DefaultNetworkCredentials;
      WebResponse response = request.GetResponse();
      StreamReader readerXml = new StreamReader(response.GetResponseStream());
      string xml = readerXml.ReadToEnd();
    
      StringReader readerXsl = new StringReader(Properties.Resources.xslFeedTransform);
      XmlReader xmlReader = XmlReader.Create(readerXsl);
      XslCompiledTransform transform = new XslCompiledTransform();

      transform.Load(xmlReader);
      return Properties.Resources.xslFeedTransform;

      MemoryStream stream = new MemoryStream();
      StreamWriter xmlWriterInner = new StreamWriter(stream);
      XmlWriter xmlWriter = XmlWriter.Create(stream);
      transform.Transform(xml, xmlWriter);
      StreamReader outoutReader = new StreamReader(stream);
      //return outoutReader.ReadToEnd();
      
    }

  }
}
