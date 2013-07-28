Add-PSSnapin Microsoft.SharePoint.Powershell -ErrorAction SilentlyContinue

$WebAppUrl = "http://intranet.wingtip.com"
$featureId = New-Object System.Guid -ArgumentList "86689158-7048-4421-AD21-E0DEF0D67C81"

$webApp = [Microsoft.SharePoint.Administration.SPWebApplication]::Lookup($WebAppUrl)
$features = $webApp.QueryFeatures($FeatureId, $true)

foreach($feature in $features){
  Write-Host "Updating feature in "$feature.Parent.Url
  $feature.Upgrade($true)
}
        
        
Write-Host
Write-Host "Upgrade Feature script has completed. Press OK to quit"
Read-Host