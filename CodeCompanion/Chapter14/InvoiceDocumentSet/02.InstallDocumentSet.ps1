write-host "Setting up PowerShell environment for SharePoint" -foregroundcolor Yellow
Add-PSSnapin "Microsoft.SharePoint.PowerShell" -ErrorAction SilentlyContinue
write-host "SharePoint PowerShell Snapin loaded." -foregroundcolor Green

write-host
write-host "Installing farm solution..." -foregroundcolor Yellow
Add-SPSolution -LiteralPath "bin\Debug\InvoiceDocumentSet.wsp"
write-host "  ... installation complete" -foregroundcolor Green
write-host "Deploying farm solution..." -foregroundcolor Yellow
Install-SPSolution -Identity "InvoiceDocumentSet.wsp" -Local -GACDeployment
write-host "  ... deployment complete" -foregroundcolor Green

write-host 
write-host "Activating the Invoice Site Columns Feature" -foregroundcolor Yellow
Enable-SPFeature -Identity 3a3a51fd-bae5-4ce3-887a-2cbb0357dca1 -Url 'http://intranet.wingtip.com'
write-host "  ... feature activated" -foregroundcolor Green

write-host "Activating the Invoice Document Set Resources Feature" -foregroundcolor Yellow
Enable-SPFeature -Identity ac485508-fa77-41e4-a24a-7816ed52cd63 -Url 'http://intranet.wingtip.com'
write-host "  ... feature activated" -foregroundcolor Green

write-host "Activating the Invoice Document Set" -foregroundcolor Yellow
Enable-SPFeature -Identity 5bb86bed-f95e-4936-ae85-602397cc3b83 -Url 'http://intranet.wingtip.com'
write-host "  ... feature activated" -foregroundcolor Green

write-host "Recycling IIS" -foregroundcolor Yellow
IISRESET.EXE