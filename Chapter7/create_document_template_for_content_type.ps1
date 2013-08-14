$siteUrl = "http://epicserver/sites/insidesp2010"
$listName = "Wingtips Document Library"
$contentTypeName = "Wingtips Content Type"
$documentTemplatePath = "c:\development\inside sharepoint 2010\chapter7\document template.docx"

function initialize {
    $site = get-spweb $siteUrl

    . cleanup_list
    . cleanup_content_type
}

function cleanup_content_type {
    $spContentType = $site.ContentTypes[$contentTypeName]
    if ($spContentType -ne $null) {
        $spContentType.Delete()
    }
    write-host "Removed $contentTypeName from site content types"
}

function cleanup_list {
    $spList = $site.Lists[$listName]
    if ($spList -ne $null) {
        $spList.Delete()
    }
    write-host "Removed $listName from site lists"
}

function add_content_type {
    $contentType = new-object Microsoft.SharePoint.SPContentType $site.ContentTypes["Document"], $site.ContentTypes, $contentTypeName
    $contentType = $site.ContentTypes.Add($contentType)
    write-host "Added $contentTypeName to site content types"
}

function add_document_template_to_content_type {
    $file = get-item $documentTemplatePath
    $fileStream = $file.OpenRead()
    $folder = $site.GetFolder("_cts/$contentTypeName")
    $folder.Files.Add("DocumentTemplate.docx", $fileStream) | Out-Null
    $fileStream.Close()
    write-host "Added 'Document Template.docx' to content type document templates"
}

function associate_document_template_with_content_type {
    $contentType.DocumentTemplate = "DocumentTemplate.docx"
    $contentType.Update($true)
    write-host "Set document template for $contentTypeName"
}

function associate_list_with_content_type {
    $listId = $site.Lists.Add($listName, "List Description", "DocumentLibrary")
    $list = $site.Lists[$listId]
    $list.ContentTypes.Add($contentType) | Out-Null
    $list.OnQuickLaunch = $true
    $list.ContentTypesEnabled = $true
    $list.Update()
    write-host "Associated $contentTypeName with $listName"
}

. initialize
. add_content_type
. add_document_template_to_content_type
. associate_document_template_with_content_type
. associate_list_with_content_type
