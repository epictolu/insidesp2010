using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace JoinPart.CamlJoinPart {
  [ToolboxItemAttribute(false)]
  public class CamlJoinPart : WebPart {
    Label messages;
    SPGridView results;

    protected override void CreateChildControls() {
      messages = new Label();
      this.Controls.Add(messages);
      results = new SPGridView();
      results.AutoGenerateColumns = false;
      this.Controls.Add(results);

      BoundField title = new BoundField();
      title.HeaderText = "Module";
      title.DataField = "Title";
      results.Columns.Add(title);

      BoundField instructor = new BoundField();
      instructor.HeaderText = "Instructor";
      instructor.DataField = "Instructor";
      results.Columns.Add(instructor);

      BoundField email = new BoundField();
      email.HeaderText = "e-mail";
      email.DataField = "Email";
      results.Columns.Add(email);

    }

    protected override void RenderContents(HtmlTextWriter writer) {
      try {
        SPWeb site = SPContext.Current.Web;
        SPList listInstructors = site.Lists["Instructors"];
        SPList listModules = site.Lists["Modules"];

        SPQuery query = new SPQuery();
        query.Query = "<Where><Eq><FieldRef Name=\"Audience\"/><Value Type=\"Text\">Developer</Value></Eq></Where>";
        query.Joins = "<Join Type=\"Inner\" ListAlias=\"classInstructors\"><Eq><FieldRef Name=\"Instructor\" RefType=\"Id\" /><FieldRef List=\"classInstructors\" Name=\"Id\" /></Eq></Join>";
        query.ProjectedFields = "<Field Name='Email' Type='Lookup' List='classInstructors' ShowField='Email'/>";
        query.ViewFields = "<FieldRef Name=\"Title\" /><FieldRef Name=\"Instructor\" /><FieldRef Name=\"Email\" />";

        SPListItemCollection items = listModules.GetItems(query);

        messages.Text = items.Count.ToString() + " returned.";

        List<ViewItem> viewItems = new List<ViewItem>();

        foreach (SPListItem item in items) {
          ViewItem viewItem = new ViewItem();
          viewItem.Title = item.Title;
          viewItem.Instructor = item["Instructor"].ToString();
          viewItem.Email = item["Email"].ToString();
          viewItems.Add(viewItem);
        }

        results.DataSource = viewItems;
        results.DataBind();

      }
      catch (Exception x) {
        messages.Text = x.Message;
      }

      results.RenderControl(writer);
      messages.RenderControl(writer);
    }
  }

  public class ViewItem {
    public string Title { get; set; }
    public string Instructor { get; set; }
    public string Email { get; set; }
  }
}
