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
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ALP_TUKU_TEMPEH
{
    public partial class Form5 : Form
    {
        MySqlConnection conniii;
        MySqlCommand sqlCommand;
        MySqlDataAdapter mySqlDataAdapter;
        MySqlDataReader mySqlDataReader;
        DataTable dp = new DataTable();
        DataTable dk = new DataTable();
        int mauedit = 0;
        public static string namacust = "";

        public Form5()
        {
            InitializeComponent();
            string connection = "server=localhost;user=root;pwd=root123456@;database=db_tukutempeh"; //menghubungkan database
            conniii = new MySqlConnection(connection);

        }

        private void Form5_Load(object sender, EventArgs e)
        {
            string command = "select * from vCustomer;";
            sqlCommand = new MySqlCommand(command, conniii);
            mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
            mySqlDataAdapter.Fill(dp);
            dataGridView1.DataSource = dp;
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled= false;
            textBox4.Enabled = false;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightYellow;
            button3.Enabled = false;
            button3.Enabled = true;
            dk.Clear();
            string comand = "select fGenIdcustomer();";
            sqlCommand = new MySqlCommand(comand, conniii);
            mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
            mySqlDataAdapter.Fill(dk);
            textBox1.Text = dk.Rows[0]["fGenIdcustomer()"].ToString();
            //int jumlahdk = dk.Rows.Count;
            //if (jumlahdk < 9)
            //{
            //    textBox1.Text = "C0000" + (jumlahdk + 1);
            //}
            //if (jumlahdk >= 9)
            //{
            //    textBox1.Text = "C000" + (jumlahdk + 1);
            //}
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string idyangdiklik = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            dk.Clear();
            string cmd = "select email, no_telpon from customer where status_del = 'F' and id_customer='" + idyangdiklik + "'; ";
            sqlCommand = new MySqlCommand(cmd, conniii);
            mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
            mySqlDataAdapter.Fill(dk);
            textBox1.Text = idyangdiklik;
            textBox2.Text = dataGridView1.CurrentRow.Cells["Nama"].Value.ToString();
            textBox3.Text = dk.Rows[0]["no_telpon"].ToString();
            textBox4.Text = dk.Rows[0]["email"].ToString();
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //button3.Enabled =true;
            //dk.Clear();
            //mauedit = 0;
            //mauedit++;
            //textBox1.Enabled=false;
            //textBox2.Enabled = true;
            //textBox3.Enabled = true;
            //textBox4.Enabled = true;


        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "" || textBox3.Text != "" || textBox4.Text != "")
            {

                dk.Clear();
                string id_customer = textBox1.Text;
                string cmd = "select id_customer from customer where id_customer='" + id_customer + "';";
                sqlCommand = new MySqlCommand(cmd, conniii);
                mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
                mySqlDataAdapter.Fill(dk);
                if (dk.Rows.Count == 0)
                {
                    dk.Clear();
                    string insertcust = $"insert into customer (nama_customer,no_telpon, email,status_del) values ('{textBox2.Text}', '{textBox3.Text}','{textBox4.Text}','F');";
                    sqlCommand = new MySqlCommand(insertcust, conniii);
                    mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
                    mySqlDataAdapter.Fill(dk);
                }
                if (Transaksi_Penjualan.sudahbukaformini > 0)
                {
                    namacust = textBox2.Text;
                    Transaksi_Penjualan.sudahbukaformini = 2;
                    Transaksi_Penjualan frm = new Transaksi_Penjualan();
                    frm.Show();
                    this.Close();
                }
                if (Laporan_Penjualan.sudahbuka > 0)
                {
                    namacust = textBox2.Text;
                    Laporan_Penjualan.sudahbuka = 2;
                    Laporan_Penjualan frm1 = new Laporan_Penjualan();
                    frm1.Show();
                    this.Close();

                }
            }
            else
            {
                MessageBox.Show("Harap isi dulu");
            }
           
            
            //else
            //{
            //    dk.Clear();
            //    string cmd = "Update customer set nama_customer='" + textBox2.Text + "', no_telpon='" + textBox3.Text + "', email='" + textBox4.Text + "' where id_customer='" + textBox1.Text +"';";
            //    sqlCommand = new MySqlCommand(cmd, conniii);
            //    mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
            //    mySqlDataAdapter.Fill(dk);
            //    mauedit = 0;
            //    dp.Clear();
            //    string command = "select id_customer as 'ID', nama_customer as'Nama' from customer where status_del = 'F';";
            //    sqlCommand = new MySqlCommand(command, conniii);
            //    mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
            //    mySqlDataAdapter.Fill(dp);
            //    dataGridView1.DataSource = dp;
            //    textBox1.Text = string.Empty;
            //    textBox2.Text = string.Empty;
            //    textBox3.Text = string.Empty;
            //    textBox4.Text = string.Empty;
            //    textBox1.Enabled = false;
            //    textBox2.Enabled = false;
            //    textBox3.Enabled = false;
            //    textBox4.Enabled = false;
         
            //}
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
