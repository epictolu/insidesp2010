$siteUrl = "http://epicserver/sites/insidesp2010"

$query = new-object Microsoft.SharePoint.SPQuery
$query.ViewXml = "
  <View>
    <Query>
      <Where>
        <Eq>
          <FieldRef Name='Comments'/>
          <Value Type='Text'>Comment A</Value>
        </Eq>
      </Where>
    </Query>
    <Joins>
      <Join Type='Inner' ListAlias='lookupValues'>
        <Eq>
          <FieldRef Name='Lookup_x0020_Value' RefType='Id'/>
          <FieldRef Name='Id' List='lookupValues'/>
        </Eq>
      </Join>
    </Joins>
    <ProjectedFields>
      <Field Name='Comments' Type='Lookup' List='lookupValues' ShowField='Comments'/>
    </ProjectedFields>
    <ViewFields>
      <FieldRef Name='Comments' />
    </ViewFields>
  </View>"

$site = get-spweb $siteUrl
$list = $site.Lists["Lookupee"]
$global:items = $list.GetItems($query)

write-host $items.Count
