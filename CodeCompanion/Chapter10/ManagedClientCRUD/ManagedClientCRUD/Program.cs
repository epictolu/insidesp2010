using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.Client;

namespace ManagedClientCRUD {
  class Program {
    static void Main(string[] args) {
      using (ClientContext ctx = new ClientContext("http://intranet.wingtip.com")) {
        //Create a new list
        ListCreationInformation listCI = new ListCreationInformation();
        listCI.Title = "My List";
        listCI.Description += "A list for use with the Client OM";
        listCI.TemplateType = (int)ListTemplateType.GenericList;
        listCI.QuickLaunchOption = Microsoft.SharePoint.Client.QuickLaunchOptions.On;
        Microsoft.SharePoint.Client.List list = ctx.Web.Lists.Add(listCI);

        ctx.ExecuteQuery();
        Console.WriteLine("List Created!");

        //Create a new list item
        ListItemCreationInformation listItemCI = new Microsoft.SharePoint.Client.ListItemCreationInformation();
        ListItem item = list.AddItem(listItemCI);

        item["Title"] = "New Item";
        item.Update();

        ctx.ExecuteQuery();
        Console.WriteLine("Item Created!");

        //Read the Site, List, and Items
        ctx.Load(ctx.Web);

        List myList = ctx.Web.Lists.GetByTitle("My List");
        ctx.Load(myList);

        StringBuilder caml = new StringBuilder();
        caml.Append("<View><Query>");
        caml.Append("<Where><Eq><FieldRef Name='Title'/>");
        caml.Append("<Value Type='Text'>New Item</Value></Eq></Where>");
        caml.Append("</Query><RowLimit>100</RowLimit></View>");

        CamlQuery query = new CamlQuery();
        query.ViewXml = caml.ToString();
        ListItemCollection myItems = myList.GetItems(query);
        ctx.Load(myItems);

        ctx.ExecuteQuery();
        Console.WriteLine("Site: " + ctx.Web.Title);
        Console.WriteLine("List: " + myList.Title);
        Console.WriteLine("Item Count: " + myItems.Count.ToString());

        //Update the Site, List, and Items
        ctx.Web.Description = "Client OM samples";
        ctx.Web.Update();

        myList.Description = "Client OM data";
        myList.Update();

        foreach (ListItem myItem in myItems) {
          myItem["Title"] = "Updated";
          myItem.Update();
        }

        ctx.ExecuteQuery();
        Console.WriteLine("Site: " + ctx.Web.Description);
        Console.WriteLine("List: " + myList.Description);
        Console.WriteLine("Item Count: " + myItems.Count.ToString());

        //Delete List
        myList.DeleteObject();
        ctx.ExecuteQuery();
        Console.WriteLine("List Deleted!");
      }
    }
  }
}
