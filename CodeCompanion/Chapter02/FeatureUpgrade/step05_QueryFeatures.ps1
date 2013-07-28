Add-PSSnapin Microsoft.SharePoint.Powershell -ErrorAction SilentlyContinue

$WebAppUrl = "http://intranet.wingtip.com"
$featureId = New-Object System.Guid -ArgumentList "86689158-7048-4421-AD21-E0DEF0D67C81"

$webApp = [Microsoft.SharePoint.Administration.SPWebApplication]::Lookup($WebAppUrl)
$features = $webApp.QueryFeatures($FeatureId)

foreach($feature in $features){
  Write-Host "Feature version "$feature.Version" is enabled in site at "$feature.Parent.Url
  Write-Host 
}
        
        
Write-Host
Write-Host "Query Features script has completed. Press OK to quit"
Read-Host