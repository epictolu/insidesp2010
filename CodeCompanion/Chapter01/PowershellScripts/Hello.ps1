Write-Host "-----------------------------------"
Write-Host "Hello World of Powershell Scripting"
Write-Host "Host name: "$(Get-Item env:\computerName).value
Write-Host "-----------------------------------"