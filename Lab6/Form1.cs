using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.Odbc;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            // tạo chuỗi kết nối tới csdl restaurant
            string connectionString = "server=LAPTOP-RPQ3FKVN\\SQLEXPRESS; database = Restaurant; Integrated Security = true ;";
            // tạo đối tượng kết nối
            SqlConnection sqlconnection = new SqlConnection(connectionString);
            // tạo đối tượng thực thi lệnh
            SqlCommand sqlcommand = sqlconnection.CreateCommand();
            // thiết lập lệnh truy vấn đối tượng Comand
            string query = "SELECT ID, Name, Type FROM Category";
            sqlcommand.CommandText = query;

            // Mở kết nối tới CSDL
            sqlconnection.Open();
            // Thực thi lệnh bằng phương thức excutereader
            SqlDataReader sqlDataReader = sqlcommand.ExecuteReader();
            this.DisplayCategory(sqlDataReader);
            sqlconnection.Close();
                     
        }
        private void DisplayCategory (SqlDataReader reader)
        {
            lvCategory.Items.Clear();
            while(reader.Read())
            {
                ListViewItem item = new ListViewItem(reader["ID"].ToString());
                lvCategory.Items.Add(item);
                item.SubItems.Add(reader["Name"].ToString());
                item.SubItems.Add(reader["Type"].ToString());
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string connectionString = "server=LAPTOP-RPQ3FKVN\\SQLEXPRESS; database = Restaurant; Integrated Security = true ;";
            // tạo đối tượng kết nối
            SqlConnection sqlconnection = new SqlConnection(connectionString);
            // tạo đối tượng thực thi lệnh
            SqlCommand sqlcommand = sqlconnection.CreateCommand();
            sqlcommand.CommandText = "INSERT INTO Category (Name, [Type])" +
                "VALUES (N'"+ txtName.Text + "' , " + txtType.Text + ")";
            sqlconnection.Open();
            int numOfRowsEffeted = sqlcommand.ExecuteNonQuery();
            sqlconnection.Close();
            if(numOfRowsEffeted == 1)
            {
                MessageBox.Show(" Thêm nhóm món ăn thành công");
                btnLoad.PerformClick();
                txtName.Text = "";
                txtType.Text = "";               
            }
            else
            {
                MessageBox.Show(" Đã có lỗi xảy ra . Vui lòng thử lại");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string connectionString = "server=LAPTOP-RPQ3FKVN\\SQLEXPRESS; database = Restaurant; Integrated Security = true ;";
            // tạo đối tượng kết nối
            SqlConnection sqlconnection = new SqlConnection(connectionString);
            // tạo đối tượng thực thi lệnh
            SqlCommand sqlcommand = sqlconnection.CreateCommand();
            sqlcommand.CommandText = "UPDATE Category SET Name = N'" + txtName.Text + "', [Type] = " + txtType.Text + " WHERE ID = " + txtID.Text;
            sqlconnection.Open();
            int numOfRowEfected = sqlcommand.ExecuteNonQuery();
            sqlconnection.Close();
            if (numOfRowEfected == 1)
            {
                ListViewItem item = lvCategory.SelectedItems[0];
                item.SubItems[1].Text = txtName.Text;
                item.SubItems[2].Text = txtType.Text;
                txtID.Text = "";
                txtName.Text = "";
                txtType.Text = "";
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
                MessageBox.Show("Cập nhật nhóm món ăn thành công");
            }
            else
            {
                MessageBox.Show("Đã có lỗi xảy ra , vui lòng thử lại");
            }

        }

        private void lvCategory_Click(object sender, EventArgs e)
        {         
            ListViewItem item = lvCategory.SelectedItems[0];
            txtID.Text = item.Text;
            txtName.Text = item.SubItems[1].Text;
            txtType.Text = item.SubItems[1].Text == "0" ? "thức uống " : " đồ ăn";
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string connectionString = "server=LAPTOP-RPQ3FKVN\\SQLEXPRESS; database = Restaurant; Integrated Security = true ;";
            // tạo đối tượng kết nối
            SqlConnection sqlconnection = new SqlConnection(connectionString);
            // tạo đối tượng thực thi lệnh
            SqlCommand sqlcommand = sqlconnection.CreateCommand();
            sqlcommand.CommandText = "DELETE FROM Category" +
                "WHERE ID = " + txtID.Text;
            sqlconnection.Open();
            int numOfRowEfected = sqlcommand.ExecuteNonQuery();
            sqlconnection.Close();
            if (numOfRowEfected == 1)
            {
                ListViewItem item = lvCategory.SelectedItems[0];
                lvCategory.Items.Remove(item);
                txtID.Text = "";
                txtName.Text = "";
                txtType.Text = "";
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
                MessageBox.Show(" Xóa nhóm món ăn thành công");
            }
            else
            {
                MessageBox.Show("Đã có lỗi xảy ra , vui lòng thử lại");
            }
        }

        private void tsmDelete_Click(object sender, EventArgs e)
        {
            if (lvCategory.SelectedItems.Count > 0)
                btnDelete.PerformClick();
        }

        private void tsmViewFood_Click(object sender, EventArgs e)
        {
            if(txtID.Text != "")
            {
                frmFood foodform = new frmFood();
                foodform.Show(this);
                foodform.LoadFood(Convert.ToInt32(txtID.Text));
            }
        }
    }
}
