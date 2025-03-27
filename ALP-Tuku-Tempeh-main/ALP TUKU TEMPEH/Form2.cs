using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace ALP_TUKU_TEMPEH
{
    public partial class ForgotPass : Form
    {
        MySqlConnection conn;
        MySqlCommand sqlCommand;
        MySqlDataAdapter mySqlDataAdapter;
        MySqlDataReader mySqlDataReader;
        DataTable dy = new DataTable();
        public ForgotPass()
        {
            InitializeComponent();
            string connection = "server=localhost;user=root;pwd=root123456@;database=db_tukutempeh"; //menghubungkan database
            conn = new MySqlConnection(connection);
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void ForgotPass_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dy.Clear();
            string username = tb_username.Text;
            string password = tb_password.Text;
            string confirmpasword = textBox1.Text;
            string cmdcekusername = "select username,pass from login where username='" + username + "';";
            sqlCommand = new MySqlCommand(cmdcekusername, conn);
            mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
            mySqlDataAdapter.Fill(dy);
            if(dy.Rows.Count == 1)
            {
                if(password == confirmpasword)
                {
                    string cmdupdatepass = "update login set `username` = '" + username+ "', `pass` ='" + password+  "' where username ='" + username + "';";
                    try
                    {
                        conn.Open();
                        sqlCommand = new MySqlCommand(cmdupdatepass, conn);
                        mySqlDataReader = sqlCommand.ExecuteReader();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        conn.Close();
                        FormLogin frmlogin = new FormLogin();
                        frmlogin.Show();
                        this.Close();
                    }
                }
                if (password !=  confirmpasword) 
                {
                    MessageBox.Show("Password dan Confirm Password berbeda");
                }
            }
            else
            {
                MessageBox.Show("Username tidak ditemukan");
            }


        }

        private void tb_username_TextChanged(object sender, EventArgs e)
        {

        }

        private void tb_username_Click(object sender, EventArgs e)
        {
            tb_username.Text = string.Empty;
        }

        private void tb_password_Click(object sender, EventArgs e)
        {
            tb_password.Text = string.Empty;

        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
        }
    }
}
