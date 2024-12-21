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

namespace TicariOtomasyon
{
    public partial class FrmFirmalar : Form
    {
        public FrmFirmalar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void firmalistesi()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_FIRMALAR", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        void sehirlistesi()
        {
            SqlCommand komut = new SqlCommand("Select SEHİR from TBL_ILLER", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                Cmbil.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        void carikodaciklamalar()
        {
            SqlCommand komut = new SqlCommand("Select FIRMAKOD1 from TBL_KODLAR", bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                RchKod1.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();
        }
        void temizle()
        {
            TxtAd.Focus();
            TxtAd.Text = "";
            Txtid.Text = "";
            TxtKod1.Text = "";
            TxtKod2.Text = "";
            TxtKod3.Text = "";
            TxtMail.Text = "";
            TxtSektör.Text = "";
            TxtVergi.Text = "";
            TxtYetkili.Text = "";
            TxtYetkiliGorev.Text = "";
            MskYetkiliTC.Text = "";
            MskFax.Text = "";
            MskTelefon1.Text = "";
            MskTelefon2.Text = "";
            MskTelefon3.Text = "";
            RchAdres.Text = "";
        }
        private void FrmFirmalar_Load(object sender, EventArgs e)
        {
            firmalistesi();

            sehirlistesi();

            carikodaciklamalar();

            temizle();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                Txtid.Text = dr["ID"].ToString();
                TxtAd.Text = dr["AD"].ToString();
                TxtYetkiliGorev.Text = dr["YETKILISTATU"].ToString();
                TxtYetkili.Text = dr["YETKILIADSOYAD"].ToString();
                MskYetkiliTC.Text = dr["YETKILITC"].ToString();
                TxtSektör.Text = dr["SEKTOR"].ToString();
                MskTelefon1.Text = dr["TELEFON1"].ToString();
                MskTelefon2.Text = dr["TELEFON2"].ToString();
                MskTelefon3.Text = dr["TELEFON3"].ToString();
                TxtMail.Text = dr["MAIL"].ToString();
                MskFax.Text = dr["FAX"].ToString();
                Cmbil.Text = dr["IL"].ToString();
                Cmbilce.Text = dr["ILCE"].ToString();
                TxtVergi.Text = dr["VERGIDAIRE"].ToString();
                RchAdres.Text = dr["ADRES"].ToString();
                TxtKod1.Text = dr["OZELKOD1"].ToString();
                TxtKod2.Text = dr["OZELKOD2"].ToString();
                TxtKod3.Text = dr["OZELKOD3"].ToString();
            }
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            DialogResult diyalog;
            diyalog = MessageBox.Show("Firmayı sisteme kaydetmek istediğinizden emin misiniz?", "Soru Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (diyalog==DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("insert into TBL_FIRMALAR (AD,YETKILISTATU,YETKILIADSOYAD,YETKILITC,SEKTOR,TELEFON1,TELEFON2,TELEFON3,MAIL,FAX,IL,ILCE,VERGIDAIRE,ADRES,OZELKOD1,OZELKOD2,OZELKOD3) VALUES(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17)", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", TxtAd.Text);
                komut.Parameters.AddWithValue("@p2", TxtYetkiliGorev.Text);
                komut.Parameters.AddWithValue("@p3", TxtYetkili.Text);
                komut.Parameters.AddWithValue("@p4", MskYetkiliTC.Text);
                komut.Parameters.AddWithValue("@p5", TxtSektör.Text);
                komut.Parameters.AddWithValue("@p6", MskTelefon1.Text);
                komut.Parameters.AddWithValue("@p7", MskTelefon2.Text);
                komut.Parameters.AddWithValue("@p8", MskTelefon3.Text);
                komut.Parameters.AddWithValue("@p9", TxtMail.Text);
                komut.Parameters.AddWithValue("@p10", MskFax.Text);
                komut.Parameters.AddWithValue("@p11", Cmbil.Text);
                komut.Parameters.AddWithValue("@p12", Cmbilce.Text);
                komut.Parameters.AddWithValue("@p13", TxtVergi.Text);
                komut.Parameters.AddWithValue("@p14", RchAdres.Text);
                komut.Parameters.AddWithValue("@p15", TxtKod1.Text);
                komut.Parameters.AddWithValue("@p16", TxtKod2.Text);
                komut.Parameters.AddWithValue("@p17", TxtKod3.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Firma Sisteme Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                firmalistesi();
                temizle();
            }
           
        }

        private void Cmbil_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cmbilce.Properties.Items.Clear();

            SqlCommand komut = new SqlCommand("Select ILCE from TBL_ILCELER where Sehir=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", Cmbil.SelectedIndex + 1);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                Cmbilce.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            DialogResult diyalog;
            diyalog = MessageBox.Show("Firma bilgilerini silmek istediğinizden emin misiniz?", "Soru Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (diyalog==DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("Delete from TBL_FIRMALAR where ID=@p1", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", Txtid.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                firmalistesi();
                MessageBox.Show("Firma Listeden Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                temizle();
            }
            
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            DialogResult diyalog;
            diyalog = MessageBox.Show("Firma bilgilerini güncellemek istediğinizden emin misiniz?", "Soru Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (diyalog==DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("Update TBL_FIRMALAR set AD=@p1,YETKILISTATU=@p2,YETKILIADSOYAD=@p3,YETKILITC=@p4,SEKTOR=@p5,TELEFON1=@p6,TELEFON2=@p7,TELEFON3=@p8,MAIL=@p9,IL=@p10,ILCE=@p11,FAX=@p12,VERGIDAIRE=@p13,ADRES=@p14,OZELKOD1=@p15,OZELKOD2=@p16,OZELKOD3=@p17 WHERE ID=@p18", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", TxtAd.Text);
                komut.Parameters.AddWithValue("@p2", TxtYetkiliGorev.Text);
                komut.Parameters.AddWithValue("@p3", TxtYetkili.Text);
                komut.Parameters.AddWithValue("@p4", MskYetkiliTC.Text);
                komut.Parameters.AddWithValue("@p5", TxtSektör.Text);
                komut.Parameters.AddWithValue("@p6", MskTelefon1.Text);
                komut.Parameters.AddWithValue("@p7", MskTelefon2.Text);
                komut.Parameters.AddWithValue("@p8", MskTelefon3.Text);
                komut.Parameters.AddWithValue("@p9", TxtMail.Text);
                komut.Parameters.AddWithValue("@p10", Cmbil.Text);
                komut.Parameters.AddWithValue("@p11", Cmbilce.Text);
                komut.Parameters.AddWithValue("@p12", MskFax.Text);
                komut.Parameters.AddWithValue("@p13", TxtVergi.Text);
                komut.Parameters.AddWithValue("@p14", RchAdres.Text);
                komut.Parameters.AddWithValue("@p15", TxtKod1.Text);
                komut.Parameters.AddWithValue("@p16", TxtKod2.Text);
                komut.Parameters.AddWithValue("@p17", TxtKod3.Text);
                komut.Parameters.AddWithValue("@p18", Txtid.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Firma Bilgileri Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                firmalistesi();
                temizle();
            }
            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}