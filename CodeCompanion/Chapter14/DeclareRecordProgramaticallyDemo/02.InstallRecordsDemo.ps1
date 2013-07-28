write-host "Setting up PowerShell environment for SharePoint" -foregroundcolor Yellow
Add-PSSnapin "Microsoft.SharePoint.PowerShell" -ErrorAction SilentlyContinue
write-host "SharePoint PowerShell Snapin loaded." -foregroundcolor Green

write-host
write-host "Installing farm solution..." -foregroundcolor Yellow
Add-SPSolution -LiteralPath "bin\Debug\DeclareRecordProgramaticallyDemo.wsp"
write-host "  ... installation complete" -foregroundcolor Green
write-host "Deploying farm solution..." -foregroundcolor Yellow
Install-SPSolution -Identity "DeclareRecordProgramaticallyDemo.wsp" -Local -GACDeployment
write-host "  ... deployment complete" -foregroundcolor Green

write-host 
write-host "Activating the Declare Record Programatically Demo Feature" -foregroundcolor Yellow
Enable-SPFeature -Identity 9c149ce9-2c27-415e-971b-89532b6c1e44 -Url 'http://intranet.wingtip.com'
write-host "  ... feature activated" -foregroundcolor Green