using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLDCAM.Business_Logic_Layer;
using QLDCAM.Graphical_User_Interface;

namespace QLDCAM
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        TaiKhoanBLL tkBLL = new TaiKhoanBLL();
        private void frmLogin_Load(object sender, EventArgs e)
        {
            txtUser.Focus();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string user = txtUser.Text.Trim();
            string pass = txtPass.Text.Trim();

            
            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tài khoản và mật khẩu!", "Thông báo");
                return;
            }


            if (tkBLL.CheckLogin(user, pass))
            {
                SessionUser.UserHienTai = tkBLL.LayThongTinTaiKhoan(user, pass);
                MessageBox.Show("Đăng nhập thành công!", "Thông báo");

                
                frmMain main = new frmMain();
                this.Hide();
                main.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác!", "Lỗi");
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            frmRegister register = new frmRegister();
            register.ShowDialog();
        }
    }
}
