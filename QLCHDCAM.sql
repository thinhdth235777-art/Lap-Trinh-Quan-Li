CREATE DATABASE QLCHDungCuAmNhac
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
