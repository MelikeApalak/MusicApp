using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MuzikUygulaması
{
    class AdminGirisSınıfı
    {
        SqlConnection baglanti = new SqlConnection("Data Source =DESKTOP-BD7IC7E; Initial Catalog = MuzikUygulamasi; Integrated Security = True");
        SqlCommand command;
        SqlDataReader reader;
        public void GirisYap(string ad, string sifre, Form frm1)
        {
            command = new SqlCommand("select * from adminler where admin_adi='" + ad + "' and sifre='" + sifre + "'", baglanti);
            baglanti.Open();
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                AdminİslemFormu admingirisi = new AdminİslemFormu();
                admingirisi.Show();

                frm1.Hide();
            }
            else
            {
                MessageBox.Show("hatali giris", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            baglanti.Close();
            baglanti.Dispose();
        }
    }
}
