$siteUrl = "http://epicserver/sites/insidesp2010"
$firstListName = "Lookupee"
$secondListName = "Lookup List Values"

$site = get-spweb $siteUrl
$firstList = $site.Lists[$firstListName]
$firstListId = $firstList.Id.ToString()

$secondList = $site.Lists[$secondListName]
$secondListId = $secondList.Id.ToString()

$query = new-object Microsoft.SharePoint.SPSiteDataQuery
$query.Query = "<Where><Eq><FieldRef Name='Title'/><Value Type='Text'>Item A</Value></Eq></Where>"
$query.Lists = "<Lists><List ID='$firstListId'/><List ID='$secondListId'/></Lists>"
$query.ViewFields = "<FieldRef Name='Title'/><FieldRef Name='Comments'/><FieldRef Name='Lookup_x0020_Value'/>"
$query.Webs = "<Webs Scope='SiteCollection'/>"

$items = $site.GetSiteData($query)
$items.Count
