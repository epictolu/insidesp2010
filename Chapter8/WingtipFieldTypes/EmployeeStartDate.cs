using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// In order to get this to work I had to manually perform an iisreset after the visual studio deployment
namespace WingtipFieldTypes
{
    public class EmployeeStartDate : SPFieldDateTime
    {
        public EmployeeStartDate(SPFieldCollection fields, string fldName) : base(fields, fldName) { }
        public EmployeeStartDate(SPFieldCollection fields, string typeName, string displayName) : base(fields, typeName, displayName) { }

        public override void OnAdded(SPAddFieldOptions op)
        {
            DisplayFormat = SPDateTimeFieldFormatType.DateOnly;
            Update();
        }

        public override string DefaultValue
        {
            get
            {
                var startDate = DateTime.Today;

                while (startDate.DayOfWeek != DayOfWeek.Monday)
                    startDate = startDate.AddDays(1);

                return SPUtility.CreateISO8601DateTimeFromSystemDateTime(startDate);
            }
            set
            {
                base.DefaultValue = value;
            }
        }

        public override string GetValidatedString(object value)
        {
            var input = Convert.ToDateTime(value);

            if (input.DayOfWeek != DayOfWeek.Monday)
                throw new SPFieldValidationException("Employee start date must be a Monday");

            return base.GetValidatedString(value);
        }
    }
}
