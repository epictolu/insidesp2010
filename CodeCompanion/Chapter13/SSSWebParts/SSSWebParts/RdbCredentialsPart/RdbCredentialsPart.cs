using System;
using System.Security;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.BusinessData;
using Microsoft.BusinessData.Infrastructure;
using Microsoft.BusinessData.Infrastructure.SecureStore;
using Microsoft.Office.SecureStoreService;
using Microsoft.Office.SecureStoreService.Server;

namespace SSSWebParts.RdbCredentialsPart
{
    [ToolboxItemAttribute(false)]
    public class RdbCredentialsPart : WebPart
    {
        Label messages;

        private string appId;
        private string dbName;
        private string serverName;

        [Personalizable(PersonalizationScope.Shared),
         WebBrowsable(true),
         WebDisplayName("ApplicationId"),
         WebDescription("The ID of the Target Application in the Secure Store Service"),
         Category("Secure Store Service")]
        public string ApplicationId
        {
            get { return appId; }
            set { appId = value; }
        }

        [Personalizable(PersonalizationScope.Shared), 
         WebBrowsable(true),
         WebDisplayName("Database Name"),
         WebDescription("Name of the database to query."),
         Category("Secure Store Service")]
        public string DatabaseName
        {
            get { return dbName; }
            set { dbName = value; }
        }

        [Personalizable(PersonalizationScope.Shared),
         WebBrowsable(true),
         WebDisplayName("Server Name"),
         WebDescription("Name of the database server."),
         Category("Secure Store Service")]
        public string ServerName
        {
            get { return serverName; }
            set { serverName = value; }
        }

        protected override void CreateChildControls()
        {

            messages = new Label();
            this.Controls.Add(messages);
        }

        protected override void OnPreRender(EventArgs e)
        {
            string username = string.Empty;
            string password = string.Empty;

            try
            {

                ISecureStoreProvider p = SecureStoreProviderFactory.Create();

                using (SecureStoreCredentialCollection creds = p.GetCredentials(ApplicationId))
                {
                    foreach (SecureStoreCredential c in creds)
                    {
                        switch (c.CredentialType)
                        {
                            case SecureStoreCredentialType.UserName:
                                username = ConvertToString(c.Credential);
                                break;

                            case SecureStoreCredentialType.Password:
                                password = ConvertToString(c.Credential);
                                break;

                            case SecureStoreCredentialType.WindowsUserName:
                                username = ConvertToString(c.Credential);
                                break;

                            case SecureStoreCredentialType.WindowsPassword:
                                password = ConvertToString(c.Credential);
                                break;
                        }
                    }
                }


                SqlConnectionStringBuilder cBuilder = new SqlConnectionStringBuilder();
                cBuilder.DataSource = ServerName;
                cBuilder.InitialCatalog = DatabaseName;
                cBuilder.UserID = username;
                cBuilder.Password = password;

                messages.Text = cBuilder.ConnectionString;

            }
            catch (Exception x)
            {
                messages.Text = x.Message;
            }
        }

        private String ConvertToString(SecureString s)
        {
            IntPtr b = Marshal.SecureStringToBSTR(s);
            try { return Marshal.PtrToStringBSTR(b); }
            finally { Marshal.FreeBSTR(b); }
        }
    }
}
