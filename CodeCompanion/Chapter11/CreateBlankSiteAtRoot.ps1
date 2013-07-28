Write-Host 
# CreateTeamSitesAtRoot.ps1
# define variables for script
$SiteTitle = "Wingtip Dev Site"
$SiteUrl = "http://journeyman2010"
$SiteTemplate = "STS#1"
$Owner = "JOURNEYMAN2010\tedp"

# check to ensure Microsoft.SharePoint.PowerShell is loaded
$snapin = Get-PSSnapin | Where-Object {$_.Name -eq 'Microsoft.SharePoint.Powershell'}
if ($snapin -eq $null) {
Write-Host "Loading SharePoint Powershell Snapin"
Add-PSSnapin "Microsoft.SharePoint.Powershell"
}

# delete any existing site found at target URL
$targetUrl = Get-SPSite | Where-Object {$_.Url -eq $SiteUrl}
if ($targetUrl -ne $null) {
  Write-Host "Deleting existing site at" $SiteUrl
  Remove-SPSite -Identity $SiteUrl -Confirm:$false
}

# create new site at target URL
Write-Host "Creating new site at" $SiteUrl 
$NewSite = New-SPSite -URL $SiteUrl -OwnerAlias $Owner -Template $SiteTemplate -Name $SiteTitle
$RootWeb = $NewSite.RootWeb
# display site info to user
Write-Host "-------------------------------------"
Write-Host "Site created successfully"
Write-Host "Title:" $RootWeb.Title -foregroundcolor Green
Write-Host "URL:" $RootWeb.Url -foregroundcolor Red
Write-Host "ID:" $RootWeb.Id.ToString() -foregroundcolor Blue
Write-Host "-------------------------------------"

