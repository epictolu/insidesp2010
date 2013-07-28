using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Reflection;
using System.Globalization;
using System.Diagnostics.Eventing;
using System.Diagnostics;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.BusinessData;
using Microsoft.SharePoint.BusinessData.Administration;
using Microsoft.SharePoint.BusinessData.SharedService;
using Microsoft.BusinessData.MetadataModel;
using Microsoft.BusinessData.Runtime;

namespace MakeModel {
  public class Program {
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

      AdministrationMetadataCatalog catalog = sap.GetAdministrationMetadataCatalog();

      Model model = Model.Create("MiniCRM", true, catalog);

      LobSystem lob = model.OwnedReferencedLobSystems.Create("Customer", true, SystemType.Database);

      LobSystemInstance lobi = lob.LobSystemInstances.Create("MiniCRM", true);

      lobi.Properties.Add("AuthenticationMode", "PassThrough");
      lobi.Properties.Add("DatabaseAccessProvider", "SqlServer");
      lobi.Properties.Add("RdbConnection Data Source", "CONTOSOSERVER");
      lobi.Properties.Add("RdbConnection Initial Catalog", "MiniCRM.Names");
      lobi.Properties.Add("RdbConnection Integrated Security", "SSPI");
      lobi.Properties.Add("RdbConnection Pooling", "true");

      Entity ect = Entity.Create("Customer", "MiniCRM", true,
                   new Version("1.0.0.0"), 10000,
                   CacheUsage.Default, lob, model, catalog);

      ect.Identifiers.Create("CustomerId", true, "System.Int32");

      Method specificFinder = ect.Methods.Create("GetCustomer", true, false, "GetCustomer");

      specificFinder.Properties.Add("RdbCommandText", "SELECT [CustomerId] , [FirstName] , [LastName] , [Phone] , [EmailAddress] , [CompanyName] FROM MiniCRM.Names WHERE [CustomerId] = @CustomerId");
      specificFinder.Properties.Add("RdbCommandType", "Text");

      Parameter idParam = specificFinder.Parameters.Create("@CustomerId", true, DirectionType.In);

      idParam.CreateRootTypeDescriptor(
          "CustomerId", true, "System.Int32", "CustomerId",
          new IdentifierReference("CustomerId",
              new EntityReference("MiniCRM", "Customer", catalog), catalog),
          null, TypeDescriptorFlags.None, null, catalog);

      Parameter custParam = specificFinder.Parameters.Create("Customer", true, DirectionType.Return);

      TypeDescriptor returnRootCollectionTypeDescriptor =
          custParam.CreateRootTypeDescriptor(
              "Customers", true,
              "System.Data.IDataReader, System.Data, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089",
              "Customers", null, null, TypeDescriptorFlags.IsCollection, null, catalog);

      TypeDescriptor returnRootElementTypeDescriptor =
          returnRootCollectionTypeDescriptor.ChildTypeDescriptors.Create(
              "Customer", true,
              "System.Data.IDataRecord, System.Data, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089",
              "Customer", null, null, TypeDescriptorFlags.None, null);

      returnRootElementTypeDescriptor.ChildTypeDescriptors.Create(
              "CustomerId", true, "System.Int32", "CustomerId",
              new IdentifierReference("CustomerId",
                  new EntityReference("MiniCRM", "Customer", catalog), catalog),
              null, TypeDescriptorFlags.None, null);

      returnRootElementTypeDescriptor.ChildTypeDescriptors.Create(
              "FirstName", true, "System.String", "FirstName",
              null, null, TypeDescriptorFlags.None, null);

      returnRootElementTypeDescriptor.ChildTypeDescriptors.Create(
              "LastName", true, "System.String", "LastName",
              null, null, TypeDescriptorFlags.None, null);

      returnRootElementTypeDescriptor.ChildTypeDescriptors.Create(
              "Phone", true, "System.String", "Phone",
              null, null, TypeDescriptorFlags.None, null);

      returnRootElementTypeDescriptor.ChildTypeDescriptors.Create(
              "EmailAddress", true, "System.String", "EmailAddress",
              null, null, TypeDescriptorFlags.None, null);

      returnRootElementTypeDescriptor.ChildTypeDescriptors.Create(
              "CompanyName", true, "System.String", "CompanyName",
              null, null, TypeDescriptorFlags.None, null);

      specificFinder.MethodInstances.Create("GetCustomer", true, returnRootElementTypeDescriptor,
          MethodInstanceType.SpecificFinder, true);

      Method finder = ect.Methods.Create("GetCustomers", true, false, "GetCustomers");

      finder.Properties.Add("RdbCommandText", "SELECT [CustomerId] , [FirstName] , [LastName] , [Phone] , [EmailAddress] , [CompanyName] FROM MiniCRM.Names");
      finder.Properties.Add("RdbCommandType", "Text");

      Parameter custsParam = finder.Parameters.Create("Customer", true, DirectionType.Return);

      TypeDescriptor returnRootCollectionTypeDescriptor2 =
          custsParam.CreateRootTypeDescriptor(
              "Customers", true,
              "System.Data.IDataReader, System.Data, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089",
              "Customers", null, null, TypeDescriptorFlags.IsCollection, null, catalog);

      TypeDescriptor returnRootElementTypeDescriptor2 =
          returnRootCollectionTypeDescriptor2.ChildTypeDescriptors.Create(
              "Customer", true,
              "System.Data.IDataRecord, System.Data, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089",
              "Customer", null, null, TypeDescriptorFlags.None, null);

      returnRootElementTypeDescriptor2.ChildTypeDescriptors.Create(
              "CustomerId", true, "System.Int32", "CustomerId",
              new IdentifierReference("CustomerId",
                  new EntityReference("MiniCRM", "Customer", catalog), catalog),
              null, TypeDescriptorFlags.None, null);

      returnRootElementTypeDescriptor2.ChildTypeDescriptors.Create(
              "FirstName", true, "System.String", "FirstName",
              null, null, TypeDescriptorFlags.None, null);

      returnRootElementTypeDescriptor2.ChildTypeDescriptors.Create(
              "LastName", true, "System.String", "LastName",
              null, null, TypeDescriptorFlags.None, null);

      returnRootElementTypeDescriptor2.ChildTypeDescriptors.Create(
              "Phone", true, "System.String", "Phone",
              null, null, TypeDescriptorFlags.None, null);

      returnRootElementTypeDescriptor2.ChildTypeDescriptors.Create(
              "EmailAddress", true, "System.String", "EmailAddress",
              null, null, TypeDescriptorFlags.None, null);

      returnRootElementTypeDescriptor2.ChildTypeDescriptors.Create(
              "CompanyName", true, "System.String", "CompanyName",
              null, null, TypeDescriptorFlags.None, null);

      finder.MethodInstances.Create("GetCustomers", true, returnRootCollectionTypeDescriptor2,
          MethodInstanceType.Finder, true);

      ect.Activate();
    }
  }
}

