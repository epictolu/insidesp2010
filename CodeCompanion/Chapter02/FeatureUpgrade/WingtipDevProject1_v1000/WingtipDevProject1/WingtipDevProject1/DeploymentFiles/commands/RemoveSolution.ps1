Write-Host "Executing Powershell script RemoveSolution.ps1"

$SolutionPackageName  = $args[0]

# Load SharePoint Powershell Snap-In
Add-PSSnapin Microsoft.SharePoint.Powershell

# remove previous version of solution package solution package
$solution = Get-SPSolution | where-object {$_.Name -eq $SolutionPackageName}
if ($solution -ne $null) {
  if($solution.Deployed -eq $true){
    Write-Host "Retracting currently deployed version of solution package"
    Uninstall-SPSolution -Identity $SolutionPackageName -Local -Confirm:$false
  }
  Write-Host "Uninstalling currently installed version of solution package"
  Remove-SPSolution -Identity $SolutionPackageName -Confirm:$false
  Write-Host "Solution Package has been removed from farm solution store"
}
else {
  Write-Host "Solution Package was not found in farm solution store"
}


