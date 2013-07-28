Add-PSSnapin Microsoft.SharePoint.PowerShell -ErrorAction "SilentlyContinue"

$name = "Wingtip Testing Web App"
$port = 1001
$hostHeader = "intranet.wingtip.com"
$url = "http://intranet.wingtip.com"
$appPoolName = "SharePoint Default Appl Pool"
$appPoolAccount = Get-SPManagedAccount "WINGTIP\SP_WorkerProcess"

New-SPWebApplication -Name $name -Port $port -HostHeader $hostHeader -URL $url -ApplicationPool $appPoolName -ApplicationPoolAccount $appPoolAccount