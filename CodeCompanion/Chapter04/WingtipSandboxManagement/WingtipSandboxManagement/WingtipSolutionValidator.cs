using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.UserCode;

namespace WingtipSandboxManagement {

  [Guid("deadbeef-2bad-dead-beef-BadBadBadBad")]
  public class WingtipSolutionValidator : SPSolutionValidator  {
   
    private const string validatorName = "Wingtip Solution Validator";

    public WingtipSolutionValidator() { }

    public WingtipSolutionValidator(SPUserCodeService userCodeService)
      : base(validatorName, userCodeService) {
      this.Signature = 1111;
    }

    public override void ValidateSolution(SPSolutionValidationProperties properties) {
      bool isValid = true;
      //Check the name of the package
      if (properties.PackageFile.Location.StartsWith("Bad_", StringComparison.CurrentCultureIgnoreCase))
        isValid = false;
      //Look at the files in the package
      foreach (SPSolutionFile file in properties.Files) {
        if (file.Location.StartsWith("Bad_", StringComparison.CurrentCultureIgnoreCase))
          isValid = false;
      }
      //set error handler
      properties.ValidationErrorMessage = "The solution did not pass the requirements of the Wingtip Solution Validator.";
      properties.ValidationErrorUrl = "/_layouts/WingtipSandboxManagement/ValidationError.aspx?SolutionName=" + properties.Name;
      properties.Valid = isValid;
    }

    public override void ValidateAssembly(SPSolutionValidationProperties properties, SPSolutionFile assembly) {
      base.ValidateAssembly(properties, assembly);
      bool isValid = true;
      //Check the name of the assembly
      if (assembly.Location.StartsWith("Bad_", StringComparison.CurrentCultureIgnoreCase))
        isValid = false;
      //set error handler
      properties.ValidationErrorMessage = "Failed simple validation.";
      properties.ValidationErrorUrl = "/_layouts/WingtipSandboxManagement/ValidationError.aspx?SolutionName=" + properties.Name;
      properties.Valid = isValid;
    }
  }
}
