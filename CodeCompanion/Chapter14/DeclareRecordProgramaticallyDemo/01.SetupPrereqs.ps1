write-host "Setting up PowerShell environment for SharePoint" -foregroundcolor Yellow
Add-PSSnapin "Microsoft.SharePoint.PowerShell" -ErrorAction SilentlyContinue
write-host "SharePoint PowerShell Snapin loaded." -foregroundcolor Green

write-host 
write-host "Activating the In Place Records Management Feature" -foregroundcolor Yellow
Enable-SPFeature -Identity InPlaceRecords -Url 'http://intranet.wingtip.com'
write-host "Document InPlaceRecords activated" -foregroundcolor Green