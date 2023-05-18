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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            CountVehicles();
            CountUsers();
            CountDrivers();
            CountBookings();
            CountCust();
            SumAmt();
   
        }
        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-BAUB2U6\SQLEXPRESS;Initial Catalog=Nomad;Integrated Security=True");
        private void CountVehicles()
        {
            Con.Open();
            string Query = "select count(*) from VehicleTb1";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            VNumLb.Text = dt.Rows[0][0].ToString();
            Con.Close();    
        }
        private void CountUsers()
        {
            Con.Open();
            string Query = "select count(*) from UserTb1";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            UNumLb.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void CountDrivers()
        {
            Con.Open();
            string Query = "select count(*) from DriverTb1";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            DNumLb.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void CountBookings()
        {
            Con.Open();
            string Query = "select count(*) from BookingTb1";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            BookNumLb.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void CountCust()
        {
            Con.Open();
            string Query = "select count(*) from CustomerTb1";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            CNumLb.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void SumAmt()
        {
            Con.Open();
            string Query = "select Sum(Amount) from BookingTb1";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            IncNumLb.Text ="USD" + dt.Rows[0][0].ToString();
            Con.Close();
        }
       
        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void Dashboard_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Drivers obj = new Drivers();
            obj.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Customers obj = new Customers();
            obj.Show();
            this.Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Bookings obj = new Bookings();
            obj.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Vehicles obj = new Vehicles();
            obj.Show();
            this.Hide();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }
    }
}
