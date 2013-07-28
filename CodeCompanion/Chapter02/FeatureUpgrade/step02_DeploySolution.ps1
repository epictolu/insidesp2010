Add-PSSnapin Microsoft.SharePoint.Powershell -ErrorAction SilentlyContinue

$SolutionPackageName  = "WingtipDevProject1.wsp"
$SolutionPackagePath = "WingtipDevProject1_v1000\WingtipDevProject1.wsp"

$solution = Get-SPSolution | where-object {$_.Name -eq $SolutionPackageName}
if ($solution -ne $null) {
  if($solution.Deployed -eq $true){
    Uninstall-SPSolution -Identity $SolutionPackageName -Local -Confirm:$false
  }
  Remove-SPSolution -Identity $SolutionPackageName -Confirm:$false
}

Write-Host "Installing Solution..."
Add-SPSolution -LiteralPath $SolutionPackagePath
Write-Host
Write-Host "Installation Complete"
Write-Host "Deploying Solution..."
Install-SPSolution -Identity $SolutionPackageName -Local -GACDeployment
Write-Host "Deployment Complete"

Write-Host
Write-Host "Deploy Solution script has completed. Press OK to quit"
Read-Host