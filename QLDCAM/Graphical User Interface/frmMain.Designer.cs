namespace QLDCAM.Graphical_User_Interface
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuHeThong = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDangXuat = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuThoat = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDoiMatKhau = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDanhMuc = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSanPham = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNhanVien = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLoaiSanPham = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNhaCungCap = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuKhachHang = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGiaoDich = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLapHoaDon = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDanhSachHoaDon = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNhapHang = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuThongKe = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDoanhThuTheoThang = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSanPhamBanChay = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHangTonKho = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTroGiup = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHuongDanSuDung = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuThongTinPhanMem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHeThong,
            this.mnuDanhMuc,
            this.mnuGiaoDich,
            this.mnuThongKe,
            this.mnuTroGiup});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(9, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1158, 33);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuHeThong
            // 
            this.mnuHeThong.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDangXuat,
            this.mnuThoat,
            this.mnuDoiMatKhau});
            this.mnuHeThong.Name = "mnuHeThong";
            this.mnuHeThong.Size = new System.Drawing.Size(103, 29);
            this.mnuHeThong.Text = "Hệ thống";
            // 
            // mnuDangXuat
            // 
            this.mnuDangXuat.Name = "mnuDangXuat";
            this.mnuDangXuat.Size = new System.Drawing.Size(270, 34);
            this.mnuDangXuat.Text = "Đăng xuất";
            this.mnuDangXuat.Click += new System.EventHandler(this.mnuDangXuat_Click);
            // 
            // mnuThoat
            // 
            this.mnuThoat.Name = "mnuThoat";
            this.mnuThoat.Size = new System.Drawing.Size(270, 34);
            this.mnuThoat.Text = "Thoát";
            // 
            // mnuDoiMatKhau
            // 
            this.mnuDoiMatKhau.Name = "mnuDoiMatKhau";
            this.mnuDoiMatKhau.Size = new System.Drawing.Size(270, 34);
            this.mnuDoiMatKhau.Text = "Đổi mật khẩu";
            // 
            // mnuDanhMuc
            // 
            this.mnuDanhMuc.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSanPham,
            this.mnuNhanVien,
            this.mnuLoaiSanPham,
            this.mnuNhaCungCap,
            this.mnuKhachHang});
            this.mnuDanhMuc.Name = "mnuDanhMuc";
            this.mnuDanhMuc.Size = new System.Drawing.Size(173, 29);
            this.mnuDanhMuc.Text = "Quản lý danh mục";
            // 
            // mnuSanPham
            // 
            this.mnuSanPham.Name = "mnuSanPham";
            this.mnuSanPham.Size = new System.Drawing.Size(270, 34);
            this.mnuSanPham.Text = "Sản Phẩm";
            this.mnuSanPham.Click += new System.EventHandler(this.mnuSanPham_Click);
            // 
            // mnuNhanVien
            // 
            this.mnuNhanVien.Name = "mnuNhanVien";
            this.mnuNhanVien.Size = new System.Drawing.Size(270, 34);
            this.mnuNhanVien.Text = "Nhân viên";
            this.mnuNhanVien.Click += new System.EventHandler(this.mnuNhanVien_Click);
            // 
            // mnuLoaiSanPham
            // 
            this.mnuLoaiSanPham.Name = "mnuLoaiSanPham";
            this.mnuLoaiSanPham.Size = new System.Drawing.Size(270, 34);
            this.mnuLoaiSanPham.Text = "Loại sản phẩm";
            // 
            // mnuNhaCungCap
            // 
            this.mnuNhaCungCap.Name = "mnuNhaCungCap";
            this.mnuNhaCungCap.Size = new System.Drawing.Size(270, 34);
            this.mnuNhaCungCap.Text = "Nhà cung cấp";
            // 
            // mnuKhachHang
            // 
            this.mnuKhachHang.Name = "mnuKhachHang";
            this.mnuKhachHang.Size = new System.Drawing.Size(270, 34);
            this.mnuKhachHang.Text = "Khách hàng";
            this.mnuKhachHang.Click += new System.EventHandler(this.mnuKhachHang_Click);
            // 
            // mnuGiaoDich
            // 
            this.mnuGiaoDich.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuLapHoaDon,
            this.mnuDanhSachHoaDon,
            this.mnuNhapHang});
            this.mnuGiaoDich.Name = "mnuGiaoDich";
            this.mnuGiaoDich.Size = new System.Drawing.Size(102, 29);
            this.mnuGiaoDich.Text = "Giao dịch";
            // 
            // mnuLapHoaDon
            // 
            this.mnuLapHoaDon.Name = "mnuLapHoaDon";
            this.mnuLapHoaDon.Size = new System.Drawing.Size(270, 34);
            this.mnuLapHoaDon.Text = "Lập hóa đơn";
            // 
            // mnuDanhSachHoaDon
            // 
            this.mnuDanhSachHoaDon.Name = "mnuDanhSachHoaDon";
            this.mnuDanhSachHoaDon.Size = new System.Drawing.Size(270, 34);
            this.mnuDanhSachHoaDon.Text = "Danh sách hóa đơn";
            // 
            // mnuNhapHang
            // 
            this.mnuNhapHang.Name = "mnuNhapHang";
            this.mnuNhapHang.Size = new System.Drawing.Size(270, 34);
            this.mnuNhapHang.Text = "Nhập hàng";
            // 
            // mnuThongKe
            // 
            this.mnuThongKe.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDoanhThuTheoThang,
            this.mnuSanPhamBanChay,
            this.mnuHangTonKho});
            this.mnuThongKe.Name = "mnuThongKe";
            this.mnuThongKe.Size = new System.Drawing.Size(102, 29);
            this.mnuThongKe.Text = "Thống kê";
            // 
            // mnuDoanhThuTheoThang
            // 
            this.mnuDoanhThuTheoThang.Name = "mnuDoanhThuTheoThang";
            this.mnuDoanhThuTheoThang.Size = new System.Drawing.Size(290, 34);
            this.mnuDoanhThuTheoThang.Text = "Doanh thu theo tháng";
            // 
            // mnuSanPhamBanChay
            // 
            this.mnuSanPhamBanChay.Name = "mnuSanPhamBanChay";
            this.mnuSanPhamBanChay.Size = new System.Drawing.Size(290, 34);
            this.mnuSanPhamBanChay.Text = "Sản phẩm bán chạy";
            // 
            // mnuHangTonKho
            // 
            this.mnuHangTonKho.Name = "mnuHangTonKho";
            this.mnuHangTonKho.Size = new System.Drawing.Size(290, 34);
            this.mnuHangTonKho.Text = "Hàng tồn kho";
            // 
            // mnuTroGiup
            // 
            this.mnuTroGiup.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHuongDanSuDung,
            this.mnuThongTinPhanMem});
            this.mnuTroGiup.Name = "mnuTroGiup";
            this.mnuTroGiup.Size = new System.Drawing.Size(93, 29);
            this.mnuTroGiup.Text = "Trợ giúp";
            // 
            // mnuHuongDanSuDung
            // 
            this.mnuHuongDanSuDung.Name = "mnuHuongDanSuDung";
            this.mnuHuongDanSuDung.Size = new System.Drawing.Size(281, 34);
            this.mnuHuongDanSuDung.Text = "Hướng dẫn sử dụng";
            // 
            // mnuThongTinPhanMem
            // 
            this.mnuThongTinPhanMem.Name = "mnuThongTinPhanMem";
            this.mnuThongTinPhanMem.Size = new System.Drawing.Size(281, 34);
            this.mnuThongTinPhanMem.Text = "Thông tin phần mềm";
            // 
            // pnlContent
            // 
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 33);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(1158, 629);
            this.pnlContent.TabIndex = 1;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1158, 662);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Times New Roman", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmMain";
            this.Text = "frmMain";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuHeThong;
        private System.Windows.Forms.ToolStripMenuItem mnuDangXuat;
        private System.Windows.Forms.ToolStripMenuItem mnuThoat;
        private System.Windows.Forms.ToolStripMenuItem mnuDanhMuc;
        private System.Windows.Forms.ToolStripMenuItem mnuKhachHang;
        private System.Windows.Forms.ToolStripMenuItem mnuNhanVien;
        private System.Windows.Forms.ToolStripMenuItem mnuGiaoDich;
        private System.Windows.Forms.ToolStripMenuItem mnuDanhSachHoaDon;
        private System.Windows.Forms.ToolStripMenuItem mnuThongKe;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.ToolStripMenuItem mnuDoiMatKhau;
        private System.Windows.Forms.ToolStripMenuItem mnuLoaiSanPham;
        private System.Windows.Forms.ToolStripMenuItem mnuNhaCungCap;
        private System.Windows.Forms.ToolStripMenuItem mnuNhapHang;
        private System.Windows.Forms.ToolStripMenuItem mnuDoanhThuTheoThang;
        private System.Windows.Forms.ToolStripMenuItem mnuSanPhamBanChay;
        private System.Windows.Forms.ToolStripMenuItem mnuHangTonKho;
        private System.Windows.Forms.ToolStripMenuItem mnuTroGiup;
        private System.Windows.Forms.ToolStripMenuItem mnuHuongDanSuDung;
        private System.Windows.Forms.ToolStripMenuItem mnuThongTinPhanMem;
        private System.Windows.Forms.ToolStripMenuItem mnuSanPham;
        private System.Windows.Forms.ToolStripMenuItem mnuLapHoaDon;
    }
}