using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace WingtipFieldTypes {

  public class EmployeeStatus : SPFieldText {
    public EmployeeStatus(SPFieldCollection fields, string fieldName)
      : base(fields, fieldName) { }

    public EmployeeStatus(SPFieldCollection fields, string typeName, string displayName)
      : base(fields, typeName, displayName) { }

    public override BaseFieldControl FieldRenderingControl {
      get {
        BaseFieldControl ctr = new EmployeeStatusFieldControl();
        ctr.FieldName = this.InternalName;
        return ctr;
      }
    }

    public override string DefaultValue {
      get {
        return "Full-time Employee";
      }
    }

  }

  public class EmployeeStatusFieldControl : BaseFieldControl {

    protected RadioButtonList lstEmployeeStatus;
    
    protected override string DefaultTemplateName {
      get {
        return "EmployeeStatusRenderingTemplate";
      }
    }

    protected override void CreateChildControls() {
      base.CreateChildControls();
      lstEmployeeStatus = (RadioButtonList)TemplateContainer.FindControl("lstEmployeeStatus");
      if (lstEmployeeStatus != null) {
        lstEmployeeStatus.Items.Clear();
        lstEmployeeStatus.Items.Add("Full-time Employee");
        lstEmployeeStatus.Items.Add("Part-time Employee");
        
        // check to see if contractors are allowed
        if (this.Field.GetCustomProperty("AllowContractors") != null) {
          bool AllowContactors = (bool)this.Field.GetCustomProperty("AllowContractors");
          if (AllowContactors) {
            lstEmployeeStatus.Items.Add("Contractor");
          }
        }
        
        // check to see if interns are allowed
        if (this.Field.GetCustomProperty("AllowInterns") != null) {
          bool AllowInterns = (bool)this.Field.GetCustomProperty("AllowInterns");
          if (AllowInterns) {
            lstEmployeeStatus.Items.Add("Intern");
          }
        }
      }
    }

    public override object Value {
      get {
        this.EnsureChildControls();
        return lstEmployeeStatus.SelectedValue;
      }
      set {
        this.EnsureChildControls();
        lstEmployeeStatus.Items.FindByValue(ItemFieldValue.ToString()).Selected = true;
      }
    }
  }
}
