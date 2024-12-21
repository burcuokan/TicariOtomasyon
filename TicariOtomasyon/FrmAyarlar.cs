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
    public partial class FrmAyarlar : Form
    {
        public FrmAyarlar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();
        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBL_ADMIN", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        private void FrmAyarlar_Load_1(object sender, EventArgs e)
        {
            listele();
            TxtKullaniciAdi.Text = "";
            TxtSifre.Text = "";
        }

        private void Btnislem_Click(object sender, EventArgs e)
        {
            if (Btnislem.Text=="Kaydet")
            {
            SqlCommand komut = new SqlCommand("insert into TBL_ADMIN values (@p1,@p2)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtKullaniciAdi.Text);
            komut.Parameters.AddWithValue("@p2", TxtSifre.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Yeni Admin Sisteme Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            }
            if (Btnislem.Text=="Güncelle")
            {
                SqlCommand komut1 = new SqlCommand("Update TBL_ADMIN set Sifre=@p2 where KullaniciAd=@p1", bgl.baglanti());
                komut1.Parameters.AddWithValue("@p1", TxtKullaniciAdi.Text);
                komut1.Parameters.AddWithValue("@p2", TxtSifre.Text);
                komut1.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Kayıt Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                listele();
            }

        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if (dr !=null)
            {
                TxtKullaniciAdi.Text = dr["KullaniciAd"].ToString();
                TxtSifre.Text = dr["Sifre"].ToString();
            }
        }

        private void TxtKullaniciAdi_TextChanged(object sender, EventArgs e)
        {
            if (TxtKullaniciAdi.Text!="")
            {
                Btnislem.Text = "Güncelle";
                Btnislem.BackColor = Color.GreenYellow;
            }
            else
            {
                Btnislem.Text = "Kaydet";
                Btnislem.BackColor = Color.MediumTurquoise;
            }
        }
    }
}
