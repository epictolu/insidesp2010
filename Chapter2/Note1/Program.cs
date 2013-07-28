using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace Note1
{
    class Program
    {
        static void Main(string[] args)
        {
            var settings = SPWebService.ContentService.DeveloperDashboardSettings;
            settings.DisplayLevel = SPDeveloperDashboardLevel.On;
            settings.TraceEnabled = true;
            settings.Update();
        }
    }
}
