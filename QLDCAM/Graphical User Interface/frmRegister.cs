using QLDCAM.Business_Logic_Layer;
using QLDCAM.Data_Transfer_Object;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDCAM.Graphical_User_Interface
{
    public partial class frmRegister : Form
    {
        TaiKhoanBLL bll = new TaiKhoanBLL();
        public frmRegister()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            TaiKhoanDTO tk = new TaiKhoanDTO();
            tk.TenDangNhap = txtUser.Text.Trim();
            tk.MatKhau = txtPass.Text.Trim();
            tk.Email = txtEmail.Text.Trim();
            string confirm = txtConfirm.Text.Trim();

            string result = bll.RegisterAccount(tk, confirm);

            if (result == "Thành công")
            {
                MessageBox.Show("Chúc mừng! Tài khoản " + tk.TenDangNhap + " đã được tạo thành công.");
                this.Close(); 
            }
            else
            {
                MessageBox.Show(result, "Lỗi");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
