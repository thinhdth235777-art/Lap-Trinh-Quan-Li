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
    public partial class frmNhanVien : Form
    {
        NhanVienBLL bll = new NhanVienBLL();
        bool xuLyThem = false;
        int id;

        public frmNhanVien()
        {
            InitializeComponent();
        }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            HienThi();

            if (dgvNhanVien.Rows.Count > 0)
            {
                dgvNhanVien.CurrentCell = dgvNhanVien.Rows[0].Cells[0];
                HienThiChiTiet(dgvNhanVien.Rows[0]);
            }

            setButton(true);
        }
        void HienThi()
        {
            dgvNhanVien.DataSource = bll.LayDanhSachNV();
            // Ẩn cột mật khẩu trên bảng để bảo mật
            if (dgvNhanVien.Columns.Contains("MatKhau"))
                dgvNhanVien.Columns["MatKhau"].Visible = false;
        }

        void HienThiChiTiet(DataGridViewRow row)
        {
            // Lấy giá trị mã từ dòng được chọn
            id = Convert.ToInt32(row.Cells["MaNhanVien"].Value);

            // THÊM DÒNG NÀY: Hiển thị mã lên TextBox
            txtMaNhanVien.Text = id.ToString();

            // Các dòng cũ của bạn
            txtHoVaTen.Text = row.Cells["HoTen"].Value?.ToString();
            txtDienThoai.Text = row.Cells["SoDienThoai"].Value?.ToString();
            txtChucVu.Text = row.Cells["ChucVu"].Value?.ToString();
            txtTaiKhoan.Text = row.Cells["TaiKhoan"].Value?.ToString();

            // NÊN THÊM: Hiển thị mật khẩu để khi bấm "Sửa" không bị mất pass
            txtMatKhau.Text = row.Cells["MatKhau"].Value?.ToString();
        }

        void setButton(bool val)
        {
            btnThem.Enabled = val;
            btnSua.Enabled = val;
            btnXoa.Enabled = val;

            btnLuu.Enabled = !val;
            btnHuyBo.Enabled = !val;

            // KHÓA ô Mã nhân viên: chỉ xem, không nhập
            txtMaNhanVien.ReadOnly = true;

            txtHoVaTen.Enabled = !val;
            txtDienThoai.Enabled = !val;
            txtChucVu.Enabled = !val;
            txtTaiKhoan.Enabled = !val;
            txtMatKhau.Enabled = !val;
        }

        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                HienThiChiTiet(dgvNhanVien.Rows[e.RowIndex]);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            xuLyThem = true;
            setButton(false);

            txtHoVaTen.Clear();
            txtDienThoai.Clear();
            txtChucVu.Clear();
            txtTaiKhoan.Clear();
            txtMatKhau.Clear();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            xuLyThem = false;
            setButton(false);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Xóa nhân viên?", "Xác nhận",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string kq = bll.XoaNhanVien(id);

                if (kq == "Thành công")
                {
                    MessageBox.Show("OK");
                    HienThi();
                }
                else
                {
                    MessageBox.Show(kq);
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            NhanVienDTO nv = new NhanVienDTO()
            {
                MaNhanVien = xuLyThem ? 0 : id,
                HoTen = txtHoVaTen.Text,
                SoDienThoai = txtDienThoai.Text,
                ChucVu = txtChucVu.Text,
                TaiKhoan = txtTaiKhoan.Text,
                MatKhau = txtMatKhau.Text
            };

            string kq = bll.KiemTraVaLuu(nv, xuLyThem ? "THEM" : "SUA");

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
            dgvNhanVien.DataSource = bll.TimKiem(txtTimKiem.Text);
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
