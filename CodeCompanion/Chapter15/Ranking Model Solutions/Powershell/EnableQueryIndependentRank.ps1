$p = Get-SPEnterpriseSearchServiceApplication|Get-SPEnterpriseSearchMetadataManagedProperty -Identity 319
$p
$p.EnabledForQueryIndependentRank = $true