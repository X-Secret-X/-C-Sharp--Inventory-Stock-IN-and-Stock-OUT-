using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Runtime.CompilerServices;
using System.IO;

namespace INSERT__UPDATE_and_DELETE_with_MySQL_Database
{
    public partial class SellProductForm : Form
    {
        MySqlConnection con = new MySqlConnection(@"datasource=localhost;port=3306;username=root;password=secret");

        public SellProductForm()
        {
            DataTable dtable1 = new DataTable();
            InitializeComponent();
            label1.Text = Form1.Productname;
            label3.Text = Form1.Pnumber;
            label4.Text = Form1.Prisyo;
            label8.Text = Form1.Qtytwo;
            label9.Text = Form1.DateDlvr;
            textBox2.Text = Form1.Picsure;
            string nam = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10)) + @"\DataPictures\";
            pictureBox1.Image = Image.FromFile(nam + textBox2.Text);



        }

        private void label1_Click(object sender, EventArgs e)
        {
          

        }

        private void SellProductForm_Load(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "")
            {
                textBox1.BackColor = Color.DarkRed;
                MessageBox.Show("Input Data Quantity");
               
                return;
            }
            string connection = "datasource=localhost;port=3306;username=root;password=secret";
            string query = "UPDATE frozenproducts.new_table SET Quantity='" + this.label12.Text + "' WHERE id_product='" + this.label3.Text + "'";
            MySqlConnection conn = new MySqlConnection(connection);
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader dr;
            conn.Open();
            dr = cmd.ExecuteReader();
            MessageBox.Show("Succesfully Saved");
            conn.Close();

            this.Close();

        }


        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
    
            label3.Text = Form1.Pnumber;
            var Price1 = Convert.ToInt32(label4.Text);
            var TotalQty = Convert.ToInt32(label8.Text);
            var Resultqty = Convert.ToInt32(label12.Text);
            if (textBox1.Text == "")
            {
                //MessageBox.Show("Input Numbers Only");
                label14.Text = "000";
                label12.Text = "000";
                return;
            }
           
            var QtyDataInput = Convert.ToInt32(textBox1.Text);
            if (QtyDataInput >= TotalQty + 1)
            {
                MessageBox.Show("Please check your remaining quantity");
            }
            int priceXqty = Price1 * QtyDataInput;
            var allqty = TotalQty - QtyDataInput;
            label12.Text = allqty.ToString();
            label14.Text = priceXqty.ToString();

      
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
                MessageBox.Show("Input Numbers Only");
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }
    }
}
