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
    public partial class Selling : Form
    {
        Access Con = new Access();
        int grandTotal = 0, n = 0;
        public Selling()
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
            SqlCommand com = new SqlCommand("SELECT * FROM Products_tb", Con.getCon());
            SqlDataAdapter Da = new SqlDataAdapter(com);
            DataTable Dt = new DataTable();
            Da.Fill(Dt);
            productsGridView.DataSource = Dt;
            productsGridView.RowTemplate.Height = 130;
            DataGridViewImageColumn im = new DataGridViewImageColumn();
            im = (DataGridViewImageColumn)productsGridView.Columns[5];
            im.ImageLayout = DataGridViewImageCellLayout.Stretch;
        }

       private void getBills()
        {
            SqlCommand com = new SqlCommand("SELECT * FROM Bills_tb", Con.getCon());
            SqlDataAdapter Da = new SqlDataAdapter(com);
            DataTable Dt = new DataTable();
            Da.Fill(Dt);
            billsGridView.DataSource = Dt;
        }

        private void getCat()
        {
            SqlCommand com = new SqlCommand("SELECT * FROM Categ_tb", Con.getCon());
            SqlDataAdapter Da = new SqlDataAdapter(com);
            DataTable Dt = new DataTable();
            Da.Fill(Dt);
            cmboCat.DataSource = Dt;
            cmboCat.ValueMember = "CatName";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SqlCommand com = new SqlCommand("SELECT * FROM Products_tb WHERE ProCat='" + cmboCat.Text + "' ", Con.getCon());
            SqlDataAdapter Da = new SqlDataAdapter(com);
            DataTable Dt = new DataTable();
            Da.Fill(Dt);
            productsGridView.DataSource = Dt;
        }

        private void productsGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow row = productsGridView.Rows[e.RowIndex];
                txtProName.Text = row.Cells[1].Value.ToString();
                txtProPrice.Text = row.Cells[3].Value.ToString();
            }
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            getTable();
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            if(txtProName.Text=="" || txtProQTE.Text == "")
            {
                MessageBox.Show("Missing Info", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int total = Convert.ToInt32(txtProPrice.Text) * Convert.ToInt32(txtProQTE.Text);
                DataGridViewRow addRow = new DataGridViewRow();

                addRow.CreateCells(orderGridView);
                addRow.Cells[0].Value = ++n;
                addRow.Cells[1].Value = txtProName.Text;
                addRow.Cells[2].Value = txtProPrice.Text;
                addRow.Cells[3].Value = txtProQTE.Text;
                addRow.Cells[4].Value = total;

                orderGridView.Rows.Add(addRow);
                grandTotal += total;
                lblTotal.Text = grandTotal + " LE";
                txtProQTE.Clear();
            }
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

        private void btnDeleteBill_Click(object sender, EventArgs e)
        {
            if (txtBillID.Text != "")
            {

                DialogResult result = MessageBox.Show("Are You Sure You Want To Delete Bill", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        string deleteQ = "Delete from Bills_tb WHERE BillID=" + txtBillID.Text + " ";
                        SqlCommand com = new SqlCommand(deleteQ, Con.getCon());
                        Con.open();
                        com.ExecuteNonQuery();
                        MessageBox.Show("Bill Deleted", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Con.close();
                        getBills();
                        txtBillID.Clear();

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

        private void billsGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = billsGridView.Rows[e.RowIndex];

                txtBillID.Text = row.Cells[0].Value.ToString();
            }
        }

        private void btnAddProduct_MouseEnter(object sender, EventArgs e)
        {
            btnAddProduct.BackColor = Color.FromArgb(32, 252, 143);
        }

        private void btnAddProduct_MouseMove(object sender, MouseEventArgs e)
        {
            btnAddProduct.BackColor = Color.FromArgb(244, 68, 46);
        }

        private void btnAddBill_Click(object sender, EventArgs e)
        {
            try
            {

                string insertQ = "INSERT INTO Bills_tb VALUES('" + lblName.Text + "','" + lblDate.Text + "', '" + grandTotal.ToString() + "'  ) ";
                SqlCommand com = new SqlCommand(insertQ, Con.getCon());
                Con.open();
                com.ExecuteNonQuery();
                MessageBox.Show("Order Added ^^", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Con.close();
                getBills();



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Selling_Load(object sender, EventArgs e)
        {

            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            lblDate.Text = DateTime.Today.ToShortDateString();
            lblName.Text = LogIn.SellerName;
            productsGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            productsGridView.RowTemplate.Height = 130;
            productsGridView.AllowUserToAddRows = false;
            getTable();
            getCat();
            getBills();
        }
    }
}
