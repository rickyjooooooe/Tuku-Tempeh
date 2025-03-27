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
    public partial class FormLogin : Form
    {
        MySqlConnection conn;
        MySqlCommand sqlCommand;
        MySqlDataAdapter mySqlDataAdapter;
        MySqlDataReader mySqlDataReader;
        DataTable dt = new DataTable();
        DataTable dn = new DataTable();
        public static string username = "";
        public FormLogin()
        {
            InitializeComponent();
            string connection = "server=localhost;user=root;pwd=root123456@;database=db_tukutempeh"; //menghubungkan database
            conn = new MySqlConnection(connection);
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
       
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dt.Clear();
            string command = "select username,pass from login where username='"+tb_username.Text +"'&& pass='" + tb_password.Text + "';"; //ngecekapakah loginnya bener atau error
            sqlCommand = new MySqlCommand(command, conn);
            mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
            mySqlDataAdapter.Fill(dt);
            if (dt.Rows.Count == 1) //kalo bener username dan passwordnya pasti di sql munculnya 1 baris 
            {
                username = tb_username.Text;
                Homepage frm3 = new Homepage();
                frm3.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Login Failed"); //ini kalo login failed munculnya ini

            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
         
            ForgotPass forgot = new ForgotPass();
            forgot.Show();
            this.Close();
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
    }
}
