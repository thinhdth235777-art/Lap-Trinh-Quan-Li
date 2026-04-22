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
    public partial class frmNhapHang : Form
    {
        DataTable dtNhapHang = new DataTable();
        // Giả sử bạn có lớp BLL cho sản phẩm và nhân viên
        SanPhamBLL spBLL = new SanPhamBLL();
        NhanVienBLL nvBLL = new NhanVienBLL();
        // Bạn cần tạo thêm NhaCungCapBLL hoặc dùng chung tùy cấu trúc
        NhaCungCapBLL nccBLL = new NhaCungCapBLL();
        public frmNhapHang()
        {
            InitializeComponent();
        }

        private void frmNhapHang_Load(object sender, EventArgs e)
        {
            dtNhapHang.Columns.Add("MaSP");
            dtNhapHang.Columns.Add("TenSP");
            dtNhapHang.Columns.Add("SoLuong", typeof(int));
            dtNhapHang.Columns.Add("DonGiaNhap", typeof(decimal));
            dtNhapHang.Columns.Add("ThanhTien", typeof(decimal));

            dgvDanhSachNhap.DataSource = dtNhapHang;

            // 2. Load dữ liệu vào các ComboBox
            LoadComboBox();
        }
        void LoadComboBox()
        {
            // 1. Load Sản phẩm - Đã sửa đúng tên hàm của Thịnh
            cbSanPham.DataSource = spBLL.LayDanhSachSP();
            cbSanPham.DisplayMember = "TenSanPham";
            cbSanPham.ValueMember = "MaSanPham";

            // 2. Load Nhân viên - Đã sửa đúng tên hàm của Thịnh
            cbNV.DataSource = nvBLL.LayDanhSachNV();
            cbNV.DisplayMember = "HoTen";

            // PHẢI SỬA: Thay "MaNV" bằng tên cột thực tế trong Database của Thịnh
            cbNV.ValueMember = "MaNhanVien"; // Hoặc bất cứ tên gì bạn đặt trong SQL

            // 3. Load Nhà cung cấp
            // Nhớ dùng nccBLL (không phải nvBLL) và điền DisplayMember
            cbNCC.DataSource = nccBLL.LayDanhSach();
            cbNCC.DisplayMember = "TenNCC"; // Kiểm tra lại cả tên cột này nữa nhé!

            // SỬA TẠI ĐÂY: Thay "MaNCC" bằng tên cột thực tế trong SQL của Thịnh
            cbNCC.ValueMember = "MaNhaCungCap";

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string maSP = cbSanPham.SelectedValue.ToString();
            string tenSP = cbSanPham.Text;
            int soLuong = (int)numSL.Value;
            decimal giaNhap = decimal.Parse(txtGiaNhap.Text);
            decimal thanhTien = soLuong * giaNhap;

            // Kiểm tra nếu sản phẩm đã có trong danh sách thì cộng dồn số lượng
            bool daCo = false;
            foreach (DataRow row in dtNhapHang.Rows)
            {
                if (row["MaSP"].ToString() == maSP)
                {
                    row["SoLuong"] = (int)row["SoLuong"] + soLuong;
                    row["ThanhTien"] = (int)row["SoLuong"] * giaNhap;
                    daCo = true;
                    break;
                }
            }

            if (!daCo)
            {
                dtNhapHang.Rows.Add(maSP, tenSP, soLuong, giaNhap, thanhTien);
            }

            TinhTongTien();
        }
        void TinhTongTien()
        {
            decimal tong = 0;
            foreach (DataRow row in dtNhapHang.Rows)
            {
                tong += Convert.ToDecimal(row["ThanhTien"]);
            }
            lblTongTien.Text = tong.ToString("N0") + " VNĐ";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (dtNhapHang.Rows.Count == 0)
            {
                MessageBox.Show("Vui lòng thêm sản phẩm vào danh sách!");
                return;
            }

            try
            {
                // 2. Tạo đối tượng PhieuNhapDTO và gán giá trị từ các ô nhập liệu
                PhieuNhapDTO phieuNhapDTO = new PhieuNhapDTO();
                phieuNhapDTO.MaNhanVien = (int)cbNV.SelectedValue;
                phieuNhapDTO.MaNhaCungCap = (int)cbNCC.SelectedValue;
                phieuNhapDTO.NgayNhap = dtNhap.Value;

                // Lọc sạch chữ VNĐ và dấu phẩy để lấy con số tính toán
                string sTongTien = lblTongTien.Text.Replace(",", "").Replace(" VNĐ", "");
                phieuNhapDTO.TongTien = decimal.Parse(sTongTien);

                // 3. Gọi tầng BLL để thực hiện lưu xuống Database và CỘNG KHO
                PhieuNhapBLL phieuNhapBLL = new PhieuNhapBLL();

                // Chỉ khi hàm này trả về true (thành công) mới hiện thông báo
                if (phieuNhapBLL.LuuPhieu(phieuNhapDTO, dtNhapHang))
                {
                    MessageBox.Show("Nhập hàng thành công! Số lượng sản phẩm đã được cộng vào kho.");

                    // Xóa trắng bảng tạm để chuẩn bị nhập phiếu mới
                    dtNhapHang.Clear();
                    TinhTongTien();

                    // (Tùy chọn) Gọi hàm load lại bảng Sản phẩm nếu bạn muốn cập nhật ngay con số -1 kia
                    // HienThiSanPham(); 
                }
                else
                {
                    MessageBox.Show("Lưu phiếu nhập thất bại! Vui lòng kiểm tra lại kết nối.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem Thịnh đã chọn dòng nào trên lưới chưa
            // Giả sử DataGridView của Thịnh tên là dgvNhapHang
            if (dgvDanhSachNhap.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn dòng sản phẩm cần xóa trong danh sách!");
                return;
            }

            // 2. Hỏi xác nhận để tránh việc Thịnh lỡ tay bấm nhầm
            DialogResult r = MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này khỏi phiếu nhập không?",
                                             "Xác nhận xóa",
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Question);

            if (r == DialogResult.Yes)
            {
                // 3. Lấy vị trí (Index) của dòng đang được chọn
                int index = dgvDanhSachNhap.CurrentRow.Index;

                // 4. Xóa dòng tương ứng trong DataTable tạm (dtNhapHang)
                // Khi xóa ở DataTable, DataGridView sẽ tự động cập nhật mất dòng đó luôn
                dtNhapHang.Rows.RemoveAt(index);

                // 5. Rất quan trọng: Phải gọi lại hàm tính tổng tiền để con số 60,000,000 VNĐ cập nhật lại
                // Dòng này dựa trên hàm Thịnh đã viết ở các bước trước
                TinhTongTien();

                MessageBox.Show("Đã xóa dòng thành công!");
            }
        }
    }
}
