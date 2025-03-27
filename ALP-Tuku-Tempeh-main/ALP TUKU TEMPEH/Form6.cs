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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            textBox1.Text = Transaksi_Penjualan.totalpenjualan.ToString();
            textBox1.Enabled = false;
            textBox3.Enabled = false;
            textBox3.Text = "0";

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                int selisih = Convert.ToInt32(textBox2.Text) - Convert.ToInt32(textBox1.Text);
                textBox3.Text = selisih.ToString();
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // Jika bukan digit, membatalkan input
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int kembalian = Convert.ToInt32(textBox3.Text);
            if(kembalian <0)
            {
                MessageBox.Show("Pembayaran tidak cukup");
            }
            else
            {
                Transaksi_Penjualan homepage = new Transaksi_Penjualan();
                homepage.Show();
                this.Close();
                MessageBox.Show("Transaksi Penjualan Berhasil");
            }
        }
    }
}
