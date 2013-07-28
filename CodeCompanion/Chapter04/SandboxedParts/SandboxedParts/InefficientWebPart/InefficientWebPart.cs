using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.Utilities;

namespace SandboxedParts.InefficientWebPart {
  [ToolboxItemAttribute(false)]
  public class InefficientWebPart : WebPart {

    protected Button cmd1;
    protected Button cmd2;
    protected Button cmd3;
    const string startStartSpinningGears =
      @"setTimeout('$get(""imgGears"").src=""/_layouts/images/GEARS_AN.GIF""', 100); setTimeout('$get(""divStatus"").innerHTML=""""', 100); return true;";

    protected override void CreateChildControls() {
      cmd1 = new Button();
      cmd1.Text = "Spin for 3 seconds";
      cmd1.Width = new Unit("100%");
      cmd1.Click += new EventHandler(cmd1_Click);
      cmd1.OnClientClick = startStartSpinningGears;
      Controls.Add(cmd1);
      cmd2 = new Button();
      cmd2.Text = "Spin for 10 seconds";
      cmd2.Width = new Unit("100%");
      cmd2.Click += new EventHandler(cmd2_Click);
      cmd2.OnClientClick = startStartSpinningGears;
      Controls.Add(cmd2);
      cmd3 = new Button();
      cmd3.Text = "Spin for 20 seconds";
      cmd3.Width = new Unit("100%");
      cmd3.Click += new EventHandler(cmd3_Click);
      cmd3.OnClientClick = startStartSpinningGears;
      Controls.Add(cmd3);
    }

    long counter = 0;

    void Spin(int seconds) {
      counter = 0;
      DateTime start = DateTime.Now;
      while (true) {
        TimeSpan span = DateTime.Now - start;
        if (span.TotalSeconds <= seconds) {
          SPWeb site = SPContext.Current.Web;
          foreach (SPList list in site.Lists) {
            foreach (SPListItem item in list.Items) {
              counter += 1;
              string temp = item.ID.ToString();
            }
          }
        }
        else {
          break;
        }
      }
    }

    void cmd1_Click(object sender, EventArgs e) {
        Spin(3);      
    }

    void cmd2_Click(object sender, EventArgs e) {
      Spin(10);
    }

    void cmd3_Click(object sender, EventArgs e) {
      Spin(20);
    }


    protected override void RenderContents(HtmlTextWriter writer) {
      writer.RenderBeginTag(HtmlTextWriterTag.Table);
      writer.RenderBeginTag(HtmlTextWriterTag.Tr);
      writer.RenderBeginTag(HtmlTextWriterTag.Td);
      writer.RenderBeginTag(HtmlTextWriterTag.Table);
      writer.RenderBeginTag(HtmlTextWriterTag.Tr);
      writer.AddStyleAttribute(HtmlTextWriterStyle.Padding, "4px");
      writer.RenderBeginTag(HtmlTextWriterTag.Td);      
      cmd1.RenderControl(writer);
      writer.RenderEndTag(); // </td>
      writer.RenderEndTag(); // </tr>
      writer.RenderBeginTag(HtmlTextWriterTag.Tr);
      writer.AddStyleAttribute(HtmlTextWriterStyle.Padding, "4px");
      writer.RenderBeginTag(HtmlTextWriterTag.Td);      
      cmd2.RenderControl(writer);
      writer.RenderEndTag(); // </td>
      writer.RenderEndTag(); // </tr>
      writer.RenderBeginTag(HtmlTextWriterTag.Tr);
      writer.AddStyleAttribute(HtmlTextWriterStyle.Padding, "4px");
      writer.RenderBeginTag(HtmlTextWriterTag.Td);      
      cmd3.RenderControl(writer);
      writer.RenderEndTag(); // </td>
      writer.RenderEndTag(); // </tr>
      writer.RenderEndTag(); // </table>
      writer.RenderEndTag(); // </td>
      writer.AddStyleAttribute(HtmlTextWriterStyle.Padding, "20px");      
      writer.RenderBeginTag(HtmlTextWriterTag.Td);      
      writer.Write("<img id='imgGears' src='/_layouts/images/blank.gif' style='margin-left:10px;' />");
      if (counter > 0) writer.Write("<div id='divStatus' >The last request did " + counter.ToString() + " reads from the content database</div>");
      writer.RenderEndTag(); // </td>
      writer.RenderEndTag(); // </tr>
      writer.RenderEndTag(); // </table>
    }
  }
}
