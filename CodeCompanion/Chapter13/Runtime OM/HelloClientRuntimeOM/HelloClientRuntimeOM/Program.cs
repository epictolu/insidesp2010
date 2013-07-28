using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.BusinessData;
using Microsoft.BusinessData.Runtime;
using Microsoft.BusinessData.MetadataModel;
using Microsoft.BusinessData.MetadataModel.Collections;
using Microsoft.Office.BusinessData.MetadataModel;
using Microsoft.Office.BusinessData.Runtime;


namespace HelloClientRuntimeOM {
  class Program {
    static void Main(string[] args) {
      RemoteSharedFileBackedMetadataCatalog catalog = new RemoteSharedFileBackedMetadataCatalog();
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
