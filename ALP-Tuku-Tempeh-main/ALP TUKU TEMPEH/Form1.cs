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
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void picture_next_Click(object sender, EventArgs e)
        {
            FormLogin frmlogin = new FormLogin();
            frmlogin.Show();
            this.Hide();
        }
    }
}
