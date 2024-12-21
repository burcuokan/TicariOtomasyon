﻿using System;
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
    public partial class FrmBankalar : Form
    {
        public FrmBankalar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

       void listele()
        {

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Execute BankaBilgileri", bgl.baglanti());
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
        void firmalistesi()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select ID,AD from TBL_FIRMALAR", bgl.baglanti());
            da.Fill(dt);
            lookUpEdit1.Properties.ValueMember = "ID";
            lookUpEdit1.Properties.DisplayMember = "AD";
            lookUpEdit1.Properties.DataSource = dt;
        }

        void temizle()
        {
            TxtBankaAdı.Text = "";
            TxtHesapNo.Text = "";
            TxtHesapTuru.Text = "";
            TxtIban.Text = "";
            Txtid.Text = "";
            TxtSube.Text = "";
            TxtYetkili.Text = "";
            MskTarih.Text = "";
            MskTelefon.Text = "";
            lookUpEdit1.Text = "";
            Cmbil.Text = "";
            Cmbilce.Text = "";
        }
        private void FrmBankalar_Load(object sender, EventArgs e)
        {
            listele();

            sehirlistesi();

            firmalistesi();

            temizle();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            DialogResult diyalog;
            diyalog = MessageBox.Show("Banka bilgilerini kaydetmek istediğinizden emin misiniz?", "Soru Pencresi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (diyalog==DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("insert into TBL_BANKALAR (BANKAADI,IL,ILCE,SUBE,IBAN,HESAPNO,YETKILI,TELEFON,TARIH,HESAPTURU,FIRMAID) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11)", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", TxtBankaAdı.Text);
                komut.Parameters.AddWithValue("@p2", Cmbil.Text);
                komut.Parameters.AddWithValue("@p3", Cmbilce.Text);
                komut.Parameters.AddWithValue("@p4", TxtSube.Text);
                komut.Parameters.AddWithValue("@p5", TxtIban.Text);
                komut.Parameters.AddWithValue("@p6", TxtHesapNo.Text);
                komut.Parameters.AddWithValue("@p7", TxtYetkili.Text);
                komut.Parameters.AddWithValue("@p8", MskTelefon.Text);
                komut.Parameters.AddWithValue("@p9", MskTarih.Text);
                komut.Parameters.AddWithValue("@p10", TxtHesapTuru.Text);
                komut.Parameters.AddWithValue("@p11", lookUpEdit1.EditValue);
                
                komut.ExecuteNonQuery();
                listele();
                bgl.baglanti().Close();
                MessageBox.Show("Banka Bilgisi Sisteme Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                Txtid.Text = dr["ID"].ToString();
                TxtBankaAdı.Text = dr["BANKAADI"].ToString();
                Cmbil.Text = dr["IL"].ToString();
                Cmbilce.Text = dr["ILCE"].ToString();
                TxtSube.Text = dr["SUBE"].ToString();
                TxtIban.Text = dr["IBAN"].ToString();
                TxtHesapNo.Text = dr["HESAPNO"].ToString();
                TxtYetkili.Text = dr["YETKILI"].ToString();
                MskTelefon.Text = dr["TELEFON"].ToString();
                MskTarih.Text = dr["TARIH"].ToString();
                TxtHesapTuru.Text = dr["HESAPTURU"].ToString();
            }
        }
        private void BtnSil_Click(object sender, EventArgs e)
        {
            DialogResult diyalog;
            diyalog = MessageBox.Show("Banka bilgilerini silmek istediğinizden emin misiniz?", "Soru Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (diyalog==DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("Delete from TBL_BANKALAR where ID=@p1", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", Txtid.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                temizle();
                MessageBox.Show("Banka Bilgisi Sistemden Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                listele();
            }
            
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            DialogResult diyalog;
            diyalog = MessageBox.Show("Banka bilgilerini güncellemek istediğinizden emin misiniz?", "Soru Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (diyalog==DialogResult.Yes)
            {
                SqlCommand komut = new SqlCommand("Update TBL_BANKALAR set BANKAADI=@p1,IL=@p2,ILCE=@p3,SUBE=@p4,IBAN=@p5,HESAPNO=@p6,YETKILI=@p7,TELEFON=@p8,TARIH=@p9,HESAPTURU=@p10,FIRMAID=@p11 where ID=@p12", bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", TxtBankaAdı.Text);
                komut.Parameters.AddWithValue("@p2", Cmbil.Text);
                komut.Parameters.AddWithValue("@p3", Cmbilce.Text);
                komut.Parameters.AddWithValue("@p4", TxtSube.Text);
                komut.Parameters.AddWithValue("@p5", TxtIban.Text);
                komut.Parameters.AddWithValue("@p6", TxtHesapNo.Text);
                komut.Parameters.AddWithValue("@p7", TxtYetkili.Text);
                komut.Parameters.AddWithValue("@p8", MskTelefon.Text);
                komut.Parameters.AddWithValue("@p9", MskTarih.Text);
                komut.Parameters.AddWithValue("@p10", TxtHesapTuru.Text);
                komut.Parameters.AddWithValue("@p11", lookUpEdit1.EditValue);
                komut.Parameters.AddWithValue("@p12", Txtid.Text);
                komut.ExecuteNonQuery();
                listele();
                bgl.baglanti().Close();
                MessageBox.Show("Banka Bilgisi Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}
