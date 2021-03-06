
Add-PSSnapin Microsoft.SharePoint.Powershell -ErrorAction SilentlyContinue

write-host "Getting http://intranet.wingtip.com" -foregroundcolor red
$siteCollection = Get-SPSite -Limit All  | Where-Object {$_.Url -eq "http://intranet.wingtip.com"}

Write-Host "Show resource quota settings for site" -foregroundcolor red
$siteCollection.Quota
