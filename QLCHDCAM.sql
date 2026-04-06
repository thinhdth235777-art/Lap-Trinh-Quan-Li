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
    TrangThai NVARCHAR(50),
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
