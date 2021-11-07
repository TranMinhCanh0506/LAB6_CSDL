using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab6
{
    public partial class frmFood : Form
    {
        public frmFood()
        {
            InitializeComponent();
        }
        public void LoadFood (int categoryID)
        {
            string connectionString = "server=LAPTOP-RPQ3FKVN\\SQLEXPRESS; database = Restaurant; Integrated Security = true ;";
            // tạo đối tượng kết nối
            SqlConnection sqlconnection = new SqlConnection(connectionString);
            // tạo đối tượng thực thi lệnh
            SqlCommand sqlcommand = sqlconnection.CreateCommand();
            sqlcommand.CommandText = "SELECT Name FROM Category WHERE ID = " + categoryID;
            sqlconnection.Open();
            string catName = sqlcommand.ExecuteScalar().ToString();

            this.Text = " Danh sách các món ăn thuộc nhóm : " + catName;
            sqlcommand.CommandText = "SELECT * FROM Food WHERE FoodCategoryID = " + categoryID;
            SqlDataAdapter da = new SqlDataAdapter(sqlcommand);

            DataTable dt = new DataTable("Food");
            da.Fill(dt);
            dgvFood.DataSource = dt;
            sqlconnection.Close();
            sqlconnection.Dispose();
            da.Dispose();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string connectionString = "server=LAPTOP-RPQ3FKVN\\SQLEXPRESS; database = Restaurant; Integrated Security = true ;";
            // tạo đối tượng kết nối
            SqlConnection sqlconnection = new SqlConnection(connectionString);
            // tạo đối tượng thực thi lệnh
            SqlCommand sqlcommand = sqlconnection.CreateCommand();
            sqlcommand.CommandText = "INSERT INTO Food ([Name],[Unit],[FoodCategoryID], [Price],[Notes])" +
                "VALUES (N'" + txtName.Text + "' , N'" + txtUnit.Text + "', " +txtFoodCategory +  " , " + txtPrice + ",N'" + txtNotes +"')";
            sqlconnection.Open();
            int numOfRowsEffeted = sqlcommand.ExecuteNonQuery();
            sqlconnection.Close();
            if (numOfRowsEffeted == 1)
            {
                MessageBox.Show(" Thêm món ăn thành công");            
                txtID.Text = "";
                txtName.Text = "";
                txtFoodCategory.Text = "";
                txtNotes.Text = "";
                txtPrice.Text = "";
                txtUnit.Text = "";
            }
            else
            {
                MessageBox.Show(" Đã có lỗi xảy ra . Vui lòng thử lại");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvFood.SelectedRows.Count == 0) return;
            var rowSelect = dgvFood.SelectedRows[0];
            string foodID = rowSelect.Cells[0].Value.ToString();
            string connectionString = "server=LAPTOP-RPQ3FKVN\\SQLEXPRESS; database = Restaurant; Integrated Security = true ;";
            // tạo đối tượng kết nối
            SqlConnection sqlconnection = new SqlConnection(connectionString);
            // tạo đối tượng thực thi lệnh
            SqlCommand sqlcommand = sqlconnection.CreateCommand();
            sqlcommand.CommandText = "DELETE FROM Food" +
               "WHERE ID = " + txtID.Text;
            sqlconnection.Open();
            int numOfRowEfected = sqlcommand.ExecuteNonQuery();
            sqlconnection.Close();
            if (numOfRowEfected == 1)
            {             
                dgvFood.Rows.Remove(rowSelect);
                txtID.Text = "";
                txtName.Text = "";
                txtFoodCategory.Text = "";
                txtNotes.Text = "";
                txtPrice.Text = "";
                txtUnit.Text = "";
                MessageBox.Show(" Xóa món ăn thành công");
            }
            else
            {
                MessageBox.Show("Đã có lỗi xảy ra , vui lòng thử lại");
            }
        }
    }
    
}
