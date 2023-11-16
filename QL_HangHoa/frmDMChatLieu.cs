using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; //Sử dụng thư viện để làm việc SQL server
using QL_HangHoa.Class;
namespace QL_HangHoa
{
    public partial class frmDMChatLieu : Form
    {
        DataTable tblCL;
        public static SqlConnection Con;
        public frmDMChatLieu()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void frmDMChatLieu_Load(object sender, EventArgs e)
        {
            txtMachatlieu.Enabled = false;
            btnLuu.Enabled = false;
            btnBoqua.Enabled = false;
            LoadDataGridView(); 

        }
        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT MaChatLieu, TenChatLieu FROM tblChatLieu";
            tblCL = Class.Functions.GetDataToTable(sql); 
            dgvChatLieu.DataSource = tblCL;          
            dgvChatLieu.Columns[0].HeaderText = "Mã chất liệu";
            dgvChatLieu.Columns[1].HeaderText = "Mã chất liệu";
            dgvChatLieu.Columns[0].Width = 100;
            dgvChatLieu.Columns[1].Width = 300;
            dgvChatLieu.AllowUserToAddRows = false; 
            dgvChatLieu.EditMode = DataGridViewEditMode.EditProgrammatically; 
        }
       

        private void dgvChatLieu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMachatlieu.Focus();
                return;
            }
            if (tblCL.Rows.Count == 0) 
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMachatlieu.Text = dgvChatLieu.CurrentRow.Cells["MaChatLieu"].Value.ToString();
            txtTenChatLieu.Text = dgvChatLieu.CurrentRow.Cells["TenChatLieu"].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoqua.Enabled = true;

        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoqua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValue(); 
            txtMachatlieu.Enabled = true; 
            txtMachatlieu.Focus();
        }
        private void ResetValue()
        {
            txtMachatlieu.Text = "";
            txtTenChatLieu.Text = "";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql; 
            if (txtMachatlieu.Text.Trim().Length == 0) 
            {
                MessageBox.Show("Bạn phải nhập mã chất liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMachatlieu.Focus();
                return;
            }
            if (txtTenChatLieu.Text.Trim().Length == 0) 
            {
                MessageBox.Show("Bạn phải nhập tên chất liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenChatLieu.Focus();
                return;
            }
            sql = "Select MaChatLieu From tblChatLieu where MaChatLieu=N'" + txtMachatlieu.Text.Trim() + "'";
            if (Class.Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã chất liệu này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMachatlieu.Focus();
                return;
            }

            sql = "INSERT INTO tblChatLieu VALUES(N'" +
                txtMachatlieu.Text + "',N'" + txtTenChatLieu.Text + "')";
            Class.Functions.RunSQL(sql); 
            LoadDataGridView(); 
            ResetValue();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnBoqua.Enabled = false;
            btnLuu.Enabled = false;
            txtMachatlieu.Enabled = false;
        }
      
      

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql; 
            if (tblCL.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMachatlieu.Text == "") 
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenChatLieu.Text.Trim().Length == 0) 
            {
                MessageBox.Show("Bạn chưa nhập tên chất liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            sql = "UPDATE tblChatLieu SET TenChatLieu=N'" +
                txtTenChatLieu.Text.ToString() +
                "' WHERE MaChatLieu=N'" + txtMachatlieu.Text + "'";
            Class.Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValue();

            btnBoqua.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblCL.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMachatlieu.Text == "") 
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE tblChatLieu WHERE MaChatLieu=N'" + txtMachatlieu.Text + "'";
                Class.Functions.RunSqlDel(sql);
                LoadDataGridView();
                ResetValue();
            }
        }
        

        private void btnBoqua_Click(object sender, EventArgs e)
        {
            ResetValue();
            btnBoqua.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtMachatlieu.Enabled = false;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoqua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValue();
            txtMachatlieu.Enabled = true;
            txtMachatlieu.Focus();

        }
    }
}
