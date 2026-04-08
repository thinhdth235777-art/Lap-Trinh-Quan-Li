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
    public partial class frmThuongHieu : Form
    {
        ThuongHieuBLL bllTH = new ThuongHieuBLL();
        bool xuLyThem = false;
        int id;
        public frmThuongHieu()
        {
            InitializeComponent();
        }

        private void frmThuongHieu_Load(object sender, EventArgs e)
        {
            HienThiThuongHieu();
            if (dtgThuongHieu.Rows.Count > 0)
            {
                dtgThuongHieu.CurrentCell = dtgThuongHieu.Rows[0].Cells[0];
                HienThiChiTiet(dtgThuongHieu.Rows[0]);
            }
            setButton(true);
        }
        void HienThiThuongHieu()
        {
            dtgThuongHieu.DataSource = bllTH.LayDanhSachTH();

            // Chỉnh tiêu đề cột cho khớp với bảng ThuongHieu
            if (dtgThuongHieu.Columns.Contains("MaThuongHieu"))
                dtgThuongHieu.Columns["MaThuongHieu"].HeaderText = "Mã Thương Hiệu";

            if (dtgThuongHieu.Columns.Contains("TenThuongHieu"))
            {
                dtgThuongHieu.Columns["TenThuongHieu"].HeaderText = "Tên Thương Hiệu";
                dtgThuongHieu.Columns["TenThuongHieu"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            foreach (DataGridViewColumn col in dtgThuongHieu.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.Font = new Font("Arial", 9, FontStyle.Bold);
            }
        }
        private void HienThiChiTiet(DataGridViewRow row)
        {
            try
            {
                id = Convert.ToInt32(row.Cells["MaThuongHieu"].Value);
                txtTenTH.Text = row.Cells["TenThuongHieu"].Value?.ToString();
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

            txtTenTH.Enabled = !val;
        }
        private ThuongHieuDTO LayDuLieuTuForm()
        {
            ThuongHieuDTO th = new ThuongHieuDTO();
            th.MaThuongHieu = xuLyThem ? 0 : id;
            th.TenThuongHieu = txtTenTH.Text.Trim();
            return th;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            xuLyThem = true;
            setButton(false);
            txtTenTH.Clear();
            txtTenTH.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dtgThuongHieu.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn thương hiệu cần sửa!", "Thông báo");
                return;
            }
            xuLyThem = false;
            setButton(false);
            txtTenTH.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dtgThuongHieu.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn thương hiệu cần xóa!", "Thông báo");
                return;
            }

            DialogResult r = MessageBox.Show("Bạn có chắc chắn muốn xóa thương hiệu này không?", "Xác nhận",
                                             MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (r == DialogResult.Yes)
            {
                bool ketQua = bllTH.XoaTH(id);
                if (ketQua)
                {
                    MessageBox.Show("Đã xóa thương hiệu thành công!", "Thành công");
                    HienThiThuongHieu();
                }
                else
                {
                    MessageBox.Show("Không thể xóa thương hiệu này vì đã có sản phẩm thuộc thương hiệu này!",
                                    "Lỗi ràng buộc", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            ThuongHieuDTO dto = LayDuLieuTuForm();
            string action = xuLyThem ? "THEM" : "SUA";

            string ketQua = bllTH.KiemTraVaLuu(dto, action);

            if (ketQua == "Thành công")
            {
                MessageBox.Show("Đã lưu dữ liệu!", "Thông báo");
                setButton(true);
                HienThiThuongHieu();
            }
            else
            {
                MessageBox.Show(ketQua, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            setButton(true);
            if (dtgThuongHieu.CurrentRow != null)
                HienThiChiTiet(dtgThuongHieu.CurrentRow);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát không?", "Thoát",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiem.Text.Trim();
            if (string.IsNullOrEmpty(tuKhoa))
            {
                HienThiThuongHieu();
            }
            else
            {
                DataTable dt = bllTH.TimKiem(tuKhoa);
                if (dt != null && dt.Rows.Count > 0)
                {
                    dtgThuongHieu.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thương hiệu: " + tuKhoa, "Thông báo");
                    txtTimKiem.Clear();
                    HienThiThuongHieu();
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            HienThiThuongHieu();
            setButton(true);
            if (dtgThuongHieu.Rows.Count > 0)
            {
                dtgThuongHieu.CurrentCell = dtgThuongHieu.Rows[0].Cells[0];
                HienThiChiTiet(dtgThuongHieu.Rows[0]);
            }
            MessageBox.Show("Đã làm mới danh sách!", "Thông báo");
        }

        private void dtgThuongHieu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                HienThiChiTiet(dtgThuongHieu.Rows[e.RowIndex]);
            }
        }
    }
}
