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
    public partial class UyeOlmaFormu : Form
    {
        public UyeOlmaFormu()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source =DESKTOP-BD7IC7E; Initial Catalog = MuzikUygulamasi; Integrated Security = True;MultipleActiveResultSets=True");

        public void bilgileriGoster()
        {  // kullanıcıları listviewde görüntüler
            listView1.Items.Clear();
            baglanti.Close();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * from kullanicilar as k inner join ulkeler as u on k.ulke_id = u.ulke_id inner join abonelik_turleri as at on k.abonelik_turu_id = at.abonelik_turu_id", baglanti);

            SqlDataReader oku = komut.ExecuteReader();

            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["kullanici_id"].ToString();
                ekle.SubItems.Add(oku["kullanici_adi"].ToString());
                ekle.SubItems.Add(oku["email"].ToString());
                ekle.SubItems.Add(oku["sifre"].ToString());
                ekle.SubItems.Add(oku["abone_turu"].ToString());
                ekle.SubItems.Add(oku["ulke_ad"].ToString());

                listView1.Items.Add(ekle);

            }

            baglanti.Close();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form1 giris = new Form1();
            giris.Show(); this.Hide();
        }

        private void UyeOlmaFormu_Load(object sender, EventArgs e)
        {   //üyelik türleri comboboxta listeleme işlemi
            baglanti.Open();
            SqlCommand command = new SqlCommand("select abonelik_turu_id,abone_turu from abonelik_turleri", baglanti);
            SqlDataReader dr = command.ExecuteReader();

            while (dr.Read())
            {
                ComboboxItem item = new ComboboxItem();
                item.Text = dr[1].ToString();
                item.Value = dr[0];
                //comboBox2.Items.Add(item);
                
            }
            baglanti.Close();

            //ülkeleri comboboxta listeleme işlemi
            baglanti.Open();
            SqlCommand command2 = new SqlCommand("select ulke_id,ulke_ad from ulkeler", baglanti);
            SqlDataReader dr2 = command2.ExecuteReader();
            while (dr2.Read())
            {
                ComboboxItem item = new ComboboxItem();
                item.Text = dr2[1].ToString();
                item.Value = dr2[0];
                comboBox1.Items.Add(item);

            }
            baglanti.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            bilgileriGoster();
        }


        bool durum;
        void kullanıcı_adı_kontrol()
        {
            baglanti.Open();
            SqlCommand command = new SqlCommand("select * from kullanicilar where kullanici_adi =@k1", baglanti);
            command.Parameters.AddWithValue("@k1", textBox1.Text);
            SqlDataReader dr = command.ExecuteReader();
            if (dr.Read())
            {
                durum = false;
            }
            else
            {
                durum = true;
            }
            baglanti.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            int calmalistesiid = 31;
            int kullaniciid = 26;
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || comboBox1.Text == "" || textBox3.Text != textBox4.Text || checkBox1.Checked == false)
            {
                MessageBox.Show("LÜTFEN BİLGİLERİ EKSİKSİZ VE DOGRU DOLDURUN");
            }

            else
            {
                if (radioButton1.Checked == true && checkBox2.Checked == true)
                {
                    MessageBox.Show("NORMAL KULLANICILAR ÖDEME YAPAMAZ");
                }
                else if (radioButton1.Checked == true && checkBox2.Checked == false)
                {
                    kullanıcı_adı_kontrol();
                    if (durum == true)
                    {
                        baglanti.Open();
                        SqlCommand komut = new SqlCommand("Insert into kullanicilar (kullanici_adi,email,sifre,abonelik_turu_id,ulke_id) Values(@k1,@k2,@k3,@k4,@k5)", baglanti);
                        komut.Parameters.AddWithValue("@k1", textBox1.Text);
                        komut.Parameters.AddWithValue("@k2", textBox2.Text);
                        komut.Parameters.AddWithValue("@k3", textBox3.Text);
                        komut.Parameters.AddWithValue("@k4", "1");
                        komut.Parameters.AddWithValue("@k5", (comboBox1.SelectedItem as ComboboxItem).Value.ToString());
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        MessageBox.Show("KAYIT EKLENDİ");
                        bilgileriGoster();
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox3.Clear();
                        textBox4.Clear();
                        // oluşan her kullanıcı için çalma listesi oluşturma
                        baglanti.Open();
                        SqlCommand komut1 = new SqlCommand("insert into kullanicilarin_calma_listeleri (kullanici_id,calma_listesi_id) Values(@kk1,@kk2)", baglanti);
                        komut1.Parameters.AddWithValue("@kk1", kullaniciid);
                        komut1.Parameters.AddWithValue("@kk2", calmalistesiid);

                        calmalistesiid++;
                        komut1.ExecuteNonQuery();
                        baglanti.Close();

                        // oluşan her kullanıcı için çalma listesi oluşturma
                        baglanti.Open();
                        SqlCommand komut2 = new SqlCommand("insert into kullanicilarin_calma_listeleri (kullanici_id,calma_listesi_id) Values(@kk1,@kk2)", baglanti);
                        komut2.Parameters.AddWithValue("@kk1", kullaniciid);
                        komut2.Parameters.AddWithValue("@kk2", calmalistesiid);

                        calmalistesiid++;
                        komut2.ExecuteNonQuery();
                        baglanti.Close();

                        //oluşan her kullanıcı için çalma listesi oluşturma
                        baglanti.Open();
                        SqlCommand komut3 = new SqlCommand("insert into kullanicilarin_calma_listeleri (kullanici_id,calma_listesi_id) Values(@kk1,@kk2)", baglanti);
                        komut3.Parameters.AddWithValue("@kk1", kullaniciid);
                        komut3.Parameters.AddWithValue("@kk2", calmalistesiid);

                        calmalistesiid++;
                        komut3.ExecuteNonQuery();
                        baglanti.Close();

                        kullaniciid++;

                    }
                    else
                    {
                        MessageBox.Show("BU KULLANICI ADI MEVCUT", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (radioButton2.Checked == true && checkBox2.Checked == false)
                {
                    MessageBox.Show("PREMİUM KULLANICILAR ÖDEME YAPMAK ZORUNDADIR");
                }
                else if (radioButton2.Checked == true && checkBox2.Checked == true)
                {
                    kullanıcı_adı_kontrol();
                    if (durum == true)
                    {
                        baglanti.Open();
                        SqlCommand komut = new SqlCommand("Insert into kullanicilar (kullanici_adi,email,sifre,abonelik_turu_id,ulke_id) Values(@k1,@k2,@k3,@k4,@k5)", baglanti);
                        komut.Parameters.AddWithValue("@k1", textBox1.Text);
                        komut.Parameters.AddWithValue("@k2", textBox2.Text);
                        komut.Parameters.AddWithValue("@k3", textBox3.Text);
                        komut.Parameters.AddWithValue("@k4","2");
                        komut.Parameters.AddWithValue("@k5", (comboBox1.SelectedItem as ComboboxItem).Value.ToString());
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        MessageBox.Show("KAYIT EKLENDİ");
                        bilgileriGoster();
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox3.Clear();
                        textBox4.Clear();

                        // oluşan her kullanıcı için çalma listesi oluşturma
                        baglanti.Open();
                        SqlCommand komut1 = new SqlCommand("insert into kullanicilarin_calma_listeleri (kullanici_id,calma_listesi_id) Values(@kk1,@kk2)", baglanti);
                        komut1.Parameters.AddWithValue("@kk1", kullaniciid );
                        komut1.Parameters.AddWithValue("@kk2", calmalistesiid);

                        calmalistesiid++; 
                        komut1.ExecuteNonQuery();
                        baglanti.Close();

                        // oluşan her kullanıcı için çalma listesi oluşturma
                        baglanti.Open();
                        SqlCommand komut2 = new SqlCommand("insert into kullanicilarin_calma_listeleri (kullanici_id,calma_listesi_id) Values(@kk1,@kk2)", baglanti);
                        komut2.Parameters.AddWithValue("@kk1", kullaniciid);
                        komut2.Parameters.AddWithValue("@kk2", calmalistesiid );

                        calmalistesiid++;
                        komut2.ExecuteNonQuery();
                        baglanti.Close();

                        //oluşan her kullanıcı için çalma listesi oluşturma
                        baglanti.Open();
                        SqlCommand komut3 = new SqlCommand("insert into kullanicilarin_calma_listeleri (kullanici_id,calma_listesi_id) Values(@kk1,@kk2)", baglanti);
                        komut3.Parameters.AddWithValue("@kk1", kullaniciid );
                        komut3.Parameters.AddWithValue("@kk2", calmalistesiid);

                        calmalistesiid++;
                        komut3.ExecuteNonQuery();
                        baglanti.Close();

                        kullaniciid++;



                    }
                    else
                    {
                        MessageBox.Show("BU KULLANICI ADI MEVCUT", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }



            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
