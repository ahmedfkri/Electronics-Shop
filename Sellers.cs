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
    public partial class Sellers : Form
    {
        Access Con = new Access();
        public Sellers()
        {
            InitializeComponent();
            menuStrip1.Renderer = new MyRenderer();
        }
     /// <summary>
     /// Meneu strip
     
        private class MyRenderer : ToolStripProfessionalRenderer
        {
            public MyRenderer() : base(new MyColors()) { }
        }

        private class MyColors : ProfessionalColorTable
        {
            public override Color MenuItemSelected
            {
                get { return Color.Yellow; }
            }
            public override Color MenuItemSelectedGradientBegin
            {
                get { return Color.FromArgb(2, 1, 45); }
            }
            public override Color MenuItemSelectedGradientEnd
            {
                get { return Color.FromArgb(2,1,34); }
            }
        }
        /// </summary>
        private void getTable()
        {
            try
            {

            
            SqlCommand com = new SqlCommand("SELECT * FROM Sellers_tb", Con.getCon());
            SqlDataAdapter Da = new SqlDataAdapter(com);
            DataTable Dt = new DataTable();
            Da.Fill(Dt);
            SellersGridView.DataSource = Dt;
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private bool isNull()
        {
            if (txtSellerAge.Text == string.Empty || txtSellerID.Text == string.Empty || txtSellerName.Text == string.Empty || txtSellerPass.Text == string.Empty || txtSellerPhone.Text == string.Empty)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void clear()
        {
            txtSellerID.Clear();
            txtSellerName.Clear();
            txtSellerAge.Clear();
            txtSellerPass.Clear();
            txtSellerPhone.Clear();


        }

        private void productsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Products pro = new Products();
            pro.Show();
            this.Hide();
        }

        private void sellersToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void categoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Categories cat = new Categories();
            cat.Show();
            this.Hide();
        }

        private void Sellers_Load(object sender, EventArgs e)
        {
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            getTable();
        }

        private void categoriesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Categories cat = new Categories();
            cat.Show();
            this.Hide();

        }

        private void productsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Products pro = new Products();
            pro.Show();
            this.Hide();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!isNull())
            {
                try
                {
                    string insertQ = "INSERT INTO Sellers_tb VALUES(" + txtSellerID.Text + ",'" + txtSellerName.Text + "','" + txtSellerPhone.Text + "'," + txtSellerAge.Text + ",'" + txtSellerPass.Text + "')";
                    Con.open();
                    SqlCommand com = new SqlCommand(insertQ, Con.getCon());
                    com.ExecuteNonQuery();
                    MessageBox.Show("User Added","",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    getTable();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else
            {
                MessageBox.Show("Missing Info", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogIn log = new LogIn();
            log.Show();
            this.Hide();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!isNull())
            {
                try
                {
                    string editQ = "UPDATE Sellers_tb SET SellerName='" + txtSellerName.Text + "', SellerAge=" + txtSellerAge.Text + ", SellerPhone='" + txtSellerPhone.Text + "', SellerPass='" + txtSellerPass.Text + "' WHERE SellerID=" + txtSellerID.Text + " ";
                    SqlCommand com = new SqlCommand(editQ, Con.getCon());
                    Con.open();
                    com.ExecuteNonQuery();
                    MessageBox.Show("User Updated", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Con.close();
                    getTable();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else
            {
                MessageBox.Show("Missing Info", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SellersGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = SellersGridView.Rows[e.RowIndex];
                txtSellerID.Text = row.Cells[0].Value.ToString();
                txtSellerName.Text = row.Cells[1].Value.ToString();
                txtSellerAge.Text = row.Cells[3].Value.ToString();
                txtSellerPhone.Text = row.Cells[2].Value.ToString();
                txtSellerPass.Text = row.Cells[4].Value.ToString();

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            
            if (txtSellerID.Text != "")
            {
                DialogResult result = MessageBox.Show("Are You Sure You Want To Delete This Seller", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        string deleteQ = "Delete from Sellers_tb WHERE SellerID=" + txtSellerID.Text + " ";
                        SqlCommand com = new SqlCommand(deleteQ, Con.getCon());
                        Con.open();
                        com.ExecuteNonQuery();
                        MessageBox.Show("Seller Deleted", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Con.close();
                        getTable();
                        clear();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                    
            }
            else
            {
                MessageBox.Show("Please Enter ID to delete", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_MouseLeave(object sender, EventArgs e)
        {
            btnAdd.BackColor = Color.FromArgb(244, 68, 46);
        }

        private void btnDelete_MouseLeave(object sender, EventArgs e)
        {
            btnDelete.BackColor = Color.FromArgb(244,68,46);
        }

        private void btnDelete_MouseEnter(object sender, EventArgs e)
        {
            btnDelete.BackColor = Color.FromArgb(148, 27, 12);
        }

        private void btnAdd_MouseEnter(object sender, EventArgs e)
        {
            btnAdd.BackColor = Color.FromArgb(32, 252, 143);
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            menuStrip1.BackColor = Color.FromArgb(2,1,34);
        }

        private void menuStrip1_TabStopChanged(object sender, EventArgs e)
        {
            menuStrip1.BackColor = Color.FromArgb(2, 1, 34);
        }

        private void SellersGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void xToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEdit_MouseEnter(object sender, EventArgs e)
        {
            btnEdit.BackColor = Color.FromArgb(252, 47, 0);
        }

        private void btnEdit_MouseLeave(object sender, EventArgs e)
        {
            btnEdit.BackColor = Color.FromArgb(244, 68, 46);
        }
    }
    
}
