Add-PSSnapin Microsoft.SharePoint.Powershell -ErrorAction SilentlyContinue

$SolutionPackageName  = "WingtipDevProject1.wsp"
$SolutionPackagePath = "WingtipDevProject1_v2000\WingtipDevProject1.wsp"

# ensure previous version of solution package solution package is already deployed
$solution = Get-SPSolution | where-object {$_.Name -eq $SolutionPackageName}
if ($solution -ne $null) {
  if($solution.Deployed -eq $true){
    Write-Host "Updating old version of solution package"
    Update-SPSolution -Identity $SolutionPackageName -LiteralPath $SolutionPackagePath -Local -GACDeployment
    Write-Host " "
    Write-Host "Solution has been updated"
    Get-SPSolution | where-object {$_.Name -eq $SolutionPackageName} | Format-List Name, DisplayName, SolutionId, Deployed, Status, ContainsGlobalAssembly, DeploymentState, DeployedServers
  }
  else {
    Write-Host "Solution package cannot be updated because it is not currently deployed"  
  }
}

Write-Host " "
Write-Host
Write-Host "Update Solution script has completed. Press OK to quit"
Read-Host