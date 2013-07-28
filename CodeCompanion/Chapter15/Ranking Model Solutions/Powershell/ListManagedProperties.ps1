Write-Host "Managed Properties"
Get-SPEnterpriseSearchServiceApplication|Get-SPEnterpriseSearchMetadataManagedProperty

#Write-Host "Crawled Properties"
#Get-SPEnterpriseSearchServiceApplication|Get-SPEnterpriseSearchMetadataCrawledProperty | foreach {$_.Name}