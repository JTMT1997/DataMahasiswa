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
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
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
            string connection = "server=localhost;database=tugasPL;userid=root;password=''";
            string query = "insert into login values('" + this.textBox1.Text + "','" + hashPassword(this.textBox2.Text) + "')";
            MySqlConnection conn = new MySqlConnection(connection);
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader dr;
            conn.Open();
            dr = cmd.ExecuteReader();
            MessageBox.Show("sucess Save");
            conn.Close();
        }
    }
}
