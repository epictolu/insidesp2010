using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.UserCode;

namespace SandboxSolutionValidator
{
    public class FeatureReceiver : SPFeatureReceiver
    {
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            var userCodeService = SPUserCodeService.Local;
            var validator = new SimpleSolutionValidator(userCodeService);
            userCodeService.SolutionValidators.Add(validator);
        }

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            var userCodeService = SPUserCodeService.Local;
            var validator = new SimpleSolutionValidator(userCodeService);
            userCodeService.SolutionValidators.Remove(validator.Id);
        }
    }
}
