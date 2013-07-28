$webAppUrl = "http://epicserver"
$featureId = new-object System.Guid "4BE86715-DD82-4148-86C3-1D6740801117"

$webApp = [Microsoft.SharePoint.Administration.SPWebApplication]::Lookup($webAppUrl)
$features = $webApp.QueryFeatures($featureId, $true)

foreach ($feature in $features) {
    $feature.Upgrade($true)
}
