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
    public partial class FrmSekreterDetay : Form
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }

        SqlBaglanti bgl = new SqlBaglanti();
        public string tcno;
        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {
            lbltc.Text = tcno;
            SqlCommand komut = new SqlCommand("Select SekreterAd,SekreterSoyad from Tbl_Sekreter where SekreterTc=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", lbltc.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lbladsoyad.Text = dr[0] + " " + dr[1];
            }
            bgl.baglanti().Close();


            //Branşları Datagride aktarma
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Tbl_Branslar", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            //Doktorları datagride aktarma 
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("Select DoktorId,(DoktorAd +' '+ DoktorSoyad) as 'Doktorlar',DoktorBrans from Tbl_Doktorlar", bgl.baglanti());
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;

            SqlCommand komut3 = new SqlCommand("Select BransId, BransAd from Tbl_Branslar", bgl.baglanti());
            SqlDataReader dr2 = komut3.ExecuteReader();
            while (dr2.Read())
            {
                cmbbrans.Items.Add(dr2[1]);
            }
            bgl.baglanti().Close();
                  

        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut2 = new SqlCommand("insert into Tbl_Randevular (RandevuTarih,RandevuSaat,RandevuBrans,RandevuDoktor) values (@r1,@r2,@r3,@r4)", bgl.baglanti());
            komut2.Parameters.AddWithValue("@r1", mskdtxttarih.Text);
            komut2.Parameters.AddWithValue("@r2", mskdtxtsaat.Text);
            komut2.Parameters.AddWithValue("@r3", cmbbrans.Text);
            komut2.Parameters.AddWithValue("@r4", cmbdoktor.Text);
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kaydınız başarıyla Oluşturuldu");

        }

        private void cmbbrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbdoktor.Items.Clear();
            SqlCommand komut4 = new SqlCommand("Select DoktorAd,DoktorSoyad from Tbl_Doktorlar where DoktorBrans=@b1", bgl.baglanti());
            komut4.Parameters.AddWithValue("@b1", cmbbrans.Text);
            SqlDataReader dr3 = komut4.ExecuteReader();
            while (dr3.Read())
            {
                cmbdoktor.Items.Add(dr3[0] + " " + dr3[1]);

            }
            bgl.baglanti().Close();
        }

        private void btnolustur_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_Duyurular (Duyuru) values (@d1)", bgl.baglanti());
            komut.Parameters.AddWithValue("@d1", rcduyuru.Text);
            komut.ExecuteNonQuery();bgl.baglanti().Close();
            MessageBox.Show("Duyurunuz başarılı bir şekilde oluşturuldu");
        }

        private void btndoktorlar_Click(object sender, EventArgs e)
        {
            FrmDoktorEkle fr = new FrmDoktorEkle();
            fr.Show();

        }

        private void btnbranslar_Click(object sender, EventArgs e)
        {
            FrmBransPaneli fr2 = new FrmBransPaneli();
            fr2.Show();
        }

        private void btnrandevuliste_Click(object sender, EventArgs e)
        {
            FrmRandevuListesi fr3 = new FrmRandevuListesi();
            fr3.Show();
        }

        private void btnduyurular_Click(object sender, EventArgs e)
        {
            FrmDuyurular fr4 = new FrmDuyurular();
            fr4.Show();
        }
    }
}
