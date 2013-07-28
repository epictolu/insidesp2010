using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.Utilities;

namespace WingtipDocuments.Layouts.WingtipDocuments {
  public partial class CreateNewDocument : LayoutsPageBase {

    protected override void OnInit(EventArgs e) {
      cmdCreateDocument.Click += new EventHandler(cmdCreateDocument_Click);
    }

    
    void cmdCreateDocument_Click(object sender, EventArgs e) {

      
      SPWeb site = this.Web;
      Guid libraryID = new Guid(lstTargetLibrary.SelectedValue);
      SPDocumentLibrary library = (SPDocumentLibrary)site.Lists[libraryID];
      
      string name = txtDocumentName.Text;
      string contents = txtDocumentContent.Text;

      switch (lstDocumentTypes.SelectedValue) {
        case "Text":
          DocumentFactory.CreateTextDocument(name, library, contents);
          break;
        case "Xml":
          DocumentFactory.CreateXmlDocument(name, library, contents);
          break;
        case "WordDocument":
          DocumentFactory.CreateWordDocument(name, library, contents);
          break;
      }

      SPUtility.Redirect(library.DefaultViewUrl, SPRedirectFlags.Default, this.Context);
  
    }

    protected override void OnPreRender(EventArgs e) {
      
      // populate list with document libraries
      SPWeb site = this.Web;
      foreach (SPList list in site.Lists) {
        if (list.BaseTemplate == SPListTemplateType.DocumentLibrary &&
            !list.Title.Equals("Customized Reports") &&
            !list.Title.Equals("Style Library") ) {
          SPDocumentLibrary docLib = (SPDocumentLibrary)list;
          // Add document library to DropDownList control 
          lstTargetLibrary.Items.Add(
             new ListItem(docLib.Title, docLib.ID.ToString()));
        }
      }

      if (string.IsNullOrEmpty(txtDocumentName.Text))
        txtDocumentName.Text = "Doc1";

      if (string.IsNullOrEmpty(txtDocumentContent.Text))
        txtDocumentContent.Text = "The quick brown fox jumped over the lazy dog";

      if (DocumentFactory.IsOpenXmlSdkInstalled() == false) {
        ListItem WordDocumentOption = lstDocumentTypes.Items.FindByValue("WordDocument");
        WordDocumentOption.Enabled = false;
        WordDocumentOption.Text = "Microsoft Word Document (*.docx) [Option requires Open XML SDK 2.0]";
      }

    }

  }
}
