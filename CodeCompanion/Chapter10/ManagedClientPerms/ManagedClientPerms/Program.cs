using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.Client;

namespace ManagedClientPerms {
  class Program {
    static void Main(string[] args) {

      using (ClientContext ctx = 
               new ClientContext("http://intranet.wingtip.com")) {
        ctx.Load(ctx.Web);
        ctx.ExecuteQuery();

        List list = null;

        //Set up permissions mask
        BasePermissions permissions = new BasePermissions();
        permissions.Set(PermissionKind.ManageLists);

        //Conditional scope
        ConditionalScope scope = new ConditionalScope(
            ctx,
            () => ctx.Web.DoesUserHavePermissions(permissions).Value == true);

        using (scope.StartScope()) {
          //Operations are limited in scopes
          //so just read
          Console.WriteLine(ctx.Web.Title);
        }

        //Execute
        ctx.ExecuteQuery();

        //Did scope execute?
        Console.WriteLine(scope.TestResult.Value);

        if (scope.TestResult.Value) {
          //Create a new list, we have permissions
          ListCreationInformation listCI = new ListCreationInformation();
          listCI.Title = "My List";
          listCI.TemplateType = (int)ListTemplateType.GenericList;
          listCI.QuickLaunchOption = Microsoft.SharePoint.Client.QuickLaunchOptions.On;

          list = ctx.Web.Lists.Add(listCI);
          ctx.ExecuteQuery();

          Console.WriteLine(list.Title);
        }
      }
    }
  }
}

