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
    public partial class AdminGirisFormu : Form
    {    
        public AdminGirisFormu()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source =DESKTOP-BD7IC7E; Initial Catalog = MuzikUygulamasi; Integrated Security = True");
        public static string gonderilecekAdres;
        private void button1_Click(object sender, EventArgs e)
        { // geri butonu
            Form1 giris = new Form1();
            giris.Show(); this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        { // admin işlem için giriş butonu
            string kullanıcıAd = textBox1.Text;
            string sifre = textBox2.Text;

            AdminGirisSınıfı giris = new AdminGirisSınıfı();
            giris.GirisYap(kullanıcıAd, sifre, this);
        }

        private void AdminGirisFormu_Load(object sender, EventArgs e)
        {

        }
    }
}
