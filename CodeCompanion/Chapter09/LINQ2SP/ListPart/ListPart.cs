using System;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

//Key libraries
using System.Linq;
using Microsoft.SharePoint.Linq;

namespace LINQ2SP.ListPart
{
    public class ListPart : WebPart
    {
        protected ListBox moduleList;
        protected TextBox moduleName;
        protected Label moduleInstructor;
        protected Button updateButton;

        protected override void OnPreRender(EventArgs e)
        {
            if (!Page.IsPostBack)
                FillList();
        }

        protected override void CreateChildControls()
        {
            moduleList = new ListBox();
            moduleList.Width = Unit.Pixel(440);
            moduleList.Height = Unit.Pixel(200);
            moduleList.AutoPostBack = true;
            moduleList.SelectedIndexChanged += new EventHandler(moduleList_SelectedIndexChanged);
            Controls.Add(moduleList);

            moduleName = new TextBox();
            moduleName.Width = Unit.Pixel(440);
            Controls.Add(moduleName);

            moduleInstructor = new Label();
            Controls.Add(moduleInstructor);

            updateButton = new Button();
            updateButton.Text = "Update";
            updateButton.Click += new EventHandler(updateButton_Click);
            Controls.Add(updateButton);
        }

        public void updateButton_Click(object sender, EventArgs e)
        {
            using (Entities dc = new Entities(SPContext.Current.Web.Url))
            {


                var q = (from m in dc.Modules
                         where m.Title.ToUpper().Trim() == moduleList.SelectedItem.Text.Substring(0, moduleList.SelectedItem.Text.IndexOf("[")).ToUpper().Trim()
                         select m).First();

                q.Title = moduleName.Text;

                dc.SubmitChanges(ConflictMode.FailOnFirstConflict, true);
                
            }
           
            FillList();
        }

        public void moduleList_SelectedIndexChanged(object sender, EventArgs e)
        {
            moduleName.Text = moduleList.SelectedItem.Text.Substring(0, moduleList.SelectedItem.Text.IndexOf("["));
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            writer.Write("<table border=\"0\" cellpadding=\"5\" cellspacing=\"5\" >");
            writer.Write("<tr><td>");
            moduleList.RenderControl(writer);
            writer.Write("</td></tr>");
            writer.Write("<tr><td>");
            moduleInstructor.RenderControl(writer);
            writer.Write("</td></tr>");
            writer.Write("<tr><td>");
            moduleName.RenderControl(writer);
            writer.Write("</td></tr>");
            writer.Write("<tr><td>");
            updateButton.RenderControl(writer);
            writer.Write("</td></tr>");
            writer.Write("</table>");
        }

        private void FillList()
        {
            moduleName.Text = string.Empty;
            moduleList.Items.Clear();

            using (Entities dc = new Entities(SPContext.Current.Web.Url))
            {

                var q = from m in dc.Modules
                        orderby m.ModuleID
                        select new { m.Title, Presenter = m.Instructor.FullName };


                foreach (var module in q)
                {
                    ListItem item = new ListItem(module.Title + " [" + module.Presenter + "]");
                    moduleList.Items.Add(item);
                }
            }
        }
    }
}
