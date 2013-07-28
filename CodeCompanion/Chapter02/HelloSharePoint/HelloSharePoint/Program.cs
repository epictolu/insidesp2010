using System;
using Microsoft.SharePoint;

namespace HelloSharePoint {
  class Program {
    static void Main() {
      const string siteUrl = "http://intranet.wingtip.com";
      using (SPSite siteCollection = new SPSite(siteUrl)) {
        SPWeb site = siteCollection.RootWeb;
        foreach (SPList list in site.Lists) {
          if (!list.Hidden) {
            Console.WriteLine(list.Title);
          }
        }
      }
    }
  }
}
