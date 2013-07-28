using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.Client;

namespace ManagedClientErrors {
  class Program {
    static void Main(string[] args) {

      using (ClientContext ctx = 
               new ClientContext("http://intranet.wingtip.com")) {
        try {
          //Fails because the object was not initialized
          //Requires Load() and ExecuteQuery()
          Console.WriteLine(ctx.Web.Title);
        }
        catch (PropertyOrFieldNotInitializedException x) {
          Console.WriteLine("Property not initialized. " + x.Message);
        }

        try {
          //Fails because Skip() and Take() are meaningless
          ctx.Load(ctx.Web, w => w.Lists.Skip(5).Take(10));
          ctx.ExecuteQuery();
        }
        catch (InvalidQueryExpressionException x) {
          Console.WriteLine("Invalid LINQ query. " + x.Message);
        }

        try {
          //Fails because InvalidObject is a meaningless object
          InvalidObject o = new InvalidObject(ctx, null);
          ctx.Load(o);
          ctx.ExecuteQuery();
        }
        catch (ClientRequestException x) {
          Console.WriteLine("Bad request. " + x.Message);
        }

        try {
          //Fails because the list does not exist
          //The failure occurs on the server during processing
          ctx.Load(ctx.Web, w => w.Lists);
          List myList = ctx.Web.Lists.GetByTitle("Non-Existant List");
          myList.Description = "A new description";
          myList.Update();
          ctx.ExecuteQuery();

        }
        catch (ServerException x) {
          Console.WriteLine("Exception on server. " + x.Message);
        }
      }
    }

    class InvalidObject : ClientObject {
      public InvalidObject(ClientRuntimeContext c, ObjectPath p)
        : base(c, p) {
      }
    }
  }
}
