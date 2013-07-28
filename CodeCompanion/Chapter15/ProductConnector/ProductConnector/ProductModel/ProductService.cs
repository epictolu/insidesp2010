using System;
using System.Security;
using System.Security.Principal;
using System.Security.AccessControl;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.Office.Server;
using Microsoft.Office.Server.Diagnostics;

namespace ProductConnector.ProductModel {

  public class ProductService {

    public static Product ReadItem(string id) {
      try {
        XDocument d = XDocument.Load(SPUtility.GetGenericSetupPath("/") + "TEMPLATE\\LAYOUTS\\ProductConnector\\ProductData.xml");

        var q = from c in d.Descendants("Product")
                where c.Attribute("ID").Value.Equals(id)
                select new {
                  ID = c.Attribute("ID").Value,
                  Name = c.Attribute("Name").Value,
                  Manufacturer = c.Attribute("Manufacturer").Value
                };

        Product product = new Product() {
          ID = q.First().ID,
          Name = q.First().Name,
          Manufacturer = q.First().Manufacturer,
          SecurityDescriptor = ReadSecurityDescriptor(
          q.First().ID, WindowsIdentity.GetCurrent().Name)
        };

        return product;
      }
      catch (Exception x) {
        PortalLog.LogString("Product Model (Read Item): {0}", x.Message);
        return null;
      }

    }

    public static IEnumerable<Product> ReadList() {
      try {
        XDocument d = XDocument.Load(SPUtility.GetGenericSetupPath("/") + "TEMPLATE\\LAYOUTS\\ProductConnector\\ProductData.xml");

        var q = from c in d.Descendants("Product")
                select new {
                  ID = c.Attribute("ID").Value,
                  Name = c.Attribute("Name").Value,
                  Manufacturer = c.Attribute("Manufacturer").Value
                };

        List<Product> products = new List<Product>();

        foreach (var p in q) {
          products.Add(new Product() {
            ID = p.ID,
            Name = p.Name,
            Manufacturer = p.Manufacturer,
            SecurityDescriptor = ReadSecurityDescriptor(
            p.ID, WindowsIdentity.GetCurrent().Name)
          });

        }

        return products;

      }
      catch (Exception x) {
        PortalLog.LogString("Product Model (ReadList): {0}", x.Message);
        return null;
      }
    }

    public static byte[] ReadSecurityDescriptor(string id, string username) {
      try {

        //Grant everyone access
        NTAccount workerAcc = new NTAccount(username.Split('\\')[0], username.Split('\\')[1]);
        SecurityIdentifier workerSid = (SecurityIdentifier)workerAcc.Translate(typeof(SecurityIdentifier));
        SecurityIdentifier everyone = new SecurityIdentifier(WellKnownSidType.WorldSid, null);
        CommonSecurityDescriptor csd = new CommonSecurityDescriptor(false, false, ControlFlags.None, workerSid, null, null, null);
        csd.SetDiscretionaryAclProtection(true, false);
        csd.DiscretionaryAcl.AddAccess(AccessControlType.Allow, everyone, unchecked((int)0xffffffffL), InheritanceFlags.None, PropagationFlags.None);

        byte[] secDes = new byte[csd.BinaryLength];
        csd.GetBinaryForm(secDes, 0);
        return secDes;

      }
      catch (Exception x) {
        PortalLog.LogString("Product Model (ReadSecurityDescriptor): {0}", x.Message);
        return null;
      }
    }
  }
}
