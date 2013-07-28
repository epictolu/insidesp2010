Write-Host "Enable property for ranking"
$p = Get-SPEnterpriseSearchServiceApplication|Get-SPEnterpriseSearchMetadataManagedProperty -Identity "AverageRating"
$p.EnabledForQueryIndependentRank = $true
$p.Update()

Write-Host "Adding Ranking Models ratings"
Get-SPEnterpriseSearchServiceApplication|New-SPEnterpriseSearchRankingModel -RankingModelXML "<rankingModel name='SortByRatingIncreasing' id='c978ef2b-300a-444b-af9a-d51261294587' description = 'ranking model that sorts items in increasing order of rating' xmlns='http://schemas.microsoft.com/office/2009/rankingModel'><queryDependentFeatures><queryDependentFeature name='Body' pid='1' weight='0' lengthNormalization='10.0000000000'/></queryDependentFeatures><queryIndependentFeatures><categoryFeature name='AverageRating' pid='403' default='0'><category name='Zero' value='0' weight='1000000'/><category name='One' value='1' weight='100000'/><category name='Two' value='2' weight='10000'/><category name='Three' value='3' weight='1000'/><category name='Four' value='4' weight='10'/><category name='Five' value='5' weight='1'/></categoryFeature></queryIndependentFeatures></rankingModel>"
Get-SPEnterpriseSearchServiceApplication|New-SPEnterpriseSearchRankingModel -RankingModelXML "<rankingModel name='SortByRatingDecreasing' id='0D4CB5B6-2FA3-4D7F-AF79-EF0DE64F242C' description = 'ranking model that sorts items in decreasing order of rating' xmlns='http://schemas.microsoft.com/office/2009/rankingModel'><queryDependentFeatures><queryDependentFeature name='Body' pid='1' weight='0' lengthNormalization='10.0000000000'/></queryDependentFeatures><queryIndependentFeatures><categoryFeature name='AverageRating' pid='403' default='0'><category name='Five' value='5' weight='1000000'/><category name='Four' value='4' weight='100000'/><category name='Three' value='3' weight='10000'/><category name='Two' value='2' weight='1000'/><category name='One' value='1' weight='10'/><category name='Zero' value='0' weight='1'/></categoryFeature></queryIndependentFeatures></rankingModel>"

Write-Host "List ranking models"
Get-SPEnterpriseSearchServiceApplication | Get-SPEnterpriseSearchRankingModel
