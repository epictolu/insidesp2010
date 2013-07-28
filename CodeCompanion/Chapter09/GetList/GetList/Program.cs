using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;

namespace GetList {
  class Program {
    static void Main(string[] args) {
      using (SPSite siteCollection = new SPSite("http://intranet.wingtip.com")) {
        using (SPWeb site = siteCollection.OpenWeb()) {
          Console.WriteLine("Accessing list collection");
          foreach (SPList list in site.Lists) {
            Console.WriteLine(list.Title);
          }
          Console.WriteLine();

          Console.WriteLine("Access a specifc list by name");
          SPList myList = site.Lists.TryGetList("Toys");
          if (myList != null)
            Console.WriteLine(myList.Title);
          else
            Console.WriteLine("List not found");
          Console.WriteLine();

          Console.WriteLine("Access a specific list by URL");
          try {
            SPList urlList = site.GetList("/Lists/Tasks");
            Console.WriteLine(urlList.Title);
          }
          catch (FileNotFoundException x) {
            Console.WriteLine("List not found");
          }

          Console.WriteLine();

        }
      }
    }
  }
}
