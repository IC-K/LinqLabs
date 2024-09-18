using LinqLabs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MyHomeWork
{
    public partial class Frm作業_1 : Form
    {
        private int _currentPage = 1;

        public Frm作業_1()
        {
            InitializeComponent();
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);
            this.ordersTableAdapter1.Fill(this.nwDataSet1.Orders);
            this.order_DetailsTableAdapter1.Fill(this.nwDataSet1.Order_Details);


            var q1 = from o in this.nwDataSet1.Orders
                     select o.OrderDate.Year;
            this.comboBox1.DataSource = q1.Distinct().ToList();

        }
        private void button13_Click(object sender, EventArgs e)
        {
            int.TryParse(this.textBox1.Text, out countPerPage);

            page += 1;
            this.dataGridView1.DataSource = this.nwDataSet1.Products.Skip(countPerPage * page).Take(countPerPage).ToList();

        }

        private void button14_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files =  dir.GetFiles();

            //files[0].CreationTime
            this.dataGridView1.DataSource = files;

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.lblMaster.Text = "Orders";


            this.dataGridView1.DataSource = nwDataSet1.Orders.ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files = dir.GetFiles();

            this.lblMaster.Text = "2023 Files";
            IEnumerable<System.IO.FileInfo> q = files
                .Where(finfo => finfo.CreationTime.Year == 2023)
                .OrderBy(finfo => finfo.CreationTime);
            var newinfo = q.ToList();
            this.dataGridView1.DataSource = newinfo;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.lblMaster.Text = "Big Files";
            System.IO.DirectoryInfo dirs = new System.IO.DirectoryInfo(@"c:\windows");
            FileInfo[] files = dirs.GetFiles();

            var q = from f in files
                    where f.Length > 10000
                    select f;
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int year;
            int.TryParse(this.comboBox1.Text, out year);

            var q = from o in this.nwDataSet1.Orders
                    where o.OrderDate.Year == year
                    select o;
            this.bindingSource1.DataSource = q.ToList();
            this.dataGridView1.DataSource = this.bindingSource1;
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {

        }
        int page = -1;
        int countPerPage = 10;
        private void button12_Click(object sender, EventArgs e)
        {
            int.TryParse(this.textBox1.Text, out countPerPage);

            page -= 1;
            this.dataGridView1.DataSource = this.nwDataSet1.Products.Skip(countPerPage * page).Take(countPerPage).ToList();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblMaster.Text = "Orders";

            int year = 1997;
            int.TryParse(this.comboBox1.Text, out year);

            var q = from o in this.nwDataSet1.Orders
                    where o.OrderDate.Year == year
                    select o;

            this.bindingSource1.DataSource = q.ToList();
            this.dataGridView1.DataSource = this.bindingSource1;


        }
    }
}
