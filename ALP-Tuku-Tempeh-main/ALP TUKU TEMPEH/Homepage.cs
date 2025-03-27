using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ALP_TUKU_TEMPEH
{
    public partial class Homepage : Form
    {
       
        public Homepage()
        {
            InitializeComponent();
           

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            label4.Font = new Font(label4.Font, FontStyle.Bold);
            label3.Font = new Font(label3.Font, FontStyle.Underline);
            pictureBox4.Visible = true;
            pictureBox5.Visible = true;
            pictureBox6.Visible = false;
            pictureBox7.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            label3.Font = new Font(label3.Font, FontStyle.Bold);
            label4.Font = new Font(label4.Font, FontStyle.Underline);
            pictureBox4.Visible = false;
            pictureBox5.Visible = false;
            pictureBox6.Visible = true;
            pictureBox7.Visible = true;
            label5.Visible = false;
            label6.Visible = false;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Transaksi_Penjualan tranpenj = new Transaksi_Penjualan();
            tranpenj.Show();
            this.Close();
        }
        private void pictureBox6_Click(object sender, EventArgs e)
        {
       
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void Homepage_Load(object sender, EventArgs e)
        {
            label6.Text = FormLogin.username;
            label5.Parent = pictureBox1;
            label5.BackColor = Color.Transparent;
            label6.Parent = pictureBox1;
            label6.BackColor = Color.Transparent;
            if (label6.Text == "Kasir01")
            {
                label2.Visible = false;
                label4.Visible = false;
                label3.Location = new Point(553, 33);
            }
           
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Form3 frm3 = new Form3();
            frm3.Show();
            this.Close();
        }

        private void pictureBox6_Click_1(object sender, EventArgs e)
        {
            Transaksi_Pembelian tranpem = new Transaksi_Pembelian();
            tranpem.Show();
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Form4 frm = new Form4();
            frm.Show();
            this.Close();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Laporan_Penjualan frm = new Laporan_Penjualan();
            frm.Show(); this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
