using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.UserCode;

namespace SandboxSolutionValidator
{
    [Guid("4d1899db-517f-446c-a1cd-48ce5076f514")]
    public class SimpleSolutionValidator : SPSolutionValidator
    {
        [Persisted]
        List<string> allowedPublishers;

        const string validatorName = "Simple Solution Validator";

        public SimpleSolutionValidator() { }
        public SimpleSolutionValidator(SPUserCodeService userCodeService) : base(validatorName, userCodeService)
        {
            Signature = 5555;
        }
        
        public override void ValidateSolution(SPSolutionValidationProperties properties)
        {
            ValidateSolution(properties);
            
            var isValid = true;

            if (properties.PackageFile.Location.StartsWith("Bad_", StringComparison.CurrentCultureIgnoreCase))
                isValid = false;

            if (properties.Files.Any(x => x.Location.StartsWith("Bad_", StringComparison.CurrentCultureIgnoreCase)))
                isValid = false;

                properties.ValidationErrorMessage = "Failed simple validation.";
//                properties.ValidationErrorUrl = "/_layouts/Simple_Validator/ValidationError.aspx?SolutionName=" + properties.Name;
                properties.Valid = isValid;
        }

        public override void ValidateAssembly(SPSolutionValidationProperties properties, SPSolutionFile assembly)
        {
            ValidateAssembly(properties, assembly);

            var isValid = true;

            if (assembly.Location.StartsWith("Bad_", StringComparison.CurrentCultureIgnoreCase))
                isValid = false;

                properties.ValidationErrorMessage = "Failed simple validation.";
//                properties.ValidationErrorUrl = "/_layouts/Simple_Validator/ValidationError.aspx?SolutionName=" + properties.Name;
                properties.Valid = isValid;
        }
    }
}
