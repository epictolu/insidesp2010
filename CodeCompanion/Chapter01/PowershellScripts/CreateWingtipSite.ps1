Add-PSSnapin Microsoft.SharePoint.PowerShell -ErrorAction "SilentlyContinue"

$title= "Wingtip Dev Site"
$url = "http://intranet.wingtip.com:1001"
$owner = "WINGTIP\Administrator"
$template = "STS#1"

# delete target site collection if it exists
$targetSite = Get-SPSite | Where-Object {$_.Url -eq $url}
if ($targetSite -ne $null) {
    Remove-SPSite -Identity $targetSite -Confirm:$false
}

$sc = New-SPSite -URL $url -Name $title -OwnerAlias $owner -Template $template
$site = $sc.RootWeb
$site.Title = "My New Site Title"
$site.Update()