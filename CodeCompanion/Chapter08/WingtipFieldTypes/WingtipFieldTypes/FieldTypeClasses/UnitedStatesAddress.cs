using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;


namespace WingtipFieldTypes {

  public class UnitedStatesAddress : SPFieldMultiColumn {
    public UnitedStatesAddress(SPFieldCollection fields, string fieldName)
      : base(fields, fieldName) { }

    public UnitedStatesAddress(SPFieldCollection fields, string typeName, string displayName)
      : base(fields, typeName, displayName) { }

    public override BaseFieldControl FieldRenderingControl {
      get {
        BaseFieldControl ctr = new UnitedStatesAddressFieldControl();
        ctr.FieldName = this.InternalName;
        return ctr;
      }
    }

    
  }

  public class UnitedStatesAddressFieldControl : BaseFieldControl {
    protected override string DefaultTemplateName {
      get {
        return "UnitedStatesAddressRenderingTemplate";
      }
    }
    protected TextBox txtStreet;
    protected TextBox txtCity;
    protected TextBox txtState;
    protected TextBox txtZipcode;


    protected override void CreateChildControls() {
      base.CreateChildControls();
      txtStreet = (TextBox)this.TemplateContainer.FindControl("txtStreet");
      txtCity = (TextBox)this.TemplateContainer.FindControl("txtCity");
      txtState = (TextBox)this.TemplateContainer.FindControl("txtState");
      txtZipcode = (TextBox)this.TemplateContainer.FindControl("txtZipcode");
    }

    public override object Value {
      get {
        this.EnsureChildControls();
        SPFieldMultiColumnValue mcv = new SPFieldMultiColumnValue(4);
        mcv[0] = txtStreet.Text;
        mcv[1] = txtCity.Text;
        mcv[2] = txtState.Text;
        mcv[3] = txtZipcode.Text;
        return mcv;
      }
      set {
        this.EnsureChildControls();
        SPFieldMultiColumnValue mcv = (SPFieldMultiColumnValue)this.ItemFieldValue;
        txtStreet.Text = mcv[0];
        txtCity.Text = mcv[1];
        txtState.Text = mcv[2]; ;
        txtZipcode.Text = mcv[3];
      }
    }
  }
}
