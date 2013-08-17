using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WingtipFieldTypes
{
    public class EmployeeStatus : SPFieldText
    {
        public EmployeeStatus(SPFieldCollection fields, string fieldName) : base(fields, fieldName) { }
        public EmployeeStatus(SPFieldCollection fields, string typeName, string displayName) : base(fields, typeName, displayName) { }

        public override BaseFieldControl FieldRenderingControl
        {
            get
            {
                var fieldControl = new EmployeeStatusFieldControl();
                fieldControl.FieldName = InternalName;

                return fieldControl;
            }
        }
    }
}
