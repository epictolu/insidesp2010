using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace SimpleCRUDOperations.CRUDPart
{
    [ToolboxItemAttribute(false)]
    public class CRUDPart : WebPart
    {
        private string listName;
        private SPGridView itemGrid;
        private Button createButton;
        private Button readButton;
        private Button updateButton;
        private Button deleteButton;
        private Label messages;

        [Personalizable(PersonalizationScope.Shared),
        WebBrowsable(true),
        WebDisplayName("Target List"),
        WebDescription("The list to use for CRUD operations"),
        Category("Configuration")]
        public string ListName
        {
            get { return listName; }
            set { listName = value; }
        }

        protected override void CreateChildControls()
        {
            itemGrid = new SPGridView();
            itemGrid.AutoGenerateColumns = false;
            this.Controls.Add(itemGrid);
            BoundField titleField = new BoundField();
            titleField.DataField="Title";
            titleField.HeaderText="Title";
            titleField.HeaderStyle.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Left;
            itemGrid.Columns.Add(titleField);

            createButton = new Button();
            createButton.Text = "Create";
            createButton.Click += new EventHandler(createButton_Click);
            this.Controls.Add(createButton);

            readButton = new Button();
            readButton.Text = "Read";
            readButton.Click += new EventHandler(readButton_Click);
            this.Controls.Add(readButton);

            updateButton = new Button();
            updateButton.Text = "Update";
            updateButton.Click += new EventHandler(updateButton_Click);
            this.Controls.Add(updateButton);

            deleteButton = new Button();
            deleteButton.Text = "Delete";
            deleteButton.Click += new EventHandler(deleteButton_Click);
            this.Controls.Add(deleteButton);

            messages = new Label();
            this.Controls.Add(messages);
        }

        void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                SPList list = SPContext.Current.Web.Lists.TryGetList(ListName);
                if (list == null)
                {
                    messages.Text = "List does not exist.";
                }
                else
                {
                    list.Items[0].Delete();
                }
            }
            catch (Exception x)
            {
                messages.Text = x.Message;
            }
        }

        void updateButton_Click(object sender, EventArgs e)
        {
            try
            {
                SPList list = SPContext.Current.Web.Lists.TryGetList(ListName);
                if (list == null)
                {
                    messages.Text = "List does not exist.";
                }
                else
                {
                    SPListItem updateItem = list.Items[0];
                    updateItem["Title"] = "Updated Item " + DateTime.Now.ToLongDateString();
                    updateItem.Update();

                }
            }
            catch (Exception x)
            {
                messages.Text = x.Message;
            }
        }

        void readButton_Click(object sender, EventArgs e)
        {
            try
            {
                SPList list = SPContext.Current.Web.Lists.TryGetList(ListName);
                if (list == null)
                {
                    messages.Text = "List does not exist.";
                }
                else
                {
                    SPListItemCollection items = list.Items;
                    itemGrid.DataSource = items;
                    itemGrid.DataBind();
                }
            }
            catch (Exception x)
            {
                messages.Text = x.Message;
            }
        }

        void createButton_Click(object sender, EventArgs e)
        {
            try
            {
                SPList list = SPContext.Current.Web.Lists.TryGetList(ListName);
                if (list == null)
                {
                    messages.Text = "List does not exist.";
                }
                else
                {
                    SPListItem newItem = list.Items.Add();
                    newItem["Title"] = "Created Item " + DateTime.Now.ToLongDateString(); ;
                    newItem.Update();
                }
            }
            catch (Exception x)
            {
                messages.Text = x.Message;
            }
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            itemGrid.RenderControl(writer);
            writer.Write("<br/>");
            createButton.RenderControl(writer);
            readButton.RenderControl(writer);
            updateButton.RenderControl(writer);
            deleteButton.RenderControl(writer);
            writer.Write("<br/>");
            messages.RenderControl(writer);
        }
    }
}
