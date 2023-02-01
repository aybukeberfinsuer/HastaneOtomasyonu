using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace HastaneOtomasyonu
{
    public partial class FrmDoktorBilgiDuzenle : Form
    {
        public FrmDoktorBilgiDuzenle()
        {
            InitializeComponent();
        }

        SqlBaglanti bgl = new SqlBaglanti();
        public string tcno;
        private void FrmDoktorBilgiDuzenle_Load(object sender, EventArgs e)
        {
            msktc.Text = tcno;
            SqlCommand komut = new SqlCommand("Select * from Tbl_Doktorlar where DoktorTc=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", msktc.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                txtad.Text=dr[1].ToString();
                txtsoyad.Text = dr[2].ToString();
                txtsifre.Text = dr[5].ToString();

            }
            bgl.baglanti().Close();


        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut2 = new SqlCommand("Update Tbl_Doktorlar set DoktorAd=@u1,DoktorSoyad=@u3, DoktorSifre=@u4 where DoktorTc=@u2", bgl.baglanti());
            komut2.Parameters.AddWithValue("@u1", txtad.Text);
            komut2.Parameters.AddWithValue("@u2", msktc.Text);
            komut2.Parameters.AddWithValue("@u3", txtsoyad.Text);
            komut2.Parameters.AddWithValue("@u4", txtsifre.Text);
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Başarıyla güncellendi");

        }
    }
}
