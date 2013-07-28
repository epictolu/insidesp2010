using System;
using System.Data;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.Office.Server;
using Microsoft.Office.Server.Search;
using Microsoft.Office.Server.Search.Query;
using Microsoft.Office.Server.Search.Administration;

namespace CustomSearchParts.RecentDocs
{
    [ToolboxItemAttribute(false)]
    public class RecentDocs : WebPart
    {
        DataGrid docGrid;
        Label message;

        const string queryString = "SELECT url, title, author " +
                               "FROM Scope() " +
                               "WHERE \"scope\" = 'All Sites' " +
                               "AND isDocument=1 " +
                               "AND write >DATEADD(Day,-7,GetGMTDate())";

        protected override void CreateChildControls()
        {
            docGrid = new DataGrid();
            docGrid.AutoGenerateColumns = false;
            docGrid.BorderWidth = 0;

            HyperLinkColumn title = new HyperLinkColumn();
            title.HeaderText = "Document Title";
            title.DataNavigateUrlField = "URL";
            title.DataTextField = "Title";
            docGrid.Columns.Add(title);

            BoundColumn author = new BoundColumn();
            author.HeaderText = "Author";
            author.DataField = "Author";
            docGrid.Columns.Add(author);

            Controls.Add(docGrid);

            message = new Label();
            Controls.Add(message);

        }

        protected override void RenderContents(System.Web.UI.HtmlTextWriter writer)
        {
            try
            {
                SearchServiceApplicationProxy proxy = (SearchServiceApplicationProxy)SearchServiceApplicationProxy.GetProxy(SPServiceContext.GetContext(SPContext.Current.Site)); 
                FullTextSqlQuery queryObject = new FullTextSqlQuery(proxy);
                queryObject.ResultsProvider = SearchProvider.Default;
                queryObject.ResultTypes = ResultType.RelevantResults;
                queryObject.EnableStemming = true;
                queryObject.TrimDuplicates = true;
                queryObject.QueryText = queryString;

                ResultTableCollection results = queryObject.Execute();
                ResultTable result = results[ResultType.RelevantResults];

                if (result.RowCount > 0)
                {
                    DataTable table = new DataTable();
                    table.Load(result, LoadOption.OverwriteChanges);
                    docGrid.DataSource = table;
                    docGrid.DataBind();
                }

                queryObject.Dispose();
            }
            catch (Exception x)
            {
                message.Text = x.Message;
            }
        }
    }
}
