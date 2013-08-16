using Microsoft.SharePoint.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace WingtipFieldTypes
{
    public class SocialSecurityNumberFieldControl : BaseFieldControl
    {
        protected TextBox userInput;

        protected override string DefaultTemplateName
        {
            get { return "SocialSecurityNumberRenderingTemplate"; }
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            userInput = (TextBox) TemplateContainer.FindControl("txtUserInput");
        }

        public override object Value
        {
            get
            {
                EnsureChildControls();
                return userInput.Text;
            }
            set
            {
                EnsureChildControls();
                userInput.Text = ItemFieldValue as string;
            }
        }
    }
}
