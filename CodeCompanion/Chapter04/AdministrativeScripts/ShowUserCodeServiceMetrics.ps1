cls
Add-PSSnapin Microsoft.SharePoint.Powershell -ErrorAction SilentlyContinue

Write-Host "Getting reference to user code service..." -foregroundcolor red
$usercode = [Microsoft.SharePoint.Administration.SPUserCodeService]::Local

Write-Host "Show all resource quota measures..." -foregroundcolor red
$usercode.ResourceMeasures | Format-Table Name

$PercentProcessorTime = $usercode.ResourceMeasures["PercentProcessorTime"]

Write-Host "Before Update"
Write-Host "PercentProcessorTime.AbsoluteLimit = " + $PercentProcessorTime.AbsoluteLimit

write-host
write-host "performing update" -foregroundcolor red
$PercentProcessorTime.AbsoluteLimit = 100
$PercentProcessorTime.Update()
write-host

Write-Host "After Update"
Write-Host "PercentProcessorTime.AbsoluteLimit = " + $PercentProcessorTime.AbsoluteLimit

Read-Host