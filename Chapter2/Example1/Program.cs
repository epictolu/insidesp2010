using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;

namespace Example1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var siteCollection = new SPSite("http://epicserver/sites/insidesp2010"))
            {
                var site = siteCollection.RootWeb;

                foreach (SPList list in site.Lists)
                {
                    if (!list.Hidden)
                        Console.WriteLine(list.Title);
                }
            }
        }
    }
}
