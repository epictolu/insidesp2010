using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Microsoft.SharePoint.Taxonomy;
using Microsoft.SharePoint;

namespace ImportTaxonomy {
  class Program {
    static void Main(string[] args) {
      using (SPSite siteCollection = new SPSite("http://intranet.wingtip.com")) {
        #region prime Managed Metadata SA - create group & term set if doesn't exist
        // get refrerence to the taxonomy term store
        TaxonomySession taxonomySession = new TaxonomySession(siteCollection);
        // get reference to first term store (can also get by name)
        TermStore termStore = taxonomySession.TermStores[0];
        string termGroupName = "Locations";
        Group termGroup = termStore.Groups.Where(x => x.Name == termGroupName).Count() > 0 ?
                            termStore.Groups[termGroupName] :
                            termStore.CreateGroup(termGroupName);
        string termSetName = "United States Geography";
        TermSet termSet = termGroup.TermSets.Where(x => x.Name == termSetName).Count() > 0 ?
                            termGroup.TermSets[termSetName] :
                            termGroup.CreateTermSet(termSetName);
        termStore.CommitAll();
        #endregion

        // load taxonomy XML doc
        XDocument taxonomyDocument = XDocument.Load(@"USAGeographyTaxonomy.xml");

        // get & loop through all taxonomies...
        var query = from x in taxonomyDocument.Descendants("Taxonomy")
                    select x;
        List<XElement> taxonomies = query.ToList<XElement>();
        foreach (XElement item in taxonomies) {
          ProcessTaxonomy(item, termSet);
        }

        // commit changes
        termStore.CommitAll();
      }
    }

    static void ProcessTaxonomy(XElement element, TermSet termSet) {
      Console.WriteLine(element.Attribute("Name").Value);

      // go through all terms...
      var query = from x in element.Elements("Term")
                  select x;
      List<XElement> terms = query.ToList<XElement>();
      foreach (XElement term in terms) {
        ProcessTerm(term, termSet);
      }
    }

    private static void ProcessTerm(XElement term, TermSetItem termSetItem) {
      Console.WriteLine(String.Format(
                "{0} :: {1}",
                term.Attribute("Name").Value,
                term.Attribute("Culture").Value
              ));

      // create term
      Term newTerm = termSetItem.CreateTerm(term.Attribute("Name").Value, Int32.Parse(term.Attribute("Culture").Value));


      // if labels, add them
      var labelQuery = from x in term.Elements("Labels").Elements("Label")
                       select x;
      if (labelQuery.Count() >= 0) {
        foreach (var label in labelQuery) {
          newTerm.CreateLabel(
            label.Attribute("Name").Value,
            Int32.Parse(label.Attribute("Culture").Value),
            false
          );
        }
      }


      // check if there are child terms...
      var childTermQuery = from x in term.Elements("ChildTerms").Elements("Term")
                           select x;
      if (childTermQuery.Count() >= 0) {
        List<XElement> childTerms = childTermQuery.ToList<XElement>();
        foreach (XElement childTerm in childTerms) {
          ProcessTerm(childTerm, newTerm);
        }
      }
    }
  }
}
