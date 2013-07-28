using System;
using System.Data;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.Office.Server;
using Microsoft.Office.Server.Search;
using Microsoft.Office.Server.Search.Query;
using Microsoft.Office.Server.Search.Administration;

namespace CustomSearchParts.EmployeeDirectory
{
    [ToolboxItemAttribute(false)]
    public class EmployeeDirectory : WebPart
    {
        private string query = string.Empty;
        private LinkButton a;
        private LinkButton b;
        private LinkButton c;
        private LinkButton d;
        private LinkButton e;
        private LinkButton f;
        private LinkButton g;
        private LinkButton h;
        private LinkButton i;
        private LinkButton j;
        private LinkButton k;
        private LinkButton l;
        private LinkButton m;
        private LinkButton n;
        private LinkButton o;
        private LinkButton p;
        private LinkButton q;
        private LinkButton r;
        private LinkButton s;
        private LinkButton t;
        private LinkButton u;
        private LinkButton v;
        private LinkButton w;
        private LinkButton x;
        private LinkButton y;
        private LinkButton z;

        private DataGrid names;

        protected override void CreateChildControls()
        {
            a = new LinkButton();
            a.Text = "A";
            a.Click += new EventHandler(a_Click);
            Controls.Add(a);

            b = new LinkButton();
            b.Text = "B";
            b.Click += new EventHandler(b_Click);
            Controls.Add(b);

            c = new LinkButton();
            c.Text = "C";
            c.Click += new EventHandler(c_Click);
            Controls.Add(c);

            d = new LinkButton();
            d.Text = "D";
            d.Click += new EventHandler(d_Click);
            Controls.Add(d);

            e = new LinkButton();
            e.Text = "E";
            e.Click += new EventHandler(e_Click);
            Controls.Add(a);

            f = new LinkButton();
            f.Text = "F";
            f.Click += new EventHandler(f_Click);
            Controls.Add(f);

            g = new LinkButton();
            g.Text = "G";
            g.Click += new EventHandler(g_Click);
            Controls.Add(g);

            h = new LinkButton();
            h.Text = "H";
            h.Click += new EventHandler(h_Click);
            Controls.Add(h);

            i = new LinkButton();
            i.Text = "I";
            i.Click += new EventHandler(i_Click);
            Controls.Add(i);

            j = new LinkButton();
            j.Text = "J";
            j.Click += new EventHandler(j_Click);
            Controls.Add(j);

            k = new LinkButton();
            k.Text = "K";
            k.Click += new EventHandler(k_Click);
            Controls.Add(k);

            l = new LinkButton();
            l.Text = "L";
            l.Click += new EventHandler(l_Click);
            Controls.Add(l);

            m = new LinkButton();
            m.Text = "M";
            m.Click += new EventHandler(m_Click);
            Controls.Add(m);

            n = new LinkButton();
            n.Text = "N";
            n.Click += new EventHandler(n_Click);
            Controls.Add(n);

            o = new LinkButton();
            o.Text = "O";
            o.Click += new EventHandler(o_Click);
            Controls.Add(o);

            p = new LinkButton();
            p.Text = "P";
            p.Click += new EventHandler(p_Click);
            Controls.Add(p);

            q = new LinkButton();
            q.Text = "Q";
            q.Click += new EventHandler(q_Click);
            Controls.Add(q);

            r = new LinkButton();
            r.Text = "R";
            r.Click += new EventHandler(r_Click);
            Controls.Add(r);

            s = new LinkButton();
            s.Text = "S";
            s.Click += new EventHandler(s_Click);
            Controls.Add(s);

            t = new LinkButton();
            t.Text = "T";
            t.Click += new EventHandler(t_Click);
            Controls.Add(t);

            u = new LinkButton();
            u.Text = "U";
            u.Click += new EventHandler(u_Click);
            Controls.Add(u);

            v = new LinkButton();
            v.Text = "V";
            v.Click += new EventHandler(v_Click);
            Controls.Add(v);

            w = new LinkButton();
            w.Text = "W";
            w.Click += new EventHandler(w_Click);
            Controls.Add(w);

            x = new LinkButton();
            x.Text = "X";
            x.Click += new EventHandler(x_Click);
            Controls.Add(x);

            y = new LinkButton();
            y.Text = "Y";
            y.Click += new EventHandler(y_Click);
            Controls.Add(y);

            z = new LinkButton();
            z.Text = "Z";
            z.Click += new EventHandler(z_Click);
            Controls.Add(z);

            names = new DataGrid();
            names.AutoGenerateColumns = false;
            names.BorderWidth = 0;
            names.ShowHeader = false;

            HyperLinkColumn personColumn = new HyperLinkColumn();
            personColumn.HeaderText = "Person";
            personColumn.DataNavigateUrlField = "Path";
            personColumn.DataTextField = "Title";
            names.Columns.Add(personColumn);

            Controls.Add(names);

        }

        void a_Click(object sender, EventArgs e)
        {
            query = "lastname:A*";
        }

        void b_Click(object sender, EventArgs e)
        {
            query = "lastname:B*";
        }

        void c_Click(object sender, EventArgs e)
        {
            query = "lastname:C*";
        }

        void d_Click(object sender, EventArgs e)
        {
            query = "lastname:D*";
        }

        void e_Click(object sender, EventArgs e)
        {
            query = "lastname:E*";
        }

        void f_Click(object sender, EventArgs e)
        {
            query = "lastname:F*";
        }

        void g_Click(object sender, EventArgs e)
        {
            query = "lastname:G*";
        }

        void h_Click(object sender, EventArgs e)
        {
            query = "lastname:H*";
        }

        void i_Click(object sender, EventArgs e)
        {
            query = "lastname:I*";
        }

        void j_Click(object sender, EventArgs e)
        {
            query = "lastname:J*";
        }

        void k_Click(object sender, EventArgs e)
        {
            query = "lastname:K*";
        }

        void l_Click(object sender, EventArgs e)
        {
            query = "lastname:L*";
        }

        void m_Click(object sender, EventArgs e)
        {
            query = "lastname:M*";
        }

        void n_Click(object sender, EventArgs e)
        {
            query = "lastname:N*";
        }

        void o_Click(object sender, EventArgs e)
        {
            query = "lastname:O*";
        }

        void p_Click(object sender, EventArgs e)
        {
            query = "lastname:P*";
        }

        void q_Click(object sender, EventArgs e)
        {
            query = "lastname:Q*";
        }

        void r_Click(object sender, EventArgs e)
        {
            query = "lastname:R*";
        }

        void s_Click(object sender, EventArgs e)
        {
            query = "lastname:S*";
        }

        void t_Click(object sender, EventArgs e)
        {
            query = "lastname:T*";
        }

        void u_Click(object sender, EventArgs e)
        {
            query = "lastname:U*";
        }

        void v_Click(object sender, EventArgs e)
        {
            query = "lastname:V*";
        }

        void w_Click(object sender, EventArgs e)
        {
            query = "lastname:W*";
        }

        void x_Click(object sender, EventArgs e)
        {
            query = "lastname:X*";
        }

        void y_Click(object sender, EventArgs e)
        {
            query = "lastname:Y*";
        }

        void z_Click(object sender, EventArgs e)
        {
            query = "lastname:Z*";
        }

        protected override void RenderContents(System.Web.UI.HtmlTextWriter writer)
        {
            try
            {
                if (query.Length > 0)
                {
                    SearchServiceApplicationProxy proxy = (SearchServiceApplicationProxy)SearchServiceApplicationProxy.GetProxy(SPServiceContext.GetContext(SPContext.Current.Site)); 
                    KeywordQuery keywordQuery = new KeywordQuery(proxy);
                    keywordQuery.ResultsProvider = SearchProvider.Default; 
                    keywordQuery.ResultTypes = ResultType.RelevantResults;
                    keywordQuery.EnableStemming = false;
                    keywordQuery.TrimDuplicates = true;
                    keywordQuery.QueryText = query;

                    ResultTableCollection results = keywordQuery.Execute();
                    ResultTable result = results[ResultType.RelevantResults];

                    DataTable table = new DataTable();
                    table.Load(result, LoadOption.OverwriteChanges);
                    names.DataSource = table;
                    names.DataBind();
                }

                writer.Write("<table border=\"0\" width=\"100%\">");
                writer.Write("<tr><td>");
                a.RenderControl(writer);
                writer.Write("</td><td>");
                b.RenderControl(writer);
                writer.Write("</td><td>");
                c.RenderControl(writer);
                writer.Write("</td><td>");
                d.RenderControl(writer);
                writer.Write("</td><td>");
                e.RenderControl(writer);
                writer.Write("</td><td>");
                f.RenderControl(writer);
                writer.Write("</td><td>");
                g.RenderControl(writer);
                writer.Write("</td><td>");
                h.RenderControl(writer);
                writer.Write("</td><td>");
                i.RenderControl(writer);
                writer.Write("</td><td>");
                j.RenderControl(writer);
                writer.Write("</td><td>");
                k.RenderControl(writer);
                writer.Write("</td><td>");
                l.RenderControl(writer);
                writer.Write("</td><td>");
                m.RenderControl(writer);
                writer.Write("</td><td>");
                n.RenderControl(writer);
                writer.Write("</td><td>");
                o.RenderControl(writer);
                writer.Write("</td><td>");
                p.RenderControl(writer);
                writer.Write("</td><td>");
                q.RenderControl(writer);
                writer.Write("</td><td>");
                r.RenderControl(writer);
                writer.Write("</td><td>");
                s.RenderControl(writer);
                writer.Write("</td><td>");
                t.RenderControl(writer);
                writer.Write("</td><td>");
                u.RenderControl(writer);
                writer.Write("</td><td>");
                v.RenderControl(writer);
                writer.Write("</td><td>");
                w.RenderControl(writer);
                writer.Write("</td><td>");
                x.RenderControl(writer);
                writer.Write("</td><td>");
                y.RenderControl(writer);
                writer.Write("</td><td>");
                z.RenderControl(writer);
                writer.Write("</td></tr>");
                writer.Write("<tr><td colspan=\"26\">");
                names.RenderControl(writer);
                writer.Write("</td></tr>");
                writer.Write("</table>");

            }
            catch (Exception x)
            {
                writer.Write("<p>" + x.Message + "</p>");
            }
        }
    }
}
