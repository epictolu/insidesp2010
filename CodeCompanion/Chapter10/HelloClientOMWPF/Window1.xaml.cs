using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;

//key libraries
//Contained in Microsoft.SharePoint.Client.dll
//and Microsoft.SharePoint.Client.Runtime.dll
using Microsoft.SharePoint.Client;

namespace HelloClientOM
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void getTitle_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                messages.Text = string.Empty;
                siteTitle.Text = string.Empty;

                //Get a context
                using (ClientContext clientContext = new ClientContext(siteUrl.Text))
                {

                    //Get the site
                    Web site = clientContext.Web;

                    //Fill the Title Property
                    site.Retrieve("Title");
                    clientContext.ExecuteQuery();

                    //read Title now
                    siteTitle.Text = site.Title;
                }
            }
            catch (ClientRequestException x)
            {
                messages.Text += "\r\n" + x.Message;
            }
            catch (PropertyOrFieldNotInitializedException x)
            {
                messages.Text += "\r\n" + x.Message;
            }
            catch (ServerUnauthorizedAccessException x)
            {
                messages.Text += "\r\n" + x.Message;
            }
            catch (ServerException x)
            {
                messages.Text += "\r\n" + x.Message;
            }
            catch (Exception x)
            {
                messages.Text += "\r\n" + x.Message;
            }
        }

        private void getLists_Click(object sender, RoutedEventArgs e)
        {

            messages.Text = string.Empty;
            siteLists.Items.Clear();

            //Get a context
            using (ClientContext clientContext = new ClientContext(siteUrl.Text))
            {

                //Get the site
                Web site = clientContext.Web;
                clientContext.Load(site);

                //Get Lists
                clientContext.Load(site.Lists);

                //Include titles
                clientContext.Load(clientContext.Web,
                    x => x.Lists.Include(l => l.Title).Where(l => l.Title != null));

                //Query
                clientContext.ExecuteQuery();

                //Fill List
                foreach (Microsoft.SharePoint.Client.List list in site.Lists)
                {
                    siteLists.Items.Add(list.Title);
                }

            }

        }

        private void updateList_Click(object sender, RoutedEventArgs e)
        {
            messages.Text = string.Empty;

            using (ClientContext clientContext = new ClientContext(siteUrl.Text))
            {
                //Set credentials
                //clientContext.AuthenticationMode = Microsoft.SharePoint.Client.ClientAuthenticationMode.FormsAuthentication;
                //ClientOM.FormsAuthenticationLoginInfo loginInfo = new Microsoft.SharePoint.Client.FormsAuthenticationLoginInfo("WORKSTATION\\bob", "pass@word1");
                //clientContext.FormsAuthenticationLoginInfo = loginInfo;

                //Set up error handling
                ExceptionHandlingScope xScope = new ExceptionHandlingScope(clientContext);

                using (xScope.StartScope())
                {

                    using (xScope.StartTry())
                    {

                        //Try to update a list that does not exist
                        messages.Text += "Trying to update description, but can't!\r\n";
                        Microsoft.SharePoint.Client.List myList = clientContext.Web.Lists.GetByTitle(listTitle.Text);
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
                        myList.Description += "Then updated in the finally block.";
                        myList.Update();
                    }
                }

                //Execute the entire try-catch as a batch!
                clientContext.ExecuteQuery();
                messages.Text += "Done";
            }
        }
    }
}

