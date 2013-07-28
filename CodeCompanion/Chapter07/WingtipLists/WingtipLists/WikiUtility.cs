using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WingtipLists {
  public class WikiUtility {

    protected const string WikiPageTemplate =         
        @"<table id='layoutsTable' style='width:100%'>
            <tbody>
              <tr style='vertical-align:top'>
                <td style='width:100%'>
                  <div class='ms-rte-layoutszone-outer' style='width:100%'>
                    <div class='ms-rte-layoutszone-inner' style='min-height:420px'>
                      @PageContent
                    </div>
                  </div>
                </td>
              </tr>
            </tbody>
          </table>
          <span id='layoutsData' style='display:none'>false,false,1</span>";

    public static string CreateWikiPageContent(string content) {
      return WikiPageTemplate.Replace("@PageContent", content);
    }

  }
}
