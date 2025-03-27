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

namespace ALP_TUKU_TEMPEH
{
    public partial class Form4 : Form
    {
        MySqlConnection connijiwa;
        MySqlCommand sqlCommand;
        MySqlDataAdapter mySqlDataAdapter;
        MySqlDataReader mySqlDataReader;
        DataTable dr = new DataTable();
        DataTable dn = new DataTable();
        
        public Form4()
        {
            InitializeComponent();
            string connection = "server=localhost;user=root;pwd=root123456@;database=db_tukutempeh"; //menghubungkan database
            connijiwa = new MySqlConnection(connection);
            textBox1.MaxLength = 6;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            dr.Clear();
            string cmd = "select * from vdetailbarang;";
            sqlCommand = new MySqlCommand(cmd, connijiwa);
            mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
            mySqlDataAdapter.Fill(dr);
            dataGridView1.DataSource = dr;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightYellow;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dn.Clear();
           textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
           textBox5.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            string cmmd = "select harga_beli, harga_jual from barang where id_barang='" + textBox1.Text + "';";
            sqlCommand = new MySqlCommand(cmmd, connijiwa);
            mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
            mySqlDataAdapter.Fill(dn);
           textBox3.Text = dn.Rows[0]["harga_beli"].ToString();
           textBox4.Text = dn.Rows[0]["harga_jual"].ToString();
            textBox1.Enabled = false; textBox2.Enabled = false;
            textBox3.Enabled = false; textBox4.Enabled = false;
            textBox5.Enabled = false;

            

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // Jika bukan digit, membatalkan input
                e.Handled = true;
            }

        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // Jika bukan digit, membatalkan input
                e.Handled = true;
            }

        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // Jika bukan digit, membatalkan input
                e.Handled = true;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dn.Clear();
            string cmd = "select fGenIdbarang();";
            sqlCommand = new MySqlCommand(cmd, connijiwa);
            mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
            mySqlDataAdapter.Fill(dn);
            textBox1.Text = dn.Rows[0]["fGenIdbarang()"].ToString();
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = 0.ToString();
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int cekapakahkodebarangada = 0;
            if(textBox1.Enabled==true)
            {
                if (textBox1.Text.Length != 0 || textBox2.Text.Length != 0 || textBox3.Text.Length != 0 || textBox4.Text.Length != 0 || textBox5.Text.Length != 0)
                {
                    dn.Clear();
                    string command = "select id_barang \r\nfrom barang;";
                    sqlCommand = new MySqlCommand(command, connijiwa);
                    mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
                    mySqlDataAdapter.Fill(dn);
                    for (int a =0; a<dn.Rows.Count; a++)
                    {
                        if (dn.Rows[a]["id_barang"].ToString()==textBox1.Text)
                        {
                            MessageBox.Show("Kode barang sudah ada");
                            cekapakahkodebarangada++;
                        }
                    }
                    if (cekapakahkodebarangada == 0)
                    {
                        dn.Clear();
                        if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "")
                        {
                            MessageBox.Show("Harap diisi dulu");
                        }
                        else
                        {
                         
                            string cmmmmd = $"insert into barang values ('{textBox1.Text}', '{textBox2.Text}', '{textBox3.Text}','{textBox4.Text}','{textBox5.Text}','F');";
                            sqlCommand = new MySqlCommand(cmmmmd, connijiwa);
                            mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
                            mySqlDataAdapter.Fill(dn);
                            dr.Clear();
                            string cmd = "select id_barang as 'Id', nama_barang as 'Nama Barang', Stok\r\nfrom barang\r\nwhere status_del = 'F';";
                            sqlCommand = new MySqlCommand(cmd, connijiwa);
                            mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
                            mySqlDataAdapter.Fill(dr);
                            dataGridView1.DataSource = dr;
                            textBox1.Text = string.Empty;
                            textBox2.Text = string.Empty;
                            textBox3.Text = string.Empty;
                            textBox4.Text = string.Empty;
                            textBox5.Text = 0.ToString();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Harap isi detail barang dengan lengkap");
                }

            }
            else
            {
                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "")
                {

                    MessageBox.Show("Harap isi detail barang dengan lengkap");
                }
                else
                {
               
                    dn.Clear();
                    string cmmmmd = $"update barang set nama_barang='" + textBox2.Text + "',harga_beli='" + textBox3.Text + "',harga_jual='" + textBox4.Text + "' where id_barang='" + textBox1.Text + "';";
                    sqlCommand = new MySqlCommand(cmmmmd, connijiwa);
                    mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
                    mySqlDataAdapter.Fill(dn);
                    dr.Clear();
                    string cmd = "select id_barang as 'Id', nama_barang as 'Nama Barang', Stok\r\nfrom barang\r\nwhere status_del = 'F';";
                    sqlCommand = new MySqlCommand(cmd, connijiwa);
                    mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
                    mySqlDataAdapter.Fill(dr);
                    dataGridView1.DataSource = dr;
                    textBox1.Text = string.Empty;
                    textBox2.Text = string.Empty;
                    textBox3.Text = string.Empty;
                    textBox4.Text = string.Empty;
                    textBox5.Text = 0.ToString();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            textBox4.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dn.Clear();
            string cmmmmd = $"update barang set status_del='T' where id_barang='" + textBox1.Text + "';";
            sqlCommand = new MySqlCommand(cmmmmd, connijiwa);
            mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
            mySqlDataAdapter.Fill(dn);
            dr.Clear();
            string cmd = "select id_barang as 'Id', nama_barang as 'Nama Barang', Stok\r\nfrom barang\r\nwhere status_del = 'F';";
            sqlCommand = new MySqlCommand(cmd, connijiwa);
            mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
            mySqlDataAdapter.Fill(dr);
            dataGridView1.DataSource = dr;
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = 0.ToString();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Homepage hm = new Homepage();
            hm.Show();
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
