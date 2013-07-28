using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.WebControls;

namespace WingtipFieldTypes {

  public class EmployeeStartDate : SPFieldDateTime {

    public EmployeeStartDate(SPFieldCollection fields, string fieldName)
      : base(fields, fieldName) { }

    public EmployeeStartDate(SPFieldCollection fields, string typeName, string displayName)
      : base(fields, typeName, displayName) { }


    // configure field to display date but not time
    public override void OnAdded(SPAddFieldOptions op) {
      this.DisplayFormat = SPDateTimeFieldFormatType.DateOnly;
      this.Update();
    }

    // add logic to create default date as first Monday
    public override string DefaultValue {
      get {
        DateTime startDate = DateTime.Today;
        while (startDate.DayOfWeek != DayOfWeek.Monday) {
          startDate = startDate.AddDays(1);
        }
        return SPUtility.CreateISO8601DateTimeFromSystemDateTime(startDate);
      }
    }
  
    // add validation to ensure start date is a Monday
    public override string GetValidatedString(object value) {
      DateTime input = System.Convert.ToDateTime(value);
      if (input.DayOfWeek != DayOfWeek.Monday) {
        throw new SPFieldValidationException("Employee start date must be a monday");
      }
      return base.GetValidatedString(value);
    }


  }
}
