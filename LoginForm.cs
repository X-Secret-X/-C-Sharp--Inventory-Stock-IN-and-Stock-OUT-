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

namespace INSERT__UPDATE_and_DELETE_with_MySQL_Database
{
    public partial class LogIn : Form
    {
        MySqlConnection con3 = new MySqlConnection(@"datasource=localhost;port=3306;username=root;password=secret");
        public static string LoginT= "";
        public LogIn()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (TextUserBox.Text != "" & TextPassBox.Text != "")
            {
                String UPass = "SELECT COUNT(*) FROM new_user_and_password.userpass WHERE username=@myuser AND password=@mypassword ";
                MySqlCommand cmd = new MySqlCommand(UPass, con3);
                con3.Open();
                cmd.Parameters.AddWithValue("@myuser", TextUserBox.Text);
                cmd.Parameters.AddWithValue("@mypassword", TextPassBox.Text);
                int myresult = Convert.ToInt32(cmd.ExecuteScalar());
                LoginT = TextUserBox.Text;
                if (myresult > 0)
                {
                    //  new Form1().ShowDialog();  --> show new form without closing previos winform

                    Form1 openform1 = new Form1();
                    openform1.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("User Not Found");

                }
                con3.Close();
            }
        }

        private void TextUserBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
