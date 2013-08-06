using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace WingtipControls.WingtipControls
{
    public class WingtipDelegateCustomControl : Control
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var clientScriptManager = Page.ClientScript;
            var key = "WingtipScript";
            var script = @"
                            ExecuteOrDelayUntilScriptLoaded(SayHello, 'sp.js');
                            function SayHello() {
                            var message = 'Hello from a delegate custom control';
                            SP.UI.Notify.addNotification(message);
                            }";

            clientScriptManager.RegisterStartupScript(GetType(), key, script, true);

            var key2 = "WingtipScript2";
            var script2 = Properties.Resources.WingtipDelegateCustomControl_js;
            clientScriptManager.RegisterStartupScript(GetType(), key2, script2, true);
        }
    }
}
