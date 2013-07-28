using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace WingtipFieldTypes {

  public class SocialSecurityNumber : SPFieldText {
    public SocialSecurityNumber(SPFieldCollection fields, string fieldName)
      : base(fields, fieldName) { }

    public SocialSecurityNumber(SPFieldCollection fields, string typeName, string displayName)
      : base(fields, typeName, displayName) { }

    public override BaseFieldControl FieldRenderingControl {
      get {
        BaseFieldControl ctr = new SocialSecurityNumberFieldControl();
        ctr.FieldName = this.InternalName;
        return ctr;
      }
    }

    public override string GetValidatedString(object value) {
      string UserInput = value.ToString();
      string SSN_RegularExpression = @"^\d{3}-\d{2}-\d{4}$";
      if ((this.Required || !string.IsNullOrEmpty(UserInput)) &
            (!Regex.IsMatch(UserInput, SSN_RegularExpression))) {
        throw new SPFieldValidationException("SSN must be form of 123-45-6789");
      }
      return base.GetValidatedString(value);
    }
  }

  public class SocialSecurityNumberFieldControl : BaseFieldControl {
    protected TextBox txtUserInput;
    protected override string DefaultTemplateName {
      get {
        return "SocialSecurityNumberRenderingTemplate";
      }
    }
    protected override void CreateChildControls() {
      base.CreateChildControls();
      txtUserInput = (TextBox)this.TemplateContainer.FindControl("txtUserInput");
    }

    public override object Value {
      get {
        this.EnsureChildControls();
        return txtUserInput.Text;
      }
      set {
        this.EnsureChildControls();
        txtUserInput.Text = (string)this.ItemFieldValue;
      }
    }
  }


}
