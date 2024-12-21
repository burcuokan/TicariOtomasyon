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
    public partial class FrmGiderler : Form
    {
        public FrmGiderler()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();
        void giderlistesi()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_GIDERLER", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        private void FrmGiderler_Load(object sender, EventArgs e)
        {
            giderlistesi();

            temizle();
        }

        void temizle()
        {
            TxtDoğalgaz.Text = "";
            TxtEkstra.Text = "";
            TxtElektrik.Text = "";
            Txtid.Text = "";
            Txtİnternet.Text = "";
            TxtMaaşlar.Text = "";
            TxtSu.Text = "";
            CmbAy.Text = "";
            CmbYıl.Text = "";
            RchNotlar.Text = "";
        }
        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            DialogResult diyalog;
            diyalog = MessageBox.Show("Giderleri tabloya eklemek istediğinizden emin misiniz?", "Soru Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (diyalog==DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("insert into TBL_GIDERLER (AY,YIL,ELEKTRIK,SU,DOGALGAZ,INTERNET,MAASLAR,EKSTRA,NOTLAR) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", CmbAy.Text);
                komut.Parameters.AddWithValue("@p2", CmbYıl.Text);
                komut.Parameters.AddWithValue("@p3", decimal.Parse(TxtElektrik.Text));
                komut.Parameters.AddWithValue("@p4", decimal.Parse(TxtSu.Text));
                komut.Parameters.AddWithValue("@p5", decimal.Parse(TxtDoğalgaz.Text));
                komut.Parameters.AddWithValue("@p6", decimal.Parse(Txtİnternet.Text));
                komut.Parameters.AddWithValue("@p7", decimal.Parse(TxtMaaşlar.Text));
                komut.Parameters.AddWithValue("@p8", decimal.Parse(TxtEkstra.Text));
                komut.Parameters.AddWithValue("@p9", RchNotlar.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Gider Tabloya Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                giderlistesi();
                
            }
            
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr!=null)
            {
                Txtid.Text = dr["ID"].ToString();
                CmbAy.Text = dr["AY"].ToString();
                CmbYıl.Text = dr["YIL"].ToString();
                TxtElektrik.Text = dr["ELEKTRIK"].ToString();
                TxtSu.Text = dr["SU"].ToString();
                TxtDoğalgaz.Text = dr["DOGALGAZ"].ToString();
                Txtİnternet.Text = dr["INTERNET"].ToString();
                TxtMaaşlar.Text = dr["MAASLAR"].ToString();
                TxtEkstra.Text = dr["EKSTRA"].ToString();
                RchNotlar.Text = dr["NOTLAR"].ToString();
            }
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            DialogResult diyalog;
            diyalog = MessageBox.Show("Gider bilgilerini silmek istediğinizden emin misiniz?", "Soru Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (diyalog==DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("Delete from TBL_GIDERLER where ID=@p1", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", Txtid.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                giderlistesi();
                MessageBox.Show("Gider Listeden Silindi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                temizle();
            }
           
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            DialogResult diyalog;
            diyalog = MessageBox.Show("Gider bilgilerini güncellemek istediğinizden emin misiniz?", "Soru Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (diyalog==DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("Update TBL_GIDERLER set AY=@p1,YIL=@p2,ELEKTRIK=@p3,SU=@p4,DOGALGAZ=@p5,INTERNET=@p6,MAASLAR=@p7,EKSTRA=@p8,NOTLAR=@p9 where ID=@p10", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", CmbAy.Text);
                komut.Parameters.AddWithValue("@p2", CmbYıl.Text);
                komut.Parameters.AddWithValue("@p3", decimal.Parse(TxtElektrik.Text));
                komut.Parameters.AddWithValue("@p4", decimal.Parse(TxtSu.Text));
                komut.Parameters.AddWithValue("@p5", decimal.Parse(TxtDoğalgaz.Text));
                komut.Parameters.AddWithValue("@p6", decimal.Parse(Txtİnternet.Text));
                komut.Parameters.AddWithValue("@p7", decimal.Parse(TxtMaaşlar.Text));
                komut.Parameters.AddWithValue("@p8", decimal.Parse(TxtEkstra.Text));
                komut.Parameters.AddWithValue("@p9", RchNotlar.Text);
                komut.Parameters.AddWithValue("@p10", Txtid.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Gider Bilgisi Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                giderlistesi();
                temizle();
            }
           
        }
    }
}
