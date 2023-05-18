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
    public partial class Drivers : Form
    {
        public Drivers()
        {
            InitializeComponent();
            //GetCars();
            ShowDrivers();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-BAUB2U6\SQLEXPRESS;Initial Catalog=Nomad;Integrated Security=True");
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

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

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Vehicles obj = new Vehicles();
            obj.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        
        private void Clear()
        {
            DrNameTb.Text = "";
            GenCb.SelectedIndex = -1;
            PhoneTb.Text = "";
            DrAdd.Text = "";
        }
        private void ShowDrivers()
        {
            Con.Open();
            string Query = "select * from DriverTb1";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            DriverDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        /*
        private void GetCars()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select * from VehicleTb1", Con);
            SqlDataReader rdr;
             rdr = cmd.ExecuteReader();  
            DataTable dt = new DataTable();
            dt.Columns.Add("VLp", typeof(string));
            dt.Load(rdr);
            VehicleCb.ValueMember = "VLp";
            VehicleCb.DataSource = dt;
            Con.Close();
        }*/
        private void button1_Click(object sender, EventArgs e)
        {
            if (DrNameTb.Text == "" || GenCb.SelectedIndex == -1 || PhoneTb.Text == "" || DrAdd.Text == "" || RatingCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into DriverTb1 (DrName, Drphone, DrAdd, DrDOB, DrJoinDate, DrGen, DrRating) values(@DRN,  @DrP, @DrA, @DrD,  @DrJ, @DrG, @DrR)", Con);
                    cmd.Parameters.AddWithValue("@DRN", DrNameTb.Text);
                    cmd.Parameters.AddWithValue("@DRA", DrAdd.Text);
                    cmd.Parameters.AddWithValue("@DRP", PhoneTb.Text);
                    cmd.Parameters.AddWithValue("@DRD", DOB.Value.ToString());
                    cmd.Parameters.AddWithValue("@DRJ", JoinDate.Value.ToString());
                    cmd.Parameters.AddWithValue("@DRG", GenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@DRR", RatingCb.SelectedItem.ToString());

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Driver Recorded");
                    Con.Close();
                    ShowDrivers();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
                 
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (DrNameTb.Text == "" || GenCb.SelectedIndex == -1 || PhoneTb.Text == "" || DrAdd.Text == "" || RatingCb.SelectedIndex == -1)
            {
                MessageBox.Show("Select a Driver");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update DriverTb1 set DrName=@DRN,  Drphone=@DrP, DrAdd=@DrA, DrDOB=@DrD, DrJoinDate=@DrJ, DrGen=@DrG, DrRating=@DrR where DrId=@DrKey", Con);
                    cmd.Parameters.AddWithValue("@DRN", DrNameTb.Text);
                    cmd.Parameters.AddWithValue("@DRA", DrAdd.Text);
                    cmd.Parameters.AddWithValue("@DRP", PhoneTb.Text);
                    cmd.Parameters.AddWithValue("@DRD", DOB.Value.ToString());
                    cmd.Parameters.AddWithValue("@DRJ", JoinDate.Value.ToString());
                    cmd.Parameters.AddWithValue("@DRG", GenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@DRR", RatingCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@DrKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Driver Updated");
                    Con.Close();
                    ShowDrivers();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select a Driver");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from DriverTb1 where DrId=@DrKey", Con);
                    cmd.Parameters.AddWithValue("@DrKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Driver Deleted");
                    Con.Close();
                    ShowDrivers();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }


            }
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
        int Key = 0;
        private void DriverDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DriverDGV.DefaultCellStyle.ForeColor = Color.Black;
            if (e.RowIndex >= 0 && e.RowIndex < DriverDGV.Rows.Count)
            {
                DrNameTb.Text = DriverDGV.Rows[e.RowIndex].Cells[1].Value.ToString();
            }
            if (e.RowIndex >= 0 && e.RowIndex < DriverDGV.Rows.Count)
            {
                PhoneTb.Text = DriverDGV.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
            if (e.RowIndex >= 0 && e.RowIndex < DriverDGV.Rows.Count)
            {
                DrAdd.Text = DriverDGV.Rows[e.RowIndex].Cells[3].Value.ToString();
            }
            if (e.RowIndex >= 0 && e.RowIndex < DriverDGV.Rows.Count)
            {
                DOB.Text = DriverDGV.Rows[e.RowIndex].Cells[4].Value.ToString();
            }
            if (e.RowIndex >= 0 && e.RowIndex < DriverDGV.Rows.Count)
            {
                JoinDate.Text = DriverDGV.Rows[e.RowIndex].Cells[5].Value.ToString();
            }
            if (e.RowIndex >= 0 && e.RowIndex < DriverDGV.Rows.Count)
            {
                GenCb.SelectedItem = DriverDGV.Rows[e.RowIndex].Cells[6].Value.ToString();
            }
            if (e.RowIndex >= 0 && e.RowIndex < DriverDGV.Rows.Count)
            {
                RatingCb.SelectedItem = DriverDGV.Rows[e.RowIndex].Cells[7].Value.ToString();
            }
            if (DrNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(DriverDGV.Rows[e.RowIndex].Cells[0].Value.ToString());
            }
        }
    }
}
