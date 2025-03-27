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

namespace ALP_TUKU_TEMPEH
{
    public partial class Transaksi_Penjualan : Form
    {
        MySqlConnection connijawa;
        MySqlCommand sqlCommand;
        MySqlDataAdapter mySqlDataAdapter;
        MySqlDataReader mySqlDataReader;
        DataTable dy = new DataTable();
        DataTable dt = new DataTable();
        DataTable dm = new DataTable();
        public static int totalpenjualan = 0;
        public static int sudahbukaformini = 0;
        public static int sudahbukaformbarang = 0;
        int maueditt = 0;
        int ada = 0;
        public static int inijanganhilang = 0;
        public static int inijanganhilang1 = 0;
        public Transaksi_Penjualan()
        {
            InitializeComponent();
            string connection = "server=localhost;user=root;pwd=root123456@;database=db_tukutempeh"; //menghubungkan database
            connijawa = new MySqlConnection(connection);
        }

        private void Transaksi_Penjualan_Load(object sender, EventArgs e)
        {
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightYellow;

            textBox3.MaxLength = 6;
            dy.Clear();
            string command = "select fGenIdpenjualan();";
            sqlCommand = new MySqlCommand(command, connijawa);
            mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
            mySqlDataAdapter.Fill(dy);
            textBox1.Text = dy.Rows[0]["fGenIdpenjualan()"].ToString();
            int jumlahdy = dy.Rows.Count;
            //if(jumlahdy <9)
            //{
            //    textBox1.Text = "P0000" + (jumlahdy + 1);
            //}
            //if (jumlahdy >= 9)
            //{
            //    textBox1.Text = "P000" + (jumlahdy + 1);
            //}
            dm.Clear();
            string cmd = "call pcostumer();";
            sqlCommand = new MySqlCommand(cmd, connijawa);
            mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
            mySqlDataAdapter.Fill(dm);
            comboBox1.DataSource = dm;
            comboBox1.DisplayMember = "nama_customer";
            
            if (sudahbukaformini == 0 && inijanganhilang ==0)
            {
                comboBox1.Text = "UMUM";
            }
            if(sudahbukaformini >= 0 && inijanganhilang != 0)
            {
                comboBox1.Text = Form5.namacust.ToString();
                sudahbukaformini = 0;
                inijanganhilang++;
                
            }
  
            dt.Columns.Add("Kode Barang");
            dt.Columns.Add("Nama Barang");
            dt.Columns.Add("Jumlah");
            dt.Columns.Add("Harga Jual");
            dt.Columns.Add("Subtotal");
            dataGridView1.DataSource = dt;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
            textBox6.Text = string.Empty;
            textBox7.Text = string.Empty;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sudahbukaformini++;
            inijanganhilang++;
            Form5 frm = new Form5();
            frm.ShowDialog();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text.Length != 0 && textBox4.Text.Length != 0 && textBox5.Text.Length != 0 && textBox6.Text.Length != 0 && textBox7.Text.Length != 0)
            {
                if (maueditt == 0)
                {
                    ada = 0;
                    dy.Clear();
                    int stokbarangyangmaudiinput = Convert.ToInt32(textBox5.Text);
                    string comand = "select stok from barang where id_barang='" + textBox3.Text + "';";
                    sqlCommand = new MySqlCommand(comand, connijawa);
                    mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
                    mySqlDataAdapter.Fill(dy);
                    string stokyangada = dy.Rows[0]["stok"].ToString();
                    int stokyangadaa = Convert.ToInt32(stokyangada);
                    if (stokbarangyangmaudiinput > stokyangadaa)
                    {
                        MessageBox.Show("Stok tidak cukup");
                    }
                    if(stokbarangyangmaudiinput <= stokyangadaa)
                    {
                     
                        dy.Clear();
                        for (int a = 0; a<dt.Rows.Count; a++)
                        {
                            if (dataGridView1.Rows[a].Cells[0].Value.ToString() == textBox3.Text)
                            {
                                ada++;
                                dy.Clear();
                                string comand1 = "select stok from barang where id_barang='" + textBox3.Text + "';";
                                sqlCommand = new MySqlCommand(comand1, connijawa);
                                mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
                                mySqlDataAdapter.Fill(dy);
                                string stokyangadaaa = dy.Rows[0]["stok"].ToString();
                                int stokyangadaaaa = Convert.ToInt32(stokyangadaaa);
                                dy.Clear();
                                int jumlahbarangbaru = Convert.ToInt32(textBox5.Text);
                                int updatebeneran = stokyangadaaaa - jumlahbarangbaru;
                                string commmand = "update barang set stok='" + updatebeneran + "' where id_barang='" + textBox3.Text + "';";
                                sqlCommand = new MySqlCommand(commmand, connijawa);
                                mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
                                mySqlDataAdapter.Fill(dy);
                                int subtotalsementara = Convert.ToInt32(dataGridView1.Rows[a].Cells[4].Value);
                                int subtotalupdate = Convert.ToInt32(textBox7.Text);
                                int currentValue = Convert.ToInt32(dataGridView1.Rows[a].Cells[2].Value);
                                int newValue = currentValue + Convert.ToInt32(textBox5.Text);
                                dataGridView1.Rows[a].Cells[4].Value = (subtotalsementara + subtotalupdate);
                                dataGridView1.Rows[a].Cells[2].Value = newValue;
                                textBox3.Text = string.Empty;
                                textBox4.Text = string.Empty;
                                textBox5.Text = string.Empty;
                                textBox6.Text = string.Empty;
                                textBox7.Text = string.Empty;
              
                        

                            }
                        }
                        if (ada == 0)
                        {
                            dy.Clear();
                            string comand1 = "select stok from barang where id_barang='" + textBox3.Text + "';";
                            sqlCommand = new MySqlCommand(comand1, connijawa);
                            mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
                            mySqlDataAdapter.Fill(dy);
                            string stokyangadaaa = dy.Rows[0]["stok"].ToString();
                            int stokyangadaaaa = Convert.ToInt32(stokyangadaaa);
                            dy.Clear();
                            int yangmaudiinputpertama = Convert.ToInt32(textBox5.Text);
                            int updatebeneran = stokyangadaaaa - yangmaudiinputpertama;
                            string commmand = "update barang set stok='" + updatebeneran + "' where id_barang='" + textBox3.Text + "';";
                                sqlCommand = new MySqlCommand(commmand, connijawa);
                                mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
                                mySqlDataAdapter.Fill(dy);
                            
                            dt.Rows.Add(textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text, textBox7.Text);
                            dataGridView1.DataSource = dt;
                            textBox3.Text = string.Empty;
                            textBox4.Text = string.Empty;
                            textBox5.Text = string.Empty;
                            textBox6.Text = string.Empty;
                            textBox7.Text = string.Empty;

                        }
          
                    }
                }
                else
                {
                    dy.Clear();
                    string comand1 = "select stok from barang where id_barang='" + textBox3.Text + "';";
                    sqlCommand = new MySqlCommand(comand1, connijawa);
                    mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
                    mySqlDataAdapter.Fill(dy);
                    string stokyangadaaa = dy.Rows[0]["stok"].ToString();
                    int stokyangadaaaa = Convert.ToInt32(stokyangadaaa);
                    int tb5 = Convert.ToInt32(textBox5.Text);
                    int valuedatagrid = Convert.ToInt32(dataGridView1.CurrentRow.Cells[2].Value);
                        dy.Clear();
                        int selisih = valuedatagrid - tb5;
                        int stokupdatee = stokyangadaaaa + selisih;
                        string commmand = "update barang set stok='" + stokupdatee + "' where id_barang='" + textBox3.Text + "';";
                        sqlCommand = new MySqlCommand(commmand, connijawa);
                        mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
                        mySqlDataAdapter.Fill(dy);
                    

                    dataGridView1.CurrentRow.Cells[0].Value = textBox3.Text;
                    dataGridView1.CurrentRow.Cells[1].Value = textBox4.Text;
                    dataGridView1.CurrentRow.Cells[2].Value = textBox5.Text;
                    dataGridView1.CurrentRow.Cells[3].Value = textBox6.Text;
                    dataGridView1.CurrentRow.Cells[4].Value = textBox7.Text;
                    dataGridView1.DataSource = dt;
                    textBox3.Text = string.Empty;
                    textBox4.Text = string.Empty;
                    textBox5.Text = string.Empty;
                    textBox6.Text = string.Empty;
                    textBox7.Text = string.Empty;
                    textBox3.Enabled = true;
                    maueditt = 0;
                   
                }
                int jumlahrowdtt = dt.Rows.Count;
                int hitung = 0;
                for (int a = 0; a < jumlahrowdtt; a++)
                {
                    hitung = hitung + Convert.ToInt32(dataGridView1.Rows[a].Cells[4].Value);
                }
                label9.Text = hitung.ToString();
     
                sudahbukaformini = 0;
            }
            else
            {
                MessageBox.Show("Harap diisi dulu");
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

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string kodebarang = textBox3.Text;
            if (kodebarang.Length == 6)
            {
                dy.Clear();
                string namabarang = "select nama_barang, harga_jual from barang where id_barang='" + kodebarang + "' and status_del='F';";
                sqlCommand = new MySqlCommand(namabarang, connijawa);
                mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
                mySqlDataAdapter.Fill(dy);
                if (dy.Rows.Count == 0)
                {
                    MessageBox.Show("Kode barang tidak valid");

                }
                else
                {
                    string namaabarang = dy.Rows[0]["nama_barang"].ToString();
                    int hargajual = Convert.ToInt32(dy.Rows[0]["harga_jual"]);
                    textBox4.Text = namaabarang;
                    textBox6.Text = Convert.ToString(hargajual);
                }
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (textBox5.Text.Length != 0)
            {
                int jumlah = Convert.ToInt32(textBox5.Text);
                int harga = Convert.ToInt32(textBox6.Text);
                textBox7.Text = Convert.ToString(jumlah * harga);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dt.Rows.Count != 0)
            {
                dy.Clear();
                int jumlah = Convert.ToInt32(dataGridView1.CurrentRow.Cells[2].Value);
                string comand = "select stok from barang where id_barang='" + dataGridView1.CurrentRow.Cells[0].Value + "';";
                sqlCommand = new MySqlCommand(comand, connijawa);
                mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
                mySqlDataAdapter.Fill(dy);
                string stokyangada = dy.Rows[0]["stok"].ToString();
                int stokyangadaa = Convert.ToInt32(stokyangada);
                dy.Clear();
                int stokupdate = stokyangadaa + jumlah;
                string commmmand = "update barang set stok='" + stokupdate + "' where id_barang='" + dataGridView1.CurrentRow.Cells[0].Value + "';";
                sqlCommand = new MySqlCommand(commmmand, connijawa);
                mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
                mySqlDataAdapter.Fill(dy);

                int rowsindex = dataGridView1.CurrentCell.RowIndex;
                dataGridView1.Rows.RemoveAt(rowsindex);
                int jumlahrowdtt = dt.Rows.Count;
                int hitung = 0;
                for (int a = 0; a < jumlahrowdtt; a++)
                {
                    hitung = hitung + Convert.ToInt32(dataGridView1.Rows[a].Cells[4].Value);
                }
                label9.Text = hitung.ToString();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dt.Rows.Count != 0)
            {
                maueditt = 1;
                textBox3.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                textBox4.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBox5.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                textBox6.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                textBox7.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = true;
                textBox6.Enabled = false;
                textBox7.Enabled = false;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Homepage frm = new Homepage();
            frm.Show();
            this.Close();
            sudahbukaformini = 0;
        }

        private void button6_Click(object sender, EventArgs e)
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
                string idpenj = textBox1.Text;
                string totalpenj = label9.Text;
                dy.Clear();
                string comandnyariid = "select id_customer from customer where nama_customer='" + comboBox1.Text + "';";
                sqlCommand = new MySqlCommand(comandnyariid, connijawa);
               mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
                mySqlDataAdapter.Fill(dy);
                string idcustomerketemu = dy.Rows[0]["id_customer"].ToString();
                dy.Clear();
                string commmand = $"insert into penjualan values ('{idpenj}', '{idcustomerketemu}', '{waktutimestamp}', '{totalpenj}','F');";

                    sqlCommand = new MySqlCommand(commmand, connijawa);
                    mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
                    mySqlDataAdapter.Fill(dy);
                
                for (int a = 0; a < dt.Rows.Count; a++)
                {
                    string idpenjualan = textBox1.Text;
                    string kodebarang = dataGridView1.Rows[a].Cells[0].Value.ToString();
                    int jumlahdidgv = Convert.ToInt32(dataGridView1.Rows[a].Cells[2].Value);
                    int hargajual = Convert.ToInt32(dataGridView1.Rows[a].Cells[3].Value);
                    int subtotal = Convert.ToInt32(dataGridView1.Rows[a].Cells[4].Value);
                    dy.Clear();
                    string insertdetailpenj = $"insert into detail_penjualan values ('{idpenjualan}', '{kodebarang}', '{jumlahdidgv}','{hargajual}','{subtotal}','F');";
                    sqlCommand = new MySqlCommand(insertdetailpenj, connijawa);
                    mySqlDataAdapter = new MySqlDataAdapter(sqlCommand);
                    mySqlDataAdapter.Fill(dy);

                }
                string totalhitung = label9.Text;
                totalpenjualan = Convert.ToInt32(totalhitung);
                Form6 frmhome = new Form6();
                frmhome.Show();
                this.Close();
               
              
            }
            else
            {
                MessageBox.Show("Tidak ada barang yang dibeli");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
            textBox6.Text   = string.Empty;
            textBox7.Text   = string.Empty;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
