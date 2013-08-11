using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace WingtipWebParts.WebPartVerbsFontDemo
{
    [ToolboxItemAttribute(false)]
    public class WebPartVerbsFontDemo : WebPart
    {
        [Personalizable, 
         WebBrowsable, 
         WebDescription("Select a font size"), 
         WebDisplayName("Font Size")]
        public int FontSize { get; set; }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            writer.WriteLine("Check out the web part verbs on this web part");
            writer.WriteBreak();
            writer.WriteBreak();
            writer.WriteLine(string.Format("Font Size: {0}", FontSize));
            writer.WriteBreak();
            writer.WriteLine("Note that you have to reload the page for this to get updated. Fix this if you can.");
            writer.WriteBreak();
        }

        public override WebPartVerbCollection Verbs
        {
            get
            {
                var increaseFontSize = new WebPartVerb("IncreaseFontSize", IncreaseFontSize)
                    {
                        Text = "Increase Font Size",
                        Description = "Increases the font size",
                    };

                var decreaseFontSize = new WebPartVerb("DecreaseFontSize", DecreaseFontSize)
                    {
                        Text = "Decrease Font Size",
                        Description = "Decreases the font size"
                    };

                var makeFontBlue = new WebPartVerb("MakeFontBlue", MakeFontBlue)
                    {
                        Text = "Make Font Blue",
                        Description = "Makes the font blue"
                    };

                var makeFontRed = new WebPartVerb("MakeFontRed", MakeFontRed)
                    {
                        Text = "Make Font Red",
                        Description = "Makes the font Red"
                    };

                var makeFontGreen = new WebPartVerb("MakeFontGreen", MakeFontGreen)
                    {
                        Text = "Make Font Green",
                        Description = "Makes the font Green"
                    };

                var verbs = new WebPartVerb[] { increaseFontSize, decreaseFontSize, makeFontBlue, makeFontGreen, makeFontRed };
                return new WebPartVerbCollection(verbs);
            }
        }

        public void IncreaseFontSize(object sender, EventArgs e)
        {
            var site = SPContext.Current.Web;
            var webPartManager = site.GetFile(Context.Request.Url.AbsolutePath).GetLimitedWebPartManager(PersonalizationScope.Shared);
            var webPart = webPartManager.WebParts[ID] as WebPartVerbsFontDemo;

            webPart.FontSize = FontSize + 1;
            webPartManager.SaveChanges(webPart);

            // How do you refresh the contents of the web part on the page??
        }

        public void DecreaseFontSize(object sender, EventArgs e)
        {
            var site = SPContext.Current.Web;
            var webPartManager = site.GetFile(Context.Request.Url.AbsolutePath).GetLimitedWebPartManager(PersonalizationScope.Shared);
            var webPart = webPartManager.WebParts[ID] as WebPartVerbsFontDemo;

            webPart.FontSize = FontSize - 1;
            webPartManager.SaveChanges(webPart);

            // Once again ... how do refresh the contents of the web part on the page??
        }

        public void MakeFontBlue(object sender, EventArgs e)
        { 
        }

        public void MakeFontRed(object sender, EventArgs e)
        { 
        }

        public void MakeFontGreen(object sender, EventArgs e)
        { 
        }
    }
}
