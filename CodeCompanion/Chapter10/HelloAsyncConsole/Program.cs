using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.Client;

namespace HelloAsyncConsole {
  class Program {
    delegate void Async();
    ClientContext context;
    Web site;

    static void Main(string[] args) {
      Program p = new Program();
      p.Run();
      Console.WriteLine("Waiting...");
    }

    void callback(IAsyncResult arg) {
      Console.WriteLine("Callback received...");
      Console.WriteLine(site.Title);
      Console.WriteLine("Callback done!");
    }

    public void Run() {
      context = new ClientContext("http://intranet.wingtip.com");
      site = context.Web;
      context.Load(site, s => s.Title);
      Async async = new Async(context.ExecuteQuery);
      async.BeginInvoke(callback, null);
    }


  }
}
