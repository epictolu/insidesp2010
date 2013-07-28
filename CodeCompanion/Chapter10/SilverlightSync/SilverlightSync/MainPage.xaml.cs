using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.SharePoint.Client;

namespace SilverlightSync {
  public partial class MainPage : UserControl {
    public MainPage() {
      InitializeComponent();
    }

    private void SyncCall_Click(object sender, RoutedEventArgs e) {
      using (ClientContext ctx = 
               new ClientContext("http://intranet.wingtip.com")) {
        try {
          Site site = ctx.Site;
          ctx.Load(site);
          ctx.ExecuteQuery();
          Results.Text = site.Url;
        }
        catch (Exception x) {
          Results.Text = x.Message;
        }
      }
    }
  }
}
