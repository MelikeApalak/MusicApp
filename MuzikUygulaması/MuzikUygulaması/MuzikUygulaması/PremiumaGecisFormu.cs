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
    public partial class PremiumaGecisFormu : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source =DESKTOP-BD7IC7E; Initial Catalog = MuzikUygulamasi; Integrated Security = True;MultipleActiveResultSets=True");
        public string _kullanıcı_adi;
        public string _calmalistesisahibi;
        public PremiumaGecisFormu(string kullanıcı_adi,string calmalistesisahibi)
        {
            InitializeComponent();
            _kullanıcı_adi = kullanıcı_adi;
            _calmalistesisahibi = calmalistesisahibi;
        }

        private void PremiumaGecisFormu_Load(object sender, EventArgs e)
        {

        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            listView3.Items.Clear();

            

            baglanti.Close();
            baglanti.Open();

            SqlCommand komut = new SqlCommand("Select kl.calma_listesi_id,k.kullanici_adi,s.sarki_ad,att.abone_turu,t.tur_adi from kullanicilarin_calma_listeleri as kl" +
                " inner join kullanicilar as k on k.kullanici_id = kl.kullanici_id " +
                "inner join abonelik_turleri as att on k.abonelik_turu_id = att.abonelik_turu_id" +
                " inner join calma_listeleri as cl on cl.calma_listesi_id = kl.calma_listesi_id" +
                " inner join sarkilar as s on s.sarki_id = cl.sarki_id" +
                " inner join albumler as a on a.album_id = s.album_id" +
                " inner join turler as t on t.tur_id = a.tur_id where k.kullanici_adi = '" + _kullanıcı_adi + "' and t.tur_adi = 'pop' ", baglanti);


            SqlDataReader oku = komut.ExecuteReader();

            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["sarki_ad"].ToString();
                ekle.SubItems.Add(oku["abone_turu"].ToString());
                ekle.SubItems.Add(oku["tur_adi"].ToString());

                listView3.Items.Add(ekle);

            }

            baglanti.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listView8.Items.Clear();
            baglanti.Close();
            baglanti.Open();

            SqlCommand komut = new SqlCommand("Select kl.calma_listesi_id,k.kullanici_adi,s.sarki_ad,att.abone_turu,t.tur_adi from kullanicilarin_calma_listeleri as kl" +
                " inner join kullanicilar as k on k.kullanici_id = kl.kullanici_id " +
                "inner join abonelik_turleri as att on k.abonelik_turu_id = att.abonelik_turu_id" +
                " inner join calma_listeleri as cl on cl.calma_listesi_id = kl.calma_listesi_id" +
                " inner join sarkilar as s on s.sarki_id = cl.sarki_id" +
                " inner join albumler as a on a.album_id = s.album_id" +
                " inner join turler as t on t.tur_id = a.tur_id where k.kullanici_adi = '" + _kullanıcı_adi + "' and t.tur_adi = 'jazz' ", baglanti);


            SqlDataReader oku = komut.ExecuteReader();

            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["sarki_ad"].ToString();
                ekle.SubItems.Add(oku["abone_turu"].ToString());
                ekle.SubItems.Add(oku["tur_adi"].ToString());

                listView8.Items.Add(ekle);

            }

            baglanti.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listView4.Items.Clear();
            baglanti.Close();
            baglanti.Open();

            SqlCommand komut = new SqlCommand("Select kl.calma_listesi_id,k.kullanici_adi,s.sarki_ad,att.abone_turu,t.tur_adi from kullanicilarin_calma_listeleri as kl" +
                " inner join kullanicilar as k on k.kullanici_id = kl.kullanici_id " +
                "inner join abonelik_turleri as att on k.abonelik_turu_id = att.abonelik_turu_id" +
                " inner join calma_listeleri as cl on cl.calma_listesi_id = kl.calma_listesi_id" +
                " inner join sarkilar as s on s.sarki_id = cl.sarki_id" +
                " inner join albumler as a on a.album_id = s.album_id" +
                " inner join turler as t on t.tur_id = a.tur_id where k.kullanici_adi = '" + _kullanıcı_adi + "' and t.tur_adi = 'klasik' ", baglanti);


            SqlDataReader oku = komut.ExecuteReader();

            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["sarki_ad"].ToString();
                ekle.SubItems.Add(oku["abone_turu"].ToString());
                ekle.SubItems.Add(oku["tur_adi"].ToString());


                listView4.Items.Add(ekle);

            }

            baglanti.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem selectedItem in listView3.SelectedItems)
            {
                listView3.Items.Remove(selectedItem);
                listView1.Items.Add(selectedItem);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem selectedItem in listView1.SelectedItems)
            {
                listView1.Items.Remove(selectedItem);
                listView3.Items.Add(selectedItem);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem selectedItem in listView8.SelectedItems)
            {
                listView8.Items.Remove(selectedItem);
            
                listView7.Items.Add(selectedItem);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem selectedItem in listView7.SelectedItems)
            {
                listView7.Items.Remove(selectedItem);
                listView8.Items.Add(selectedItem);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem selectedItem in listView4.SelectedItems)
            {
               listView4.Items.Remove(selectedItem);
                listView2.Items.Add(selectedItem);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem selectedItem in listView2.SelectedItems)
            {
                listView2.Items.Remove(selectedItem);
                listView4.Items.Add(selectedItem);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            KullanıcıGirisFormu giris = new KullanıcıGirisFormu(_calmalistesisahibi);
            giris.Show(); this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
