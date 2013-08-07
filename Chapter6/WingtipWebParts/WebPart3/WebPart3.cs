using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace WingtipWebParts.WebPart3
{
    [ToolboxItemAttribute(false)]
    public class WebPart3 : WebPart
    {
        protected TextBox firstName;
        protected TextBox lastName;
        protected Button addSalesLead;

        protected override void CreateChildControls()
        {
            firstName = new TextBox();
            Controls.Add(firstName);

            lastName = new TextBox();
            Controls.Add(lastName);

            addSalesLead = new Button();
            addSalesLead.Text = "Add Sales Lead";
            Controls.Add(addSalesLead);
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            writer.RenderBeginTag(HtmlTextWriterTag.Table);
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            writer.Write("First Name:");
            writer.RenderEndTag();
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            firstName.RenderControl(writer);
            writer.RenderEndTag();
            writer.RenderEndTag();

            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            writer.Write("Last Name:");
            writer.RenderEndTag();
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            lastName.RenderControl(writer);
            writer.RenderEndTag();
            writer.RenderEndTag();

            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.AddAttribute(HtmlTextWriterAttribute.Colspan, "2");
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            addSalesLead.RenderControl(writer);
            writer.RenderEndTag();
            writer.RenderEndTag();
        }
    }
}
