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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace ALP_TUKU_TEMPEH
{
    public partial class Transaksi_Pembelian : Form
    {
        MySqlConnection conni;
        MySqlCommand sqlCommand;
        MySqlDataAdapter mySqlDataAdapter;
        MySqlDataReader mySqlDataReader;
        DataTable dt = new DataTable();
        DataTable dn = new DataTable();
        DataTable dk  = new DataTable();
        int mauedit = 0;
        int rowsindex = 0;
        public Transaksi_Pembelian()
        {
            InitializeComponent();
            string connection = "server=localhost;user=root;pwd=root123456@;database=db_tukutempeh"; //menghubungkan database
            conni = new MySqlConnection(connection);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Transaksi_Pembelian_Load(object sender, EventArgs e)
        {
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightYellow;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
            textBox6.Text = string.Empty;
            textBox2.MaxLength = 6;
            dt.Columns.Add("Kode Barang");
            dt.Columns.Add("Nama Barang");
            dt.Columns.Add("Jumlah");
            dt.Columns.Add("Harga Beli");
            dt.Columns.Add("Subtotal");
            dataGridView1.DataSource = dt;
            dn.Clear();
            string cmd = "select fGenIdpembelian();";
            sqlCommand = new MySqlCommand(cmd, conni);
            mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
            mySqlDataAdapter.Fill(dn);
            textBox1.Text = dn.Rows[0]["fGenIdpembelian()"].ToString();
            int jumlahdn = dn.Rows.Count;
            //if (jumlahdn < 9)
            //{
            //    textBox1.Text = "PB000" + (jumlahdn+1);
            //}
            //if (jumlahdn >=9)
            //{
            //    textBox1.Text = "PB00" + (jumlahdn + 1);
            //}
        }

  

        private void textBox2_MouseLeave(object sender, EventArgs e)
        {
       
        }

        private void textBox2_MouseEnter(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string kodebarang = textBox2.Text;
            if (kodebarang.Length == 6)
            {
                dk.Clear();
                string namabarang = "select nama_barang, harga_beli from barang where id_barang='" + kodebarang + "' and status_del='F';";
                sqlCommand = new MySqlCommand(namabarang, conni);
                mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
                mySqlDataAdapter.Fill(dk);
                if (dk.Rows.Count == 0)
                {
                    MessageBox.Show("Kode barang tidak valid");
                  
                }
                else
                {
                    string namaabarang = dk.Rows[0]["nama_barang"].ToString();
                    int hargabeli = Convert.ToInt32(dk.Rows[0]["harga_beli"]);
                    textBox3.Text = namaabarang;
                    textBox5.Text = Convert.ToString(hargabeli);
                }
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text.Length != 0)
            {
                int jumlah = Convert.ToInt32(textBox4.Text);
            int harga = Convert.ToInt32(textBox5.Text);

                textBox6.Text = string.Empty;
                textBox6.Text = Convert.ToString(jumlah * harga);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
            textBox6.Text = string.Empty;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int ada = 0;
     
            if (textBox2.Text.Length != 0 && textBox3.Text.Length != 0 && textBox2.Text.Length != 0 && textBox4.Text.Length != 0 && textBox5.Text.Length != 0 && textBox6.Text.Length != 0)
            {
                int jumlahrowdt = dt.Rows.Count;
                for (int i = 0; i<jumlahrowdt;i++)
                {
                    if (dataGridView1.Rows[i].Cells[0].Value.ToString() == textBox2.Text && mauedit == 0)
                    {
                        ada++;
                        int subtotalsementara = Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value);
                        int subtotalupdate = Convert.ToInt32(textBox6.Text);
                        int currentValue = Convert.ToInt32(dataGridView1.Rows[i].Cells[2].Value);
                        int newValue = currentValue + Convert.ToInt32(textBox4.Text);
                        dataGridView1.Rows[i].Cells[4].Value = (subtotalsementara + subtotalupdate);
                        dataGridView1.Rows[i].Cells[2].Value = newValue;
                        textBox2.Text = string.Empty;
                        textBox3.Text = string.Empty;
                        textBox4.Text = string.Empty;
                        textBox5.Text = string.Empty;
                        textBox6.Text = string.Empty;

                    }
                }
                if (ada == 0 && mauedit ==0)
                {
                    dt.Rows.Add(textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text);
                    dataGridView1.DataSource = dt;
                    textBox2.Text = string.Empty;
                    textBox3.Text = string.Empty;
                    textBox4.Text = string.Empty;
                    textBox5.Text = string.Empty;
                    textBox6.Text = string.Empty;
                }
               if(ada == 0 && mauedit >0)
                {
                    int jumlahedit = Convert.ToInt32(textBox4.Text);
                    int hargabeli = Convert.ToInt32(textBox5.Text);
                    dataGridView1.Rows[rowsindex].Cells[0].Value = textBox2.Text;
                    dataGridView1.Rows[rowsindex].Cells[1].Value = textBox3.Text;
                    dataGridView1.Rows[rowsindex].Cells[2].Value = textBox4.Text;
                    dataGridView1.Rows[rowsindex].Cells[3].Value = textBox5.Text;
                    dataGridView1.Rows[rowsindex].Cells[4].Value = (jumlahedit*hargabeli);
                    dataGridView1.DataSource = dt;
                    textBox2.Text = string.Empty;
                    textBox3.Text = string.Empty;
                    textBox4.Text = string.Empty;
                    textBox5.Text = string.Empty;
                    textBox6.Text = string.Empty;
                    textBox2.Enabled = true;
                    mauedit = 0;
                }
                int jumlahrowdtt = dt.Rows.Count;
                int hitung = 0;
                for (int a = 0; a < jumlahrowdtt; a++)
                {
                    hitung = hitung + Convert.ToInt32(dataGridView1.Rows[a].Cells[4].Value);
                }
                label4.Text = hitung.ToString();
            }
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dt.Rows.Count != 0)
            {
                int rowsindex = dataGridView1.CurrentCell.RowIndex;
                dataGridView1.Rows.RemoveAt(rowsindex);
                int jumlahrowdtt = dt.Rows.Count;
                int hitung = 0;
                for (int a = 0; a < jumlahrowdtt; a++)
                {
                    hitung = hitung + Convert.ToInt32(dataGridView1.Rows[a].Cells[4].Value);
                }
                label4.Text = hitung.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dt.Rows.Count != 0)
            {
                rowsindex = 0;
                mauedit = 0;
                mauedit++;
                rowsindex = dataGridView1.CurrentCell.RowIndex;
                textBox2.Text = dataGridView1.Rows[rowsindex].Cells[0].Value.ToString();
                textBox3.Text = dataGridView1.Rows[rowsindex].Cells[1].Value.ToString();
                textBox4.Text = dataGridView1.Rows[rowsindex].Cells[2].Value.ToString();
                textBox5.Text = dataGridView1.Rows[rowsindex].Cells[3].Value.ToString();
                textBox6.Text = dataGridView1.Rows[rowsindex].Cells[4].Value.ToString();
                textBox2.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dt.Rows.Count > 0)
            {
                string date = dateTimePicker1.Value.ToString();
                string waktu = date.Substring(11, 5);
                string[] pisah = date.Split('/');
                string tahun = pisah[2].Substring(0, 4);
                string bulan = pisah[1];
                string tanggal = pisah[0];
                string waktutimestamp = tahun + "-" + bulan + "-" + tanggal + " " + waktu + ":00";
                string idpemb = textBox1.Text;
                string totalpemb = label4.Text;
                string commmand = $"insert into pembelian values ('{idpemb}', '{waktutimestamp}', '{totalpemb}','F');";
                dk.Clear();
                sqlCommand = new MySqlCommand(commmand, conni);
                mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
                mySqlDataAdapter.Fill(dk);
                for (int a=0; a<dt.Rows.Count; a++) 
                {
                    string idpembelian = textBox1.Text;
                    dk.Clear();
                    string kodebarang = dataGridView1.Rows[a].Cells[0].Value.ToString();
                    string jumlahbarang = "select stok from barang where id_barang='" + kodebarang + "';";
                    sqlCommand = new MySqlCommand(jumlahbarang, conni);
                    mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
                    mySqlDataAdapter.Fill(dk);
                    int jumlahdidgv = Convert.ToInt32(dataGridView1.Rows[a].Cells[2].Value);
                    int stok = Convert.ToInt32(dk.Rows[0]["stok"]);
                    int updatebarang = stok + jumlahdidgv;
                    int hargabeli = Convert.ToInt32(dataGridView1.Rows[a].Cells[3].Value);
                    int subtotal = Convert.ToInt32(dataGridView1.Rows[a].Cells[4].Value);
            
                    string insertdetailpemb = $"insert into detail_pembelian values ('{idpemb}', '{kodebarang}', '{jumlahdidgv}','{hargabeli}','{subtotal}','F');";
                 
                        dk.Clear();
                        sqlCommand = new MySqlCommand(insertdetailpemb, conni);
                        mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
                        mySqlDataAdapter.Fill(dk);
                    //string updatejmlh = "update barang set stok ='" + updatebarang + "'where id_barang='" + kodebarang + "';";
                    //dk.Clear();
                    //sqlCommand = new MySqlCommand(updatejmlh, conni);
                    //mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
                    //mySqlDataAdapter.Fill(dk);

                }
                Homepage frmhome = new Homepage();
                frmhome.Show();
                this.Close();

            }
            else
            {
                MessageBox.Show("Tidak ada barang yang dibeli");
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Homepage frmhome = new Homepage();
            frmhome.Show();
            this.Close();
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // Jika bukan digit, membatalkan input
                e.Handled = true;
            }
        }
    }
}
