Add-PSSnapin Microsoft.SharePoint.PowerShell -ErrorAction SilentlyContinue
$sss = Get-SPServiceApplicationProxy | Where {$_ -match "Secure Store Service"}
$sss
