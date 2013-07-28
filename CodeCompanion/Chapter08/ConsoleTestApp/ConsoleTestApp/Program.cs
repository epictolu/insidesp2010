using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace ConsoleTestApp {
  class Program {

    static void Main() {

      //DisplayWebTemplates();
      //DisplayFeatureDefinitons();
      //DisplayFeaturesEnabledInSite();
      DisplayStapledFeaturesBasic();
      DisplayStapledFeaturesWorkflow();      
      DisplayStapledFeaturesPremium();
      DisplayStapledFeaturesPublishing();
    }

    static void DisplayWebTemplates() {
      SPSite sc = new SPSite("http://intranet.wingtip.com");
      SPWeb site = sc.RootWeb;

      foreach (SPWebTemplate temp in sc.GetWebTemplates(1033)) {
        Console.Write(temp.Name);
        Console.Write(" - ");
        Console.WriteLine(temp.Title);        
      }

    }

    static void DisplayFeatureDefinitons() {
      SPFarm farm = SPFarm.Local;
      foreach (SPFeatureDefinition def in farm.FeatureDefinitions) {
        Console.Write(def.Id.ToString());
        Console.Write(" - ");
        Console.WriteLine(def.GetTitle(new System.Globalization.CultureInfo(1033)));
      }


    }

    static void DisplayFeaturesEnabledInSite() {

      SPSite sc = new SPSite("http://intranet.wingtip.com");

      Console.WriteLine("Site collection scoped features:");
      foreach (SPFeature feature in sc.Features) {
        Console.Write(feature.Definition.Id.ToString());
        Console.Write(" - ");
        Console.WriteLine(feature.Definition.GetTitle(new System.Globalization.CultureInfo(1033)));
      }

      Console.WriteLine();
      Console.WriteLine("Site scoped features:");
      foreach (SPFeature webfeature in sc.RootWeb.Features) {
        Console.Write(webfeature.Definition.Id.ToString());
        Console.Write(" - ");
        Console.WriteLine(webfeature.Definition.GetTitle(new System.Globalization.CultureInfo(1033)));
      }

    }

    static void DisplayStapledFeaturesBasic() {
      SPFarm farm = SPFarm.Local;
      List<Guid> featureIds = new List<Guid> {
        new Guid("B21B090C-C796-4b0f-AC0F-7EF1659C20AE"),
        new Guid("99FE402E-89A0-45aa-9163-85342E865DC8"),
        new Guid("541F5F57-C847-4e16-B59A-B31E90E6F9EA"),
        new Guid("4BCCCD62-DCAF-46dc-A7D4-E38277EF33F4"),
        new Guid("068BC832-4951-11DC-8314-0800200C9A66"),
        new Guid("7094BD89-2CFE-490a-8C7E-FBACE37B4A34"),
        new Guid("063C26FA-3CCC-4180-8A84-B6F98E991DF3"),
        new Guid("7201D6A4-A5D3-49A1-8C19-19C4BAC6E668"),
        new Guid("915c240e-a6cc-49b8-8b2c-0bff8b553ed3"),
      };

      Console.WriteLine("Features stapled by BaseSiteStapling feature");
      foreach (Guid id in featureIds) {
        SPFeatureDefinition def = farm.FeatureDefinitions[id];
        Console.Write(def.Id.ToString());
        Console.Write(" - ");
        Console.WriteLine(def.GetTitle(new System.Globalization.CultureInfo(1033)));     
      }
      Console.WriteLine();
      
    }

    static void DisplayStapledFeaturesWorkflow() {

      SPFarm farm = SPFarm.Local;
      List<Guid> featureIds = new List<Guid> {
        new Guid("C85E5759-F323-4EFB-B548-443D2216EFB5"),
        new Guid("0AF5989A-3AEA-4519-8AB0-85D91ABE39FF"),
        new Guid("A44D2AA3-AFFC-4d58-8DB4-F4A3AF053188")

      };

      Console.WriteLine("Features stapled by StapledWorkflows feature");
      foreach (Guid id in featureIds) {
        SPFeatureDefinition def = farm.FeatureDefinitions[id];
        Console.Write(def.Id.ToString());
        Console.Write(" - ");
        Console.WriteLine(def.GetTitle(new System.Globalization.CultureInfo(1033)));
      }
      Console.WriteLine();

    }

    static void DisplayStapledFeaturesPremium() {
      
      SPFarm farm = SPFarm.Local;
      List<Guid> featureIds = new List<Guid> {
        new Guid("8581A8A7-CF16-4770-AC54-260265DDB0B2"),
        new Guid("4248E21F-A816-4c88-8CAB-79D82201DA7B"),
        new Guid("0806D127-06E6-447a-980E-2E90B03101B8")

      };


      Console.WriteLine("Features stapled by PremiumSiteStapling feature");
      foreach (Guid id in featureIds) {
        SPFeatureDefinition def = farm.FeatureDefinitions[id];
        Console.Write(def.Id.ToString());
        Console.Write(" - ");
        Console.WriteLine(def.GetTitle(new System.Globalization.CultureInfo(1033)));     
      }
      Console.WriteLine();
    
    }

    static void DisplayStapledFeaturesPublishing() {

      SPFarm farm = SPFarm.Local;
      List<Guid> featureIds = new List<Guid> {
        new Guid("F6924D36-2FA8-4f0b-B16D-06B7250180FA"),
        new Guid("94C94CA6-B32F-4da9-A9E3-1F3D343D7ECB")
      };

      Console.WriteLine("Features stapled by PublishingStapling feature");
      foreach (Guid id in featureIds) {
        SPFeatureDefinition def = farm.FeatureDefinitions[id];
        Console.Write(def.Id.ToString());
        Console.Write(" - ");
        Console.WriteLine(def.GetTitle(new System.Globalization.CultureInfo(1033)));
      }
      Console.WriteLine();

      }


    
  }
}
