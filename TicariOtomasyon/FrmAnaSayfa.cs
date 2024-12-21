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
using System.Xml;

namespace TicariOtomasyon
{
    public partial class FrmAnaSayfa : Form
    {
        public FrmAnaSayfa()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void stoklar()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select URUNAD, Sum(ADET) as 'Adet' From TBL_URUNLER Group by URUNAD Having Sum(ADET)<=20 order by Sum(ADET)", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void ajanda()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select top 10 TARIH,SAAT,BASLIK From TBL_NOTLAR order by ID desc", bgl.baglanti());
            da.Fill(dt);
            gridControl2.DataSource = dt;
        }
        void FirmaHareketleri()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Exec FirmaHareket2", bgl.baglanti());
            da.Fill(dt);
            gridControl3.DataSource = dt;
        }

        void fihrist()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select AD,TELEFON1 From TBL_FIRMALAR", bgl.baglanti());
            da.Fill(dt);
            gridControl4.DataSource = dt;
        }

        void haberler()
        {
            XmlTextReader xmloku = new XmlTextReader("http://www.hurriyet.com.tr/rss/anasayfa");
            while (xmloku.Read())
            {
                if (xmloku.Name == "title")
                {
                    listBox1.Items.Add(xmloku.ReadString());
                }
            }
        }
        private void FrmAnaSayfa_Load(object sender, EventArgs e)
        {
            stoklar();

            ajanda();

            FirmaHareketleri();

            fihrist();

            haberler();

            webBrowser1.Navigate("http://www.tcmb.gov.tr/kurlar/today.xml");
        }
    }
}

