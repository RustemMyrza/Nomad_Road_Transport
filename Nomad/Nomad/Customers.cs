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
    public partial class Customers : Form
    {
        public Customers()
        {
            InitializeComponent();
            ShowCustomers();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-BAUB2U6\SQLEXPRESS;Initial Catalog=Nomad;Integrated Security=True");
        private void Clear()
        {
            CustNameTb.Text = "";
            CustGenCb.SelectedIndex = -1;
            CustPhoneTb.Text = "";
            CustAddTb.Text = "";        
        }
        private void ShowCustomers()
        {
            Con.Open();
            string Query = "select * from CustomerTb1";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            CustomerDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (CustNameTb.Text == "" || CustGenCb.SelectedIndex == -1 || CustPhoneTb.Text == "" || CustAddTb.Text == "")
            {
                MessageBox.Show("Select a Customer");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into CustomerTb1 (CustName, CustPhone, CustAdd, CustGen) values(@CN,  @CP, @CA, @CG)", Con);
                    cmd.Parameters.AddWithValue("@CN", CustNameTb.Text); 
                    cmd.Parameters.AddWithValue("@CP", CustPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@CA", CustAddTb.Text);
                    cmd.Parameters.AddWithValue("@CG", CustGenCb.SelectedItem.ToString());
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Recorded");
                    Con.Close();
                    ShowCustomers();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        int Key = 0;
        private void button3_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select a Customer");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from CustomerTb1 where CustId=@CustKey", Con);
                    cmd.Parameters.AddWithValue("@CustKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Deleted");
                    Con.Close();
                    ShowCustomers();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }


            }
        }

        private void CustomerDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < CustomerDGV.Rows.Count)
            {
                CustNameTb.Text = CustomerDGV.Rows[e.RowIndex].Cells[1].Value.ToString();
            }
            if (e.RowIndex >= 0 && e.RowIndex < CustomerDGV.Rows.Count)
            {
                CustPhoneTb.Text = CustomerDGV.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
            if (e.RowIndex >= 0 && e.RowIndex < CustomerDGV.Rows.Count)
            {
                CustAddTb.Text = CustomerDGV.Rows[e.RowIndex].Cells[3].Value.ToString();
            }
            if (e.RowIndex >= 0 && e.RowIndex < CustomerDGV.Rows.Count)
            {
                CustGenCb.SelectedItem = CustomerDGV.Rows[e.RowIndex].Cells[4].Value.ToString();
            }
            if (CustNameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(CustomerDGV.Rows[e.RowIndex].Cells[0].Value.ToString());    
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (CustNameTb.Text == "" || CustGenCb.SelectedIndex == -1 || CustPhoneTb.Text == "" || CustAddTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update CustomerTb1 set CustName=@CN, CustPhone=@CP, CustAdd=@CA, CustGen=@CG where CustId=@CKey", Con);
                    cmd.Parameters.AddWithValue("@CN", CustNameTb.Text);
                    cmd.Parameters.AddWithValue("@CP", CustPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@CA", CustAddTb.Text);
                    cmd.Parameters.AddWithValue("@CG", CustGenCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@CKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Updated");
                    Con.Close();
                    ShowCustomers();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Drivers obj = new Drivers();
            obj.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Vehicles obj = new Vehicles();
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
