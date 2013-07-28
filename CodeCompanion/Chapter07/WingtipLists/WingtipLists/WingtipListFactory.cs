using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;

namespace WingtipLists {
  class WingtipListFactory {

    public static void CreateMarketingTasksList(SPWeb site) {

      SPList list = site.Lists.TryGetList("Marketing Tasks");
      if (list != null)
        list.Delete();

      Guid ListId = site.Lists.Add("MarketingTasks", "", SPListTemplateType.Tasks);
      list = site.Lists[ListId];
      list.Title = "Marketing Tasks";
      list.OnQuickLaunch = true;
      list.Update();

    }

    public static void CreateWingtipSientistsList(SPWeb site) {
      SPList list = site.Lists.TryGetList("Wingtip Scientists");
      if (list != null)
        list.Delete();

      Guid ListId = site.Lists.Add("WingtipScientists", "", SPListTemplateType.Contacts);
      list = site.Lists[ListId];
      list.Title = "Wingtip Scientists";
      list.OnQuickLaunch = true;
      list.EnableAttachments = false;
      list.EnableFolderCreation = false;
      list.DisableGridEditing = true;
      list.EnableVersioning = true;
      list.MajorVersionLimit = 3;
      list.ReadSecurity = 2;
      list.WriteSecurity = 2;
      list.Update();
    }

    public static void CreateProductRelatedLists(SPWeb site) {
      // this method calls to private helper methods
      DeleteProductRelatedLists(site);
      Guid ProductCategoriesListId, ProductsListId;
      ProductCategoriesListId = CreateProductCategoriesList(site);
      ProductsListId = CreateProductsList(site, ProductCategoriesListId);

    }

    #region "Private helper methods to create product-related lists


    private static Guid CreateProductCategoriesList(SPWeb site) {
      // create new list
      Guid ProductCategoriesListId = site.Lists.Add("Product Categories", "",
                                                     SPListTemplateType.GenericList);
      // update list properties
      SPList ProductCategoriesList = site.Lists[ProductCategoriesListId];
      ProductCategoriesList.EnableAttachments = false;
      ProductCategoriesList.EnableFolderCreation = false;
      ProductCategoriesList.OnQuickLaunch = true;
      ProductCategoriesList.Fields["Title"].Title = "Category";
      ProductCategoriesList.Fields["Title"].Update();
      ProductCategoriesList.Fields.Add("Description", SPFieldType.Text, false);
      ProductCategoriesList.Update();

      SPView view = ProductCategoriesList.Views["All Items"];
      view.ViewFields.Add("Description");
      view.Update();

      SPListItem category1 = ProductCategoriesList.Items.Add();
      category1["Title"] = "Electronics";
      category1["Description"] = "Toys with batteries";
      category1.Update();

      SPListItem category2 = ProductCategoriesList.Items.Add();
      category2["Title"] = "Trains";
      category2["Description"] = "Model trains and accessories";
      category2.Update();


      return ProductCategoriesListId;
    }

    private static Guid CreateProductsList(SPWeb site, Guid ProductCategoriesListId) {

      // create new list
      Guid ProductsListId = site.Lists.Add("Products", "",
                                           SPListTemplateType.GenericList);

      // update list properties
      SPList ProductsList = site.Lists[ProductsListId];
      ProductsList.EnableAttachments = true;
      ProductsList.EnableFolderCreation = false;
      ProductsList.OnQuickLaunch = true;

      ProductsList.Fields["Title"].Title = "Product";
      ProductsList.Fields["Title"].Update();

      ProductsList.Fields.AddLookup("Category", ProductCategoriesListId, true);
      SPFieldLookup fldCategory = (SPFieldLookup)ProductsList.Fields["Category"];
      fldCategory.Indexed = true;
      fldCategory.RelationshipDeleteBehavior = SPRelationshipDeleteBehavior.Restrict;
      fldCategory.Update();

      string fldNameListPrice = ProductsList.Fields.Add("ListPrice", SPFieldType.Currency, true);
      SPFieldCurrency fldListPrice = (SPFieldCurrency)ProductsList.Fields.GetFieldByInternalName(fldNameListPrice);
      fldListPrice.Title = "List Price";
      fldListPrice.DefaultValue = "0";
      fldListPrice.MinimumValue = 0;
      fldListPrice.MaximumValue = 10000;
      fldListPrice.CurrencyLocaleId = 1033; // local ID for United States Dollars
      fldListPrice.DisplayFormat = SPNumberFormatTypes.TwoDecimals;
      fldListPrice.Update();


      string fldAgeGroupName = ProductsList.Fields.Add("AgeGroup", SPFieldType.Choice, true);
      SPFieldChoice fldAgeGroup =
      (SPFieldChoice)ProductsList.Fields.GetFieldByInternalName(fldAgeGroupName);

      // change display name
      fldAgeGroup.Title = "Age Group";

      // add choice values
      fldAgeGroup.Choices.Add("Ages 1-4");
      fldAgeGroup.Choices.Add("Ages 5-12");
      fldAgeGroup.Choices.Add("Ages 12 and up");
      fldAgeGroup.Choices.Add("Ages 18 and up");

      // modify field rendering format
      fldAgeGroup.EditFormat = SPChoiceFormatType.RadioButtons;
      fldAgeGroup.Update(true);

      ProductsList.Update();

      SPView view = ProductsList.DefaultView;
      view.ViewFields.Add("Category");
      view.ViewFields.Add("ListPrice");
      view.ViewFields.Add("AgeGroup");
      view.Update();

      return ProductsListId;
    }

    #endregion

    public static void DeleteProductRelatedLists(SPWeb site) {

      try {
        SPList ProductsList = site.Lists["Products"];
        SPFieldLookup fldProductCategory = (SPFieldLookup)ProductsList.Fields["Category"];
        fldProductCategory.Delete();
        ProductsList.Delete();
      }
      catch { }

      try {
        SPList ProductCategoriesList = site.Lists["Product Categories"];
        ProductCategoriesList.Delete();
      }
      catch { }
    }

    public static void CreateSalesLeadsList(SPWeb site) {

      SPList list = site.Lists.TryGetList("Sales Leads");
      if (list != null)
        list.Delete();

      Guid ListId = site.Lists.Add("SalesLeads", "", SPListTemplateType.GenericList);
      list = site.Lists[ListId];
      list.Title = "Sales Leads";
      list.OnQuickLaunch = true;
      list.Update();

      // modify display name of Title field
      SPField fldTitle = list.Fields.GetFieldByInternalName("Title");
      fldTitle.Title = "Sales Lead";
      fldTitle.Update();

      // add custom fields
      list.Fields.Add("Email", SPFieldType.Text, false);
      list.Fields.Add("Phone", SPFieldType.Text, false);

      SPView view = list.DefaultView;
      view.InlineEdit = "TRUE"; ;

      // add custom fields to default view
      view.ViewFields.Add("Email");
      view.ViewFields.Add("Phone");

      // save changes
      view.Query = @"<OrderBy>
                       <FieldRef Name='Title' />
                     </OrderBy>
                     <Where>
                       <IsNotNull>
                         <FieldRef Name='Email' />
                       </IsNotNull>
                     </Where>";
      view.Update();

    }

    public static void CreateEmployeeStatusSiteColumn(SPWeb site) {

      string fldInternalName = "EmployeeStatus";

      if (!site.AvailableFields.ContainsField(fldInternalName)) {
        site.Fields.Add("EmployeeStatus", SPFieldType.Choice, true);
        SPFieldChoice fld =
          (SPFieldChoice)site.Fields.GetFieldByInternalName("EmployeeStatus");
        // update site column display name
        fld.Title = "Employee Status";
        fld.Group = "Wingtip Site Columns";
        // add choices
        fld.Choices.Add("Full Time");
        fld.Choices.Add("Part Time");
        // configure rendering format
        fld.EditFormat = SPChoiceFormatType.RadioButtons;
        // save changes
        fld.Update();
      }

    }

    public static void UpdateEmployeeStatusSiteColumn(SPWeb site) {

      SPFieldChoice fld =
        (SPFieldChoice)site.Fields.GetFieldByInternalName("EmployeeStatus");
      fld.Choices.Add("Intern");
      fld.Update(true);
    }

    public static void DeleteEmployeeStatusSiteColumn(SPWeb site) {
      try { site.Lists["Employees"].Delete(); }
      catch { }
      try { site.Fields.GetFieldByInternalName("EmployeeStatus").Delete(); }
      catch { }
    }

    public static void CreateEmployeesList(SPWeb site) {

      SPList list = site.Lists.TryGetList("Employees");
      if (list != null)
        list.Delete();

      Guid ListId = site.Lists.Add("Employees", "", SPListTemplateType.GenericList);
      list = site.Lists["Employees"];

      list.OnQuickLaunch = true;
      list.Update();

      // modify display name of title field
      SPField fld = list.Fields["Title"];
      fld.Title = "Last Name";
      fld.Update();

      // add fields using site columns available in current site
      SPFieldCollection fields = site.AvailableFields;
      list.Fields.Add(fields.GetFieldByInternalName("FirstName"));
      list.Fields.Add(fields.GetFieldByInternalName("EmployeeStatus"));
      list.Fields.Add(fields.GetFieldByInternalName("WorkPhone"));
      list.Fields.Add(fields.GetFieldByInternalName("HomePhone"));
      list.Fields.Add(fields.GetFieldByInternalName("CellPhone"));

      list.Fields.Add(fields.GetFieldByInternalName("EMail"));
      list.Fields.Add(fields.GetFieldByInternalName("HomeAddressStreet"));
      list.Fields.Add(fields.GetFieldByInternalName("HomeAddressCity"));
      list.Fields.Add(fields.GetFieldByInternalName("HomeAddressStateOrProvince"));
      list.Fields.Add(fields.GetFieldByInternalName("HomeAddressPostalCode"));
      list.Fields.Add(fields.GetFieldByInternalName("StartDate"));
      list.Fields.Add(fields.GetFieldByInternalName("SpouseName"));
      list.Fields.Add(fields.GetFieldByInternalName("Birthday"));

      // add selected fields to default view
      SPView view = list.DefaultView;
      view.InlineEdit = "TRUE";
      view.ViewFields.Add("FirstName");
      view.ViewFields.Add("EmployeeStatus");
      view.ViewFields.Add("WorkPhone");
      view.ViewFields.Add("HomePhone");
      view.ViewFields.Add("CellPhone");
      view.Update();
    }

    public static void CreatePersonContentType(SPWeb site) {

      try { site.Lists["People"].Delete(); }
      catch { }
      try { site.ContentTypes["Wingtip Person"].Delete(); }
      catch { }

      SPContentType ctypeParent = site.ContentTypes["Item"];
      string ctypeName = "Wingtip Person";
      SPContentType ctype;
      ctype = new SPContentType(ctypeParent, site.ContentTypes, ctypeName);
      ctype.Description = "Basic tracking information for a person";
      ctype.Group = "Wingtip";
      site.ContentTypes.Add(ctype);

      // change display name of Title field
      SPFieldLink fldLinkTitle = ctype.FieldLinks["Title"];
      fldLinkTitle.DisplayName = "Last Name";

      ctype.FieldLinks.Add(
        new SPFieldLink(site.Fields.GetFieldByInternalName("FirstName")));
      ctype.FieldLinks.Add(
            new SPFieldLink(site.Fields.GetFieldByInternalName("EMail")));
      ctype.FieldLinks.Add(
        new SPFieldLink(site.Fields.GetFieldByInternalName("HomePhone")));
      ctype.FieldLinks.Add(
        new SPFieldLink(site.Fields.GetFieldByInternalName("CellPhone")));
      ctype.FieldLinks.Add(
        new SPFieldLink(site.Fields.GetFieldByInternalName("WorkPhone")));

      SPFieldLink fldLinkFirstName = ctype.FieldLinks["FirstName"];
      fldLinkFirstName.Required = true;


      ctype.Update();



    }

    public static void UpdateContactContentType(SPWeb site) {

      SPContentType ctypeContact = site.ContentTypes["Contact"];
      SPFieldLink fldLinkFirstNamex = ctypeContact.FieldLinks["FirstName"];
      fldLinkFirstNamex.Required = true;
      ctypeContact.Update(true);


    }

    public static void DeletePersonContentType(SPWeb site) {
      try { site.Lists["People"].Delete(); }
      catch { }
      try { site.ContentTypes["Wingtip Person"].Delete(); }
      catch { }
    }

    public static void CreatePeopleList(SPWeb site) {

      string ListTitle = "People";
      string ListDescription = "wingtip customers list";

      // delete Customers list if it exists
      SPList list = site.Lists.TryGetList(ListTitle);
      if (list != null)
        list.Delete();

      // create new Customers list
      Guid ListId = site.Lists.Add(ListTitle, ListDescription, SPListTemplateType.GenericList);
      list = site.Lists[ListId];
      list.OnQuickLaunch = true;
      list.ContentTypesEnabled = true;
      SPField fldTitle = list.Fields["Title"];
      fldTitle.Title = "Last Name";
      fldTitle.Update();

      SPContentType ctype = site.AvailableContentTypes["Wingtip Person"];

      SPContentType ctList = list.ContentTypes.Add(ctype);
      list.ContentTypes["Item"].Delete();

      list.Update();

      // add selected fields to default view
      SPView view = list.DefaultView;
      view.InlineEdit = "TRUE";
      view.ViewFields.Add("FirstName");
      view.ViewFields.Add("EMail");
      view.ViewFields.Add("HomePhone");
      view.ViewFields.Add("CellPhone");
      view.ViewFields.Add("WorkPhone");
      view.Update();

    }

    public static void CreateWikePageLibrary(SPWeb site) {

      string ListTitle = "Wiki Pages";
      string ListDescription = "";
      SPList list;
      // delete list if it already exists
      list = site.Lists.TryGetList(ListTitle);
      if (list != null)
        list.Delete();

      // create new list
      Guid ListId = site.Lists.Add(ListTitle, ListDescription, SPListTemplateType.WebPageLibrary);
      // update list properties
      SPDocumentLibrary WikiLib = (SPDocumentLibrary)site.Lists[ListId];
      WikiLib.OnQuickLaunch = true;
      WikiLib.Update();

      SPFile page1 = SPUtility.CreateNewWikiPage(WikiLib, WikiLib.RootFolder.ServerRelativeUrl + @"\Page1.aspx");
      SPListItem page1Item = page1.GetListItem(new string[] { "WikiField" });
      page1Item["WikiField"] = WikiUtility.CreateWikiPageContent(
        @"<h1>The Chicken</h1>
          <p>Came either right before or right after the <a><a id='0::Page2|Egg' class='ms-wikilink' href='/Wiki%20Pages/Page2.aspx'>Egg</a></a></p>");
      page1Item.UpdateOverwriteVersion();

      SPFile page2 = SPUtility.CreateNewWikiPage(WikiLib, WikiLib.RootFolder.ServerRelativeUrl + @"\Page2.aspx");
      SPListItem page2Item = page2.GetListItem(new string[] { "WikiField" });
      page2Item["WikiField"] = WikiUtility.CreateWikiPageContent(
        @"<h1>The Egg</h1>
          <p>Came either right before or right after the <a><a id='0::Page1|Chicken' class='ms-wikilink' href='/Wiki%20Pages/Page1.aspx'>Chicken</a></a></p>");
      page2Item.UpdateOverwriteVersion();


      SPFile page3 = SPUtility.CreateNewWikiPage(WikiLib, WikiLib.RootFolder.ServerRelativeUrl + @"\Page3.aspx");
      SPListItem page3Item = page3.GetListItem(new string[] { "WikiField" });
      page3Item["WikiField"] = WikiUtility.CreateWikiPageContent("<h1>Product Ideas</h1>");
      page3Item.UpdateOverwriteVersion();

    }
  }
}
