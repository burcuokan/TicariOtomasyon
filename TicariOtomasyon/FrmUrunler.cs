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
    public partial class FrmUrunler : Form
    {
        public FrmUrunler()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From TBL_URUNLER", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }     
        void temizle()
        {
            TxtAd.Text = "";
            TxtAlıs.Text = "";
            Txtid.Text = "";
            TxtMarka.Text = "";
            TxtModel.Text = "";
            TxtSatış.Text = "";
            MskYıl.Text = "";
            NudAdet.Value = 0;
            RchDetay.Text = "";
        }
        private void FrmUrunler_Load(object sender, EventArgs e)
        {
            listele();

            temizle();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            DialogResult diyalog;
            diyalog = MessageBox.Show("Ürünü sisteme eklemek istediğinizden emin misiniz?", "Soru Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (diyalog==DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("insert into TBL_URUNLER (URUNAD,MARKA,MODEL,YIL,ADET,ALISFIYAT,SATISFIYAT,DETAY) values(@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", TxtAd.Text);
                komut.Parameters.AddWithValue("@p2", TxtMarka.Text);
                komut.Parameters.AddWithValue("@p3", TxtModel.Text);
                komut.Parameters.AddWithValue("@p4", MskYıl.Text);
                komut.Parameters.AddWithValue("@p5", int.Parse((NudAdet.Value).ToString()));
                komut.Parameters.AddWithValue("@p6", decimal.Parse(TxtAlıs.Text));
                komut.Parameters.AddWithValue("@p7", decimal.Parse(TxtSatış.Text));
                komut.Parameters.AddWithValue("@p8", RchDetay.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Ürün Sisteme Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
           
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            DialogResult diyalog;
            diyalog = MessageBox.Show("Ürünü silmek istediğinizden emin misiniz?", "Soru Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (diyalog==DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("Delete from TBL_URUNLER where ID=@p1", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", Txtid.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Ürün Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                listele();
            }
            
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            Txtid.Text = dr["ID"].ToString();
            TxtAd.Text = dr["URUNAD"].ToString();
            TxtMarka.Text = dr["MARKA"].ToString();
            TxtModel.Text = dr["MODEL"].ToString();
            MskYıl.Text = dr["YIL"].ToString();
            NudAdet.Text = dr["ADET"].ToString();
            TxtAlıs.Text = dr["ALISFIYAT"].ToString();
            TxtSatış.Text = dr["SATISFIYAT"].ToString();
            RchDetay.Text = dr["DETAY"].ToString();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            DialogResult diyalog;
            diyalog = MessageBox.Show("Ürün bilgisini güncellemek istediğinizden emin misiniz?", "Soru Pencresi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (diyalog==DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("Update TBL_URUNLER set URUNAD=@p1,MARKA=@p2,MODEL=@p3,YIL=@p4,ADET=@p5,ALISFIYAT=@p6,SATISFIYAT=@p7,DETAY=@p8 where ID=@p9", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", TxtAd.Text);
                komut.Parameters.AddWithValue("@p2", TxtMarka.Text);
                komut.Parameters.AddWithValue("@p3", TxtModel.Text);
                komut.Parameters.AddWithValue("@p4", MskYıl.Text);
                komut.Parameters.AddWithValue("@p5", int.Parse((NudAdet.Value).ToString()));
                komut.Parameters.AddWithValue("@p6", decimal.Parse(TxtAlıs.Text));
                komut.Parameters.AddWithValue("@p7", decimal.Parse(TxtSatış.Text));
                komut.Parameters.AddWithValue("@p8", RchDetay.Text);
                komut.Parameters.AddWithValue("@p9", Txtid.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Ürün Bilgisi Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                listele();
            }
           
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}
