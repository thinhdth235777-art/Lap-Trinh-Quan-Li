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
    public partial class frmLoaiSanPham : Form
    {
        LoaiSanPhamBLL bllLoai = new LoaiSanPhamBLL();
        bool xuLyThem = false;
        int id;
        public frmLoaiSanPham()
        {
            InitializeComponent();
        }

        private void frmLoaiSanPham_Load(object sender, EventArgs e)
        {
            HienThiLoaiSP();
            if (dtgLoaiSP.Rows.Count > 0)
            {
                dtgLoaiSP.CurrentCell = dtgLoaiSP.Rows[0].Cells[0];
                HienThiChiTiet(dtgLoaiSP.Rows[0]);
            }
            setButton(true);
        }
        void HienThiLoaiSP()
        {
            dtgLoaiSP.DataSource = bllLoai.LayDanhSachLoai();

            // Chỉnh tiêu đề và độ rộng cột cho đẹp
            if (dtgLoaiSP.Columns.Contains("MaLoai"))
                dtgLoaiSP.Columns["MaLoai"].HeaderText = "Mã Loại";

            if (dtgLoaiSP.Columns.Contains("TenLoai"))
            {
                dtgLoaiSP.Columns["TenLoai"].HeaderText = "Tên Loại Sản Phẩm";
                dtgLoaiSP.Columns["TenLoai"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            foreach (DataGridViewColumn col in dtgLoaiSP.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.Font = new Font("Arial", 9, FontStyle.Bold);
            }
        }
        private void HienThiChiTiet(DataGridViewRow row)
        {
            try
            {
                id = Convert.ToInt32(row.Cells["MaLoai"].Value);
                txtTenLoai.Text = row.Cells["TenLoai"].Value?.ToString();
            }
            catch { }
        }
        private void setButton(bool val)
        {
            btnThem.Enabled = val;
            btnSua.Enabled = val;
            btnXoa.Enabled = val;
            btnThoat.Enabled = val;

            btnLuu.Enabled = !val;
            btnHuyBo.Enabled = !val;

            txtTenLoai.Enabled = !val;
        }
        private LoaiSanPhamDTO LayDuLieuTuForm()
        {
            LoaiSanPhamDTO loai = new LoaiSanPhamDTO();
            loai.MaLoai = xuLyThem ? 0 : id;
            loai.TenLoai = txtTenLoai.Text.Trim();
            return loai;
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dtgLoaiSP.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn loại sản phẩm cần sửa!", "Thông báo");
                return;
            }
            xuLyThem = false;
            setButton(false);
            txtTenLoai.Focus();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            xuLyThem = true;
            setButton(false);
            txtTenLoai.Clear();
            txtTenLoai.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem người dùng đã chọn dòng nào chưa
            if (dtgLoaiSP.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn loại sản phẩm cần xóa!", "Thông báo");
                return;
            }

            // 2. Hỏi xác nhận trước khi xóa (Cho chắc ăn)
            DialogResult r = MessageBox.Show("Bạn có chắc chắn muốn xóa loại này không?", "Xác nhận",
                                             MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (r == DialogResult.Yes)
            {
                // 3. Gọi BLL để thực hiện xóa
                bool ketQua = bllLoai.XoaLoai(id);

                if (ketQua)
                {
                    // Nếu xóa thành công (không dính khóa ngoại)
                    MessageBox.Show("Đã xóa loại sản phẩm thành công!", "Thành công",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    HienThiLoaiSP();
                }
                else
                {
                    MessageBox.Show("Không thể xóa loại sản phẩm này vì đang có sản phẩm thuộc loại này trong hệ thống!",
                                    "Lỗi ràng buộc dữ liệu",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            LoaiSanPhamDTO dto = LayDuLieuTuForm();
            string action = xuLyThem ? "THEM" : "SUA";

            string ketQua = bllLoai.KiemTraVaLuu(dto, action);

            if (ketQua == "Thành công")
            {
                MessageBox.Show("Đã lưu dữ liệu!", "Thông báo");
                setButton(true);
                HienThiLoaiSP();
            }
            else
            {
                MessageBox.Show(ketQua, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            setButton(true);
            if (dtgLoaiSP.CurrentRow != null)
                HienThiChiTiet(dtgLoaiSP.Rows[0]);
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

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiem.Text.Trim();

            if (string.IsNullOrEmpty(tuKhoa))
            {
                HienThiLoaiSP();
            }
            else
            {
                DataTable dt = bllLoai.TimKiem(tuKhoa);

                if (dt != null && dt.Rows.Count > 0)
                {
                    dtgLoaiSP.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy loại sản phẩm nào có tên: " + tuKhoa, "Thông báo");
                    txtTimKiem.Clear();
                    HienThiLoaiSP();
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            HienThiLoaiSP();
            setButton(true);

            if (dtgLoaiSP.Rows.Count > 0)
            {
                dtgLoaiSP.CurrentCell = dtgLoaiSP.Rows[0].Cells[0];
                HienThiChiTiet(dtgLoaiSP.Rows[0]);
            }
            else
            {
                txtTenLoai.Clear();
            }

            MessageBox.Show("Đã làm mới danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dtgLoaiSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                HienThiChiTiet(dtgLoaiSP.Rows[e.RowIndex]);
            }
        }
    }
}
