using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Electronics_Shop
{
    public partial class Print : Form
    {
        public string Id, Name, Date, Total;
        private Bitmap img;

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Rectangle page = e.PageBounds;
            e.Graphics.DrawImage(img,(page.Width/2)-(this.panel1.Width/2),this.panel1.Location.Y);

        }

        private void getArea(Panel pn)
        {
            img = new Bitmap(pn.Width, pn.Height);
            pn.DrawToBitmap(img, new Rectangle(0, 0, pn.Width, pn.Height));
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            printMethod(this.panel1);
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {

        }

        public Print()
        {
            InitializeComponent();
        }
        private void printMethod(Panel pn )
        {
            panel1 = pn;
            getArea(pn);
            printPreviewDialog1.Document = printDocument1;
            printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(printDocument1_PrintPage);
            printPreviewDialog1.ShowDialog();

        }

        private void Print_Load(object sender, EventArgs e)
        {
            label1.Text = DateTime.Today.ToShortDateString();
            lblID.Text = Id;
            lblName.Text = Name;
            lblBillDate.Text = Date;
            lblTotal.Text = Total;


        }
    }
}
