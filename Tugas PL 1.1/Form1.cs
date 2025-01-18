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
using System.Security.Cryptography;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        MySqlCommand cmd;
        MySqlConnection connection;
        MySqlDataReader dr;
        public Form1()
        {
            InitializeComponent();
            txtPassword.Text = "";
            // The password character is an asterisk.
            txtPassword.PasswordChar = '*';
            // The control will allow no more than 14 characters.
            txtPassword.MaxLength = 14;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            Application.Exit();
            //this.Hide();
            //Register form = new Register();
            //form.Show();

        }


        public static string hashPassword(string password)
        {
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();

            byte[] password_bytes = Encoding.ASCII.GetBytes(password);
            byte[] encripted_bytes = sha1.ComputeHash(password_bytes);
            return Convert.ToBase64String(encripted_bytes);
        }
        private void button1_Click(object sender, EventArgs e)
        {
   
            string server = "server=localhost;database=tugasPL;userid=root;password=''";
            connection = new MySqlConnection(server);
            connection.Open();
            try
            {


                cmd = new MySqlCommand("Select * From login where username ='" + txtUserName.Text + "' and Password='" + hashPassword(txtPassword.Text )+ "'", connection);
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    MessageBox.Show("Login Berhasil", "berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    Form2 form = new Form2();
                    form.Show();
                }
                else
                {
                    MessageBox.Show("Tidak masuk", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

  
    }   
}
