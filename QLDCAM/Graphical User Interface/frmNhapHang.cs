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

            // 1. Tạo đối tượng PhieuNhapDTO và gán giá trị từ form
            // 2. Gọi BLL để lưu
            // if (phieuNhapBLL.LuuPhieu(phieuNhapDTO, dtNhapHang)) { ... }

            MessageBox.Show("Nhập hàng thành công! Số lượng sản phẩm đã được cộng vào kho.");
            dtNhapHang.Clear();
            TinhTongTien();
        }
    }
}
