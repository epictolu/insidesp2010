using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;

namespace WingtipDocuments.Features.MainSite {


  [Guid("9753d1f9-415a-4336-904a-38ab3a4a4c01")]
  public class MainSiteEventReceiver : SPFeatureReceiver {

    Guid defID = new Guid("DEADBEEF-BADD-BADD-BADD-BADBADBAD2B8");


    public override void FeatureActivated(SPFeatureReceiverProperties properties) {
     
      SPSite siteCollection = (SPSite)properties.Feature.Parent;
      SPWeb site = siteCollection.RootWeb;
   
      SPDocumentLibrary doclib = (SPDocumentLibrary)site.Lists["Product Proposals"];
      string templateUrl = @"ProductProposals/Forms/Proposal.dotx";
      doclib.DocumentTemplateUrl = templateUrl;
      doclib.Update();

      DocumentLibraryFactory.CreateProductSpecificationsLibrary(site);
      DocumentLibraryFactory.CreateContentTypeWithStandardDocTemplate(site);
  
     
    }

    public override void FeatureDeactivating(SPFeatureReceiverProperties properties) {
      SPSite siteCollection = (SPSite)properties.Feature.Parent;
      SPWeb site = siteCollection.RootWeb;

      // delete doc libs if required
    }

  }
}
