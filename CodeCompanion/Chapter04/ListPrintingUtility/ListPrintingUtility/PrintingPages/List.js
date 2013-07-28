var list;
var printView;

function initPage() {
  $get("listTitle").innerHTML = 'getting list data...'; 
  ExecuteOrDelayUntilScriptLoaded(getListData, "sp.js");  
}

function getListData() {
  // get QueryString parameters
  var queryString = window.location.search.substring(1);
  var args = queryString.split('&');
  var listId = args[0].split('=')[1];
  var viewId = args[1].split('=')[1];
  // use client object model to get list items
  var context = new SP.ClientContext.get_current();
  var site = context.get_web();
  context.load(site);
  var lists = site.get_lists();
  context.load(lists);
  list = lists.getById(listId);
  context.load(list);
  var views = list.get_views();
  context.load(views);
  var view = views.getById(viewId);
  context.load(view);
  printView = view.renderAsHtml();
  context.executeQueryAsync(success, failure);
}

function success() {
  $get("listTitle").innerHTML = list.get_title();
  $get("listTable").innerHTML = printView.get_value();
}

function failure() {
  $get("listTitle").innerHTML = "error running Client OM query";
  $get("listTable").innerHTML = "";
}