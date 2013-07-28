Write-Host "Executing Powershell script RecycleApplicationPool.ps1"

$appPoolDisplayName = "SharePoint Default App Pool"
$appPoolName = "W3SVC/APPPOOLS/" + $appPoolDisplayName 
$appPool = Get-WmiObject -namespace "root\MicrosoftIISv2" -class "IIsApplicationPool" | Where-Object {$_.Name -eq $appPoolName}
if($appPool -ne $null) {
  $appPool.Recycle()
  Write-Host "The application pool [$appPoolDisplayName] has been recycled"
}
else {
  Write-Host "The application pool [$appPoolDisplayName] was not found"
}

