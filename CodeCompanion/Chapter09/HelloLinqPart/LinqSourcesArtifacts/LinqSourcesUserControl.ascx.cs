using System;
using System.IO;
using System.Diagnostics;
using System.Xml;
using System.Xml.Linq;
using System.Data.Linq;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace HelloLinqPart.VisualPart
{
    public partial class LinqSourcesUserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                results.Items.Add("Query Results List");
        }

        public void showFiles_OnClick(object sender, EventArgs e)
        {
            results.Items.Clear();

            DirectoryInfo di = new DirectoryInfo("C:\\");

            var q = from d in di.GetDirectories()
                    orderby di.Name
                    select new { d.Name };

            foreach (var i in q)
            {
                results.Items.Add(i.Name);
            }
        }

        public void showProcesses_OnClick(object sender, EventArgs e)
        {
            results.Items.Clear();

            var q = from p in Process.GetProcesses()
                    orderby p.WorkingSet64 descending
                    select new { p.ProcessName };

            foreach (var i in q)
            {
                results.Items.Add(i.ProcessName);
            }
        }

        public void showArray_OnClick(object sender, EventArgs e)
        {
            results.Items.Clear();

            string[] names = { "Ted", "Scot", "Andrew", "Wouter" };

            var q = from n in names
                    orderby n
                    select n;

            foreach (var i in q)
            {
                results.Items.Add(i);
            }
        }

        public void showXML_OnClick(object sender, EventArgs e)
        {
            results.Items.Clear();

            string xml = "<Contacts><Contact FirstName=\"Scot\" LastName=\"Hillier\" /><Contact FirstName=\"Ted\" LastName=\"Pattison\" /><Contact FirstName=\"Andrew\" LastName=\"Connell\" /><Contact FirstName=\"Wouter\" LastName=\"van Vugt\" /></Contacts>";
            XDocument d = XDocument.Parse(xml);

            var q = from c in d.Descendants("Contact")
                    select new { c.Attribute("FirstName").Value };

            foreach (var i in q)
            {
                results.Items.Add(i.Value);
            }
        }

        public void showSQL_OnClick(object sender, EventArgs e)
        {
            results.Items.Clear();

            using (MiniCRM dc = new MiniCRM("Data Source=(local);Initial Catalog=MiniCRM;Integrated Security=True;"))
            {

                MiniCRM_Names[] q = (from n in dc.MiniCRM_Names
                                     select n).ToArray();

                foreach (var i in q)
                {
                    results.Items.Add(i.FirstName);
                }

            }
        }

        public void showObjects_OnClick(object sender, EventArgs e)
        {
            results.Items.Clear();

            List<Instructor> contacts = new List<Instructor>();
            contacts.Add(new Instructor("Scot", "Hillier"));
            contacts.Add(new Instructor("Wouter", "van Vugt"));
            contacts.Add(new Instructor("Andrew", "Connell"));
            contacts.Add(new Instructor("Ted", "Pattison"));

            var q = from c in contacts
                    select new { c.FirstName };

            foreach (var i in q)
            {
                results.Items.Add(i.FirstName);
            }
        }
    }

    class Instructor
    {
        public Instructor(string FirstName, string LasteName)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
