using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using System.Text.RegularExpressions;
using Microsoft.SharePoint.WebControls;

namespace WingtipFieldTypes
{
    public class SocialSecurityNumber : SPFieldText
    {
        public SocialSecurityNumber(SPFieldCollection fields, string fieldName) : base(fields, fieldName) { }
        public SocialSecurityNumber(SPFieldCollection fields, string typeName, string displayName) : base(fields, typeName, displayName) { }

        public override string GetValidatedString(object value)
        {
            var userInput = value.ToString();

            var regex = @"^\d{3}-\d{3}-\d{3}$";

            if (!string.IsNullOrEmpty(userInput) && !Regex.IsMatch(userInput, regex))
                throw new SPFieldValidationException("SSN must be form of 123-456-789");

            return base.GetValidatedString(value);
        }

        public override BaseFieldControl FieldRenderingControl
        {
            get
            {
                BaseFieldControl fldControl = new SocialSecurityNumberFieldControl();
                fldControl.FieldName = InternalName;

                return fldControl;
            }
        }
    }
}
