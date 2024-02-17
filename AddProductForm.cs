using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;

namespace INSERT__UPDATE_and_DELETE_with_MySQL_Database
{
    public partial class AddProductForm : Form
    {
        public Form1 form1;

        public AddProductForm()
        {
            InitializeComponent();
            pictureBox1.Image = Image.FromFile(Application.StartupPath + @"\Image\Noting.jpg");
        }
        private void AddProductForm_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textImage1.Text.Length == 0)
            {
                MessageBox.Show("Please Add \nImage of your Product ");
                return;
            }

            if  (Pname.Text != "" && Price.Text != "" && Qty.Text != "")
            {

                string connection = "datasource=localhost;port=3306;username=root;password=secret";
                string query = "INSERT INTO frozenproducts.new_table(Product_Name,Price,Quantity,Date_Delivered,Path_Image)VALUES('" + this.Pname.Text + "','" + this.Price.Text + "','" + this.Qty.Text + "','" + this.DDelivered.Text + "','" + Path.GetFileName(pictureBox1.ImageLocation) + "')";
                MySqlConnection conn = new MySqlConnection(connection);
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader dr;
                conn.Open();
                dr = cmd.ExecuteReader();
                File.Copy(textImage1.Text, Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10)) + @"\\DataPictures\" + Path.GetFileName(pictureBox1.ImageLocation));
                //DOUBLE BACK SAVING IMAGE DATA



                MessageBox.Show("Product successfully Saved");
                conn.Close();
                this.Close();
            }
            else
            {
                MessageBox.Show("Fill required fields");
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            if (open.ShowDialog() == DialogResult.OK)
            {
                textImage1.Text = open.FileName;
                pictureBox1.Image = new Bitmap(open.FileName);
                pictureBox1.ImageLocation = open.FileName;

            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
