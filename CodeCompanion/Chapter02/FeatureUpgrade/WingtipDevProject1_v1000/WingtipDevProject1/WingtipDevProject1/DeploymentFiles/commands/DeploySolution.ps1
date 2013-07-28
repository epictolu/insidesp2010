Write-Host "Executing Powershell script DeploySolution.ps1"

$SolutionPackageName  = $args[0]
$SolutionPackagePath = $args[1]

# Load SharePoint Powershell Snap-In
Add-PSSnapin Microsoft.SharePoint.Powershell

# remove previous version of solution package solution package
$solution = Get-SPSolution | where-object {$_.Name -eq $SolutionPackageName}
if ($solution -ne $null) {
  if($solution.Deployed -eq $true){
    Write-Host "Retracting old version of solution package"
    Uninstall-SPSolution -Identity $SolutionPackageName -Local -Confirm:$false
  }
  Write-Host "Uninstalling old version of solution package"
  Remove-SPSolution -Identity $SolutionPackageName -Confirm:$false
}


Write-Host "Installing Solution Package into Farm Solution Store"
$solution = Add-SPSolution -LiteralPath $SolutionPackagePath

Write-Host "Deploying Solution on Local WFE"
Install-SPSolution -Identity $SolutionPackageName -Local -GACDeployment

Write-Host " "
Write-Host "Solution has been installed and deployed"
Get-SPSolution | where-object {$_.Name -eq $SolutionPackageName} | Format-List Name, DisplayName, SolutionId, Deployed, Status, ContainsGlobalAssembly, DeploymentState, DeployedServers