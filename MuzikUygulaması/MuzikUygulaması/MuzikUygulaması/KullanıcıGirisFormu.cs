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
    public partial class KullanıcıGirisFormu : Form
    {
        public string _kullaniciadi;
        public KullanıcıGirisFormu(string ad)
        {
            InitializeComponent();
            _kullaniciadi = ad;
        }
        SqlConnection baglanti = new SqlConnection("Data Source =DESKTOP-BD7IC7E; Initial Catalog = MuzikUygulamasi; Integrated Security = True;MultipleActiveResultSets=True");
        private void button1_Click(object sender, EventArgs e)
        {
            Form1 giris = new Form1();
            giris.Show(); this.Hide();
        }
        public void bilgileriGoster()
        {
            listView2.Items.Clear();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from sanatcilar_sarkilar as ss inner join sarkilar as s on s.sarki_id = ss.sarki_id " +
                "inner join sanatcilar as k on ss.sanatci_id = k.sanatci_id " +
                "inner join albumler as a on a.album_id = s.album_id " +
                "inner join turler as t on a.tur_id = t.tur_id", baglanti);

            SqlDataReader oku = komut.ExecuteReader();

            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();
                //ekle.Text = oku["id"].ToString();
                ekle.SubItems.Add(oku["sarki_adi"].ToString());
                ekle.SubItems.Add(oku["sanatci_ad"].ToString());
                ekle.SubItems.Add(oku["album_adi"].ToString());

                ekle.SubItems.Add(oku["tur_adi"].ToString());

                ekle.SubItems.Add(oku["sure"].ToString());
                ekle.SubItems.Add(oku["tarih"].ToString());
                ekle.SubItems.Add(oku["dinlenme_sayisi"].ToString());

                listView2.Items.Add(ekle);

            }

            baglanti.Close();

        }

        private void KullanıcıGirisFormu_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            listView8.Items.Clear();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from kullanicilar where abonelik_turu_id=2 ", baglanti);

            SqlDataReader oku = komut.ExecuteReader();

            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["kullanici_adi"].ToString();
                ekle.SubItems.Add(oku["kullanici_id"].ToString());

                listView8.Items.Add(ekle);

            }

            baglanti.Close();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem selectedItem in listView8.SelectedItems)
            {
                listView8.Items.Remove(selectedItem);
                listView9.Items.Add(selectedItem);
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            PremiumaGecisFormu premium = new PremiumaGecisFormu(listView9.SelectedItems[0].Text,_kullaniciadi);
            premium.Show(); this.Hide();
        }
        public void TumSarkilariGoster()
        {
            // şarkı ve sanatçıları listviewde gösterme işlemi
            listView2.Items.Clear();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from sanatcilar_sarkilar as ss inner join sarkilar as s on s.sarki_id = ss.sarki_id " +
                "inner join sanatcilar as k on ss.sanatci_id = k.sanatci_id " +
                "inner join albumler as a on a.album_id = s.album_id " +
                "inner join turler as t on a.tur_id = t.tur_id", baglanti);

            SqlDataReader oku = komut.ExecuteReader();

            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["id"].ToString();
                ekle.SubItems.Add(oku["sarki_ad"].ToString());
                ekle.SubItems.Add(oku["sanatci_ad"].ToString());
                ekle.SubItems.Add(oku["album_adi"].ToString());

                ekle.SubItems.Add(oku["tur_adi"].ToString());

                ekle.SubItems.Add(oku["sure"].ToString());
                ekle.SubItems.Add(oku["tarih"].ToString());
                ekle.SubItems.Add(oku["dinlenme_sayisi"].ToString());

                listView2.Items.Add(ekle);

            }

            baglanti.Close();


        }
        private void button8_Click(object sender, EventArgs e)
        {
            TumSarkilariGoster();
        }

        private void listView8_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem selectedItem in listView4.SelectedItems)
            {
                listView4.Items.Remove(selectedItem);
                listView5.Items.Add(selectedItem);
            }
        }

        private void listView4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            sanatcıları_listele(); //sanatçıları listviewde görüntüleme

            // sanatcılisteleme(); //sanatçıları comboboxa getirme
        }

        public void sanatcıları_listele()
        {
            listView4.Items.Clear();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from sanatcilar  as s inner join ulkeler as u on s.ulke_id = u.ulke_id", baglanti);

            SqlDataReader oku = komut.ExecuteReader();

            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["sanatci_id"].ToString();
                ekle.SubItems.Add(oku["sanatci_ad"].ToString());
                ekle.SubItems.Add(oku["ulke_ad"].ToString());


                listView4.Items.Add(ekle);

            }

            baglanti.Close();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem selectedItem in listView5.SelectedItems)
            {
                listView5.Items.Remove(selectedItem);
                listView4.Items.Add(selectedItem);
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {  //ŞARKI ARAMA BUTONU
            listView2.Items.Clear();

            if (textBox1.Text == "")
            {
                MessageBox.Show("lütfen şarkı ismi girin");


            }

            else
            {

                //textBox1.Clear();
                //listView1.Items.Clear();
                baglanti.Close();
                baglanti.Open();
                SqlCommand komut = new SqlCommand("select * from sanatcilar_sarkilar as ss inner join sarkilar as s on s.sarki_id = ss.sarki_id " +
                "inner join sanatcilar as k on ss.sanatci_id = k.sanatci_id " +
                "inner join albumler as a on a.album_id = s.album_id " +
                "inner join turler as t on a.tur_id = t.tur_id where s.sarki_ad like '" + textBox1.Text + "%'", baglanti);

                SqlDataReader oku = komut.ExecuteReader();

                while (oku.Read())
                {
                    ListViewItem ekle = new ListViewItem();
                    ekle.Text = oku["sarki_id"].ToString();
                    ekle.SubItems.Add(oku["sarki_ad"].ToString());
                    ekle.SubItems.Add(oku["sanatci_ad"].ToString());
                    ekle.SubItems.Add(oku["album_adi"].ToString());

                    ekle.SubItems.Add(oku["tur_adi"].ToString());

                    ekle.SubItems.Add(oku["sure"].ToString());
                    ekle.SubItems.Add(oku["tarih"].ToString());
                   // ekle.SubItems.Add(oku["dinlenme_sayisi"].ToString());

                    listView2.Items.Add(ekle);

                }

                baglanti.Close();


            }

        }

        private void listView7_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click_2(object sender, EventArgs e)
        {
            
            foreach (ListViewItem selectedItem in listView2.SelectedItems)
            {
               
                listView2.Items.Remove(selectedItem);
                listView1.Items.Add(selectedItem);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem selectedItem in listView1.SelectedItems)
            {

                listView1.Items.Remove(selectedItem);
                listView2.Items.Add(selectedItem);
            }

        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            listView4.Items.Clear();

            if (textBox2.Text == "")
            {
                MessageBox.Show("lütfen sanatçı ismi girin");


            }

            else
            {

                //textBox1.Clear();
                //listView1.Items.Clear();
                baglanti.Close();
                baglanti.Open();
                SqlCommand komut = new SqlCommand("select * from sanatcilar as s inner join ulkeler as u on s.ulke_id = u.ulke_id" +
                    " where sanatci_ad like '" + textBox2.Text + "%'", baglanti);

                SqlDataReader oku = komut.ExecuteReader();

                while (oku.Read())
                {
                    ListViewItem ekle = new ListViewItem();
                    ekle.Text = oku["sanatci_id"].ToString();
                    ekle.SubItems.Add(oku["sanatci_ad"].ToString());
                    ekle.SubItems.Add(oku["ulke_ad"].ToString());

                    listView4.Items.Add(ekle);

                }

                baglanti.Close();


            }
        }
    }
}
