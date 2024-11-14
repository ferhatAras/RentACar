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

namespace AraçKiralama
{
    public partial class Form1 : Form
    {

        SqlConnection con = new SqlConnection("server=DEVELOPER\\SQLEXPRESS;initial Catalog=otoKiraDB;integrated Security = sspi");
        SqlCommand cmd;
        SqlDataAdapter da;

        void temizle()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            comboBox1.Text = "";
            pictureBox1.ImageLocation = "";

        }
        void listele()
        {
            da = new SqlDataAdapter("select * from arabalar", con);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            temizle();
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO arabalar (plaka, marka, model, uretimYili, km, renk, yakitTuru, kiraUcreti,  durum, resim) VALUES (@plaka, @marka, @model, @uretimYili, @km, @renk,  @yakitTuru,@kiraUcreti, @durum, @resim)", con);
                cmd.Parameters.AddWithValue("@plaka", textBox1.Text);
                cmd.Parameters.AddWithValue("@marka", textBox2.Text);
                cmd.Parameters.AddWithValue("@model", textBox3.Text);
                cmd.Parameters.AddWithValue("@uretimYili", int.Parse(textBox4.Text));
                cmd.Parameters.AddWithValue("@km", int.Parse(textBox5.Text));
                cmd.Parameters.AddWithValue("@renk", textBox6.Text);
                cmd.Parameters.AddWithValue("@yakitTuru", textBox7.Text);
                cmd.Parameters.AddWithValue("@kiraUcreti", int.Parse(textBox8.Text));
                cmd.Parameters.AddWithValue("@durum", comboBox1.Text);
                cmd.Parameters.AddWithValue("@resim", pictureBox1.ImageLocation);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Kayıt Eklendi");
                listele();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofl = new OpenFileDialog();
            ofl.Filter = "Resim dosylari |*.jpeg;*.png *.tif| Tum dosyalar |*.*";
            ofl.ShowDialog();
            pictureBox1.ImageLocation = ofl.FileName;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("UPDATE arabalar SET plaka=@plaka, marka=@marka, model=@model, uretimYili=@uretimYili, km=@km, renk=@renk, yakitTuru=@yakitTuru, kiraUcreti=@kiraUcreti, durum=@durum, resim=@resim WHERE id=@id", con);
                cmd.Parameters.AddWithValue("@plaka", textBox1.Text);
                cmd.Parameters.AddWithValue("@marka", textBox2.Text);
                cmd.Parameters.AddWithValue("@model", textBox3.Text);
                cmd.Parameters.AddWithValue("@uretimYili", int.Parse(textBox4.Text));
                cmd.Parameters.AddWithValue("@km", int.Parse(textBox5.Text));
                cmd.Parameters.AddWithValue("@renk", textBox6.Text);
                cmd.Parameters.AddWithValue("@yakitTuru", textBox7.Text);
                cmd.Parameters.AddWithValue("@kiraUcreti", int.Parse(textBox8.Text));
                cmd.Parameters.AddWithValue("@durum", comboBox1.Text);
                cmd.Parameters.AddWithValue("@resim", pictureBox1.ImageLocation);
                cmd.Parameters.AddWithValue("@id", int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()));

                cmd.ExecuteNonQuery();
                MessageBox.Show("Kayıt Güncellendi");
                listele();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);
            }
            finally
            {
                con.Close();
            }


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            textBox8.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
            pictureBox1.ImageLocation = dataGridView1.CurrentRow.Cells[10].Value.ToString();


        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM arabalar WHERE id=@id", con);
                cmd.Parameters.AddWithValue("@id", int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString()));
                cmd.ExecuteNonQuery();
                MessageBox.Show("Kayıt Silindi");
                listele();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            da = new SqlDataAdapter("select * from arabalar where marka like '" + textBox9.Text + "%' ", con);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            temizle()
        }
    }
}
