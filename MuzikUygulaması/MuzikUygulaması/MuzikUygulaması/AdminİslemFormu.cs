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
    public partial class AdminİslemFormu : Form
    {
        public AdminİslemFormu()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source =DESKTOP-BD7IC7E; Initial Catalog = MuzikUygulamasi; Integrated Security = True;MultipleActiveResultSets=True");
        public void bilgileriGoster()
        { // şarkı ve sanatçıları listviewde gösterme işlemi
            listView1.Items.Clear();
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

                listView1.Items.Add(ekle);

            }

            baglanti.Close();

        }
        public void sanatcıları_listele()
        {//sanatçıları listviewde görüntüleme işlemi
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
        public void albumlerı_listele()
        { //albümleri listviewde gösterme işlemi

            listView2.Items.Clear();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from albumler as a inner join turler as t on a.tur_id = t.tur_id", baglanti);

            SqlDataReader oku = komut.ExecuteReader();

            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["album_id"].ToString();
                ekle.SubItems.Add(oku["album_adi"].ToString());
                ekle.SubItems.Add(oku["tarih"].ToString());
                ekle.SubItems.Add(oku["tur_adi"].ToString());


                listView2.Items.Add(ekle);

            }

            baglanti.Close();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            AdminGirisFormu giris = new AdminGirisFormu();
            giris.Show(); this.Hide();
        }

        private void AdminİslemFormu_Load(object sender, EventArgs e)
        {   
            // türleri comboboxta listeleme işlemi
            baglanti.Open();
            SqlCommand command = new SqlCommand("select tur_id,tur_adi from turler", baglanti);
            SqlDataReader dr = command.ExecuteReader();

            while (dr.Read())
            {
                ComboboxItem item = new ComboboxItem();
                item.Text = dr[1].ToString();
                item.Value = dr[0];
                comboBox2.Items.Add(item);
                comboBox4.Items.Add(item);
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
                comboBox5.Items.Add(item);

            }
            baglanti.Close();
        }
        public void albumlisteleme()
        {//albümleri comboboxta gösterme işlemi
            baglanti.Close();
            baglanti.Open();
            SqlCommand command1 = new SqlCommand("select album_id,album_adi from albumler", baglanti);
            SqlDataReader dr1 = command1.ExecuteReader();
            while (dr1.Read())
            {

                ComboboxItem item = new ComboboxItem();
                item.Text = dr1[1].ToString();
                item.Value = dr1[0];
                comboBox3.Items.Add(item);

            }
            baglanti.Close();
        }
        private void sanatcılisteleme()
        {    //sanatçıları comboboxta gösterme işlemi

            baglanti.Open();
            SqlCommand command2 = new SqlCommand("select sanatci_id,sanatci_ad from sanatcilar", baglanti);
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


        private void button7_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            sanatcıları_listele(); // sanatçıları listviewde görüntüleme      
           
            sanatcılisteleme(); //sanatçıları comboboxa getirme
        }

        private void button3_Click(object sender, EventArgs e)
        {
            albumlerı_listele(); //albümleri listviewde görüntüleme
            comboBox3.Items.Clear();
            albumlisteleme(); //albümleri comboboxta görüntüleme
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //sanatçı ekleme butonu
            if (textBox3.Text == "")
            {

                MessageBox.Show("lütfen sanatçı ismi girin");


            }
            else
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into sanatcilar(sanatci_ad,ulke_id) values(@s1,@s2)", baglanti);
                komut.Parameters.AddWithValue("@s1", textBox3.Text);
                komut.Parameters.AddWithValue("@s2", (comboBox5.SelectedItem as ComboboxItem).Value.ToString());
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("sanatçı eklendi");
               

            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (textBox7.Text=="" || comboBox4.Text=="")
            {
                MessageBox.Show("ALBÜM İSMİ/TÜRÜ BOŞ BIRAKILAMAZ");
            }
            else
            {
                bool var_mı = false;
                if (comboBox4.Text=="pop")
                {
                   
                    for (int i = 0; i < listView2.Items.Count; i++)
                    {

                        if (listView2.Items[i].SubItems[1].Text == textBox7.Text)
                        {
                          
                            var_mı = true;
                            

                        }
                       
                    }
                    
                }
                else if(comboBox4.Text=="jazz")
                {
                 
                    for (int i = 0; i < listView2.Items.Count; i++)
                    {

                        if (listView2.Items[i].SubItems[1].Text == textBox7.Text)
                        {
                         
                            var_mı = true;

                        }
                    }
                }
                else
                {
                    
                    for (int i = 0; i < listView2.Items.Count; i++)
                    {

                        if (listView2.Items[i].SubItems[1].Text == textBox7.Text)
                        {
                        
                            var_mı = true;

                        }
                    }
                }

                if (var_mı)
                {
                    MessageBox.Show("LÜTFEN FARKLI ALBÜM TÜRÜ GİRİN");
                }
                else
                {
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("insert into albumler(album_adi,tarih,tur_id) values(@a1,@a2,@a3)", baglanti);
                    komut.Parameters.AddWithValue("@a1", textBox7.Text);
                    komut.Parameters.AddWithValue("@a2", dateTimePicker1.Value);
                    komut.Parameters.AddWithValue("@a3", (comboBox4.SelectedItem as ComboboxItem).Value.ToString());
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("ALBÜM EKLENDİ");
                }

                       
            }
            
        }
        int id = 0;
        int id1 = 0;
        private void button2_Click(object sender, EventArgs e)
        { //albüm silme
            baglanti.Open();
            SqlCommand komut1 = new SqlCommand("delete from albumler where album_id=(" + id1 + ")", baglanti);
            komut1.ExecuteNonQuery();

            baglanti.Close();
            albumlerı_listele();
        }

        private void button1_Click(object sender, EventArgs e)
        { //müzik ekleme
            if (textBox2.Text == "" || textBox6.Text == "" || comboBox1.Text == "" || comboBox2.Text == "" || comboBox3.Text == "")
            {
                MessageBox.Show("lütfen şarkı ismi girin");

            }
            else 
            {

                baglanti.Open();
                SqlCommand command = new SqlCommand("insert into sarkilar(sarki_ad,tarih,sure,album_id) values(@s1,@s2,@s3,@s4)", baglanti);

                command.Parameters.AddWithValue("@s1", textBox2.Text);
                command.Parameters.AddWithValue("@s2", dateTimePicker2.Value);

                //command.Parameters.AddWithValue("@s6", textBox2.Text);
                command.Parameters.AddWithValue("@s3", Convert.ToDecimal(textBox6.Text));
                command.Parameters.AddWithValue("@s4", (comboBox3.SelectedItem as ComboboxItem).Value.ToString());
               // command.Parameters.AddWithValue("@s5", (comboBox2.SelectedItem as ComboboxItem).Value.ToString());
                command.ExecuteNonQuery();
                
                baglanti.Close();
                MessageBox.Show("şarkı eklendi");

               
               
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            bilgileriGoster();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
           
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
                    "inner join turler as t on a.tur_id = t.tur_id where s.sarki_ad like '%" + textBox1.Text + "%'", baglanti);
                    
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
                        ekle.SubItems.Add(oku["dinlenme_sayisi"].ToString());

                        listView1.Items.Add(ekle);

                    }

                    baglanti.Close();
                
                
            }
           
        }

        private void button8_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from sanatcilar where sanatci_id=(" + id + ")", baglanti);

            komut.ExecuteNonQuery();

            baglanti.Close();
            sanatcıları_listele();

            baglanti.Close();
        }

        private void listView4_SelectedIndexChanged(object sender, EventArgs e)
        {
            id = int.Parse(listView4.SelectedItems[0].SubItems[0].Text);
            textBox3.Text = listView4.SelectedItems[0].SubItems[1].Text;
            comboBox5.Text = listView4.SelectedItems[0].SubItems[2].Text;
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            id1 = int.Parse(listView2.SelectedItems[0].SubItems[0].Text);
            textBox7.Text = listView2.SelectedItems[0].SubItems[1].Text;
            dateTimePicker1.Text = listView2.SelectedItems[0].SubItems[2].Text;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut1 = new SqlCommand("update albumler set album_adi='" + textBox7.Text.ToString() + "',tur_id='" + (comboBox4.SelectedItem as ComboboxItem).Value.ToString() + "' where album_id = " + id1 + "", baglanti);
            
            komut1.ExecuteNonQuery();

            baglanti.Close();
            albumlerı_listele();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut1 = new SqlCommand("update sanatcilar set sanatci_ad='" + textBox3.Text.ToString() + "',ulke_id='" + (comboBox5.SelectedItem as ComboboxItem).Value.ToString() + "' where sanatci_id = " + id + "", baglanti);

            komut1.ExecuteNonQuery();

            baglanti.Close();
            sanatcıları_listele();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        { // ŞARKILARI LİSTELEME 
            listView3.Items.Clear();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from sarkilar", baglanti);

            SqlDataReader oku = komut.ExecuteReader();

            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["sarki_id"].ToString();
                ekle.SubItems.Add(oku["sarki_ad"].ToString());
                

                listView3.Items.Add(ekle);

            }

            baglanti.Close();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indices = listView3.SelectedIndices;
            ListView.SelectedIndexCollection indices1 = listView4.SelectedIndices;
            if (indices.Count > 0 &&  indices.Count>0)
            {
                baglanti.Open();
                SqlCommand command1 = new SqlCommand("insert into sanatcilar_sarkilar(sarki_id,sanatci_id) Values(@k1,@k2) ", baglanti);
                command1.Parameters.AddWithValue("@k1", listView3.SelectedItems[0].Text);
                command1.Parameters.AddWithValue("@k2", listView4.SelectedItems[0].Text);
                command1.ExecuteNonQuery();

                baglanti.Close();
            }
            else
            {
                MessageBox.Show("LÜTFEN ŞARKI VE SANATÇI SEÇİN");
            }

            
        }
    }
}
