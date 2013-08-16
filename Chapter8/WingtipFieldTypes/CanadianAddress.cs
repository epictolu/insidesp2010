using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace WingtipFieldTypes
{
    public class CanadianAddress : SPFieldMultiColumn
    {
        public CanadianAddress(SPFieldCollection fields, string fieldName) : base(fields, fieldName) { }
        public CanadianAddress(SPFieldCollection fields, string typeName, string displayName) : base(fields, typeName, displayName) { }

        public override BaseFieldControl FieldRenderingControl
        {
            get
            {
                var fieldControl = new CanadianAddressFieldControl();
                fieldControl.FieldName = InternalName;

                return fieldControl;
            }
        }

        public override string GetValidatedString(object value)
        {
            var userInput = value as SPFieldMultiColumnValue;

            return userInput.ToString();
        }
    }
}
