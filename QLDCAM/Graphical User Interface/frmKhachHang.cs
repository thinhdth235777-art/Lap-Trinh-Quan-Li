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
    public partial class frmKhachHang : Form
    {
        KhachHangBLL bll = new KhachHangBLL();
        bool xuLyThem = false;
        int id;
        public frmKhachHang()
        {
            InitializeComponent();
        }

        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            HienThi();
            LoadTinhThanh();

            if (dgvKhachHang.Rows.Count > 0)
            {
                dgvKhachHang.CurrentCell = dgvKhachHang.Rows[0].Cells[0];
                HienThiChiTiet(dgvKhachHang.Rows[0]);
            }

            setButton(true);

        }
        void HienThi()
        {
            dgvKhachHang.DataSource = bll.LayDanhSachKH();
        }

        void HienThiChiTiet(DataGridViewRow row)
        {
            // Lấy giá trị mã từ dòng được chọn
            id = Convert.ToInt32(row.Cells["MaKhachHang"].Value);

            // THÊM DÒNG NÀY: Hiển thị mã lên TextBox
            txtMaKhachHang.Text = id.ToString();

            // Các dòng cũ của bạn
            txtHoVaTen.Text = row.Cells["HoTen"].Value?.ToString();
            txtDienThoai.Text = row.Cells["SoDienThoai"].Value?.ToString();
            txtEmail.Text = row.Cells["Email"].Value?.ToString();
            txtDiaChi.Text = row.Cells["DiaChi"].Value?.ToString();
            cboTinhThanh.Text = row.Cells["TinhThanh"].Value?.ToString();
        }

        void LoadTinhThanh()
        {
            cboTinhThanh.Items.AddRange(new string[]
            {
        "An Giang",
        "Cần Thơ",
        "TP.HCM",
        "Hà Nội",
        "Đà Nẵng"
            });
        }

        void setButton(bool val)
        {
            btnThem.Enabled = val;
            btnSua.Enabled = val;
            btnXoa.Enabled = val;

            btnLuu.Enabled = !val;
            btnHuyBo.Enabled = !val;

            // KHÓA ô Mã khách hàng: luôn luôn không cho nhập
            txtMaKhachHang.ReadOnly = true;

            // Các ô khác cho phép nhập khi nhấn Thêm/Sửa (!val)
            txtHoVaTen.Enabled = !val;
            txtDienThoai.Enabled = !val;
            txtEmail.Enabled = !val;
            txtDiaChi.Enabled = !val;
            cboTinhThanh.Enabled = !val;
        }

        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                HienThiChiTiet(dgvKhachHang.Rows[e.RowIndex]);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            xuLyThem = true;
            setButton(false);

            txtHoVaTen.Clear();
            txtDienThoai.Clear();
            txtEmail.Clear();
            txtDiaChi.Clear();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            xuLyThem = false;
            setButton(false);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvKhachHang.CurrentRow == null) return;

            if (MessageBox.Show("Xóa khách hàng?", "Xác nhận",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (bll.XoaKhachHang(id))
                {
                    MessageBox.Show("Thành công");
                    HienThi();
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            KhachHangDTO kh = new KhachHangDTO()
            {
                MaKhachHang = xuLyThem ? 0 : id,
                HoTen = txtHoVaTen.Text,
                SoDienThoai = txtDienThoai.Text,
                Email = txtEmail.Text,
                DiaChi = txtDiaChi.Text,
                TinhThanh = cboTinhThanh.Text
            };

            string kq = bll.KiemTraVaLuu(kh, xuLyThem ? "THEM" : "SUA");

            if (kq == "Thành công")
            {
                MessageBox.Show("OK");
                setButton(true);
                HienThi();
            }
            else
                MessageBox.Show(kq);
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            setButton(true);
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            dgvKhachHang.DataSource = bll.TimKiem(txtTimKiem.Text);
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            HienThi();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
