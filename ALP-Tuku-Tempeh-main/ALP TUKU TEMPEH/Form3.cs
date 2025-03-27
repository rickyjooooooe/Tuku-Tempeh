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
    public partial class Form3 : Form
    {
        MySqlConnection conny;
        MySqlCommand sqlCommand;
        MySqlDataAdapter mySqlDataAdapter;
        MySqlDataReader mySqlDataReader;
        DataTable dn = new DataTable();
        DataTable dk = new DataTable();
        public Form3()
        {
            InitializeComponent();
            string connection = "server=localhost;user=root;pwd=root123456@;database=db_tukutempeh"; //menghubungkan database
            conny = new MySqlConnection(connection);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.LightYellow;
            textBox2.MaxLength = 6;
            dn.Clear();
            string cmd = "select * from vlappembelian;";
            sqlCommand = new MySqlCommand(cmd, conny);
            mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
            mySqlDataAdapter.Fill(dn);
            dataGridView1.DataSource = dn;
            int rowdn = dn.Rows.Count;
            int hitung = 0;
            int angka = 0;
            dataGridView1.Columns[1].DefaultCellStyle.Format = "dd/MM/yyyy";
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightYellow;
            for (int a = 0; a < rowdn; a++)
            {

                angka = Convert.ToInt32(dataGridView1.Rows[a].Cells[2].Value);
                hitung = hitung + angka;

            }
            textBox1.Text = hitung.ToString();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string idbarangyangdiklik = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            dk.Clear();
            string cmd1 = "select d.id_barang as 'Kode Barang', b.nama_barang as 'Nama barang', d.jumlah_barangbeli as 'Jumlah', d.harga_beli as 'Harga', d.subtotal as 'Subtotal' from detail_pembelian d, barang b where d.id_pembelian = '" + idbarangyangdiklik + "' and d.status_del = 'F' and d.id_barang = b.id_barang;";
            sqlCommand = new MySqlCommand(cmd1, conny);
            mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
            mySqlDataAdapter.Fill(dk);
            dataGridView2.DataSource = dk;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tglawal = dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss");
            string tglakhir = dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss");
            if (textBox2.Text == "")
            {
                dn.Clear();
                string cmd1= "call plaporanpembelianberdasarkantgl('" + tglawal + "','" + tglakhir + "');";
                //string cmd1 = "select id_pembelian as 'Id Beli', tanggal_pembelian as 'Tanggal', total_pembelian as 'Subtotal' from pembelian where status_del = 'F' and tanggal_pembelian between @d1 and @d2;";
                sqlCommand = new MySqlCommand(cmd1, conny);
               // sqlCommand.Parameters.Add("@d1", MySqlDbType.Date).Value = dateTimePicker1.Value.Date;
               // sqlCommand.Parameters.Add("@d2", MySqlDbType.Date).Value = dateTimePicker2.Value.Date;
                mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
                mySqlDataAdapter.Fill(dn);
                dataGridView1.DataSource = dn;
                int rowdn = dn.Rows.Count;
                int hitung = 0;
                int angka = 0;

                if (rowdn == 0)
                {
                    MessageBox.Show("ID Pembelian tidak ditemukan");
                    dk.Clear();
                    textBox1.Text = "";
                    dn.Clear();
                    string cmd = "select id_pembelian as 'Id Beli', tanggal_pembelian as 'Tanggal', total_pembelian as 'Subtotal'from pembelian where status_del = 'F';";
                    sqlCommand = new MySqlCommand(cmd, conny);
                    mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
                    mySqlDataAdapter.Fill(dn);
                    dataGridView1.DataSource = dn;
                    textBox1.Enabled = false;
                    rowdn = dn.Rows.Count;
                    hitung = 0;
                    angka = 0;
                    for (int a = 0; a < rowdn; a++)
                    {

                        angka = Convert.ToInt32(dataGridView1.Rows[a].Cells[2].Value);
                        hitung = hitung + angka;

                    }
                    textBox1.Text = hitung.ToString();
                }
                dataGridView1.Columns[1].DefaultCellStyle.Format = "dd/MM/yyyy";
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightYellow;
                hitung = 0;
                angka = 0;
                for (int a = 0; a < rowdn; a++)
                {

                    angka = Convert.ToInt32(dataGridView1.Rows[a].Cells[2].Value);
                    hitung = hitung + angka;

                }
                textBox1.Text = hitung.ToString();

            }
            else
            {
                dn.Clear();

                try
                {
                    string cmd1 = "call plaporanpembelianberdasarkansemua ('" + tglawal + "','" + tglakhir + "','" + textBox2.Text + "');";
                    //string cmd1 = "SELECT id_pembelian as 'Id Beli', tanggal_pembelian as 'Tanggal', total_pembelian as 'Subtotal' FROM pembelian WHERE status_del = 'F' AND id_pembelian = @id_pembelian AND tanggal_pembelian BETWEEN @d1 AND @d2;";
                    sqlCommand = new MySqlCommand(cmd1, conny);
                    //sqlCommand.Parameters.Add("@id_pembelian", MySqlDbType.VarChar).Value = textBox2.Text;
                    //sqlCommand.Parameters.Add("@d1", MySqlDbType.Date).Value = dateTimePicker1.Value.Date;
                    //sqlCommand.Parameters.Add("@d2", MySqlDbType.Date).Value = dateTimePicker2.Value.Date;

                    mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
                    mySqlDataAdapter.Fill(dn);

                    int rowdn = dn.Rows.Count;
                    if (rowdn == 0)
                    {
                        MessageBox.Show("ID Pembelian tidak ditemukan");
                        dk.Clear();
                        textBox1.Text = "";
                        dn.Clear();
                        string cmd = "select id_pembelian as 'Id Beli', tanggal_pembelian as 'Tanggal', total_pembelian as 'Subtotal'from pembelian where status_del = 'F';";
                        sqlCommand = new MySqlCommand(cmd, conny);
                        mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
                        mySqlDataAdapter.Fill(dn);
                        dataGridView1.DataSource = dn;
                        textBox1.Enabled = false;
                        rowdn = dn.Rows.Count;
                        int hitung = 0;
                        int angka = 0;
                        for (int a = 0; a < rowdn; a++)
                        {

                            angka = Convert.ToInt32(dataGridView1.Rows[a].Cells[2].Value);
                            hitung = hitung + angka;

                        }
                    }
                    else
                    {
                        dataGridView1.DataSource = dn;
                        int hitung = 0;
                        for (int a = 0; a < rowdn; a++)
                        {
                            hitung += Convert.ToInt32(dataGridView1.Rows[a].Cells["Subtotal"].Value);
                        }
                        textBox1.Text = hitung.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    // Pastikan untuk menutup koneksi setelah digunakan
                    conny.Close();
                }
            }
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Homepage formhomepage = new Homepage();
            formhomepage.Show();
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    } }
        
    
