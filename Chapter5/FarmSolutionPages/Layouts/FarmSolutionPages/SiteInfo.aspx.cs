using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebControls;
using System.Collections.Generic;

namespace FarmSolutionPages.Layouts.FarmSolutionPages
{
    public partial class SiteInfo : LayoutsPageBase
    {
        protected override void OnInit(EventArgs e)
        {
            cmdReturn.Click += new EventHandler(cmdReturn_Click);
        }

        void cmdReturn_Click(object sender, EventArgs e)
        {
            SPUtility.Redirect("settings.aspx", SPRedirectFlags.RelativeToLayoutsPage, Context);
        }

        protected override bool RequireSiteAdministrator
        {
            get { return true; }
        }

        protected override void OnPreRender(EventArgs e)
        {
            var lists = new List<ListInformation>();

            foreach (SPList list in Web.Lists)
            {
                lists.Add(new ListInformation 
                            { 
                                ID = list.ID.ToString(), 
                                Title = list.Title, 
                                ItemCount = list.Items.Count.ToString(), 
                                Versioning = list.EnableVersioning.ToString() 
                            });
            }

            grdLists.DataSource = lists;
            grdLists.DataBind();
        }

        class ListInformation
        {
            public string Title { get; set; }
            public string ID { get; set; }
            public string ItemCount { get; set; }
            public string Versioning { get; set; }
        }
    }
}
