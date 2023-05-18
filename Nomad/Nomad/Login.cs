using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nomad
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-BAUB2U6\SQLEXPRESS;Initial Catalog=Nomad;Integrated Security=True");

        private void label8_Click(object sender, EventArgs e)
        {

        }
        public static string User;
        private void EditBtn_Click(object sender, EventArgs e)
        {
            Con.Open();
            string Query = "select count(*) from UserTb1 where UName='" +UnameTb.Text+ "' and UPassword='" + PasswordTb.Text + "'";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            DataTable dt = new DataTable(); 
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                User = UnameTb.Text;
                Bookings obj = new Bookings();
                obj.Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show("Wrong UserName Or Password");
                UnameTb.Text = "";
                PasswordTb.Text = "";

            }
            Con.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            AdminLogin obj = new AdminLogin();
            obj.Show();
            this.Hide();
        }
    }
}
