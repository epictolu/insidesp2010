Add-PSSnapin Microsoft.SharePoint.PowerShell -ErrorAction "SilentlyContinue"

# add variables to track parametrized values used to provision new site
$title = "Wingtip Sales Site"
$url = "http://intranet.wingtip.com/sites/sales"
$owner = "WINGTIP\Administrator"
$template = "STS#1"
$WikiHomePageFeatureId = "00BFEA71-D8FE-4FEC-8DAD-01C19A6E4053"

# create new site collection with Blank site as top-level site
$sc = New-SPSite -URL $url -Name $title -OwnerAlias $owner -Template $template

# obtain reference to top-level site
$site = $sc.RootWeb

# activate any required features
Enable-SPFeature -Identity $WikiHomePageFeatureId -Url $site.Url

# use the server-side object model to initialize site properties.
$site = $sc.RootWeb
$site.Title = "My Custom Title"
$site.Update()