using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Nomad
{
    public partial class Vehicles : Form
    {
        public Vehicles()
        {
            InitializeComponent();
            ShowVehicles();
            GetDrivers();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-BAUB2U6\SQLEXPRESS;Initial Catalog=Nomad;Integrated Security=True");
        private void Clear()
        {
            LPlateTb.Text = "";
            MarkTb.Text = "";
            ModelTb.Text = "";
            VYearCb.SelectedIndex = -1;
            EngTypeCb.SelectedIndex = -1;
            ColorTb.Text = "";
            MilleageTb.Text = "";
            TypeCb.SelectedIndex = -1;
            BookedCb.SelectedIndex = -1;
        }
        private void ShowVehicles()
        {
            Con.Open();
            string Query = "select * from VehicleTb1";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            VehicleDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void GetDrivers()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select * from DriverTb1", Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("DrName", typeof(string));
            dt.Load(rdr);
            DriverCb.ValueMember = "DrName";
            DriverCb.DataSource = dt;
            Con.Close();
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SaveBtn_Click_1(object sender, EventArgs e)
        {
            if (LPlateTb.Text == "" || MarkTb.Text == "" || ModelTb.Text == "" || VYearCb.SelectedIndex == -1 || EngTypeCb.SelectedIndex == -1 || ColorTb.Text == "" || MilleageTb.Text == "" || TypeCb.SelectedIndex == -1 || BookedCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into VehicleTb1 (VLp, Vmark, Vmodel, VYear, VEngType, VColor, VMilleage, VType, Booked, Driver) values(@VP, @Vma, @Vmo, @VY, @VEng, @VCo, @VMi, @VTy, @VB, @DR)", Con);
                    cmd.Parameters.AddWithValue("@VP", LPlateTb.Text);
                    cmd.Parameters.AddWithValue("@Vma", MarkTb.Text);
                    cmd.Parameters.AddWithValue("@Vmo", ModelTb.Text);
                    cmd.Parameters.AddWithValue("@VY", VYearCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@VEng", EngTypeCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@VCo", ColorTb.Text);
                    cmd.Parameters.AddWithValue("@VMi", MilleageTb.Text);
                    cmd.Parameters.AddWithValue("@VTY", TypeCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@VB", BookedCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@DR", DriverCb.SelectedValue.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Vehicle Recorded");
                    Con.Close();
                    ShowVehicles();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (LPlateTb.Text == "" || MarkTb.Text == "" || ModelTb.Text == "" || VYearCb.SelectedIndex == -1 || EngTypeCb.SelectedIndex == -1 || ColorTb.Text == "" || MilleageTb.Text == "" || TypeCb.SelectedIndex == -1 || BookedCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open(); 
                    SqlCommand cmd = new SqlCommand("update VehicleTb1 set  Vmark=@Vma, Vmodel=@Vmo, VYear=@VY, VEngType=@VEng, VColor=@VCo, VMilleage=@VMi, VType=@VTy, Booked=@VB, Driver=@Dr where VLp=@VP", Con);
                    cmd.Parameters.AddWithValue("@VP", LPlateTb.Text);
                    cmd.Parameters.AddWithValue("@Vma", MarkTb.Text);
                    cmd.Parameters.AddWithValue("@Vmo", ModelTb.Text);
                    cmd.Parameters.AddWithValue("@VY", VYearCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@VEng", EngTypeCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@VCo", ColorTb.Text);
                    cmd.Parameters.AddWithValue("@VMi", MilleageTb.Text);
                    cmd.Parameters.AddWithValue("@VTY", TypeCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@VB", BookedCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Dr", DriverCb.SelectedValue.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Vehicle Updated");
                    Con.Close();
                    ShowVehicles();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (LPlateTb.Text == "")
            {
                MessageBox.Show("Select a Vehicle");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from VehicleTb1 where VLP=@VPlate", Con);
                    cmd.Parameters.AddWithValue("@VPlate", LPlateTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Vehicle Deleted");
                    Con.Close();
                    ShowVehicles();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }


            }

        }

      
        private void VehicleDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < VehicleDGV.Rows.Count)
            {
                LPlateTb.Text = VehicleDGV.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
            if (e.RowIndex >= 0 && e.RowIndex < VehicleDGV.Rows.Count)
            {
                MarkTb.Text = VehicleDGV.Rows[e.RowIndex].Cells[1].Value.ToString();
            }
            if (e.RowIndex >= 0 && e.RowIndex < VehicleDGV.Rows.Count)
            {
                ModelTb.Text = VehicleDGV.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
            if (e.RowIndex >= 0 && e.RowIndex < VehicleDGV.Rows.Count)
            {
                VYearCb.SelectedItem = VehicleDGV.Rows[e.RowIndex].Cells[3].Value.ToString();
            }
            if (e.RowIndex >= 0 && e.RowIndex < VehicleDGV.Rows.Count)
            {
                EngTypeCb.SelectedItem = VehicleDGV.Rows[e.RowIndex].Cells[4].Value.ToString();
            }
            if (e.RowIndex >= 0 && e.RowIndex < VehicleDGV.Rows.Count)
            {
                ColorTb.Text = VehicleDGV.Rows[e.RowIndex].Cells[5].Value.ToString();
            }
            if (e.RowIndex >= 0 && e.RowIndex < VehicleDGV.Rows.Count)
            {
                MilleageTb.Text = VehicleDGV.Rows[e.RowIndex].Cells[6].Value.ToString();
            }
            if (e.RowIndex >= 0 && e.RowIndex < VehicleDGV.Rows.Count)
            {
                TypeCb.SelectedItem = VehicleDGV.Rows[e.RowIndex].Cells[7].Value.ToString();
            }
            if (e.RowIndex >= 0 && e.RowIndex < VehicleDGV.Rows.Count)
            {
                BookedCb.SelectedItem = VehicleDGV.Rows[e.RowIndex].Cells[8].Value.ToString();
            }
        }

        private void Vehicles_Load(object sender, EventArgs e)
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

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Dashboard obj = new Dashboard();
            obj.Show();
            this.Hide();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            this.Hide();
            Obj.Show();
        }
    }
}
