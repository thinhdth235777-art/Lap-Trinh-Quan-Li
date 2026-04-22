using QLDCAM.Business_Logic_Layer;
using QLDCAM.Data_Access_Layer;
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
    public partial class frmCTDonHang : Form
    {
        DataTable dtChiTiet = new DataTable();
        int _maHD;
        DonHangBLL bll = new DonHangBLL();
        QLDCAM.Business_Logic_Layer.KhachHangBLL khBLL = new QLDCAM.Business_Logic_Layer.KhachHangBLL();
        NhanVienBLL nvBLL = new NhanVienBLL();
        SanPhamBLL spBLL=new SanPhamBLL();
        // Constructor nhận mã hóa đơn từ Form chính
        public frmCTDonHang(int maHD)
        {
            InitializeComponent();
            this._maHD = maHD;
        }

        private void frmChiTietHoaDon_Load(object sender, EventArgs e)
        {
            txtMaHD.Text = _maHD.ToString();
            LoadCustomers();
            LoadDuLieuChiTiet();
            LoadStaffs();
            LoadSP();
            if (dtChiTiet.Columns.Count == 0) // Kiểm tra để tránh tạo trùng nếu Load lại nhiều lần
            {
                dtChiTiet.Columns.Add("MaSanPham", typeof(int));     // Đây là dòng Thịnh đang thiếu
                dtChiTiet.Columns.Add("TenSanPham", typeof(string));
                dtChiTiet.Columns.Add("SoLuong", typeof(int));
                dtChiTiet.Columns.Add("DonGia", typeof(decimal));
                dtChiTiet.Columns.Add("ThanhTien", typeof(decimal));
            }
        }

        void LoadCustomers()
        {
            DataTable dt = khBLL.LayDanhSachKH();
            cbKH.DataSource = dt;
            cbKH.DisplayMember = "HoTen";
            cbKH.ValueMember = "MaKhachHang";
            cbKH.SelectedIndexChanged -= cbKH_SelectedIndexChanged;
            cbKH.SelectedIndexChanged += cbKH_SelectedIndexChanged;
        }

        void LoadStaffs()
        {
            DataTable dt = nvBLL.LayDanhSachNV();
            cbNV.DataSource = dt;
            cbNV.DisplayMember = "HoTen";
            cbNV.ValueMember = "MaNhanVien";
        }
        void LoadSP()
        {
            DataTable dt=spBLL.LayDanhSachSP();
            cbSanPham.DataSource = dt;
            cbSanPham.DisplayMember = "TenSanPham";
            cbSanPham.ValueMember = "MaSanPham";
        }
        private void cbKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Khi chọn khách hàng, tính phí ship theo TinhThanh của khách
            string tinh = null;
            if (cbKH.SelectedItem is DataRowView drv && drv.Row.Table.Columns.Contains("TinhThanh"))
                tinh = drv["TinhThanh"].ToString();

            decimal phi = bll.TinhPhiShip(tinh);

            // Tổng tiền sản phẩm hiện có
            decimal tong = 0;
            if (dtgHoaDon.DataSource is DataTable dt)
            {
                foreach (DataRow row in dt.Rows)
                {
                    tong += Convert.ToDecimal(row["ThanhTien"]);
                }
            }

            if (txtTienSP != null) txtTienSP.Text = tong.ToString("N0") + " VNĐ";
            if (txtVanChuyen != null) txtVanChuyen.Text = phi.ToString("N0") + " VNĐ";
            txtTong.Text = (tong + phi).ToString("N0") + " VNĐ";
        }

        void LoadDuLieuChiTiet()
        {
            // Gọi BLL để lấy các món hàng của hóa đơn này
            DataTable dt = bll.LayChiTiet(_maHD);
            dtgHoaDon.DataSource = dt;

            // Ẩn cột MaSanPham nếu không muốn hiển thị mã sản phẩm trong lưới
            if (dtgHoaDon.Columns.Contains("MaSanPham"))
                dtgHoaDon.Columns["MaSanPham"].Visible = false;

            // Định dạng các cột số tiền
            if (dtgHoaDon.Columns.Contains("DonGia"))
                dtgHoaDon.Columns["DonGia"].DefaultCellStyle.Format = "N0";
            if (dtgHoaDon.Columns.Contains("ThanhTien"))
                dtgHoaDon.Columns["ThanhTien"].DefaultCellStyle.Format = "N0";

            // Tính tổng tiền sản phẩm
            decimal tong = 0;
            foreach (DataRow row in dt.Rows)
            {
                tong += Convert.ToDecimal(row["ThanhTien"]);
            }

            // Lấy phí vận chuyển đã lưu (nếu có). Theo yêu cầu: An Giang miễn phí, khác thì 30000
            decimal phi = bll.LayPhiVanChuyen(_maHD);

            // Hiển thị các ô tương ứng
            if (txtTienSP != null) txtTienSP.Text = tong.ToString("N0") + " VNĐ";
            if (txtVanChuyen != null) txtVanChuyen.Text = phi.ToString("N0") + " VNĐ";
            txtTong.Text = (tong + phi).ToString("N0") + " VNĐ";
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (cbSanPham.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm!");
                return;
            }

            // 2. Lấy thông tin từ giao diện
            int maSP = (int)cbSanPham.SelectedValue;
            string tenSP = cbSanPham.Text;
            int soLuong = (int)numSL.Value;

            // Thịnh cần lấy giá bán của sản phẩm này (có thể từ BLL hoặc biến tạm)
            // Giả sử Thịnh lấy giá từ thuộc tính của đối tượng trong ComboBox
            decimal donGia = LayGiaSanPham(maSP);

            if (soLuong <= 0)
            {
                MessageBox.Show("Số lượng phải lớn hơn 0!");
                return;
            }
            DataTable dt = spBLL.LayChiTietSP(maSP);
            int tonKhoHienTai = Convert.ToInt32(dt.Rows[0]["SoLuongTon"]);

            // 2. So sánh với số lượng Thịnh định thêm vào
            if (soLuong > tonKhoHienTai)
            {
                MessageBox.Show($"Không đủ hàng! Trong kho chỉ còn {tonKhoHienTai} sản phẩm.", "Thông báo");
                return; // Dừng lại, không cho thêm vào bảng nữa
            }
            // 3. Kiểm tra sản phẩm đã tồn tại trong lưới chưa
            bool daCo = false;
            foreach (DataRow row in dtChiTiet.Rows)
            {
                if ((int)row["MaSanPham"] == maSP)
                {
                    // Nếu có rồi thì cộng dồn số lượng
                    row["SoLuong"] = (int)row["SoLuong"] + soLuong;
                    row["ThanhTien"] = (int)row["SoLuong"] * (decimal)row["DonGia"];
                    daCo = true;
                    break;
                }
            }

            // 4. Nếu chưa có thì thêm dòng mới
            if (!daCo)
            {
                DataRow newRow = dtChiTiet.NewRow();
                newRow["MaSanPham"] = maSP;
                newRow["TenSanPham"] = tenSP;
                newRow["SoLuong"] = soLuong;
                newRow["DonGia"] = donGia;
                newRow["ThanhTien"] = soLuong * donGia;
                dtChiTiet.Rows.Add(newRow);
            }

            // 5. Cập nhật lại tổng tiền ở phía dưới
            TinhTongTien();
            dtgHoaDon.DataSource = null; // Reset lại để bảng nhận dữ liệu mới
            dtgHoaDon.DataSource = dtChiTiet;
        }
        private void TinhTongTien()
        {
            decimal tongSP = 0;
            foreach (DataRow row in dtChiTiet.Rows)
            {
                tongSP += Convert.ToDecimal(row["ThanhTien"]);
            }

            // Lấy chuỗi từ ô phí, xóa sạch dấu phẩy và chữ "VNĐ"
            string sPhi = txtVanChuyen.Text.Replace(",", "").Replace("VNĐ", "").Trim();
            decimal phiShip = 0;
            decimal.TryParse(sPhi, out phiShip);

            decimal tongThanhToan = tongSP + phiShip;

            // Hiển thị lại kết quả đã định dạng
            txtTienSP.Text = tongSP.ToString("N0");
            txtTong.Text = tongThanhToan.ToString("N0");
        }
        private decimal LayGiaSanPham(int ma)
        {
            // Giả sử Thịnh đã khai báo SanPhamBLL bllSP = new SanPhamBLL(); ở đầu Form
            // Hàm này sẽ gọi xuống BLL để lấy thông tin sản phẩm từ database
            DataTable dt = spBLL.LayChiTietSP(ma);
            if (dt.Rows.Count > 0)
            {
                return Convert.ToDecimal(dt.Rows[0]["GiaBan"]); // Lấy cột GiaBan từ bảng SanPham
            }
            return 0;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Thu thập thông tin Đơn hàng (Dùng DTO có sẵn)
                DonHangDTO hd = new DonHangDTO();
                hd.MaKhachHang = (int)cbKH.SelectedValue;
                hd.MaNhanVien = (int)cbNV.SelectedValue;
                hd.PhiVanChuyen = decimal.Parse(txtVanChuyen.Text.Replace(",", "").Replace("VNĐ", ""));
                hd.TongTien = decimal.Parse(txtTong.Text.Replace(",", "").Replace("VNĐ", ""));

                // 2. Chuyển dữ liệu từ DataTable (Grid) sang danh sách List<ChiTietDonHangDTO>
                List<ChiTietDonHangDTO> dsChiTiet = new List<ChiTietDonHangDTO>();
                foreach (DataRow row in dtChiTiet.Rows)
                {
                    ChiTietDonHangDTO ct = new ChiTietDonHangDTO();
                    ct.MaSanPham = (int)row["MaSanPham"];
                    ct.SoLuong = (int)row["SoLuong"];
                    ct.DonGia = (decimal)row["DonGia"];
                    dsChiTiet.Add(ct);
                }

                // 3. Gọi hàm "tất cả trong một" của Thịnh
                // (Giả sử bllDonHang đã được khai báo ở đầu Form)
                bool result = bll.LuuHoaDon(hd, dsChiTiet);

                if (result)
                {
                    MessageBox.Show("Lưu hoá đơn và trừ kho thành công!", "Thông báo");
                    // Làm mới giao diện
                    dtChiTiet.Clear();
                    txtTong.Text = "0 VNĐ";
                }
                else
                {
                    MessageBox.Show("Lưu thất bại, vui lòng kiểm tra lại dữ liệu!", "Lỗi");
                }
            }
            catch (Exception ex)
            {
                // Thay vì hiện thông báo chung chung, hãy hiện lỗi chi tiết
                MessageBox.Show("Lỗi chi tiết từ SQL: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dtgHoaDon.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xoá trong danh sách!");
                return;
            }

            // 2. Hỏi xác nhận cho chuyên nghiệp (như mấy phần mềm xịn)
            DialogResult r = MessageBox.Show("Bạn có chắc chắn muốn xoá sản phẩm này khỏi hoá đơn không?",
                                             "Xác nhận xoá",
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Question);

            if (r == DialogResult.Yes)
            {
                // 3. Lấy vị trí (Index) của dòng đang chọn trên Grid
                int index = dtgHoaDon.CurrentRow.Index;

                // 4. Xoá dòng đó trong DataTable (dtChiTiet)
                // Khi xoá ở đây, DataGridView sẽ tự động mất dòng đó luôn
                dtChiTiet.Rows.RemoveAt(index);

                // 5. Rất quan trọng: Phải gọi lại hàm tính tổng tiền để cập nhật con số ở dưới
                TinhTongTien();

                MessageBox.Show("Đã xoá sản phẩm thành công!");
            }
        }
    }
}
