using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.Client;

namespace HelloClientOMConsole {
  class Program {
    static void Main(string[] args) {
      try {
        //Get a context
        using (ClientContext clientContext = new
            ClientContext("http://intranet.wingtip.com")) {

          //Get the Site Collection
          Site siteCollection = clientContext.Site;

          //Try to read the URL
          try {
            Console.WriteLine("Trying to read the Site Collection URL");
            Console.WriteLine(siteCollection.Url);
          }
          catch (PropertyOrFieldNotInitializedException x) {
            Console.WriteLine("Failed to read the Site Collection URL");
            Console.WriteLine(x.Message);
            Console.WriteLine();
          }

          //Fill the Url property
          Console.WriteLine("Requesting the Site Collection URL from the server...");
          clientContext.Load(siteCollection);
          clientContext.ExecuteQuery();

          //Try to read it again
          Console.WriteLine("Site Collection Url: " + siteCollection.Url);
          Console.WriteLine();

          //Get the site
          Web site = clientContext.Web;

          //Try to read the Title
          try {
            Console.WriteLine("Trying to read the Site Title");
            Console.WriteLine(site.Title);
          }
          catch (PropertyOrFieldNotInitializedException x) {
            Console.WriteLine("Failed to read the Site Title");
            Console.WriteLine(x.Message);
            Console.WriteLine();
          }

          //Fill the Title Property
          Console.WriteLine("Requesting the Site Title from the server...");
          clientContext.Load(site, s => s.Title, s => s.ServerRelativeUrl);
          clientContext.ExecuteQuery();

          //Try to read Title now
          Console.WriteLine("Site Title: " + site.Title);

          //Get list titles
          Console.WriteLine();
          Console.WriteLine("Lists in this site");

          clientContext.Load(site.Lists);
          clientContext.ExecuteQuery();

          //Include titles (method syntax)
          clientContext.Load(clientContext.Web,
              x => x.Lists.Include(l => l.Title).Where(l => l.Title != null));

          clientContext.ExecuteQuery();

          foreach (List list in clientContext.Web.Lists) {
            Console.WriteLine(list.Title);
          }

          //Include titles (query syntax)
          var q = from l in clientContext.Web.Lists
                  where l.Title != null
                  select l;

          var r = clientContext.LoadQuery(q);
          clientContext.ExecuteQuery();

          foreach (var i in r) {
            Console.WriteLine(i.Title);
          }

          Console.WriteLine(); Console.WriteLine();
        }
      }
      catch (InvalidQueryExpressionException x) {
        Console.WriteLine(x.Message);
      }
      catch (ClientRequestException x) {
        Console.WriteLine(x.Message);
      }
      catch (PropertyOrFieldNotInitializedException x) {
        Console.WriteLine(x.Message);
      }
      catch (ServerUnauthorizedAccessException x) {
        Console.WriteLine(x.Message);
      }
      catch (ServerException x) {
        Console.WriteLine(x.Message);
      }
      catch (Exception x) {
        Console.WriteLine(x.Message);
      }
    }
  }
}
