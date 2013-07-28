using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;

namespace WingtipDocuments {
  class DocumentLibraryFactory {

    public static void CreateProductSpecificationsLibrary(SPWeb site) {

      SPList list = site.Lists.TryGetList("Product Specifications");
      if (list != null)
        list.Delete();

      // create new document library
      Guid DocLibID =
        site.Lists.Add("ProductSpecifications", "", SPListTemplateType.DocumentLibrary);
      SPDocumentLibrary DocLib = (SPDocumentLibrary)site.Lists[DocLibID];
      DocLib.Title = "Product Specifications";
      DocLib.ForceCheckout = true;
      DocLib.EnableVersioning = true;
      DocLib.MajorVersionLimit = 3;
      DocLib.EnableMinorVersions = true;
      DocLib.MajorWithMinorVersionsLimit = 5;
      DocLib.OnQuickLaunch = true;

      SPFolder formsFolder = DocLib.RootFolder.SubFolders["Forms"];
      byte[] docTemplateContent = Properties.Resources.Specification_dotx;
      formsFolder.Files.Add("Specification.dotx", docTemplateContent);
      DocLib.DocumentTemplateUrl = @"ProductSpecifications/Forms/Specification.dotx";

      DocLib.Update();

    }

    public static void CreateContentTypeWithStandardDocTemplate(SPWeb site) {

      SPList list = site.Lists.TryGetList("Wingtip Documents");
      if (list != null)
        list.Delete();

      try { site.ContentTypes["Wingtip Document"].Delete(); }
      catch { }

      string ctypeName = "Wingtip Document";
      SPContentType ctypeParent = site.ContentTypes["Document"];
      SPContentType ctype = new SPContentType(ctypeParent, site.ContentTypes, ctypeName);
      ctype.Description = "Content type using standard Wingtip document template";
      ctype.Group = "Wingtip Content Types";
      ctype = site.ContentTypes.Add(ctype);

      SPFolder ctypeFolder = site.GetFolder("_cts/Wingtip Document");
      byte[] docTemplateContent = Properties.Resources.WingtipStandard_dotx;
      ctypeFolder.Files.Add("WingtipStandard.dotx", docTemplateContent);
      ctype.DocumentTemplate = "WingtipStandard.dotx";
      ctype.Update();

      Guid DocLibId = site.Lists.Add("WingtipDocuments", "", SPListTemplateType.DocumentLibrary);
      SPDocumentLibrary DocLib = (SPDocumentLibrary)site.Lists[DocLibId];
      DocLib.Title = "Wingtip Documents";
      DocLib.ContentTypesEnabled = true;
      DocLib.OnQuickLaunch = true;
      DocLib.Update();

      // reference Wingtip Person content type
      SPContentType WingtipDocument = site.AvailableContentTypes["Wingtip Document"];
      DocLib.ContentTypes.Add(WingtipDocument);

      // delete Item content type
      DocLib.ContentTypes["Document"].Delete();


    }

  }
}
