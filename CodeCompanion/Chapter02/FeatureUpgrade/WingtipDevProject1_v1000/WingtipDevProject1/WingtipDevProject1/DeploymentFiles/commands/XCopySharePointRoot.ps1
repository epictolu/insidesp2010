Write-Host "Executing Powershell script XCopySharePointRoot.ps1"

$ProjectDirectory  = $args[0]

$Source = $ProjectDirectory + "SharePointRoot\*"
$Destination = "C:\Program Files\Common Files\Microsoft Shared\Web Server Extensions\14"

$Items = Copy-Item $Source -Destination $Destination -Recurse -Force -PassThru -Confirm:$false

Write-Host "The following files have been copied"

$DirectoryColumn  = @{expression={ (([string]($_.Directory)).SubString(69)) }; label='Directory';width=100;alignment='left'}
$Items | where {$_.Directory -ne $null } | Format-Table $DirectoryColumn, Name -autosize -wrap