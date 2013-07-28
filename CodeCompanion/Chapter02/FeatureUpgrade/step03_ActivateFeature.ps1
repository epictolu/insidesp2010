Add-PSSnapin Microsoft.SharePoint.Powershell -ErrorAction SilentlyContinue

$FeatureId = "86689158-7048-4421-AD21-E0DEF0D67C81"
$SiteUrl = "http://intranet.wingtip.com"

# Get-Help Enable-SPFeature -full
Enable-SPFeature -Identity $FeatureId -Url $SiteUrl

Write-Host
Write-Host "Activate Feature script has completed. Press OK to quit"
#Read-Host