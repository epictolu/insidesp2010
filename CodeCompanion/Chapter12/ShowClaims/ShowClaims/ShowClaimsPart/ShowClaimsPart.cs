using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.Administration.Claims;
using Microsoft.IdentityModel;
using Microsoft.IdentityModel.Claims;
using System.Web.Mvc;

namespace ShowClaims.ShowClaimsPart
{
    [ToolboxItemAttribute(false)]
    public class ShowClaimsPart : WebPart
    {
        HtmlTable table;

        private HtmlTableRow tableRow(string col1, string col2)
        {
            HtmlTableRow row = new HtmlTableRow();
            HtmlTableCell cell1 = new HtmlTableCell();
            HtmlTableCell cell2 = new HtmlTableCell();
            cell1.InnerText = col1;
            cell2.InnerText = col2;
            row.Cells.Add(cell1);
            row.Cells.Add(cell2);
            return row;
        }

        private string paragraph(string id, string text, string style)
        {
            TagBuilder builder = new TagBuilder("p");
            builder.GenerateId(id);
            builder.MergeAttribute("style", style);
            builder.SetInnerText(text);
            return builder.ToString(TagRenderMode.Normal);
        }

        protected override void CreateChildControls()
        {
            table = new HtmlTable();
            table.Border = 1;
            table.CellPadding = 3;
            table.CellSpacing = 3;
            this.Controls.Add(table);
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            try
            {
                //The SharePoint User
                SPUser spUser = SPContext.Current.Web.CurrentUser;
                writer.Write(paragraph("msgSPUser", "The current SPUser is " + spUser.Name, string.Empty));

                //The Claims Identity
                IClaimsIdentity claimsId = (IClaimsIdentity)Page.User.Identity;
                writer.Write(paragraph("msgUser", "The current User is " + claimsId.Name, string.Empty));

                //The Claims Set
                table.Rows.Add(tableRow("Claim Type", "Claim Value"));

                foreach (Claim claim in claimsId.Claims)
                {
                    table.Rows.Add(tableRow(claim.ClaimType,claim.Value));
                }

                table.RenderControl(writer);

            }
            catch (Exception x)
            {
                writer.Write(paragraph("msgError", x.Message, string.Empty));
            }
        }
    }
}
