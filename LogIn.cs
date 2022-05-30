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

namespace Electronics_Shop
{
    public partial class LogIn : Form
    {
        Access Con = new Access();
        public static string SellerName = null;
        public LogIn()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (cmboRole.SelectedIndex > -1)
            {


                if (cmboRole.SelectedItem.ToString() == "Admin")
                {
                    if (txtName.Text == "admin" && txtPass.Text == "admin")
                    {
                        Products pro = new Products();
                        pro.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Are You Really Admin?!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {

                    SqlCommand com = new SqlCommand("SELECT * FROM Sellers_tb WHERE SellerName='" + txtName.Text + "' AND SellerPass='" + txtPass.Text + "' ", Con.getCon());
                    SqlDataAdapter Da = new SqlDataAdapter(com);
                    DataTable Dt = new DataTable();
                    Da.Fill(Dt);
                    if (Dt.Rows.Count > 0)
                    {
                        SellerName = txtName.Text;
                        Selling sell = new Selling();
                        sell.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Wrong UserName Or Password", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please Select Valid Role", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void LogIn_Load(object sender, EventArgs e)
        {
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("You Want To EXIT?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                this.Close();
                Application.Exit();
            }
        }

        private void label4_MouseEnter(object sender, EventArgs e)
        {
            label4.ForeColor = Color.Red;
        }

        private void label4_MouseLeave(object sender, EventArgs e)
        {
            label4.ForeColor = Color.White;
        }
    }
}
