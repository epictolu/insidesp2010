using Microsoft.SharePoint.WebControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace WingtipFieldTypes
{
    public class EmployeeStatusFieldControl : BaseFieldControl
    {
        protected RadioButtonList lstEmployeeStatus;

        protected override string DefaultTemplateName
        {
            get
            {
                return "EmployeeStatusRenderingTemplate";
            }
        }

        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            lstEmployeeStatus = TemplateContainer.FindControl("lstEmployeeStatus") as RadioButtonList;

            lstEmployeeStatus.Items.Clear();
            lstEmployeeStatus.Items.Add("Full-Time Employee");
            lstEmployeeStatus.Items.Add("Part-Time Employee");

            if ((bool) Field.GetCustomProperty("AllowContractors"))
                lstEmployeeStatus.Items.Add("Contractor");
            if ((bool) Field.GetCustomProperty("AllowInterns"))
                lstEmployeeStatus.Items.Add("Intern");
        }
    }
}
