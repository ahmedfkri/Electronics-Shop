using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Data;
using System.Drawing.Imaging;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Electronics_Shop
{
    public partial class Products : Form
    {
        
        string imgLocation;
        int flag = 0;
        Access Con = new Access();
        public Products()
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

        public byte[] getImg()
        {
            byte[] image = null;
            FileStream stream = new FileStream(imgLocation, FileMode.Open, FileAccess.Read);
            BinaryReader bReader = new BinaryReader(stream);
            image = bReader.ReadBytes((int)stream.Length);
            return image;
            
        }

        public byte[] getImgFromBox()
        {
            byte[] image = null;
            Bitmap bmp = new Bitmap(pictureBox1.Image);
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms,ImageFormat.Bmp);
            image = ms.ToArray();
            return image;

        }

        private bool isNull()
        {
            if(txtProID.Text==string.Empty || txtProName.Text ==string.Empty || txtProPrice.Text==string.Empty || txtProQTE.Text== string.Empty || pictureBox1.Image==null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void getTable()
        {

            SqlCommand com = new SqlCommand("Select * From Products_tb", Con.getCon());
            SqlDataAdapter DA = new SqlDataAdapter(com);
            DataTable TB = new DataTable();
            DA.Fill(TB);
            productsGridView.DataSource = TB;
            productsGridView.RowTemplate.Height = 75;
            DataGridViewImageColumn im = new DataGridViewImageColumn();
            im = (DataGridViewImageColumn)productsGridView.Columns[5];
            im.ImageLayout = DataGridViewImageCellLayout.Stretch; 


        }
        private void getCat()
        {
            SqlCommand com = new SqlCommand("SELECT * From Categ_tb", Con.getCon());
            SqlDataAdapter Da = new SqlDataAdapter(com);
            DataTable Dt = new DataTable();
            Da.Fill(Dt);

            cmboCat.DataSource = Dt;
            cmboCat.ValueMember = "CatName";
        }

        private void clear()
        {
            txtProID.Clear();
            txtProName.Clear();
            txtProPrice.Clear();
            txtProQTE.Clear();
            pictureBox1.Image = null;
            cmboCat.SelectedIndex = 0;
            flag = 0;
        }
        private void categoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Categories cat = new Categories();
            cat.Show();
            this.Hide();
        }

        private void sellersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sellers sell = new Sellers();
            sell.Show();
            this.Hide();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files|*.bmp;*.gif;*.jpg;*..png|All files|*.*";
            if(dialog.ShowDialog()== DialogResult.OK)
            {
                imgLocation = dialog.FileName.ToString();
                pictureBox1.Image = Image.FromFile(dialog.FileName);
                flag = 1;

            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!isNull())
            { 
            try
            {
                string insertQ = "INSERT INTO Products_tb VALUES(" + txtProID.Text + ",'" + txtProName.Text + "'," + txtProQTE.Text + "," + txtProPrice.Text + ",'" + cmboCat.Text + "',@ProImg)";
                Con.open();
                SqlCommand com = new SqlCommand(insertQ, Con.getCon());
                com.Parameters.AddWithValue("@ProImg", getImg());
                com.ExecuteNonQuery();
                MessageBox.Show("Done");
                getTable();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            }
            else
            {
                MessageBox.Show("Missing Info", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Products_Load(object sender, EventArgs e)
        {
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            getTable();
            getCat();

        }

        private void productsGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    DataGridViewRow row = productsGridView.Rows[e.RowIndex];
                    txtProID.Text = row.Cells[0].Value.ToString();
                    txtProName.Text = row.Cells[1].Value.ToString();
                    txtProQTE.Text = row.Cells[2].Value.ToString();
                    txtProPrice.Text = row.Cells[3].Value.ToString();
                    cmboCat.Text = row.Cells[4].Value.ToString();

                    MemoryStream ms = new MemoryStream((byte[])row.Cells[5].Value);
                    pictureBox1.Image = Image.FromStream(ms);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                



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
                    string editQ = "UPDATE Products_tb SET ProName='" + txtProName.Text + "', ProQTE=" + txtProQTE.Text + ", ProPrice=" + txtProPrice.Text + ", ProCat='" + cmboCat.Text + "', ProImg=@ProImg WHERE ProID=" + txtProID.Text + " ";

                    SqlCommand com = new SqlCommand(editQ, Con.getCon());
                    Con.open();
                    com.Parameters.AddWithValue("@ProImg", getImgFromBox());
                    com.ExecuteNonQuery();
                    MessageBox.Show("Product Updated","",MessageBoxButtons.OK,MessageBoxIcon.Information);
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtProID.Text != "")
            {
                try
                {
                    string deleteQ="Delete from Products_tb WHERE ProID="+txtProID.Text+" ";
                    SqlCommand com = new SqlCommand(deleteQ, Con.getCon());
                    Con.open();
                    com.ExecuteNonQuery();
                    MessageBox.Show("Product Deleted", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Con.close();
                    getTable();
                    clear();

                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please Enter ID to delete","",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void categoriesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Categories cat = new Categories();
            cat.Show(); 
            this.Hide();
        }

        private void sellersToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Sellers sell = new Sellers();
            sell.Show();
            this.Hide();
        }

        private void logOutToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            LogIn log = new LogIn();
            log.Show();
            this.Hide();
        }

        private void xToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_MouseEnter(object sender, EventArgs e)
        {
            btnAdd.BackColor = Color.FromArgb(32, 252, 143);
        }

        private void btnAdd_MouseLeave(object sender, EventArgs e)
        {
            btnAdd.BackColor = Color.FromArgb(244, 68, 46);
        }

        private void btnEdit_MouseEnter(object sender, EventArgs e)
        {
            btnEdit.BackColor = Color.FromArgb(252, 47, 0);
        }

        private void btnEdit_MouseLeave(object sender, EventArgs e)
        {
            btnEdit.BackColor = Color.FromArgb(244, 68, 46);
        }

        private void btnDelete_MouseEnter(object sender, EventArgs e)
        {
            btnDelete.BackColor = Color.FromArgb(148, 27, 12);
        }

        private void btnDelete_MouseLeave(object sender, EventArgs e)
        {
            btnEdit.BackColor = Color.FromArgb(244, 68, 46);
        }

        private void btnBrowse_MouseHover(object sender, EventArgs e)
        {

        }
    }
}
