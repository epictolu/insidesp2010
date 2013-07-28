Write-Host 

# collect input parameters
$ProjectName = $args[0]
$ProjectDirectory = $args[1]
$TargetPath = $args[2]
$ConfigurationName = $args[3]
$SolutionPackageName = $ProjectName + ".wsp"

$SolutionPackagePath = $ProjectDirectory + "DeploymentFiles\output\" + $SolutionPackageName

# display parameters to VS output windows
# Write-Host 'ProjectName ='$ProjectName 
# Write-Host 'ProjectDirectory ='$ProjectDirectory
# Write-Host 'ConfigurationName ='$ConfigurationName

Set-Location $ProjectDirectory


switch ($ConfigurationName) { 

  "BuildSolution"          { & .\DeploymentFiles\commands\BuildSolution.ps1 $ProjectName $ProjectDirectory } 

  "InstallSolution"        { & .\DeploymentFiles\commands\BuildSolution.ps1 $ProjectName $ProjectDirectory
                             & .\DeploymentFiles\commands\InstallSolution.ps1 $SolutionPackageName $SolutionPackagePath } 

  "DeploySolution"         { & .\DeploymentFiles\commands\BuildSolution.ps1 $ProjectName $ProjectDirectory
                             & .\DeploymentFiles\commands\DeploySolution.ps1 $SolutionPackageName $SolutionPackagePath } 

  "UpdateSolution"         { & .\DeploymentFiles\commands\BuildSolution.ps1 $ProjectName $ProjectDirectory
                             & .\DeploymentFiles\commands\UpdateSolution.ps1 $SolutionPackageName $SolutionPackagePath } 

  "RetractSolution"        { & .\DeploymentFiles\commands\RetractSolution.ps1 $SolutionPackageName } 

  "RemoveSolution"         { & .\DeploymentFiles\commands\RemoveSolution.ps1 $SolutionPackageName } 

  "RecycleApplicationPool" { & .\DeploymentFiles\commands\RecycleApplicationPool.ps1 $SolutionPackageName } 

  "ResetIIS"               { & .\DeploymentFiles\commands\ResetIIS.ps1 $SolutionPackageName } 

  "RefreshAssemblyInGac"   { & .\DeploymentFiles\commands\RefreshAssemblyInGac.ps1 $TargetPath 
                             & .\DeploymentFiles\commands\RecycleApplicationPool.ps1 $SolutionPackageName } 

  "XCopySharePointRoot"    { & .\DeploymentFiles\commands\XCopySharePointRoot.ps1 $ProjectDirectory } 

  default                  { Write-Host "default handler - something's wrong in BuildCommandDispatcher.ps1" }
}

Write-Host 