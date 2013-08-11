using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace WingtipWebParts.CustomProperties3
{
    [ToolboxItemAttribute(false)]
    public class CustomProperties3 : WebPart
    {
        [Personalizable(PersonalizationScope.Shared),
         WebBrowsable(false)]
        public string UserGreeting { get; set; }

        [Personalizable(PersonalizationScope.Shared),
         WebBrowsable(false)]
        public int FontSize { get; set; }

        [Personalizable(PersonalizationScope.Shared),
         WebBrowsable(false)]
        public string FontColor { get; set; }

        public override EditorPartCollection CreateEditorParts()
        {
            var editorPart = new CustomEditorPart();
            editorPart.ID = "CustomEditorPartUniqueID";

            EditorPart[] editorParts = { editorPart };
            return new EditorPartCollection(editorParts);
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            writer.Write("Check out my web part properties");
        }
    }

    class CustomEditorPart : EditorPart
    {
        TextBox userGreeting;
        RadioButtonList fontSizes;
        RadioButtonList fontColors;
        
        protected override void CreateChildControls()
        {
            Title = "Custom Properties 3";

            var userGreetingInstruction = new Label { Text = "Type a user greeting:" };
            userGreeting = new TextBox();
            var fontSizeInstruction = new Label { Text = "Select font point size:" };

            int[] fontSizeOptions = { 14, 18, 24, 32 };
            fontSizes = new RadioButtonList { DataSource = fontSizeOptions };
            fontSizes.DataBind();

            var fontColorInstruction = new Label { Text = "Select font color:" };

            string[] fontColorOptions = { "Black", "Green", "Blue", "Purple" };
            fontColors = new RadioButtonList { DataSource = fontColorOptions };
            fontColors.DataBind();

            Controls.Add(userGreetingInstruction);
            Controls.Add(userGreeting);
            Controls.Add(fontSizeInstruction);
            Controls.Add(fontSizes);
            Controls.Add(fontColorInstruction);
            Controls.Add(fontColors);
        }

        public override void SyncChanges()
        {
            EnsureChildControls();

            var webPart = WebPartToEdit as CustomProperties3;
            userGreeting.Text = webPart.UserGreeting;
            fontSizes.Items.FindByText(webPart.FontSize.ToString()).Selected = true;
            fontColors.Items.FindByText(webPart.FontColor.ToString()).Selected = true;
        }

        public override bool ApplyChanges()
        {
            EnsureChildControls();

            var webPart = WebPartToEdit as CustomProperties3;

            webPart.UserGreeting = userGreeting.Text;
            webPart.FontColor = fontColors.Text;
            webPart.FontSize = Convert.ToInt32(fontSizes.Text);

            return true;
        }
    }
}
