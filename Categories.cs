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
    public partial class Categories : Form
    {
        Access Con = new Access();
        public Categories()
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
                get { return Color.FromArgb(2, 1, 34); }
            }
        }
        /// </summary>

        private void getTable()
        {
            SqlCommand com = new SqlCommand("SELECT * FROM Categ_tb", Con.getCon());
            SqlDataAdapter DA = new SqlDataAdapter(com);
            DataTable DT = new DataTable();
            DA.Fill(DT);
            CatGridView.DataSource = DT;
        }

        private void clear()
        {
            txtCatDes.Clear();
            txtCatID.Clear();
            txtCatName.Clear();
        }
        private bool isNull()
        {
            if (txtCatID.Text == string.Empty || txtCatName.Text == string.Empty)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void sellersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sellers sell = new Sellers();
            sell.Show();
            this.Hide();
        }

        private void productsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Products pro = new Products();
            pro.Show();
            this.Hide();
        }

        private void Categories_Load(object sender, EventArgs e)
        {
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            getTable();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!isNull())
            {
                try
                {
                    string insertQ = "INSERT INTO Categ_tb VALUES(" + txtCatID.Text + ",'" + txtCatName.Text + "','" + txtCatDes.Text + "')";
                    Con.open();
                    SqlCommand com = new SqlCommand(insertQ, Con.getCon());
                    com.ExecuteNonQuery();
                    Con.close();
                    MessageBox.Show("Category Added", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (!isNull())
            {
                try
                {
                    string editQ = "UPDATE Categ_tb SET CatName='" + txtCatName.Text + "', CatDes=" + txtCatDes.Text + " WHERE CatID=" + txtCatID.Text + " ";

                    SqlCommand com = new SqlCommand(editQ, Con.getCon());
                    Con.open();
                    com.ExecuteNonQuery();
                    MessageBox.Show("Category Updated", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("Missing Information", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CatGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = CatGridView.Rows[e.RowIndex];
                txtCatID.Text = row.Cells[0].Value.ToString();
                txtCatName.Text = row.Cells[1].Value.ToString();
                txtCatDes.Text = row.Cells[2].Value.ToString();

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtCatID.Text != "")
            {
                DialogResult result = MessageBox.Show("Are You Sure You Want To Delete This Category", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        string deleteQ = "DELETE FROM Categ_tb WHERE CatID=" + txtCatID.Text + " ";
                        SqlCommand com = new SqlCommand(deleteQ, Con.getCon());
                        Con.open();
                        com.ExecuteNonQuery();
                        MessageBox.Show("Category Deleted", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Con.close();
                        getTable();

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

        private void productsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Products pro = new Products();
            pro.Show();
            this.Hide();
        }

        private void sellersToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Sellers sell = new Sellers();
            sell.Show();
            this.Hide();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogIn log = new LogIn();
            log.Show();
            this.Hide();
        }

        private void xToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void logOutToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            LogIn log = new LogIn();
            log.Show();
            this.Hide();
        }

        private void productsToolStripMenuItem_Click_2(object sender, EventArgs e)
        {
            Products pro = new Products();
            pro.Show();
            this.Hide();
        }

        private void sellersToolStripMenuItem_Click_2(object sender, EventArgs e)
        {
            Sellers sell = new Sellers();
            sell.Show();
            this.Hide();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            menuStrip1.BackColor = Color.FromArgb(2, 1, 34);
        }

        private void menuStrip1_TabStopChanged(object sender, EventArgs e)
        {
            menuStrip1.BackColor = Color.FromArgb(2, 1, 34);
        }

        //Add
        private void btnAdd_MouseEnter(object sender, EventArgs e)
        {
            btnAdd.BackColor = Color.FromArgb(32, 252, 143);
        }

        private void btnAdd_MouseLeave(object sender, EventArgs e)
        {
            btnAdd.BackColor = Color.FromArgb(244, 68, 46);
        }

        // Edit
        private void btnEdit_MouseEnter(object sender, EventArgs e)
        {
            btnEdit.BackColor = Color.FromArgb(252, 47, 0);
        }

        private void btnEdit_MouseLeave(object sender, EventArgs e)
        {
            btnEdit.BackColor = Color.FromArgb(244, 68, 46);
        }

        //Delete

        private void btnDelete_MouseEnter(object sender, EventArgs e)
        {
            btnDelete.BackColor = Color.FromArgb(148, 27, 12);
        }

        private void btnDelete_MouseLeave(object sender, EventArgs e)
        {
            btnDelete.BackColor = Color.FromArgb(244, 68, 46);
        }
    }
}
