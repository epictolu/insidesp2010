Write-Host "Executing Powershell script BuildSolution.ps1"
Write-Host

$ProjectName = $args[0]
$ProjectDirectory = $args[1]
$DeploymentFilesDirectory = $ProjectDirectory + 'DeploymentFiles\'
$DiamondDefinitionFilePath = $DeploymentFilesDirectory + 'input\BuildSolution.ddf'

Write-Host 'Building Solution Package using MAKECAB.EXE'
Set-Location $ProjectDirectory
$MAKECAB = "C:\Windows\SysWOW64\MAKECAB.EXE"
& $MAKECAB /V1 /F $DiamondDefinitionFilePath

Write-Host "----------------------------------------------------"
Write-Host "Finishing building"$ProjectName".wsp"
Write-Host " "