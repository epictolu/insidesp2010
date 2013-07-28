using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.BusinessData;
using Microsoft.SharePoint.BusinessData.SharedService;
using Microsoft.SharePoint.BusinessData.MetadataModel;

using Microsoft.BusinessData;
using Microsoft.BusinessData.MetadataModel;
using Microsoft.BusinessData.Runtime;

namespace HelloRuntimeOM {
  class Program {
    static void Main(string[] args) {

      //The following works outside the SharePoint context
      SPFarm farm = SPFarm.Local;
      SPServiceProxyCollection spc = farm.ServiceProxies;
      BdcServiceApplicationProxy sap = 
        (BdcServiceApplicationProxy)((
          from sp in spc
          where sp.TypeName.Equals("Business Data Connectivity Service")
          select sp).First().ApplicationProxies.First());

      // The following works in the SharePoint Context
      //BdcServiceApplicationProxy p = (BdcServiceApplicationProxy)SPServiceContext.Current.GetDefaultProxy(typeof(BdcServiceApplicationProxy));

      DatabaseBackedMetadataCatalog catalog = sap.GetDatabaseBackedMetadataCatalog();
      IEntity ect = catalog.GetEntity("http://intranet.wingtip.com", "Campaign");
      ILobSystem lob = ect.GetLobSystem();
      ILobSystemInstance lobi = lob.GetLobSystemInstances()["Advertising"];
      IFilterCollection filter = ect.GetDefaultFinderFilters();
      IEntityInstanceEnumerator ects = ect.FindFiltered(filter, lobi);
      while (ects.MoveNext()) {
        Console.WriteLine(ects.Current["CampaignName"].ToString());
      }

      Console.WriteLine("Pausing...");
      Console.ReadLine();

      IView v = ect.GetCreatorView("Create");
      IFieldValueDictionary dict = v.GetDefaultValues();
      dict["CampaignName"] = "Fantastic Fall";
      dict["StartDate"] = new DateTime(2009, 10, 15);
      dict["EndDate"] = new DateTime(2009, 12, 1);
      dict["GeographyId"] = 1;
      dict["LastUpdate"] = DateTime.Today;
      Identity id = ect.Create(dict, lobi);

      Console.WriteLine("Success!");
      Console.ReadLine();
    }
  }
}
