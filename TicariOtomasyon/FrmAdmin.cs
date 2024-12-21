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
    public partial class FrmAdmin : Form
    {
        public FrmAdmin()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox1.CheckState == CheckState.Checked)
            {
                TxtSifre.Properties.UseSystemPasswordChar = false;
            }
            else if (checkBox1.CheckState == CheckState.Unchecked)
            {
                TxtSifre.Properties.UseSystemPasswordChar = true;
            }
        }

        private void BtnGirisYap_Click_1(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select * from TBL_ADMIN where KullaniciAd=@p1 and Sifre=@p2", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtKullanıcıAdı.Text);
            komut.Parameters.AddWithValue("@p2", TxtSifre.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                FrmAnaModul fr = new FrmAnaModul();
                fr.kullanici = TxtKullanıcıAdı.Text;
                fr.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı Kullanıcı Adı ya da Şifre", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            bgl.baglanti().Close();
        }

        private void BtnGirisYap_MouseLeave_1(object sender, EventArgs e)
        {
            BtnGirisYap.BackColor = Color.LightSeaGreen;
        }

        private void BtnGirisYap_MouseHover(object sender, EventArgs e)
        {
            BtnGirisYap.BackColor = Color.Yellow;
        }
    }
}
