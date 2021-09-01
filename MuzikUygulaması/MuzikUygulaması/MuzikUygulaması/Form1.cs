using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MuzikUygulaması
{
    public partial class Form1 : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source =DESKTOP-BD7IC7E; Initial Catalog = MuzikUygulamasi; Integrated Security = True");
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            AdminGirisFormu adminGirisFormu = new AdminGirisFormu();
            adminGirisFormu.Show(); this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UyeOlmaFormu uyeOlmaFormu = new UyeOlmaFormu();
            uyeOlmaFormu.Show(); this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            KullanıcıGirisSınıfı kullanıcıGiris = new KullanıcıGirisSınıfı();

            string kullanıcıAd = textBox1.Text;
            string sifre = textBox2.Text;
            
            kullanıcıGiris.GirisYap(kullanıcıAd, sifre, this);
        }
    }
}
