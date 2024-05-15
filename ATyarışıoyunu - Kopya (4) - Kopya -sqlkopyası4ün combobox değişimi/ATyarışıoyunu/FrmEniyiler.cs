using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATyarışıoyunu
{
    public partial class FrmEniyiler : Form
    {
        public FrmEniyiler()
        {
            InitializeComponent();
        }
        sqlbaglantisi bag = new sqlbaglantisi();

        private void FrmEniyiler_Load(object sender, EventArgs e)

        {
            pictureBox2.BackColor = Color.Transparent;



            string toplist = "Select Top 10 OyuncuAdi, Bahis, BitirmeSuresi, SecilenAt, KazanmaDurumu From Tbl_AtOyunTablosu  Where Kazanmadurumu = 1 order by BitirmeSuresi, Bahis desc";





            SqlDataAdapter adapter = new SqlDataAdapter(toplist, bag.baglanti());
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;


            /*
            LbBirinci.Text = dataGridView1.Rows[0].Cells[0].Value.ToString();
            Lbikinci.Text = dataGridView1.Rows[1].Cells[0].Value.ToString();
            Lbucuncu.Text = dataGridView1.Rows[2].Cells[0].Value.ToString();
            */
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                try
                {
                    DataGridViewCell cell = dataGridView1.Rows[i].Cells[0];
                    if (cell.Value != null)
                    {
                        if (i == 0)
                            LbBirinci.Text = cell.Value.ToString();
                        else if (i == 1)
                            Lbikinci.Text = cell.Value.ToString();
                        else if (i == 2)
                            Lbucuncu.Text = cell.Value.ToString();
                    }
                    else
                    {
                        if (i == 0)
                            LbBirinci.Text = "YOK";
                        else if (i == 1)
                            Lbikinci.Text = "YOK";
                        else if (i == 2)
                            Lbucuncu.Text = "YOK";
                    }
                }
                catch (NullReferenceException)
                {
                    if (i == 0)
                        LbBirinci.Text = "YOK";
                    else if (i == 1)
                        Lbikinci.Text = "YOK";
                    else if (i == 2)
                        Lbucuncu.Text = "YOK";
                }
            }

            /*
            try
            {
                LbBirinci.Text = dataGridView1.Rows[0].Cells[0].Value.ToString();
               
                
            }
            catch (NullReferenceException)
            {
                LbBirinci.Text = "SU ANLIK YOK";


            }
            try
            {

                Lbikinci.Text = dataGridView1.Rows[1].Cells[0].Value.ToString();


            }       
            catch(NullReferenceException)
            {

                Lbikinci.Text = "SU ANLIK YOK";

            }
            try
            {
                Lbucuncu.Text = dataGridView1.Rows[2].Cells[0].Value.ToString();

            }
            catch(NullReferenceException)
            {

                Lbucuncu.Text = "SU ANLIK YOK";

            }
            
        }
        */
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox4_MouseHover(object sender, EventArgs e)
        {
            pictureBox4.BackColor = Color.Red;
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            pictureBox4.BackColor = this.BackColor;
        }
    }
}
