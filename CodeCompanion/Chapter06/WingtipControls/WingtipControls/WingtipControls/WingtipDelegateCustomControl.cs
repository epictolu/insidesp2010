using System;
using System.Web;
using System.Web.UI;

namespace WingtipControls {
  class WingtipDelegateCustomControl : Control {

    protected override void OnLoad(EventArgs e) {

      // register startup script using JavaScript code in string literal
      ClientScriptManager CSM = this.Page.ClientScript;
      string key = "WingtipScript";
      string script = @"
        ExecuteOrDelayUntilScriptLoaded(SayHello, 'sp.js');             
        function SayHello() {
          var message = 'Hello from a delegate custom control';
          SP.UI.Notify.addNotification(message);
        }";

      // register startup script using JavaScript inside WingtipDelegateCustomControl.js
      CSM.RegisterStartupScript(this.GetType(), key, script, true);
      string key2 = "WingtipScript2";
      string script2 = Properties.Resources.WingtipDelegateCustomControl_js;
      CSM.RegisterStartupScript(this.GetType(), key2, script2, true);

    }
  }
}
