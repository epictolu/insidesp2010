Add-PSSnapin Microsoft.SharePoint.Powershell -ErrorAction SilentlyContinue

$SiteTitle = "Original Site Title"
$SiteUrl = "http://intranet.wingtip.com"
$SiteTemplate = "STS#1"
$siteOwner = "Administrator"

# delete any existing site found at target URL
$targetUrl = Get-SPSite | Where-Object {$_.Url -eq $SiteUrl}
if ($targetUrl -ne $null) {
  Write-Host "Deleting existing site at" $SiteUrl
  Remove-SPSite -Identity $SiteUrl -Confirm:$false
}

# create new site at target URL
Write-Host "Creating new site at" $SiteUrl 
$NewSite = New-SPSite -URL $SiteUrl -OwnerAlias $siteOwner -Template $SiteTemplate -Name $SiteTitle
$RootWeb = $NewSite.RootWeb

# display site info to user
Write-Host "-------------------------------------"
Write-Host "Site created successfully"
Write-Host "Title:" $RootWeb.Title -foregroundcolor Green
Write-Host "URL:" $RootWeb.Url -foregroundcolor Red
Write-Host "ID:" $RootWeb.Id.ToString() -foregroundcolor Blue
Write-Host "-------------------------------------"

Write-Host "Launching IE and navigating to new site"
$ie = New-Object -com "InternetExplorer.Application"
$ie.navigate($SiteUrl)
$ie.visible = $true

Write-Host
Write-Host "Create Site script has completed. Press OK to quit"
Read-Host