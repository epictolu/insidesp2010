using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Diagnostics;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.Administration.Claims;
using Microsoft.SharePoint.WebControls;
using Microsoft.IdentityModel;
using Microsoft.IdentityModel.Claims;
using Microsoft.Office.Server;
using Microsoft.Office.Server.Audience;

namespace AudienceClaims {
  public class Provider : SPClaimProvider {
    public Provider(string displayName)
      : base(displayName) {
    }

    protected override void FillClaimTypes(List<string> claimTypes) {
      if (claimTypes == null)
        throw new ArgumentException("claimTypes");

      claimTypes.Add(RoleClaimType);
    }

    protected override void FillClaimValueTypes(List<string> claimValueTypes) {
      if (claimValueTypes == null)
        throw new ArgumentException("claimValueTypes");

      claimValueTypes.Add(ValueType);
    }

    protected override void FillClaimsForEntity(System.Uri context, SPClaim entity, List<SPClaim> claims) {
      if (entity == null)
        throw new ArgumentException("entity");
      if (claims == null)
        throw new ArgumentException("claims");

      //Add audience claims
      using (SPSite ca = new SPSite(CentralAdminUrl)) {
        SPServiceContext ctx = SPServiceContext.GetContext(ca);
        AudienceManager mgr = new AudienceManager(ctx);

        foreach (Audience audience in mgr.Audiences) {
          if (audience.IsMember(entity.Value))
            claims.Add(CreateClaim(RoleClaimType, audience.AudienceName, ValueType));
        }
      }
    }

    protected override void FillEntityTypes(List<string> entityTypes) {
      entityTypes.Add(SPClaimEntityTypes.FormsRole);
    }

    protected override void FillHierarchy(System.Uri context, string[] entityTypes, string hierarchyNodeID, int numberOfLevels, SPProviderHierarchyTree hierarchy) {
      //No additional nodes need to be added to the People Picker
    }

    protected override void FillResolve(System.Uri context, string[] entityTypes, SPClaim resolveInput, List<PickerEntity> resolved) {
      if (resolveInput.ClaimType == (RoleClaimType))
        resolved.Add(CreatePickerEntityForAudience(resolveInput.Value));
    }

    protected override void FillResolve(System.Uri context, string[] entityTypes, string resolveInput, List<PickerEntity> resolved) {
      List<string> audiences = new List<string>();

      using (SPSite ca = new SPSite(CentralAdminUrl)) {
        SPServiceContext ctx = SPServiceContext.GetContext(ca);
        AudienceManager mgr = new AudienceManager(ctx);

        foreach (Audience audience in mgr.Audiences) {
          if (audience.AudienceName.StartsWith(resolveInput, StringComparison.CurrentCultureIgnoreCase))
            audiences.Add(audience.AudienceName);
        }
      }

      foreach (string audienceName in audiences)
        resolved.Add(CreatePickerEntityForAudience(audienceName));
    }

    protected override void FillSchema(SPProviderSchema schema) {
      schema.AddSchemaElement(new SPSchemaElement("Audience", "Audience", SPSchemaElementType.Both));
    }

    protected override void FillSearch(System.Uri context, string[] entityTypes, string searchPattern, string hierarchyNodeID, int maxCount, SPProviderHierarchyTree searchTree) {
      if (EntityTypesContain(entityTypes, SPClaimEntityTypes.FormsRole)) {

        List<string> audiences = new List<string>();

        using (SPSite ca = new SPSite(CentralAdminUrl)) {
          SPServiceContext ctx = SPServiceContext.GetContext(ca);
          AudienceManager mgr = new AudienceManager(ctx);

          foreach (Audience audience in mgr.Audiences) {
            if (audience.AudienceName.StartsWith(searchPattern, StringComparison.CurrentCultureIgnoreCase))
              audiences.Add(audience.AudienceName);
          }
        }

        foreach (string audienceName in audiences)
          searchTree.AddEntity(CreatePickerEntityForAudience(audienceName));

      }
    }

    public override string Name {
      get { return "Audience Claim Provider"; }
    }

    public override bool SupportsEntityInformation {
      get { return true; }
    }

    public override bool SupportsHierarchy {
      get { return true; }
    }

    public override bool SupportsResolve {
      get { return true; }
    }

    public override bool SupportsSearch {
      get { return true; }
    }

    internal static string ProviderDisplayName {
      get { return "Audience Claim Provider"; }
    }

    internal static string InternalDisplayName {
      get { return "AudienceClaimProvider"; }
    }

    internal static string CentralAdminUrl {
      get { return SPAdministrationWebApplication.Local.AlternateUrls[0].IncomingUrl; }
    }

    private static string RoleClaimType {
      get { return "http://www.microsoft.com/identity/claims/audience"; }
    }

    private static string ValueType {
      get { return Microsoft.IdentityModel.Claims.ClaimValueTypes.String; }
    }

    private PickerEntity CreatePickerEntityForAudience(string audience) {
      //Create the Picker Entity
      PickerEntity entity = CreatePickerEntity();
      entity.Claim = CreateClaim(RoleClaimType, audience, ValueType);
      entity.Description = ProviderDisplayName + ":" + audience;
      entity.DisplayText = audience;
      entity.EntityType = SPClaimEntityTypes.FormsRole;
      entity.IsResolved = true;
      entity.EntityGroupName = "Audience";
      entity.EntityData["Audience"] = audience;
      return entity;
    }

  }
}
