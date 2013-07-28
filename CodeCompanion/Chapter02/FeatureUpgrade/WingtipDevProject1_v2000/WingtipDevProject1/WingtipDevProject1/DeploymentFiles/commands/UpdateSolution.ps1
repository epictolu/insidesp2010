Write-Host "Executing Powershell script UpgradeSolution.ps1"

$SolutionPackageName  = $args[0]
$SolutionPackagePath = $args[1]

# Load SharePoint Powershell Snap-In
Add-PSSnapin Microsoft.SharePoint.Powershell

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