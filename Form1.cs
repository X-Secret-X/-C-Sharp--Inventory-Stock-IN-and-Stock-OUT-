using MySql.Data.MySqlClient;
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
using System.IO;

namespace INSERT__UPDATE_and_DELETE_with_MySQL_Database
{
    public partial class Form1 : Form
    {
        MySqlConnection con = new MySqlConnection(@"datasource=localhost;port=3306;username=root;password=secret");
        public static string Pnumber = "";
        public static string Productname = "";
        public static string Prisyo = "";
        public static string Qtytwo = "";
        public static string DateDlvr = "";
        public static string Picsure = "";

       public Form1()
        {
            InitializeComponent();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;//DataGridView adjust by Column
            txtuser.Text = LogIn.LoginT;
        }
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            button1.PerformClick();
        }
        public string actorID;

        public void GetData()
        {
            
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM frozenproducts.new_table", con);
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = cmd;
            DataTable dtable = new DataTable();
            adapter.Fill(dtable);

            dtable.Columns.Add("Pic", Type.GetType("System.Byte[]"));//add image in a data grid view
            foreach (DataRow row in dtable.Rows)
            {
                // row["Pic"] = File.ReadAllBytes(Application.StartupPath + @"\Image\" + Path.GetFileName(row["Path_Image"].ToString()));
                row["Pic"] = File.ReadAllBytes(Application.StartupPath.Substring(0, Application.StartupPath.Length - 10) + @"\DataPictures\" + Path.GetFileName(row["Path_Image"].ToString())); ;

            }

            //BindingSource bSource = new BindingSource();
            //bSource.DataSource = dtable;
            dataGridView1.DataSource = dtable;                         //Function to View all Data in Data Grid view
            //adapter.Update(dtable);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetData();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {

                //My View Location when you click DataGridView row
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                UIdLabel.Text = row.Cells["id_product"].Value.ToString();
                Pname.Text = row.Cells["Product_Name"].Value.ToString();
                Price.Text = row.Cells["Price1"].Value.ToString();
                Qty.Text = row.Cells["Quantity"].Value.ToString();
                DDelivered.Text = row.Cells["Date_Delivered"].Value.ToString();
                PathImage.Text = row.Cells["Path_Image"].Value.ToString();
                //row["Pic"] = File.ReadAllBytes(Application.StartupPath + @"\Image\" + Path.GetFileName(row["Path_Image"].ToString()));
                string nam = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10)) + @"\DataPictures\";
                pictureBox1.Image = Image.FromFile(nam + row.Cells["Path_Image"].Value.ToString());
                //pictureBox1.Image = Image.FromFile(row.Cells[6].Value.ToString());



            }
            Pnumber = UIdLabel.Text;
            Productname = Pname.Text;
            Prisyo = Price.Text;
            Qtytwo = Qty.Text;
            DateDlvr = DDelivered.Text;
            Picsure = PathImage.Text;





        }
        public void ClearTextBox()
        {


            UIdLabel.Text = "000";
            Pname.Text = "";
            Price.Text = "";
            Qty.Text = "";
            DDelivered.Text = "";
            pictureBox1.Image = Image.FromFile("C:\\Users\\Daryl Roy\\source\\repos\\C# with MySQL Database\\DataPictures\\Loading.gif");

        }
        private void button2_Click(object sender, EventArgs e)//Add Button
        {
            string connection = "datasource=localhost;port=3306;username=root;password=secret";
            string query = "INSERT INTO frozenproducts.new_table(Product_Name,Price,Quantity,Date_Delivered)VALUES('" + this.Pname.Text + "','" + this.Price.Text + "','" + this.Qty.Text + "','" + this.DDelivered.Text + "')";
            MySqlConnection conn = new MySqlConnection(connection);
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader dr;
            conn.Open();
            dr = cmd.ExecuteReader();
            MessageBox.Show("Succesfully Saved");
            conn.Close();
            ClearTextBox();
            GetData();
        }

        private void button4_Click(object sender, EventArgs e)//Update Button
        {
            string connection = "datasource=localhost;port=3306;username=root;password=secret";
            //string query = "UPDATE sakila.actor SET first_name='" + this.Pname.Text + "', last_name='"+ this.Price.Text+ "' WHERE actor_id='"+ this.actorID + "'";
            string query = "UPDATE frozenproducts.new_table SET Product_Name='" + this.Pname.Text + "', Price='" + this.Price.Text + "',Quantity='" + this.Qty.Text + "',Date_Delivered='" + this.DDelivered.Text + "' WHERE id_product='" + this.UIdLabel.Text + "'";
            MySqlConnection conn = new MySqlConnection(connection);
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader dr;
            conn.Open();
            dr = cmd.ExecuteReader();
            MessageBox.Show("Succesfully UPDATE");
            conn.Close();
            ClearTextBox();
            GetData();
        }

        private void DeleteBtn_Click(object sender, EventArgs e)//Delete Button
        {
            string message = $"Do you want to Delete \n {Pname.Text}";
            string title = "Delete";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.Yes)
            {

                string connection = "datasource=localhost;port=3306;username=root;password=secret";
                string query = "DELETE FROM frozenproducts.new_table WHERE id_product='" + this.UIdLabel.Text + "'";

                MySqlConnection conn = new MySqlConnection(connection);
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader dr;
                conn.Open();
                dr = cmd.ExecuteReader();
                MessageBox.Show($"Succesfully DELETED {Pname.Text} {Price.Text}");
                conn.Close();
                ClearTextBox();
                GetData();
            }
            else
            {
                
            }



        }

        private void AddProductBTN_Click(object sender, EventArgs e)
        {
            new AddProductForm().ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Pname.Text == "")
            {
                MessageBox.Show("Please Select Data that you want to Stock Out.");
                return;
            }
            new SellProductForm().ShowDialog();




        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }



        private void txtuser_Click(object sender, EventArgs e)
        {

        }
    }
}
