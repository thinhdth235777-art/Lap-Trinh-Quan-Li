using QLDCAM.Business_Logic_Layer;
using QLDCAM.Data_Transfer_Object;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        bool xuLyThem = false;
        int id;
        public frmSanPham()
        {
            InitializeComponent();  
        }

        private void frmSanPham_Load(object sender, EventArgs e)
        {
            RoleHelper.CheckRole(this);
            HienThiSanPham();
            LoadDataCombobox();
            if (dgvSanPham.Rows.Count > 0)
            {
                dgvSanPham.CurrentCell = dgvSanPham.Rows[0].Cells[0];
                HienThiChiTiet(dgvSanPham.Rows[0]);
            }
            setButton(true);
        }
        private void HienThiChiTiet(DataGridViewRow row)
        {
            try
            {
                // 1. PHẢI CÓ DÒNG NÀY: Lấy ID để biết đang sửa thằng nào
                id = Convert.ToInt32(row.Cells["MaSanPham"].Value);

                txtTenSP.Text = row.Cells["TenSanPham"].Value?.ToString();
                numDonGia.Value = Convert.ToDecimal(row.Cells["GiaBan"].Value);
                numSoLuong.Value = Convert.ToInt32(row.Cells["SoLuongTon"].Value);

                if (dgvSanPham.Columns.Contains("MoTa") && row.Cells["MoTa"].Value != null)
                    txtMoTaSP.Text = row.Cells["MoTa"].Value.ToString();
                else
                    txtMoTaSP.Text = "";

                cboLoai.Text = row.Cells["TenLoai"].Value?.ToString();
                cboThuongHieu.Text = row.Cells["TenThuongHieu"].Value?.ToString();

                string tenAnh = row.Cells["HinhAnh"].Value?.ToString();
                picHinhAnh.Tag = tenAnh;

                LoadAnhLenPictureBox(tenAnh);
            }
            catch (Exception ex) { }
        }
        void HienThiSanPham()
        {
            dgvSanPham.DataSource = bllSP.LayDanhSachSP();

            // 1. Ẩn cột không cần thiết
            if (dgvSanPham.Columns.Contains("MoTa"))
                dgvSanPham.Columns["MoTa"].Visible = false;

            // 2. Đặt tên tiêu đề có dấu
            dgvSanPham.Columns["MaSanPham"].HeaderText = "Mã Sản Phẩm";
            dgvSanPham.Columns["TenSanPham"].HeaderText = "Tên Sản Phẩm";
            dgvSanPham.Columns["TenLoai"].HeaderText = "Loại Sản Phẩm";
            dgvSanPham.Columns["TenThuongHieu"].HeaderText = "Thương Hiệu";
            dgvSanPham.Columns["GiaBan"].HeaderText = "Giá Bán";
            dgvSanPham.Columns["SoLuongTon"].HeaderText = "Số Lượng Tồn";
            dgvSanPham.Columns["HinhAnh"].HeaderText = "File Ảnh";

            // 3. Cấu hình Header: Cấm rớt dòng, tăng độ cao và in đậm căn giữa
            dgvSanPham.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            dgvSanPham.ColumnHeadersHeight = 35;

            foreach (DataGridViewColumn col in dgvSanPham.Columns)
            {
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.Font = new Font("Arial", 9, FontStyle.Bold);
            }

            // 4. Thiết lập độ rộng cột (Trị dứt điểm vụ cột Giá Bán chiếm chỗ)
            // Co giãn theo nội dung để vừa khít chữ
            dgvSanPham.Columns["MaSanPham"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvSanPham.Columns["TenLoai"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvSanPham.Columns["TenThuongHieu"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvSanPham.Columns["SoLuongTon"].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

            // Khóa cứng độ rộng cột Giá Bán và File Ảnh (không cho tụi nó tự fill)
            dgvSanPham.Columns["GiaBan"].Width = 130;
            dgvSanPham.Columns["HinhAnh"].Width = 230;

            // Ép Tên Sản Phẩm hốt hết khoảng trống còn lại
            dgvSanPham.Columns["TenSanPham"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvSanPham.Columns["TenSanPham"].FillWeight = 200; // Trọng số cao nhất
            dgvSanPham.Columns["TenSanPham"].MinimumWidth = 250;

            // 5. Căn lề dữ liệu và định dạng số
            dgvSanPham.Columns["GiaBan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvSanPham.Columns["GiaBan"].DefaultCellStyle.Format = "N0";

            dgvSanPham.Columns["SoLuongTon"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvSanPham.Columns["MaSanPham"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        void LoadDataCombobox()
        {
            cboLoai.DataSource = bllLoai.LayDanhSachLoai();
            cboLoai.DisplayMember = "TenLoai";
            cboLoai.ValueMember = "MaLoai";

            cboThuongHieu.DataSource = bllTH.LayDanhSachTH();
            cboThuongHieu.DisplayMember = "TenThuongHieu";
            cboThuongHieu.ValueMember = "MaThuongHieu";
        }

        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                HienThiChiTiet(dgvSanPham.Rows[e.RowIndex]);
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
        private void LoadAnhLenPictureBox(string tenFileAnh)
        {
            try
            {
                string thuMucAnh = Path.Combine(Application.StartupPath, "Images");
                string path = Path.Combine(thuMucAnh, tenFileAnh);

                if (File.Exists(path))
                {
                    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                    {
                        picHinhAnh.Image = Image.FromStream(fs);
                    }
                    picHinhAnh.SizeMode = PictureBoxSizeMode.Zoom;
                }
                else
                {
                    picHinhAnh.Image = null;
                }
            }
            catch
            {
                picHinhAnh.Image = null;
            }
        }
        private void setButton(bool val)
        {
            btnThem.Enabled = val;
            btnSua.Enabled = val;
            btnXoa.Enabled = val;
            btnThoat.Enabled = val;

            btnLuu.Enabled = !val;
            btnHuyBo.Enabled = !val;

            txtTenSP.Enabled = !val;
            txtMoTaSP.Enabled = !val;
            numSoLuong.Enabled = !val;
            numDonGia.Enabled = !val;
            cboLoai.Enabled = !val;
            cboThuongHieu.Enabled = !val;
            btnDoiAnh.Enabled = !val;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            xuLyThem = true;
            setButton(false);
            txtTenSP.Clear();
            txtMoTaSP.Clear();
            numDonGia.Value = 0;
            numSoLuong.Value = 0;
            picHinhAnh.Image = null;

            txtTenSP.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvSanPham.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần sửa trong danh sách!", "Thông báo");
                return;
            }

            xuLyThem = false;
            setButton(false);
            txtTenSP.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem đã chọn dòng nào để xóa chưa
            if (dgvSanPham.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa trong danh sách!", "Thông báo");
                return;
            }

            // 2. Hiển thị MessageBox xác nhận (Theo đúng mô tả trong ảnh)
            DialogResult r = MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này không?",
                                             "Xác nhận xóa",
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Warning);

            if (r == DialogResult.Yes)
            {
                // 3. Gọi tầng BLL để thực hiện xóa trong CSDL bằng biến 'id'
                // Biến 'id' này đã được lấy từ lúc ông Click chọn dòng trên bảng
                bool kq = bllSP.XoaSanPham(id);

                if (kq)
                {
                    MessageBox.Show("Xóa sản phẩm thành công!", "Thông báo");

                    // 4. Tải lại form (Load lại bảng và cập nhật giao diện)
                    HienThiSanPham();

                    // Nếu bảng còn dữ liệu thì chọn dòng đầu tiên, không thì xóa trắng các ô
                    if (dgvSanPham.Rows.Count > 0)
                        HienThiChiTiet(dgvSanPham.Rows[0]);
                    else
                        txtTenSP.Clear();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại! Sản phẩm có thể đang nằm trong đơn hàng nào đó.", "Lỗi");
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            SanPhamDTO sp = LayDuLieuTuForm();
            string hanhDong = xuLyThem ? "THEM" : "SUA";

            // Gọi BLL kiểm tra nghiệp vụ và lưu
            string ketQua = bllSP.KiemTraVaLuu(sp, hanhDong);

            if (ketQua == "Thành công")
            {
                MessageBox.Show("Dữ liệu đã được lưu thành công!", "Thông báo");
                setButton(true);
                HienThiSanPham();
            }
            else
            {
                MessageBox.Show(ketQua, "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            setButton(true);
            if (dgvSanPham.CurrentRow != null)
            {
                HienThiChiTiet(dgvSanPham.CurrentRow);
            }
        }

        private void btnDoiAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files(*.jpg; *.jpeg; *.png)|*.jpg; *.jpeg; *.png";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                // 1. Lấy đường dẫn file vừa chọn
                string sourceFile = ofd.FileName;
                string fileName = Path.GetFileName(sourceFile);

                // 2. Xác định thư mục đích (Images nằm cùng nơi với file .exe)
                string destFolder = Path.Combine(Application.StartupPath, "Images");

                // Tạo thư mục nếu chưa tồn tại
                if (!Directory.Exists(destFolder))
                {
                    Directory.CreateDirectory(destFolder);
                }

                string destFile = Path.Combine(destFolder, fileName);

                try
                {
                    // 3. Copy ảnh vào thư mục Images của App (Ghi đè nếu trùng tên)
                    File.Copy(sourceFile, destFile, true);

                    // 4. Hiển thị lên PictureBox và lưu tên vào Tag để tí nữa hàm LayDuLieu hốt
                    picHinhAnh.Image = Image.FromFile(destFile);
                    picHinhAnh.Tag = fileName;

                    MessageBox.Show("Đã tải ảnh lên hệ thống!", "Thông báo");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lưu ảnh: " + ex.Message);
                }
            }
        }
        private SanPhamDTO LayDuLieuTuForm()
        {
            SanPhamDTO sp = new SanPhamDTO();

            // Nếu đang sửa thì phải lấy lại cái id cũ, nếu thêm mới thì id tự tăng (0)
            sp.MaSanPham = xuLyThem ? 0 : id;

            sp.TenSanPham = txtTenSP.Text.Trim();
            sp.MaLoai = (int)cboLoai.SelectedValue;
            sp.MaThuongHieu = (int)cboThuongHieu.SelectedValue;
            sp.GiaBan = numDonGia.Value;
            sp.SoLuongTon = (int)numSoLuong.Value;
            sp.MoTa = txtMoTaSP.Text.Trim();

            // Lấy tên file ảnh (nếu có lưu đường dẫn)
            sp.HinhAnh = picHinhAnh.Tag?.ToString() ?? "no_image.jpg";

            return sp;
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiem.Text.Trim();

            if (string.IsNullOrEmpty(tuKhoa))
            {
                // Nếu ô nhập trống thì hiện lại toàn bộ danh sách
                HienThiSanPham();
            }
            else
            {
                DataTable dt = bllSP.TimKiem(tuKhoa);

                if (dt != null && dt.Rows.Count > 0)
                {
                    dgvSanPham.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy sản phẩm nào có tên: " + tuKhoa, "Thông báo");
                    txtTimKiem.Clear();
                    HienThiSanPham(); // Load lại toàn bộ cho bảng đỡ trống
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            // 1. Xóa chữ trong ô tìm kiếm
            txtTimKiem.Clear();

            // 2. Load lại toàn bộ danh sách sản phẩm từ CSDL
            HienThiSanPham();

            // 3. Đưa các nút bấm về trạng thái ban đầu (Khóa các ô nhập liệu)
            setButton(true);

            // 4. Nếu bảng có dữ liệu, chọn dòng đầu tiên và hiển thị chi tiết
            if (dgvSanPham.Rows.Count > 0)
            {
                dgvSanPham.CurrentCell = dgvSanPham.Rows[0].Cells[0];
                HienThiChiTiet(dgvSanPham.Rows[0]);
            }
            else
            {
                // Nếu bảng trống thì xóa trắng các ô nhập liệu
                txtTenSP.Clear();
                txtMoTaSP.Clear();
                numDonGia.Value = 0;
                numSoLuong.Value = 0;
                picHinhAnh.Image = null;
            }

            MessageBox.Show("Đã làm mới danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
