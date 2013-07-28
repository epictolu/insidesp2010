Write-Host "Executing Powershell script RefreshAssemblyInGac.ps1"

$TargetPath = $args[0]

$GacUtil = "C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\Bin\gacutil.exe"

& $GacUtil /i $TargetPath