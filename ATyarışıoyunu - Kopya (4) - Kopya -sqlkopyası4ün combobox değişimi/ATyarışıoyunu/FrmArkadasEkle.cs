using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ATyarışıoyunu
{
    public partial class FrmArkadasEkle : Form
    {
        public FrmArkadasEkle()
        {
            InitializeComponent();
        }
        sqlbaglantisi bag = new sqlbaglantisi();
        List<string> secilenSartlar = new List<string>();
        public string adi;
        
        private void FrmArkadasEkle_Load(object sender, EventArgs e)
        {

            lbKullaniciAdi.Text = adi;
            
            /*
            SqlCommand cmd = new SqlCommand("select KullaniciAd from Tbl_AtYarısı",bag.baglanti());
            SqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {
                if ((textBox1.Text)==(reader.GetString(0)))
                {
                    MessageBox.Show("ARKADAS EKLENDİ");

                }
                else
                {
                    MessageBox.Show("BOYLE BİR ARKADAS BULUNAMADİ");
                }
            }
            reader.Close();
            bag.baglanti().Close();
            */


            /*
            while (reader.Read())
            {
                comboBox1.Items.Add(reader[0].ToString());
                
            }
            reader.Close();
            bag.baglanti().Close();
            */


            SqlCommand md = new SqlCommand("select ArkadasAdi from Tbl_Oyuncular where KullaniciAd=  '" + adi + "' ", bag.baglanti());
            SqlDataReader reader1 = md.ExecuteReader();
            while (reader1.Read())
            {

                secilenSartlar.Add(reader1.GetString(0));
            }
            int sartSayisi = secilenSartlar.Count;


            reader1.Close();
            bag.baglanti().Close();
            string unionquery = "";
            
            if (sartSayisi != 0)
            {


                foreach (string sartlar in secilenSartlar)
                {
                    unionquery += $"SELECT KacinciOyunu, OyuncuAdi, BitirmeSuresi, Bahis, SecilenAt FROM(SELECT TOP 1 KacinciOyunu, OyuncuAdi, BitirmeSuresi, Bahis, SecilenAt FROM Tbl_AtOyunTablosu Where OyuncuAdi= '{sartlar}'AND Kazanmadurumu = 1 ORDER BY BitirmeSuresi, Bahis DESC) AS Sorgu1 UNION ALL ";


                }
                unionquery = unionquery.Substring(0, unionquery.Length - 10);


                SqlDataAdapter adapter = new SqlDataAdapter(unionquery, bag.baglanti());
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;

            }

            
            





        }

        
        bool arkadasbulundu=false;
        public string secilenSart;
        private void button1_Click(object sender, EventArgs e)
        {
           
            SqlCommand md = new SqlCommand("select KullaniciAd from Tbl_AtYarısı", bag.baglanti());
            SqlDataReader reader = md.ExecuteReader();


            while (reader.Read())
            {
                secilenSart = textBox2.Text;
                if (textBox2.Text == reader.GetString(0)&& !secilenSartlar.Contains(textBox2.Text))
                {
                    textBox2.Text=reader.GetString(0);
                    secilenSart = textBox2.Text;

                    secilenSartlar.Add(secilenSart);

                    SqlCommand cmd = new SqlCommand("insert into Tbl_Oyuncular (KullaniciAd,ArkadasAdi) values (@p1,@p2)", bag.baglanti());
                    cmd.Parameters.AddWithValue("@p1", adi);
                    cmd.Parameters.AddWithValue("@p2", textBox2.Text);
                    cmd.ExecuteNonQuery();
                    //ListBox.Items.Add(secilenSart); // Şartları göstermek için bir ListBox'a da ekleyebilirsiniz
                    bag.baglanti().Close();

                    MessageBox.Show("ARKADAS EKLENDİ");
                    arkadasbulundu = true;
                    
                    break;
                }
                else if (textBox2.Text == "")
                {
                    MessageBox.Show("BIR ARKADAS GIRILMEDI");
                    arkadasbulundu = true;
                    break;
                }                          
                else
                {
                    arkadasbulundu = false;
                }

            }
            reader.Close();
            bag.baglanti().Close();


            /*
            if (!arkadasbulundu)
            {
                MessageBox.Show("BOYLE BİR ARKADAS BULUNAMADI");
            }
            */



            /*
            if (!secilenSartlar.Contains(secilenSart) && secilenSart!=null)
            {
                secilenSartlar.Add(secilenSart);
               
                SqlCommand cmd = new SqlCommand("insert into Tbl_Oyuncular (KullaniciAd,ArkadasAdi) values (@p1,@p2)", bag.baglanti());
                cmd.Parameters.AddWithValue("@p1",adi);
                cmd.Parameters.AddWithValue("@p2", textBox2.Text);
                cmd.ExecuteNonQuery();             
                //ListBox.Items.Add(secilenSart); // Şartları göstermek için bir ListBox'a da ekleyebilirsiniz
                bag.baglanti().Close();
            }
           */
       
            if (arkadasbulundu==false&& secilenSartlar.Contains(secilenSart))
            {
                Guncelle();
      
                //MessageBox.Show("BOYLE BIR KULLANICI ZATEN GIRILDI");
            }
            else if(arkadasbulundu == false)
            {
                 MessageBox.Show("BOYLE BIR KULLANICI YOK");
            }

            /*

            //string query = "SELECT * FROM Tbl_AtOyunTablosu where OyuncuAdi='" + comboBox1.Text + "' and Kazanmadurumu = 1 order by BitirmeSuresi, Bahis desc";
            string query = "SELECT top 1 KacinciOyun,OyuncuAdi,BitirmeSuresi,Bahis,SecilenAt FROM Tbl_AtOyunTablosu where OyuncuAdi ='" + adi + "'  and Kazanmadurumu = 1 order by BitirmeSuresi, Bahis desc";
            SqlDataAdapter adapter = new SqlDataAdapter(query, bag.baglanti());
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;

            if (dataTable.Rows.Count>0)
                textBox1.AppendText(dataTable.Rows[0].Field<int>("KacinciOyun").ToString() +" - "+ dataTable.Rows[0].Field<string>("OyuncuAdi") + " - " + dataTable.Rows[0].Field<int>("BitirmeSuresi").ToString() + " - " + dataTable.Rows[0].Field<int>("Bahis").ToString() +" - " + dataTable.Rows[0].Field<int>("SecilenAt") + "\r\n");
            */

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
        }
        void Guncelle()
        {
            string unionquery = "";
            int sartSayisi = secilenSartlar.Count;
            foreach (string sartlar in secilenSartlar)
            {
                unionquery += $"SELECT KacinciOyunu, OyuncuAdi, BitirmeSuresi, Bahis, SecilenAt FROM(SELECT TOP 1 KacinciOyunu, OyuncuAdi, BitirmeSuresi, Bahis, SecilenAt FROM Tbl_AtOyunTablosu Where OyuncuAdi= '{sartlar}'AND Kazanmadurumu = 1 ORDER BY BitirmeSuresi, Bahis DESC) AS Sorgu1 UNION ALL ";


            }
            try
            {
                unionquery = unionquery.Substring(0, unionquery.Length - 10);
                SqlDataAdapter adapter = new SqlDataAdapter(unionquery, bag.baglanti());
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;

            }
            catch (Exception)
            {

                MessageBox.Show("OLMAYAN BIR SEY EKLENEMEZ");
            }

            //unionquery = unionquery.Substring(0, unionquery.Length - 10);


        }




        private void button2_Click(object sender, EventArgs e)
        {
            /*
            // secilenSartlar dizisini kullanarak SQL sorgusu oluşturun
            string sqlSorgusu = "SELECT TOP 2 KacinciOyun, OyuncuAdi, BitirmeSuresi, Bahis, SecilenAt FROM Tbl_AtOyunTablosu WHERE ( OyuncuAdi=";

            if (secilenSartlar.Count > 0)
            {
                string formattedSartlar = string.Join(" OR OyuncuAdi = ", secilenSartlar.Select(s => $"'{s}'"));
                sqlSorgusu += formattedSartlar;
            }

            sqlSorgusu += ") AND Kazanmadurumu = 1 ORDER BY BitirmeSuresi, Bahis DESC";

            SqlDataAdapter adapter = new SqlDataAdapter(sqlSorgusu, bag.baglanti());
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            */

            /*
            string sqlSorgusu =" SELECT KacinciOyun, OyuncuAdi, BitirmeSuresi, Bahis, SecilenAt FROM(SELECT TOP 1 KacinciOyun, OyuncuAdi, BitirmeSuresi, Bahis, SecilenAt FROM Tbl_AtOyunTablosu WHERE OyuncuAdi = ";

            if (secilenSartlar.Count > 0)
            {
                string formattedSartlar = string.Join(" ", secilenSartlar.Select(s => $"'{s}'"));
                sqlSorgusu += formattedSartlar;
            }

            sqlSorgusu += "AND Kazanmadurumu = 1 ORDER BY BitirmeSuresi, Bahis DESC) AS Sorgu1 UNION ALL";
            */
            
            
            /*
            void Guncelle(){
                string unionquery = "";
                int sartSayisi = secilenSartlar.Count;
                foreach (string sartlar in secilenSartlar)
                {
                    unionquery += $"SELECT KacinciOyunu, OyuncuAdi, BitirmeSuresi, Bahis, SecilenAt FROM(SELECT TOP 1 KacinciOyunu, OyuncuAdi, BitirmeSuresi, Bahis, SecilenAt FROM Tbl_AtOyunTablosu Where OyuncuAdi= '{sartlar}'AND Kazanmadurumu = 1 ORDER BY BitirmeSuresi, Bahis DESC) AS Sorgu1 UNION ALL ";


                }
                try
                {
                    unionquery = unionquery.Substring(0, unionquery.Length - 10);
                    SqlDataAdapter adapter = new SqlDataAdapter(unionquery, bag.baglanti());
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;

                }
                catch (Exception)
                {

                    MessageBox.Show("OLMAYAN BIR SEY EKLENEMEZ");
                }

                //unionquery = unionquery.Substring(0, unionquery.Length - 10);


            }
            */
            Guncelle();

          







        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            pictureBox2.BackColor = Color.Red;
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.BackColor = this.BackColor;
        }
    }
}
