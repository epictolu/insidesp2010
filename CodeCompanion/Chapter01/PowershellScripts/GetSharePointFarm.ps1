Add-PSSnapin Microsoft.SharePoint.PowerShell -ErrorAction "SilentlyContinue"

Get-SPFarm | Format-List Id, BuildVersion, Servers, Solutions