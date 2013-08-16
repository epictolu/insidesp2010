using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
namespace WingtipFieldTypes
{
    public class CanadianAddressFieldControl : BaseFieldControl
    {
        protected TextBox txtStreet;
        protected TextBox txtCity;
        protected TextBox txtState;
        protected TextBox txtZipCode;

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            txtStreet = TemplateContainer.FindControl("txtStreet") as TextBox;
            txtCity = TemplateContainer.FindControl("txtCity") as TextBox;
            txtState = TemplateContainer.FindControl("txtState") as TextBox;
            txtZipCode = TemplateContainer.FindControl("txtZipCode") as TextBox;
        }

        protected override string DefaultTemplateName
        {
            get { return "CanadianAddressRenderingTemplate"; }
        }

        public override object Value
        {
            get
            {
                EnsureChildControls();

                var multiColumnValue = new SPFieldMultiColumnValue(4);
                multiColumnValue[0] = txtStreet.Text;
                multiColumnValue[1] = txtCity.Text;
                multiColumnValue[2] = txtState.Text;
                multiColumnValue[3] = txtZipCode.Text;

                return multiColumnValue;
            }

            set
            {
                EnsureChildControls();

                var multiColumnValue = ItemFieldValue as SPFieldMultiColumnValue;
                txtStreet.Text = multiColumnValue[0];
                txtCity.Text = multiColumnValue[1];
                txtState.Text = multiColumnValue[2];
                txtZipCode.Text = multiColumnValue[3];
            }
        }
    }
}
