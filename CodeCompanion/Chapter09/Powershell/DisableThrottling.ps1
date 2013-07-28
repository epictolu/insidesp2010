Add-PSSnapin Microsoft.SharePoint.Powershell
$site = Get-SPWeb -Identity "http://contososerver/listsandschemas"
$listTitle = "Large List"
$list = $site.Lists[$listTitle]
$list.EnableThrottling = $false
$list.EnableThrottling