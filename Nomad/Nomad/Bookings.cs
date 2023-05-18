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
    public partial class Bookings : Form
    {
        public Bookings()
        {
            InitializeComponent();
            GetCustomers();
            ShowBookings();
            GetCars();
            UnameLb1.Text = Login.User;
        }
        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-BAUB2U6\SQLEXPRESS;Initial Catalog=Nomad;Integrated Security=True");

        private void GetCustomers()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select * from CustomerTb1", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CustName", typeof(string));
            dt.Load(rdr);
            CustCb.ValueMember = "CustName";
            CustCb.DataSource = dt;
            Con.Close();
        }

        private void GetDrivers()
        {
            Con.Open();
            string Query = "select * from VehicleTb1 where VLP='" + VehicleCb.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(Query, Con);    
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                DriverTb.Text = dr["Driver"].ToString();
            }
            Con.Close();
        }
        private void GetCars()  
        {
            string IsBooked = "No";
            Con.Open();
            SqlCommand cmd = new SqlCommand("select * from VehicleTb1 where Booked='"+IsBooked+"'", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("VLP", typeof(string));
            dt.Load(rdr);
            VehicleCb.ValueMember = "VLP";
            VehicleCb.DataSource = dt;
            Con.Close();
        }
        private void Clear()
        {
            CustCb.SelectedIndex = -1;
            VehicleCb.SelectedIndex = -1;
            DriverTb.Text = "";
            AmountTb.Text = "";
        }
        private void ShowBookings()
        {
            Con.Open();
            string Query = "select * from BookingTb1";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            BookingDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {

        }
        private void UpdateVehicle ()
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("update VehicleTb1 set  Booked=@VB where VLp=@VP", Con);
                cmd.Parameters.AddWithValue("@VP", VehicleCb.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@VB", "Yes");
                cmd.ExecuteNonQuery();
                MessageBox.Show("Vehicle Updated");
                Con.Close();
               
                Clear();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (CustCb.SelectedIndex == -1 || VehicleCb.SelectedIndex == -1 || DriverTb.Text == "" || AmountTb.Text == "")
            {
                MessageBox.Show("Select a Customer");
            }
            else
            {
                try
                {
                    Con.Open(); 
                    SqlCommand cmd = new SqlCommand("insert into BookingTb1 (CustName, Vehicle, Driver, PickupDate, DropoffDate, Amount, BUser) values(@CN,  @Veh, @Dri, @PDate,@DDate,  @Am, @Un)", Con);
                    cmd.Parameters.AddWithValue("@CN", CustCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Veh", VehicleCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@Dri", DriverTb.Text);
                    cmd.Parameters.AddWithValue("@PDate", PickUpDate.Value.Date);
                    cmd.Parameters.AddWithValue("@DDate", RetDate.Value.Date);
                    cmd.Parameters.AddWithValue("@AM", AmountTb.Text);
                    cmd.Parameters.AddWithValue("@UN", UnameLb1.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Vehicle Booked");
                    Con.Close();
                    ShowBookings();
                    UpdateVehicle();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        private void VehicleCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetDrivers();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Vehicles obj = new Vehicles();
            obj.Show(); 
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Customers obj = new Customers();
            obj.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Drivers obj = new Drivers();
            obj.Show();
            this.Hide();
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Dashboard obj = new Dashboard();
            obj.Show();
            this.Hide();
        }

        private void UnameLb1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            this.Hide();
            Obj.Show();
        }
    }
    }

