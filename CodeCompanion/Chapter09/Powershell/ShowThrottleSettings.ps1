Add-PSSnapin Microsoft.SharePoint.Powershell
$w = Get-SPWebApplication | Where {$_ -match "SharePoint - 80"}
Write-Host
Write-Host "Large List Throttle Settings for " $w.DisplayName
Write-Host "-------------------------------------------------------------------------------"
Write-Host "List View Threshold    " $w.MaxItemsPerThrottledOperation
Write-Host "List View Warning      " $w.MaxItemsPerThrottledOperationWarningLevel
Write-Host "Object Model Override  " $w.AllowOMCodeOverrideThrottleSettings
Write-Host "Auditors and Admins    " $w.MaxItemsPerThrottledOperationOverride
Write-Host "Max Lookup lists       " $w.MaxQueryLookupFields
Write-Host "Unthrottled Window     " $w.UnthrottledPriviledgeOperationWindowEnabled
Write-Host "Window Open (hr)       " $w.DailyStartUnthrottledPrivilegedOperationsHour
Write-Host "Window Open (min)      " $w.DailyStartUnthrottledPrivilegedOperationsMinute
Write-Host "Window Open (dur)      " $w.DailyUnthrottledPrivilegedOperationsDuration
Write-Host