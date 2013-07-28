Write-Host "Getting Search Application..."
$SearchApplication = Get-SPEnterpriseSearchServiceApplication

Write-Host "Removing Ranking Models..." 
Remove-SPEnterpriseSearchRankingModel -Identity "c978ef2b-300a-444b-af9a-d51261294587" -SearchApplication $SearchApplication 
Remove-SPEnterpriseSearchRankingModel -Identity "0D4CB5B6-2FA3-4D7F-AF79-EF0DE64F242C" -SearchApplication $SearchApplication 

Write-Host "Listing models..."
Get-SPEnterpriseSearchServiceApplication|Get-SPEnterpriseSearchRankingModel