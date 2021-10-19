using System;
using MySql.Data; 
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }
        static string conexion = "server =127.0.0.1; port = 3306; database = agenda ; uid = root ; passwords = ;";
        MySqlConnection cn = new MySqlConnection(conexion);

        public DataTable llenar_grid()
        {
            cn.Open();
            DataTable dt = new DataTable();
            string llenar = " select * from contactos ";
            MySqlCommand cmd = new MySqlCommand(llenar, cn);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            cn.Close();
            return dt;



        }
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = llenar_grid();
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtid.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txtusuario.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                txtclave.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                istnivel.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            }
            catch
            {


            }
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtid.Enabled = true;
            txtusuario.Enabled = true;
            txtclave.Enabled = true; 
            istnivel.Enabled = true;
            txtid.Text = "";
            txtusuario.Text = "";
            txtclave.Text = ""; 
            istnivel.Text = " "; 
            txtid.Focus();
            button2.Visible = false;
            button10.Visible = true;

        }

        private void button10_Click(object sender, EventArgs e)
        {
            cn.Open();
            string insertar = "insert  into  contactos (codigo,nombre,clave,nivel) Values(@codigo,@nombre,@clave,@nivel)";
            MySqlCommand cmd = new MySqlCommand(insertar, cn);
            cmd.Parameters.AddWithValue("@codigo", txtid.Text);
            cmd.Parameters.AddWithValue("@nombre", txtusuario.Text);
            cmd.Parameters.AddWithValue("@clave", txtclave.Text);
            cmd.Parameters.AddWithValue("@nivel", istnivel.Text);
            cmd.ExecuteNonQuery();
            cn.Close();
            MessageBox.Show("Usuario agregado con éxito", "Ok", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dataGridView1.DataSource = llenar_grid();
        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            cn.Open();
            string actualizar = "update contactos set codigo=@codigo,nombre=@nombre, clave=@clave where codigo=@codigo ";
            MySqlCommand cmd = new MySqlCommand(actualizar, cn);
            cmd.Parameters.AddWithValue("@codigo",txtid.Text);
            cmd.Parameters.AddWithValue("@nombre", txtusuario.Text);
            cmd.Parameters.AddWithValue("@clave", txtclave.Text);
            cmd.Parameters.AddWithValue("@nivel", istnivel.Text);

            cmd.ExecuteNonQuery();
            cn.Close();

            MessageBox.Show("Usuario modificado con éxito", "Ok", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dataGridView1.DataSource = llenar_grid();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cn.Open();
            string eliminar = "delete from  contactos  where  codigo=@codigo";
            MySqlCommand cmd = new MySqlCommand(eliminar, cn);
            cmd.Parameters.AddWithValue("@codigo", txtid.Text);
            
            cmd.ExecuteNonQuery();
            cn.Close();
            MessageBox.Show("Usuario eliminado con éxito", "Ok", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dataGridView1.DataSource = llenar_grid();

        }
    }
}
