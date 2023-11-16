using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_HangHoa
{
    public partial class frmLogin : Form
    {
        string taikhoan = "admin";
        string matkhau = "123";
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (KiemTra(txtTaiKhoan.Text,txtMatKhau.Text))
            {
                frmMain frm= new frmMain();
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu", "Lỗi");
                txtTaiKhoan.Focus();
            }
            
        }
        bool KiemTra(string taikhoan,string matkhau)
        {
            if(taikhoan == this.taikhoan && matkhau == this.matkhau)
            {
                return true;
            }
            return false;
        }
    }
}
