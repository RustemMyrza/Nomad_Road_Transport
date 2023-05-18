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
    public partial class Users : Form
    {
        public Users()
        {
            InitializeComponent();
            ShowUsers();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-BAUB2U6\SQLEXPRESS;Initial Catalog=Nomad;Integrated Security=True");
        private void Clear()
        {
            UnameTb.Text = "";
            PhoneTb.Text = "";
            PasswordTb.Text = "";  
        }
        private void ShowUsers()
        {
            Con.Open();
            string Query = "select * from UserTb1";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            UserDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void VehicleDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < UserDGV.Rows.Count)
            {
                UnameTb.Text = UserDGV.Rows[e.RowIndex].Cells[1].Value.ToString();
            }
            if (e.RowIndex >= 0 && e.RowIndex < UserDGV.Rows.Count)
            {
                PhoneTb.Text = UserDGV.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
            if (e.RowIndex >= 0 && e.RowIndex < UserDGV.Rows.Count)
            {
                PasswordTb.Text = UserDGV.Rows[e.RowIndex].Cells[3].Value.ToString();
            }
            if (UnameTb.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(UserDGV.Rows[e.RowIndex].Cells[0].Value.ToString());
            }
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (UnameTb.Text == "" ||   PhoneTb.Text == "" || PasswordTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into UserTb1 (Uname, Uphone, Upassword) values(@UN,  @UP, @UPa)", Con);
                    cmd.Parameters.AddWithValue("@UN", UnameTb.Text);
                    cmd.Parameters.AddWithValue("@UP", PhoneTb.Text);
                    cmd.Parameters.AddWithValue("@UPa", PasswordTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Recorded");
                    Con.Close();
                    ShowUsers();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
        }
        int Key = 0;
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select a User");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("delete from UserTb1 where UId=@UKey", Con);
                    cmd.Parameters.AddWithValue("@UKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Deleted");
                    Con.Close();
                    ShowUsers();
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
            if (UnameTb.Text == "" || PhoneTb.Text == "" || PasswordTb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("update UserTb1 set Uname=@UN, Uphone=@UP, Upassword=@UPa where UId=@CKey", Con);
                    cmd.Parameters.AddWithValue("@UN", UnameTb.Text);
                    cmd.Parameters.AddWithValue("@UP", PhoneTb.Text);
                    cmd.Parameters.AddWithValue("@UPa", PasswordTb.Text);
                    cmd.Parameters.AddWithValue("@UKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("User Updated");
                    Con.Close();
                    ShowUsers();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
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
