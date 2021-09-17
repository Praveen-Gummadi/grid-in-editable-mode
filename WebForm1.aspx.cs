using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private void Fill()
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
            SqlDataAdapter da = new SqlDataAdapter("select * from emp", con);
            DataSet ds = new DataSet();
            da.Fill(ds,"emp");
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                Fill();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if(Button1.Text=="save")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
                con.Open();
                SqlCommand cmd = new SqlCommand("insert into emp values('"+TextBox1.Text+ "','" + TextBox2.Text + "','" + TextBox3.Text + "')", con);
                cmd.ExecuteNonQuery();
                con.Close();
                Fill();
            }
            else if(Button1.Text=="update")
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
                con.Open();
                SqlCommand cmd = new SqlCommand("update emp set ename='"+TextBox2.Text+"',salary='"+TextBox3.Text+"' where eno='"+TextBox1.Text+"'", con);
                cmd.ExecuteNonQuery();
                con.Close();
                Fill();
                TextBox1.Text = "";
                TextBox2.Text = "";
                TextBox3.Text = "";
                Button1.Text = "save";
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmddelete")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];
                Label l1 = (Label)row.FindControl("Label1");
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
                con.Open();
                SqlCommand cmd = new SqlCommand("delete from emp where eno='" + l1.Text + "'", con);
                cmd.ExecuteNonQuery();
                con.Close();
                Fill();
            }
            else if (e.CommandName == "cmdedit")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridView1.Rows[index];
                Label l1 = (Label)row.FindControl("Label1");
                Label l2 = (Label)row.FindControl("Label2");
                Label l3 = (Label)row.FindControl("Label3");
                TextBox1.Text = l1.Text;
                TextBox2.Text = l2.Text;
                TextBox3.Text = l3.Text;
                Button1.Text = "update";
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}