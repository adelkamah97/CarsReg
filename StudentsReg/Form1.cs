using System.Data;
using System.Data.SqlClient;
using System.Drawing;

namespace StudentsReg

{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Load();
        }

        


        SqlConnection con = new SqlConnection("Data Source=Adel-PC; Initial Catalog=cardbtest; User Id=adel; Password =adel");

        SqlCommand cmd;
        SqlDataReader read;
        
        SqlDataAdapter drr;
        //SqlDataAdapter ac=
        string id;
        bool Mode = true;
        string sql;


        /*private void Form1_Load(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM car_model", con);
            DataTable ds = new DataTable();
            da.Fill(ds);
            dataGridView1.DataSource = ds;
        }*/




        public void Load()
        {
            try
            {
                sql = "SELECT * FROM car_model";
                cmd = new SqlCommand(sql, con);
                //DataTable ds = new DataTable();
                //da.Fill(ds);
                //dataGridView1.DataSource = ds;
                con.Open();
                read = cmd.ExecuteReader();
                dataGridView1.Rows.Clear();

                while (read.Read())
                {
                    dataGridView1.Rows.Add(read[0], read[1], read[2], read[3], read[4], read[5]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }




        /*public void Load()
        {
            try
            {
                sql = "select * from student";
                cmd = new SqlCommand(sql, con);
                con.Open();
                read = cmd.ExecuteReader();
                dataGridView1.Rows.Clear();

                while (read.Read())
                {
                    dataGridView1.Rows.Add(read[0], read[1], read[2], read[3]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }*/
        private void saveButton_Click(object sender, EventArgs e)
        {
            string id_car_model = txt1.Text;
            string id_car_make = txt2.Text;
            string name = txt3.Text;
            string date_create = txt4.Text;
            string date_update = txt5.Text;
            string id_car_type = txt6.Text;

            if (Mode == true)
            {
                sql = "insert into car_model(id_car_model,id_car_make,name,date_create,date_update,id_car_type) " +
                    "values(@id_car_model,@id_car_make,@name,@date_create,@date_update,@id_car_type)";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id_car_model", id_car_model);
                cmd.Parameters.AddWithValue("@id_car_make", id_car_make);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@date_create", date_create);
                cmd.Parameters.AddWithValue("@date_update", date_update);
                cmd.Parameters.AddWithValue("@id_car_type", id_car_type);
                MessageBox.Show("Record Added");
                cmd.ExecuteNonQuery();

                txt1.Clear();
                txt2.Clear();
                txt3.Clear();
                txt4.Clear();
                txt5.Clear();
                txt6.Clear();
                txt1.Focus();

            }
            else
            {
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = "Update car_model set id_car_model=@id_car_model,id_car_make=@id_car_make," +
                    "name=@name,date_create=@date_create,date_update=@date_update,id_car_type=@id_car_type where id_car_model=@id_car_model";
                    
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id_car_model", id_car_model);
                cmd.Parameters.AddWithValue("@id_car_make", id_car_make);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@date_create", date_create);
                cmd.Parameters.AddWithValue("@date_update", date_update);
                cmd.Parameters.AddWithValue("@id_car_type", id_car_type);
                MessageBox.Show("Record updated");
                cmd.ExecuteNonQuery();

                txt1.Clear();
                txt2.Clear();
                txt3.Clear();
                txt4.Clear();
                txt5.Clear();
                txt6.Clear();
                txt1.Focus();
                saveButton.Text = "Save";
                Mode = true;
            }
            con.Close();

        }
        public void getID(String id)
        {
            sql = "select * from car_model where id_car_model = '" + id + "'  ";
            cmd = new SqlCommand(sql, con);
            con.Open();
            read = cmd.ExecuteReader();

            while (read.Read())
            {
                txt1.Text = read[0].ToString();
                txt2.Text = read[1].ToString();
                txt3.Text = read[2].ToString();
                txt4.Text = read[3].ToString();
                txt5.Text = read[4].ToString();
                txt6.Text = read[5].ToString();
                
            }
            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == dataGridView1.Columns["Edit"].Index && e.RowIndex >= 0)
            {
                Mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                getID(id);
                saveButton.Text = "Edit";

            }
            else if (e.ColumnIndex == dataGridView1.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                Mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = "delete from car_model where id_car_model = @id_car_model ";
                con.Open();
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id_car_model", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Deleted");
                con.Close();
            }
        }

        private void rfrchBtn_Click(object sender, EventArgs e)
        {
           Load();
            txt1.Clear();
            txt2.Clear();
            txt3.Clear();
            txt4.Clear();
            txt5.Clear();
            txt6.Clear();
            txt1.Focus();
        }

        private void clrBtn_Click(object sender, EventArgs e)
        {
            txt1.Clear();
            txt2.Clear();
            txt3.Clear();
            txt4.Clear();
            txt5.Clear();
            txt6.Clear(); 
            txt1.Focus();
            saveButton.Text = "Save";
            Mode = true;
        }

      
    }
}