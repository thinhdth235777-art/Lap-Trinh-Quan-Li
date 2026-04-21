CREATE DATABASE QLCHDungCuAmNhac
go
Use QLCHDungCuAmNhac
go

CREATE TABLE LoaiSanPham (
    MaLoai INT PRIMARY KEY IDENTITY(1,1),
    TenLoai NVARCHAR(100) NOT NULL 
);


CREATE TABLE ThuongHieu (
    MaThuongHieu INT PRIMARY KEY IDENTITY(1,1),
    TenThuongHieu NVARCHAR(100) NOT NULL
);


CREATE TABLE SanPham (
    MaSanPham INT PRIMARY KEY IDENTITY(1,1),
    TenSanPham NVARCHAR(200) NOT NULL,
    MaLoai INT,
    MaThuongHieu INT,
    GiaBan DECIMAL(18, 2) NOT NULL,
    SoLuongTon INT DEFAULT 0,
    MoTa NVARCHAR(MAX),
    HinhAnh NVARCHAR(255),
    CONSTRAINT FK_SanPham_Loai FOREIGN KEY (MaLoai) REFERENCES LoaiSanPham(MaLoai),
    CONSTRAINT FK_SanPham_ThuongHieu FOREIGN KEY (MaThuongHieu) REFERENCES ThuongHieu(MaThuongHieu)
);

CREATE TABLE KhachHang (
    MaKhachHang INT PRIMARY KEY IDENTITY(1,1),
    HoTen NVARCHAR(100) NOT NULL,
    SoDienThoai VARCHAR(15),
    Email VARCHAR(100),
    DiaChi NVARCHAR(255),
    TinhThanh NVARCHAR(50) 
);


CREATE TABLE NhanVien (
    MaNhanVien INT PRIMARY KEY IDENTITY(1,1),
    HoTen NVARCHAR(100) NOT NULL,
    ChucVu NVARCHAR(50),
    SoDienThoai VARCHAR(15),
    TaiKhoan VARCHAR(50) UNIQUE,
    MatKhau VARCHAR(255)
);

CREATE TABLE DonHang (
    MaDonHang INT PRIMARY KEY IDENTITY(1,1),
    NgayLap DATETIME DEFAULT GETDATE(), 
    MaKhachHang INT,
    MaNhanVien INT,
    TongTien DECIMAL(18, 2),
    PhiVanChuyen DECIMAL(18, 2) DEFAULT 0,
    Chitiet ,
    CONSTRAINT FK_DonHang_KhachHang FOREIGN KEY (MaKhachHang) REFERENCES KhachHang(MaKhachHang),
    CONSTRAINT FK_DonHang_NhanVien FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien)
);

CREATE TABLE ChiTietDonHang (
    MaDonHang INT,
    MaSanPham INT,
    SoLuong INT NOT NULL,
    DonGia DECIMAL(18, 2) NOT NULL,
    PRIMARY KEY (MaDonHang, MaSanPham),
    CONSTRAINT FK_CTDH_DonHang FOREIGN KEY (MaDonHang) REFERENCES DonHang(MaDonHang),
    CONSTRAINT FK_CTDH_SanPham FOREIGN KEY (MaSanPham) REFERENCES SanPham(MaSanPham)
);

CREATE TABLE TaiKhoan (
    TenDangNhap VARCHAR(50) PRIMARY KEY,
    MatKhau VARCHAR(255) NOT NULL,
    Email NVARCHAR(100),
    Quyen NVARCHAR(20) DEFAULT N'Nhân viên', -- Admin hoặc Nhân viên
    MaNhanVien INT,
    CONSTRAINT FK_TaiKhoan_NhanVien FOREIGN KEY (MaNhanVien) REFERENCES NhanVien(MaNhanVien)
);

-- 1. CHÈN DỮ LIỆU MẪU CHO LOẠI SẢN PHẨM
INSERT INTO LoaiSanPham (TenLoai) VALUES 
(N'Piano'),
(N'Guitar'),
(N'Organ/Keyboard'),
(N'Trống (Drums)'),
(N'Phụ kiện');
GO

-- 2. CHÈN DỮ LIỆU MẪU CHO THƯƠNG HIỆU
INSERT INTO ThuongHieu (TenThuongHieu) VALUES 
(N'Yamaha'),
(N'Fender'),
(N'Roland'),
(N'Casio'),
(N'Taylor');
GO

-- 3. CHÈN DỮ LIỆU MẪU CHO SẢN PHẨM
INSERT INTO SanPham (TenSanPham, MaLoai, MaThuongHieu, GiaBan, SoLuongTon, MoTa, HinhAnh) VALUES 
(N'Piano Điện Yamaha P-125', 1, 1, 15500000, 5, N'Thiết kế nhỏ gọn, âm thanh chân thực.', 'yamaha_p125.jpg'),
(N'Guitar Điện Fender Player Stratocaster', 2, 2, 18200000, 3, N'Âm thanh cổ điển của Fender.', 'fender_strat.jpg'),
(N'Organ Casio CT-X3000', 3, 4, 7500000, 10, N'Phù hợp cho người mới học và biểu diễn.', 'casio_ctx3000.jpg'),
(N'Trống Điện Roland TD-17KVX', 4, 3, 42000000, 2, N'Cảm giác chơi như trống thật chuyên nghiệp.', 'roland_td17.jpg'),
(N'Guitar Acoustic Taylor 114e', 2, 5, 22000000, 4, N'Dòng Grand Auditorium cao cấp.', 'taylor_114e.jpg');
GO

-- 4. CHÈN DỮ LIỆU MẪU CHO NHÂN VIÊN
INSERT INTO NhanVien (HoTen, ChucVu, SoDienThoai, TaiKhoan, MatKhau) VALUES
(N'Nguyễn Minh Tuấn', N'Quản lý', '0981111111', 'admin', '123'),
(N'Lê Hoàng Nam', N'Nhân viên', '0982222222', 'namnv', '123'),
(N'Trần Quốc Bảo', N'Nhân viên', '0983333333', 'baonv', '123');

-- 5. CHÈN DỮ LIỆU MẪU CHO KHÁCH HÀNG
INSERT INTO KhachHang (HoTen, SoDienThoai, Email, DiaChi, TinhThanh) VALUES
(N'Nguyễn Văn A', '0901234567', 'a@gmail.com', N'Châu Đốc', N'An Giang'),
(N'Trần Thị B', '0912345678', 'b@gmail.com', N'Ninh Kiều', N'Cần Thơ'),
(N'Lê Văn C', '0923456789', 'c@gmail.com', N'Quận 1', N'TP.HCM'),
(N'Phạm Thị D', '0934567890', 'd@gmail.com', N'Hải Châu', N'Đà Nẵng'),
(N'Hoàng Văn E', '0945678901', 'e@gmail.com', N'Hoàn Kiếm', N'Hà Nội');
GO
INSERT INTO DonHang (MaNhanVien, MaKhachHang, NgayLap, TongTien)
VALUES (1, 1, '2026-04-21', 0),
       (2, 2, '2026-04-20', 0);
GO
-- Chi tiết cho đơn hàng 1: Mua 1 đàn Guitar (MaSP=1), 2 bộ dây (MaSP=2)
INSERT INTO ChiTietDonHang (MaDonHang, MaSanPham, SoLuong, DonGia)
VALUES (1, 1, 1, 5000000);
Go
INSERT INTO ChiTietDonHang (MaDonHang, MaSanPham, SoLuong, DonGia)
VALUES (1, 2, 2, 200000);
GO
-- Chi tiết cho đơn hàng 2: Mua 1 cây Piano (MaSP=3)
INSERT INTO ChiTietDonHang (MaDonHang, MaSanPham, SoLuong, DonGia)
VALUES (2, 3, 1, 15000000);
GO

UPDATE DonHang
SET TongTien = (
    SELECT SUM(SoLuong * DonGia)
    FROM ChiTietDonHang
    WHERE ChiTietDonHang.MaDonHang = DonHang.MaDonHang
)
WHERE EXISTS (
    SELECT 1 FROM ChiTietDonHang 
    WHERE ChiTietDonHang.MaDonHang = DonHang.MaDonHang
);
GO
CREATE TABLE NhaCungCap (
    MaNhaCungCap INT PRIMARY KEY IDENTITY(1,1),
    TenNhaCungCap NVARCHAR(200) NOT NULL,
    SoDienThoai VARCHAR(15),
    Email VARCHAR(100),
    DiaChi NVARCHAR(255)
);
GO

-- 2. Chèn dữ liệu mẫu để ComboBox có cái mà hiển thị
INSERT INTO NhaCungCap (TenNhaCungCap, SoDienThoai, Email, DiaChi) VALUES 
(N'Công ty Nhạc cụ Yamaha Việt Nam', '0281234567', 'info@yamaha.com.vn', N'Quận 1, TP.HCM'),
(N'Nhà phân phối Guitar Fender Hải Đăng', '0908889999', 'haidang@fender.vn', N'Quận Tân Bình, TP.HCM'),
(N'Kho sỉ Piano Roland Miền Nam', '0243334445', 'kho@roland.vn', N'Quận Ba Đình, Hà Nội');
GO