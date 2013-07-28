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

namespace SilverlightExceptionHandling
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

        private void goButton_Click(object sender, RoutedEventArgs e)
        {
            messages.Text = string.Empty;

            using (ClientContext clientContext = new ClientContext("http://wingtipserver/clientom"))
            {

                //Set up error handling
                ExceptionHandlingScope xScope = new ExceptionHandlingScope(clientContext);

                using (xScope.StartScope())
                {

                    using (xScope.StartTry())
                    {

                        //Try to update a list that does not exist
                        Microsoft.SharePoint.Client.List myList = clientContext.Web.Lists.GetByTitle("My List");
                        clientContext.Load(myList);
                        myList.Description = "A description for a non-existant list! ";
                        myList.Update();

                    }
                    using (xScope.StartCatch())
                    {
                        //Create a new list
                        ListCreationInformation listCI = new ListCreationInformation();
                        listCI.Title = "My List";
                        listCI.Description += "First created in the catch block. ";
                        listCI.TemplateType = (int)ListTemplateType.GenericList;
                        listCI.QuickLaunchOption = Microsoft.SharePoint.Client.QuickLaunchOptions.On;

                        Microsoft.SharePoint.Client.List list = clientContext.Web.Lists.Add(listCI);
                    }
                    using (xScope.StartFinally())
                    {
                        //Try to update the list now
                        Microsoft.SharePoint.Client.List myList = clientContext.Web.Lists.GetByTitle("My List");
                        clientContext.Load(myList);
                        myList.Description += "Then updated in the finally block.";
                        myList.Update();
                    }
                }

                //Execute the entire try-catch as a batch!
                clientContext.ExecuteQueryAsync(succeedListener, failListener);
                messages.Text += "Done";
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
            messages.Text = "Success";
        }

        public void OperationFailed(object sender, EventArgs e)
        {
            messages.Text += "Failure!";

        }
    }
}
