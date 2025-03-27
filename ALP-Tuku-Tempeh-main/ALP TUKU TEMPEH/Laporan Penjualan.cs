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
using MySql.Data;
using MySql.Data.MySqlClient;

namespace ALP_TUKU_TEMPEH
{
    public partial class Laporan_Penjualan : Form
    {
        MySqlConnection conniiihuu;
        MySqlCommand sqlCommand;
        MySqlDataAdapter mySqlDataAdapter;
        MySqlDataReader mySqlDataReader;
        DataTable dt = new DataTable();
        DataTable dk = new DataTable();
        DataTable dm = new DataTable();
        public static int sudahbuka = 0;
        public Laporan_Penjualan()
        {
            InitializeComponent();
            string connection = "server=localhost;user=root;pwd=root123456@;database=db_tukutempeh"; //menghubungkan database
            conniiihuu = new MySqlConnection(connection);
        }

        private void Laporan_Penjualan_Load(object sender, EventArgs e)
        {
            
        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            string tglawal = dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss");
            string tglakhir = dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss");
            if (textBox1.Text == "" && comboBox1.Text == "")
            {
                dt.Clear();
                string cmd1 = "call plaporanpenjualanberdasarkantglaja('"+tglawal+"','"+tglakhir+"');";
                //string cmd1 = "select p.id_penjualan as 'ID Penjualan', c.nama_customer as 'Nama Cust', p.tanggal_penjualan as 'Tanggal', p.total_penjualan as 'Total' from penjualan p, customer c where c.status_del='F' and c.id_customer = p.id_customer and tanggal_penjualan between @d1 and @d2;";
               sqlCommand = new MySqlCommand(cmd1, conniiihuu);
              // sqlCommand.Parameters.Add("@d1", MySqlDbType.Date).Value = dateTimePicker1.Value.Date;
               //sqlCommand.Parameters.Add("@d2", MySqlDbType.Date).Value = dateTimePicker2.Value.Date;
                mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
                mySqlDataAdapter.Fill(dt);
                dataGridView1.DataSource = dt;
                int rowdn = dt.Rows.Count;
                dataGridView1.Columns[2].DefaultCellStyle.Format = "dd/MM/yyyy";
                //dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
                int rowdt = dt.Rows.Count;
                if (rowdt == 0)
                {
                    MessageBox.Show("ID Penjualan tidak ditemukan");
                    dk.Clear();
                    textBox1.Text = "";
                    textBox3.Text = string.Empty;
                    dt.Clear();
                    string cmdc = "select p.id_penjualan as 'ID Penjualan', c.nama_customer as 'Nama Cust', p.tanggal_penjualan as 'Tanggal', p.total_penjualan as 'Total' from penjualan p, customer c where c.status_del='F' and c.id_customer = p.id_customer;";
                    sqlCommand = new MySqlCommand(cmdc, conniiihuu);
                    mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
                    mySqlDataAdapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                    rowdt = dt.Rows.Count;
                    int hitung = 0;
                    for (int a = 0; a < rowdt; a++)
                    {
                        hitung += Convert.ToInt32(dataGridView1.Rows[a].Cells["Total"].Value);
                    }
                    textBox3.Text = hitung.ToString();
                }
                else
                {
                    dataGridView1.DataSource = dt;
                    int hitung = 0;
                    for (int a = 0; a < rowdt; a++)
                    {
                        hitung += Convert.ToInt32(dataGridView1.Rows[a].Cells["Total"].Value);
                    }
                    textBox3.Text = hitung.ToString();
                }
            }
            if (textBox1.Text.Length != 0 && comboBox1.Text == "")
            {
                dt.Clear();
                string cmd1 = "call plaporanpenjualanberdasarkanidpenjualan ('" + tglawal + "','" + tglakhir + "','" + textBox1.Text + "');";
                //string cmd1 = "select p.id_penjualan as 'ID Penjualan', c.nama_customer as 'Nama Cust', p.tanggal_penjualan as 'Tanggal', p.total_penjualan as 'Total' from penjualan p, customer c where c.status_del='F' and c.id_customer = p.id_customer and tanggal_penjualan between @d1 and @d2 and p.id_penjualan='"+textBox1.Text+"';";
                sqlCommand = new MySqlCommand(cmd1, conniiihuu);
                mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
                mySqlDataAdapter.Fill(dt);
                dataGridView1.DataSource = dt;
                int rowdt = dt.Rows.Count;
                dataGridView1.Columns[2].DefaultCellStyle.Format = "dd/MM/yyyy";
                //dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
                if (rowdt == 0)
                {
                    MessageBox.Show("ID Penjualan tidak ditemukan");
                    dk.Clear();
                    textBox1.Text = "";
                    textBox3.Text = string.Empty;
                    dt.Clear();
                    string cmdc = "select p.id_penjualan as 'ID Penjualan', c.nama_customer as 'Nama Cust', p.tanggal_penjualan as 'Tanggal', p.total_penjualan as 'Total' from penjualan p, customer c where c.status_del='F' and c.id_customer = p.id_customer;";
                    sqlCommand = new MySqlCommand(cmdc, conniiihuu);
                    mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
                    mySqlDataAdapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                    rowdt = dt.Rows.Count;
                    int hitung = 0;
                    for (int a = 0; a < rowdt; a++)
                    {
                        hitung += Convert.ToInt32(dataGridView1.Rows[a].Cells["Total"].Value);
                    }
                    textBox3.Text = hitung.ToString();
                }
                else
                {
                    dataGridView1.DataSource = dt;
                    int hitung = 0;
                    for (int a = 0; a < rowdt; a++)
                    {
                        hitung += Convert.ToInt32(dataGridView1.Rows[a].Cells["Total"].Value);
                    }
                    textBox3.Text = hitung.ToString();
                }
            }
            if(textBox1.Text.Length == 0 && comboBox1.Text.Length !=0)
            {
                dt.Clear();
                string cmd1 = "call plaporanpenjualantgldancostumer ('" + tglawal + "','" + tglakhir + "','" + comboBox1.Text + "');";
                //string cmd1 = "select p.id_penjualan as 'ID Penjualan', c.nama_customer as 'Nama Cust', p.tanggal_penjualan as 'Tanggal', p.total_penjualan as 'Total' from penjualan p, customer c where c.status_del='F' and c.id_customer = p.id_customer and tanggal_penjualan between @d1 and @d2 and c.nama_customer='" + comboBox1.Text + "';";
                sqlCommand = new MySqlCommand(cmd1, conniiihuu);
                //sqlCommand.Parameters.Add("@d1", MySqlDbType.Date).Value = dateTimePicker1.Value.Date;
                //sqlCommand.Parameters.Add("@d2", MySqlDbType.Date).Value = dateTimePicker2.Value.Date;
                mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
                mySqlDataAdapter.Fill(dt);
                dataGridView1.DataSource = dt;
                int rowdt = dt.Rows.Count;
                dataGridView1.Columns[2].DefaultCellStyle.Format = "dd/MM/yyyy";
                //dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
                if (rowdt == 0)
                {
                    MessageBox.Show("ID Penjualan tidak ditemukan");
                    dk.Clear();
                    textBox1.Text = "";
                    textBox3.Text = string.Empty;
                    dt.Clear();
                    string cmdc = "select p.id_penjualan as 'ID Penjualan', c.nama_customer as 'Nama Cust', p.tanggal_penjualan as 'Tanggal', p.total_penjualan as 'Total' from penjualan p, customer c where c.status_del='F' and c.id_customer = p.id_customer;";
                    sqlCommand = new MySqlCommand(cmdc, conniiihuu);
                    mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
                    mySqlDataAdapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                    rowdt = dt.Rows.Count;
                    int hitung = 0;
                    for (int a = 0; a < rowdt; a++)
                    {
                        hitung += Convert.ToInt32(dataGridView1.Rows[a].Cells["Total"].Value);
                    }
                    textBox3.Text = hitung.ToString();
                }
                else
                {
                    dataGridView1.DataSource = dt;
                    int hitung = 0;
                    for (int a = 0; a < rowdt; a++)
                    {
                        hitung += Convert.ToInt32(dataGridView1.Rows[a].Cells["Total"].Value);
                    }
                    textBox3.Text = hitung.ToString();
                }

            }
            if(textBox1.Text.Length !=0 && comboBox1.Text.Length !=0)
            {
                dt.Clear();
                string cmd1 = "call plaporanpenjualanberdasarkansemua ('" + tglawal + "','" + tglakhir + "','" + comboBox1.Text + "','"+ textBox1.Text+"');";
                // string cmd1 = "select p.id_penjualan as 'ID Penjualan', c.nama_customer as 'Nama Cust', p.tanggal_penjualan as 'Tanggal', p.total_penjualan as 'Total' from penjualan p, customer c where c.status_del='F' and c.id_customer = p.id_customer and tanggal_penjualan between @d1 and @d2 and c.nama_customer='" + comboBox1.Text + "' and p.id_penjualan='"+textBox1.Text+"';";
                sqlCommand = new MySqlCommand(cmd1, conniiihuu);
                //sqlCommand.Parameters.Add("@d1", MySqlDbType.Date).Value = dateTimePicker1.Value.Date;
                //sqlCommand.Parameters.Add("@d2", MySqlDbType.Date).Value = dateTimePicker2.Value.Date;
                mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
                mySqlDataAdapter.Fill(dt);
                dataGridView1.DataSource = dt;
                dataGridView1.Columns[2].DefaultCellStyle.Format = "dd/MM/yyyy";
               // dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
                int rowdt = dt.Rows.Count;
                if (rowdt == 0)
                {
                    MessageBox.Show("ID Penjualan tidak ditemukan");
                    dk.Clear();
                    textBox1.Text = "";
                    textBox3.Text = string.Empty;
                    dt.Clear();
                    string cmdc = "select p.id_penjualan as 'ID Penjualan', c.nama_customer as 'Nama Cust', p.tanggal_penjualan as 'Tanggal', p.total_penjualan as 'Total' from penjualan p, customer c where c.status_del='F' and c.id_customer = p.id_customer;";
                    sqlCommand = new MySqlCommand(cmdc, conniiihuu);
                    mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
                    mySqlDataAdapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                    rowdt = dt.Rows.Count;
                    int hitung = 0;
                    for (int a = 0; a < rowdt; a++)
                    {
                        hitung += Convert.ToInt32(dataGridView1.Rows[a].Cells["Total"].Value);
                    }
                    textBox3.Text = hitung.ToString();
                }
                else
                {
                    dataGridView1.DataSource = dt;
                    int hitung = 0;
                    for (int a = 0; a < rowdt; a++)
                    {
                        hitung += Convert.ToInt32(dataGridView1.Rows[a].Cells["Total"].Value);
                    }
                    textBox3.Text = hitung.ToString();
                }
            }
        }

        private void Laporan_Penjualan_Load_1(object sender, EventArgs e)
        {
            textBox3.Text = string.Empty;
            textBox3.Enabled = false;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightYellow;
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.LightYellow;
            dm.Clear();
            string cmd = "call pcostumer();";
            sqlCommand = new MySqlCommand(cmd, conniiihuu);
            mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
            mySqlDataAdapter.Fill(dm);
            comboBox1.DataSource = dm;
            comboBox1.DisplayMember = "nama_customer";
            comboBox1.Text = "";
          
            if (sudahbuka == 0)
            {
                comboBox1.Text = string.Empty;
            
            }

            comboBox1.Text = string.Empty;
            dt.Clear();
            string cmdc = "select * from vlappenjualan;";
            sqlCommand = new MySqlCommand(cmdc, conniiihuu);
            mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
            mySqlDataAdapter.Fill(dt);
            dataGridView1.DataSource = dt;
            int rowdn = dt.Rows.Count;
            int hitung = 0;
            int angka = 0;
            dataGridView1.Columns[2].DefaultCellStyle.Format = "dd/MM/yyyy";
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightYellow;
            for (int a = 0; a < rowdn; a++)
            {

                angka = Convert.ToInt32(dataGridView1.Rows[a].Cells[3].Value);
                hitung = hitung + angka;

            }
            textBox3.Text = hitung.ToString();
            textBox1.Text = string.Empty;

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string idbarangyangdiklik = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            dk.Clear();
            string cmd1 = "select d.id_barang as 'Kode Barang', b.nama_barang as 'Nama barang', d.jumlah_barangjual as 'Jumlah', d.harga_jual as 'Harga', d.subtotal as 'Subtotal' from detail_penjualan d, barang b where d.id_penjualan = '" + idbarangyangdiklik + "' and d.status_del = 'F' and d.id_barang = b.id_barang;";
            sqlCommand = new MySqlCommand(cmd1, conniiihuu);
            mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
            mySqlDataAdapter.Fill(dk);
            dataGridView2.DataSource = dk;
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Homepage homepage = new Homepage();
            homepage.Show();
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
