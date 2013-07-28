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

//Key library
//Set References to 
//Microsoft.SharePoint.Client.Silverlight.dll
//Microsoft.SharePoint.Client.Silverlight.Runtime.dll
using Microsoft.SharePoint.Client;


namespace SilverlightClientOM
{
    public partial class MainPage : UserControl
    {
        private System.Threading.SynchronizationContext thread;

        //Event handlers
        public static event ClientRequestSucceededEventHandler succeedListener;
        public static event ClientRequestFailedEventHandler failListener;

        //Delegates
        public delegate void succeedDelegate(object sender, ClientRequestSucceededEventArgs e);
        public delegate void failDelegate(object sender, ClientRequestFailedEventArgs e);

        public MainPage()
        {
            InitializeComponent();

            //Create new delegates
            succeedDelegate sd = new succeedDelegate(HandleClientRequestSucceeded);
            failDelegate fd = new failDelegate(HandleClientRequestFailed);

            //Create new event handlers
            succeedListener += new ClientRequestSucceededEventHandler(sd);
            failListener += new ClientRequestFailedEventHandler(fd);

            thread = System.Threading.SynchronizationContext.Current;
            if (thread == null)
                thread = new System.Threading.SynchronizationContext();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Messages.Text = string.Empty;
            try
            {
                ClientContext clientContext = new ClientContext(SiteUrl.Text);

                ListCreationInformation listCI = new Microsoft.SharePoint.Client.ListCreationInformation();
                listCI.Title = ListName.Text;
                listCI.Description = "Created by the Silverlight Client OM";
                listCI.TemplateType = (int)ListTemplateType.Announcements;
                listCI.QuickLaunchOption = Microsoft.SharePoint.Client.QuickLaunchOptions.On;

                List list = clientContext.Web.Lists.Add(listCI);

                ListItemCreationInformation listItemCI = new Microsoft.SharePoint.Client.ListItemCreationInformation();
                ListItem item = list.AddItem(listItemCI);

                item["Title"] = "New List";
                item["Body"] = "Your new list has been created";
                item.Update();

                clientContext.ExecuteQueryAsync(succeedListener, failListener);
            }
            catch (Exception x)
            {
                Messages.Text = x.Message;
            }
        }

        //Delegate definitions
        public void HandleClientRequestSucceeded(object sender, ClientRequestSucceededEventArgs args)
        {
            thread.Post(new System.Threading.SendOrPostCallback(delegate(object state)
            {
                EventHandler h = OperationSucceeded;
                if (h != null)
                    h(this, EventArgs.Empty);
            }), null);
        }

        public void HandleClientRequestFailed(object sender, ClientRequestFailedEventArgs args)
        {
            thread.Post(new System.Threading.SendOrPostCallback(delegate(object state)
            {
                EventHandler h = OperationFailed;
                if (h != null)
                    h(this, EventArgs.Empty);
            }), null);
        }

        //Event handlers
        public void OperationSucceeded(object sender, EventArgs e)
        {
            Messages.Text = "Success";
        }

        public void OperationFailed(object sender, EventArgs e)
        {
            Messages.Text += "Failure!";
            
        }
    }
}
