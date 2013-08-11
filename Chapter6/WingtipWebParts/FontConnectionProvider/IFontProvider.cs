using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Web.UI.WebControls;

namespace WingtipWebParts.FontConnectionProvider
{
    public interface IFontProvider
    {
        FontUnit FontSize { get; }
        Color FontColor { get; }
    }
}
