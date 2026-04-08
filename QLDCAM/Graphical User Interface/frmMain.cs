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
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();

        }
        private Form activeForm = null;

        private void OpenChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();

            activeForm = childForm;

            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None; 
            childForm.Dock = DockStyle.Fill; 

            pnlContent.Controls.Add(childForm);
            pnlContent.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

 

        private void frmMain_Load(object sender, EventArgs e)
        {
            //OpenChildForm(new frmDashboard());
            
        }

    
        private void btnMenuKho_Click(object sender, EventArgs e)
        {
            if (pnlMenuKho.Height == 45)
            {
                pnlMenuKho.Height = 180; 
            }
            else
            {
                pnlMenuKho.Height = 45;  
            }
        }

        private void btnMenuDoiTac_Click(object sender, EventArgs e)
        {

            if (pnlMenuDoiTac.Height == 45)
            {
                pnlMenuDoiTac.Height = 135;
            }
            else
            {
                pnlMenuDoiTac.Height = 45;
            }
        }

        private void btnMenuBanHang_Click(object sender, EventArgs e)
        {

            if (pnlMenuBanHang.Height == 45)
            {
                pnlMenuBanHang.Height = 180;
            }
            else
            {
                pnlMenuBanHang.Height = 45;
            }
        }

        private void btnMenuThongKe_Click(object sender, EventArgs e)
        {
          if (pnlMenuThongKe.Height == 45)
            {
                pnlMenuThongKe.Height = 180;
            }
            else
            {
                pnlMenuThongKe.Height = 45;
            }
        }

        private void btnSanPham_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmSanPham());
        }

        private void btnLoaiSanPham_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmLoaiSanPham());
        }

        private void btnThuongHieu_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmThuongHieu());
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmKhachHang());
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmNhanVien());
        }

        private void btnDoanhThuTheoThang_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmThongKe("DoanhThu"));
        }

        private void btnSanPhamBanChay_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmThongKe("SanPhamBanChay"));
        }

        private void btnHangTonKho_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmThongKe("HangTonKho"));
        }

        private void btnMenuHome_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmDashboard());
        }
    }
}
