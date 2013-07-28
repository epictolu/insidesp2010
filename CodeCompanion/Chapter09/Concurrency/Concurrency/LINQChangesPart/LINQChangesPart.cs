using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.Linq;
using System.Linq;

namespace Concurrency.LINQChangesPart {
  [ToolboxItemAttribute(false)]
  public class LINQChangesPart : WebPart {
    ListBox modulesList;
    TextBox titleBox;
    Button update;
    CheckBox conflict;
    ListBox resolutionOptions;
    Button commit;
    Label messages;
    SPGridView conflictGrid;
    List<Conflict> conflicts;
    Literal caml;

    TextWriter logWriter;
    MemoryStream logStream;

    protected override void OnLoad(EventArgs e) {
      //Make sure UI is available
      EnsureChildControls();

      //Open log file
      logStream = new MemoryStream();
      logWriter = new StreamWriter(logStream);

      if (!Page.IsPostBack) {
        //Clear any simulated conflicting changes
        SPContext.Current.Web.AllowUnsafeUpdates = true;
        SPList list = SPContext.Current.Web.Lists["Modules"];
        foreach (SPListItem i in list.Items) {
          if (i["Title"].ToString().EndsWith("!")) {
            i["Title"] = i["Title"].ToString().TrimEnd('!');
            i.Update();
          }
        }

        //Resoltuion Options
        resolutionOptions.Items.Add("Do not resolve conflicts");
        resolutionOptions.Items.Add("Keep my changes");
        resolutionOptions.Items.Add("Lose my changes");

        //Fill the list
        FillList();

      }
    }

    protected override void CreateChildControls() {
      modulesList = new ListBox();
      modulesList.AutoPostBack = true;
      modulesList.EnableViewState = true;
      modulesList.SelectedIndexChanged += new EventHandler(modulesList_SelectedIndexChanged);
      this.Controls.Add(modulesList);

      titleBox = new TextBox();
      this.Controls.Add(titleBox);

      update = new Button();
      update.Text = "Update Title";
      update.ToolTip = "Updates the title in the ListBox, but not the SharePoint list";
      update.Click += new EventHandler(update_Click);
      this.Controls.Add(update);

      conflict = new CheckBox();
      conflict.Text = "Simulate Conflict on Commit";
      conflict.ToolTip = "Simulates that a user has changed values in the SharePoint list since LINQ read them.";
      this.Controls.Add(conflict);

      resolutionOptions = new ListBox();
      this.Controls.Add(resolutionOptions);

      commit = new Button();
      commit.Text = "Commit All Changes";
      commit.ToolTip = "Commits all Title changes in a batch.";
      commit.Click += new EventHandler(commit_Click);
      this.Controls.Add(commit);

      messages = new Label();
      this.Controls.Add(messages);

      conflictGrid = new SPGridView();
      conflictGrid.AutoGenerateColumns = false;
      this.Controls.Add(conflictGrid);
      BoundField oValField = new BoundField();
      oValField.DataField = "OriginalValue";
      oValField.HeaderText = "Original Value";
      oValField.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
      conflictGrid.Columns.Add(oValField);
      BoundField cValField = new BoundField();
      cValField.DataField = "CurrentValue";
      cValField.HeaderText = "Current Value";
      cValField.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
      conflictGrid.Columns.Add(cValField);
      BoundField dValField = new BoundField();
      dValField.DataField = "DatabaseValue";
      dValField.HeaderText = "Database Value";
      dValField.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
      conflictGrid.Columns.Add(dValField);

      caml = new Literal();
      this.Controls.Add(caml);
    }

    void FillList() {
      using (ModulesDataContext dc = new ModulesDataContext("http://contososserver/linq")) {
        //Turn off object tracking for read-only operation
        dc.ObjectTrackingEnabled = false;

        //Log Caml
        dc.Log = logWriter;

        //Get list items
        var q = from m in dc.Modules
                orderby m.Title
                select m;

        //Fill List
        modulesList.Items.Clear();
        foreach (var i in q) {
          ListItem item = new ListItem(i.Title, i.Id.ToString());
          modulesList.Items.Add(item);
        }
      }
    }

    void modulesList_SelectedIndexChanged(object sender, EventArgs e) {
      //Show current title for editing
      titleBox.Text = modulesList.SelectedItem.Text;
    }

    void commit_Click(object sender, EventArgs e) {
      ModulesDataContext dc = new ModulesDataContext("http://contososserver/linq");

      //Log Caml
      dc.Log = logWriter;

      try {

        //Get list of items
        var q = from m in dc.Modules
                orderby m.Title
                select m;

        //Make changes
        foreach (var i in q) {
          if (!i.Title.Equals(modulesList.Items.FindByValue(i.Id.ToString()).Text))
            i.Title = modulesList.Items.FindByValue(i.Id.ToString()).Text;
        }

        //Simulate conflicting changes from another user
        if (conflict.Checked) {
          SPList list = SPContext.Current.Web.Lists["Modules"];
          foreach (SPListItem i in list.Items) {
            ListItem t = modulesList.Items.FindByText(i["Title"].ToString());
            if (t == null) {
              i["Title"] = i["Title"].ToString() + "!";
              i.Update();
            }
          }
        }

        //Submit the changes as a batch
        dc.SubmitChanges(ConflictMode.ContinueOnConflict);
        FillList();

      }
      catch (System.InvalidOperationException x) {
        messages.Text = "Either ObjectTrackingEnabled is 'false' or there are unresolved conflicts. " + x.Message;
      }
      catch (Microsoft.SharePoint.Linq.ChangeConflictException x) {
        messages.Text = "There is a concurrency conflict. " + x.Message;

        switch (resolutionOptions.SelectedIndex) {
          case 0:
            //Don't resolve, just display
            conflicts = new List<Conflict>();
            foreach (ObjectChangeConflict cc in dc.ChangeConflicts) {
              foreach (MemberChangeConflict mc in cc.MemberConflicts) {
                Conflict conflict = new Conflict();
                conflict.OriginalValue = mc.OriginalValue.ToString();
                conflict.CurrentValue = mc.CurrentValue.ToString();
                conflict.DatabaseValue = mc.DatabaseValue.ToString();
                conflicts.Add(conflict);
              }

            }
            conflictGrid.DataSource = conflicts;
            conflictGrid.DataBind();
            break;

          case 1:
            //Keep my changes
            foreach (ObjectChangeConflict cc in dc.ChangeConflicts) {
              foreach (MemberChangeConflict mc in cc.MemberConflicts) {
                mc.Resolve(RefreshMode.KeepCurrentValues);
              }

            }
            dc.SubmitChanges();
            messages.Text = "There was a conflict, but your changes were forcibly saved.";
            FillList();
            break;

          case 2:
            //Lose my changes
            foreach (ObjectChangeConflict cc in dc.ChangeConflicts) {
              foreach (MemberChangeConflict mc in cc.MemberConflicts) {
                mc.Resolve(RefreshMode.OverwriteCurrentValues);
              }

            }
            dc.SubmitChanges();
            messages.Text = "There was a conflict and your changes have been lost."; FillList();
            break;

          default:
            break;
        }
      }
    }

    void update_Click(object sender, EventArgs e) {
      //Change the title in the list only
      modulesList.SelectedItem.Text = titleBox.Text;
      titleBox.Text = string.Empty;
    }

    protected override void RenderContents(HtmlTextWriter writer) {
      //UI
      writer.Write("<p>");
      modulesList.RenderControl(writer);
      writer.Write("</p><p>");
      titleBox.RenderControl(writer);
      writer.Write("</p><p>");
      update.RenderControl(writer);
      writer.Write("</p><p>");
      conflict.RenderControl(writer);
      writer.Write("</p><p>");
      resolutionOptions.RenderControl(writer);
      writer.Write("</p><p>");
      commit.RenderControl(writer);
      writer.Write("</p><p>");
      messages.RenderControl(writer);
      writer.Write("</p>");

      //Show conflicts, if necessary
      if (conflicts != null) {
        writer.Write("<p>");
        conflictGrid.RenderControl(writer);
        writer.Write("</p>");
      }

      //Show underlying CAML
      logStream.Position = 0;
      StreamReader logReader = new StreamReader(logStream);
      caml.Text = "<xmp>" + logReader.ReadToEnd() + "</xmp>";
      caml.RenderControl(writer);

    }
  }

  class Conflict {
    public string OriginalValue { get; set; }
    public string CurrentValue { get; set; }
    public string DatabaseValue { get; set; }
  }
}
