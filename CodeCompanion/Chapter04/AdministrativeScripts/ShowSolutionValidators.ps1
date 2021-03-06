Add-PSSnapin Microsoft.SharePoint.Powershell -ErrorAction SilentlyContinue

Write-Host "Getting reference to user code service..." -foregroundcolor red
$usercode = [Microsoft.SharePoint.Administration.SPUserCodeService]::Local
$usercode.SolutionValidators | Format-List Name, Id, Signature, Status, TypeName