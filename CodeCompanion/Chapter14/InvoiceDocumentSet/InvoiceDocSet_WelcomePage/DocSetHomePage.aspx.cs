using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.WebPartPages;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;

namespace InvoiceDocumentSet.WelcomePage
{
    public class DocSetHomePage : WebPartPage
    {
        protected Label DiscountMessage;

        protected override void OnLoad(EventArgs e)
        {
            // get reference to the current document set selected
            SPListItem docSet = SPContext.Current.ListItem;

            // if the items is paid, state it and stop
            if (docSet["InvoicePaidStatus"].ToString() == "UNPAID")
            {

                string paymentTerms = docSet["InvoicePaymentTerms"].ToString();
                int invoiceAmount = Int32.Parse(docSet["InvoiceAmount"].ToString());
                DateTime invoiceDate = DateTime.Parse(docSet["InvoiceDate"].ToString());

                // based on the terms of the payment, output different items
                switch (paymentTerms)
                {
                    case "NET 30":
                        DiscountMessage.Text = string.Format("Invoice past due in {0} days!",
                                                                invoiceDate.AddDays(30).Subtract(invoiceDate).Days);
                        break;
                    case "2% 10 NET 30":
                        // if the today < invoice date
                        if (DateTime.Today <= invoiceDate.AddDays(10))
                            DiscountMessage.Text = string.Format("Save 2% (${0}) if paid within {1} days!",
                                                                  invoiceAmount * .02,
                                                                  invoiceDate.AddDays(10).Subtract(DateTime.Today).Days);
                        // else if invoide date != due date, then it has a set due date so warn when it's due
                        else if (DateTime.Today <= invoiceDate.AddDays(30))
                            DiscountMessage.Text = string.Format("Invoice past due in {0} days!",
                                                                    invoiceDate.AddDays(30).Subtract(DateTime.Today).Days);
                        // else it must be past due
                        else
                            DiscountMessage.Text = "INVOICE PAST DUE!!!!";
                        break;
                    default:
                        DiscountMessage.Text = "";
                        break;
                }

            }
        }

    }
}