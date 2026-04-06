using QLDCAM.Business_Logic_Layer;
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
    public partial class frmSanPham : Form
    {
        SanPhamBLL bllSP = new SanPhamBLL();
        LoaiSanPhamBLL bllLoai = new LoaiSanPhamBLL();
        ThuongHieuBLL bllTH = new ThuongHieuBLL();
        public frmSanPham()
        {
            InitializeComponent();  
        }

        private void frmSanPham_Load(object sender, EventArgs e)
        {
            HienThiSanPham();
            LoadDataCombobox();
        }
        void HienThiSanPham()
        {
            dgvSanPham.DataSource = bllSP.LayDanhSachSP();
        }

        void LoadDataCombobox()
        {
            // Đổ dữ liệu cho Loại
            cboLoai.DataSource = bllLoai.LayDanhSachLoai();
            cboLoai.DisplayMember = "TenLoai";
            cboLoai.ValueMember = "MaLoai";

            // Đổ dữ liệu cho Thương hiệu
            cboThuongHieu.DataSource = bllTH.LayDanhSachTH();
            cboThuongHieu.DisplayMember = "TenThuongHieu";
            cboThuongHieu.ValueMember = "MaThuongHieu";
        }

        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSanPham.Rows[e.RowIndex];

                //txtTenSP.Text = row.Cells["TenSanPham"].Value.ToString();
                //txtGia.Text = row.Cells["GiaBan"].Value.ToString();
                //txtSoLuong.Text = row.Cells["SoLuongTon"].Value.ToString();

                cboLoai.Text = row.Cells["TenLoai"].Value.ToString();
                cboThuongHieu.Text = row.Cells["TenThuongHieu"].Value.ToString();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Bạn có chắc muốn đóng sản phẩm không?", "Thoát",

                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (r == DialogResult.Yes)

            {

                this.Close();

            }
        }
    }
}
