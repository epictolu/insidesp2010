$m = Get-SPEnterpriseSearchServiceApplication | Get-SPEnterpriseSearchRankingModel -Identity "0D4CB5B6-2FA3-4D7F-AF79-EF0DE64F242C"
$m.IsDefault = $true