using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.Client;

namespace ManagedOMDocs {
  class Program {
    static void Main(string[] args) {

      using (ClientContext ctx = 
               new ClientContext("http://intranet.wingtip.com")) {
        
        //Get site
        Web site = ctx.Web;
        ctx.Load(site);
        ctx.ExecuteQuery();

        //Create a new library
        ListCreationInformation listCI = new ListCreationInformation();
        listCI.Title = "My Docs";
        listCI.Description = "A library for use with Client OM";
        listCI.TemplateType = (int)ListTemplateType.DocumentLibrary;
        listCI.QuickLaunchOption = Microsoft.SharePoint.Client.QuickLaunchOptions.On;
        List list = site.Lists.Add(listCI);
        ctx.ExecuteQuery();

        //Create a document
        MemoryStream m = new MemoryStream();
        StreamWriter w = new StreamWriter(m);
        w.Write("Some content for the document.");
        w.Flush();

        //Add it to the library
        FileCreationInformation fileCI = new FileCreationInformation();
        fileCI.Content = m.ToArray();
        fileCI.Overwrite = true;
        fileCI.Url = "http://intranet.wingtip.com/My%20Docs/MyFile.txt";

        Folder rootFolder = site.GetFolderByServerRelativeUrl("My%20Docs");
        ctx.Load(rootFolder);
        Microsoft.SharePoint.Client.File newFile = rootFolder.Files.Add(fileCI);
        ctx.ExecuteQuery();


        //Edit Properties
        ListItem newItem = newFile.ListItemAllFields;
        ctx.Load(newItem);
        newItem["Title"] = "My new file";
        newItem.Update();
        ctx.ExecuteQuery();

        //Delete file
        // newItem.DeleteObject();
        // ctx.ExecuteQuery();
      }
    }
  }
}
