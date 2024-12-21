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
    public partial class FrmFaturalar : Form
    {
        public FrmFaturalar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();
        void listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_FATURABILGI", bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        void temizle()
        {
            TxtAlıcı.Text = "";
            Txtid.Text = "";
            TxtSeri.Text = "";
            TxtSıraNo.Text = "";
            TxtTeslimAlan.Text = "";
            TxtTeslimEden.Text = "";
            TxtVergiDaire.Text = "";
            MskTarih.Text = "";
            MskSaat.Text = "";
        }
        private void FrmFaturalar_Load(object sender, EventArgs e)
        {
            listele();

            temizle();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            if (TxtFaturaid.Text == "")
            {
                SqlCommand komut = new SqlCommand("insert into TBL_FATURABILGI (SERI,SIRANO,TARIH,SAAT,VERGIDAIRE,ALICI,TESLIMEDEN,TESLIMALAN) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", TxtSeri.Text);
                komut.Parameters.AddWithValue("@p2", TxtSıraNo.Text);
                komut.Parameters.AddWithValue("@p3", MskTarih.Text);
                komut.Parameters.AddWithValue("@p4", MskSaat.Text);
                komut.Parameters.AddWithValue("@p5", TxtVergiDaire.Text);
                komut.Parameters.AddWithValue("@p6", TxtAlıcı.Text);
                komut.Parameters.AddWithValue("@p7", TxtTeslimEden.Text);
                komut.Parameters.AddWithValue("@p8", TxtTeslimAlan.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Fatura Bilgisi Sisteme Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
            //Firma Carisi
            if (TxtFaturaid.Text != "" && CmbCariTürü.Text=="Firma")
            {
                double miktar, tutar, fiyat;
                fiyat = Convert.ToDouble(TxtFiyat.Text);
                miktar = Convert.ToDouble(TxtMiktar.Text);
                tutar = miktar * fiyat;
                TxtTutar.Text = tutar.ToString();

                SqlCommand komut2 = new SqlCommand("insert into TBL_FATURADETAY (URUNAD,MIKTAR,FIYAT,TUTAR,FATURAID) values (@p1,@p2,@p3,@p4,@p5)", bgl.baglanti());
                komut2.Parameters.AddWithValue("@p1", TxtUrunAd.Text);
                komut2.Parameters.AddWithValue("@p2", TxtMiktar.Text);
                komut2.Parameters.AddWithValue("@p3", decimal.Parse(TxtFiyat.Text));
                komut2.Parameters.AddWithValue("@p4", decimal.Parse(TxtTutar.Text));
                komut2.Parameters.AddWithValue("@p5", TxtFaturaid.Text);
                komut2.ExecuteNonQuery();
                bgl.baglanti().Close();

                //Hareket Tablosuna Veri Girişi
                SqlCommand komut3 = new SqlCommand("insert into TBL_FIRMAHAREKETLER (URUNID,ADET,PERSONEL,FIRMA,FIYAT,TOPLAM,FATURAID,TARIH) values(@h1,@h2,@h3,@h4,@h5,@h6,@h7,@h8)", bgl.baglanti());
                komut3.Parameters.AddWithValue("@h1", TxtUrunid.Text);
                komut3.Parameters.AddWithValue("@h2", TxtMiktar.Text);
                komut3.Parameters.AddWithValue("@h3", TxtPersonel.Text);
                komut3.Parameters.AddWithValue("@h4", TxtFırma.Text);
                komut3.Parameters.AddWithValue("@h5", decimal.Parse(TxtFiyat.Text));
                komut3.Parameters.AddWithValue("@h6", decimal.Parse(TxtTutar.Text));
                komut3.Parameters.AddWithValue("@h7", TxtFaturaid.Text);
                komut3.Parameters.AddWithValue("@h8", MskTarih.Text);
                komut3.ExecuteNonQuery();
                bgl.baglanti().Close();

                //Stok Sayısını Azaltma
                SqlCommand komut4 = new SqlCommand("Update TBL_URUNLER set ADET=ADET-@s1 where ID=@s2", bgl.baglanti());
                komut4.Parameters.AddWithValue("@s1", TxtMiktar.Text);
                komut4.Parameters.AddWithValue("@s2", TxtUrunid.Text);
                komut4.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Faturaya Ait Ürün Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //Müşteri Carisi
            if (TxtFaturaid.Text != "" && CmbCariTürü.Text == "Müşteri")
            {
                double miktar, tutar, fiyat;
                fiyat = Convert.ToDouble(TxtFiyat.Text);
                miktar = Convert.ToDouble(TxtMiktar.Text);
                tutar = miktar * fiyat;
                TxtTutar.Text = tutar.ToString();

                SqlCommand komut2 = new SqlCommand("insert into TBL_FATURADETAY (URUNAD,MIKTAR,FIYAT,TUTAR,FATURAID) values (@p1,@p2,@p3,@p4,@p5)", bgl.baglanti());
                komut2.Parameters.AddWithValue("@p1", TxtUrunAd.Text);
                komut2.Parameters.AddWithValue("@p2", TxtMiktar.Text);
                komut2.Parameters.AddWithValue("@p3", decimal.Parse(TxtFiyat.Text));
                komut2.Parameters.AddWithValue("@p4", decimal.Parse(TxtTutar.Text));
                komut2.Parameters.AddWithValue("@p5", TxtFaturaid.Text);
                komut2.ExecuteNonQuery();
                bgl.baglanti().Close();

                //Hareket Tablosuna Veri Girişi
                SqlCommand komut3 = new SqlCommand("insert into TBL_MUSTERIHAREKETLER (URUNID,ADET,PERSONEL,MUSTERI,FIYAT,TOPLAM,FATURAID,TARIH) values(@h1,@h2,@h3,@h4,@h5,@h6,@h7,@h8)", bgl.baglanti());
                komut3.Parameters.AddWithValue("@h1", TxtUrunid.Text);
                komut3.Parameters.AddWithValue("@h2", TxtMiktar.Text);
                komut3.Parameters.AddWithValue("@h3", TxtPersonel.Text);
                komut3.Parameters.AddWithValue("@h4", TxtFırma.Text);
                komut3.Parameters.AddWithValue("@h5", decimal.Parse(TxtFiyat.Text));
                komut3.Parameters.AddWithValue("@h6", decimal.Parse(TxtTutar.Text));
                komut3.Parameters.AddWithValue("@h7", TxtFaturaid.Text);
                komut3.Parameters.AddWithValue("@h8", MskTarih.Text);
                komut3.ExecuteNonQuery();
                bgl.baglanti().Close();

                //Stok Sayısını Azaltma
                SqlCommand komut4 = new SqlCommand("Update TBL_URUNLER set ADET=ADET-@s1 where ID=@s2", bgl.baglanti());
                komut4.Parameters.AddWithValue("@s1", TxtMiktar.Text);
                komut4.Parameters.AddWithValue("@s2", TxtUrunid.Text);
                komut4.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Faturaya Ait Ürün Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                Txtid.Text = dr["FATURABILGIID"].ToString();
                TxtSıraNo.Text = dr["SIRANO"].ToString();
                TxtSeri.Text = dr["SERI"].ToString();
                MskTarih.Text = dr["TARIH"].ToString();
                MskSaat.Text = dr["SAAT"].ToString();
                TxtAlıcı.Text = dr["ALICI"].ToString();
                TxtTeslimEden.Text = dr["TESLIMEDEN"].ToString();
                TxtTeslimAlan.Text = dr["TESLIMALAN"].ToString();
                TxtVergiDaire.Text = dr["VERGIDAIRE"].ToString();
            }
        }
        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            DialogResult diyalog;
            diyalog = MessageBox.Show("Fatura bilgilerini silmek istediğinizden emin misiniz?", "Soru Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (diyalog==DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("Delete from TBL_FATURABILGI where FATURABILGIID=@p1", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", Txtid.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Fatura Silindi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                listele();
            }
            
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            DialogResult diyalog;
            diyalog = MessageBox.Show("Fatura bilgilerini güncellemek istediğinizden emin misiniz?", "Soru Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (diyalog==DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("Update TBL_FATURABILGI set SERI=@p1,SIRANO=@p2,TARIH=@p3,SAAT=@p4,VERGIDAIRE=@p5,ALICI=@p6,TESLIMEDEN=@p7,TESLIMALAN=@p8 where FATURABILGIID=@p9", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", TxtSeri.Text);
                komut.Parameters.AddWithValue("@p2", TxtSıraNo.Text);
                komut.Parameters.AddWithValue("@p3", MskTarih.Text);
                komut.Parameters.AddWithValue("@p4", MskSaat.Text);
                komut.Parameters.AddWithValue("@p5", TxtVergiDaire.Text);
                komut.Parameters.AddWithValue("@p6", TxtAlıcı.Text);
                komut.Parameters.AddWithValue("@p7", TxtTeslimEden.Text);
                komut.Parameters.AddWithValue("@p8", TxtTeslimAlan.Text);
                komut.Parameters.AddWithValue("@p9", Txtid.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Fatura Bilgisi Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
           
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            //Formlar arası fatura ID değeri taşıma
            FrmFaturaUrunDetay fr = new FrmFaturaUrunDetay();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dr != null)
            {
                fr.id = dr["FATURABILGIID"].ToString();
            }
            fr.Show();
        }

        private void BtnBul_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select URUNAD,SATISFIYAT From TBL_URUNLER where ID=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtUrunid.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                TxtUrunAd.Text = dr[0].ToString();
                TxtFiyat.Text = dr[1].ToString();
            }
            bgl.baglanti().Close();
        }

        private void BtnTemizle_Click_1(object sender, EventArgs e)
        {
            temizle();
        }
    }
}
