write-host "Setting up PowerShell environment for SharePoint" -foregroundcolor Yellow
Add-PSSnapin "Microsoft.SharePoint.PowerShell" -ErrorAction SilentlyContinue
write-host "SharePoint PowerShell Snapin loaded." -foregroundcolor Green

write-host 
write-host "Activating the Document Set Feature" -foregroundcolor Yellow
Enable-SPFeature -Identity DocumentSet -Url 'http://intranet.wingtip.com'
write-host "DocumentSet Feature activated" -foregroundcolor Green