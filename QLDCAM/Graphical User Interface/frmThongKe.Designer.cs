using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace QLDCAM.Graphical_User_Interface
{
    partial class frmThongKe
    {
        private IContainer components = null;

        private TableLayoutPanel tableLayoutPanelMain;
        private Panel panelTop;
        private SplitContainer splitContainerMain;

        // Filters / controls
        private Label lblFrom;
        private DateTimePicker dtpFrom;
        private Label lblTo;
        private DateTimePicker dtpTo;
        private Label lblYear;
        private ComboBox cboYear;
        private Button btnLoc;
        private Button btnRefresh;

        // Export buttons (only Excel/CSV now)
        private Button btnExportExcel;

        // Summary labels
        private Label lblTongDoanhThu;
        private Label lblSoHoaDon;
        private Label lblSoKhachHang;

        // Chart and grids
        private Chart chartDoanhThu;
        private TabControl tabControlRight;
        private TabPage tabTopProducts;
        private TabPage tabLowStock;
        private TabPage tabTopCustomers;
        private DataGridView dgvChiTiet;
        private DataGridView dgvLowStock;
        private DataGridView dgvTopCustomers;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblFrom = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.lblTo = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.lblYear = new System.Windows.Forms.Label();
            this.cboYear = new System.Windows.Forms.ComboBox();
            this.btnLoc = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.lblTongDoanhThu = new System.Windows.Forms.Label();
            this.lblSoHoaDon = new System.Windows.Forms.Label();
            this.lblSoKhachHang = new System.Windows.Forms.Label();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.chartDoanhThu = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabControlRight = new System.Windows.Forms.TabControl();
            this.tabTopProducts = new System.Windows.Forms.TabPage();
            this.dgvChiTiet = new System.Windows.Forms.DataGridView();
            this.tabLowStock = new System.Windows.Forms.TabPage();
            this.dgvLowStock = new System.Windows.Forms.DataGridView();
            this.tabTopCustomers = new System.Windows.Forms.TabPage();
            this.dgvTopCustomers = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanelMain.SuspendLayout();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhThu)).BeginInit();
            this.tabControlRight.SuspendLayout();
            this.tabTopProducts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
            this.tabLowStock.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLowStock)).BeginInit();
            this.tabTopCustomers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopCustomers)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 1;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelMain.Controls.Add(this.panelTop, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.splitContainerMain, 0, 1);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 2;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(882, 553);
            this.tableLayoutPanelMain.TabIndex = 0;
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.lblFrom);
            this.panelTop.Controls.Add(this.dtpFrom);
            this.panelTop.Controls.Add(this.lblTo);
            this.panelTop.Controls.Add(this.dtpTo);
            this.panelTop.Controls.Add(this.lblYear);
            this.panelTop.Controls.Add(this.cboYear);
            this.panelTop.Controls.Add(this.btnLoc);
            this.panelTop.Controls.Add(this.btnRefresh);
            this.panelTop.Controls.Add(this.btnExportExcel);
            this.panelTop.Controls.Add(this.lblTongDoanhThu);
            this.panelTop.Controls.Add(this.lblSoHoaDon);
            this.panelTop.Controls.Add(this.lblSoKhachHang);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTop.Location = new System.Drawing.Point(3, 3);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(876, 94);
            this.panelTop.TabIndex = 0;
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.Location = new System.Drawing.Point(10, 12);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(26, 16);
            this.lblFrom.TabIndex = 0;
            this.lblFrom.Text = "Từ:";
            // 
            // dtpFrom
            // 
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(40, 8);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(100, 22);
            this.dtpFrom.TabIndex = 1;
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(150, 12);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(34, 16);
            this.lblTo.TabIndex = 2;
            this.lblTo.Text = "Đến:";
            // 
            // dtpTo
            // 
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(185, 8);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(100, 22);
            this.dtpTo.TabIndex = 3;
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.Location = new System.Drawing.Point(295, 12);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(39, 16);
            this.lblYear.TabIndex = 4;
            this.lblYear.Text = "Năm:";
            // 
            // cboYear
            // 
            this.cboYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboYear.Location = new System.Drawing.Point(330, 8);
            this.cboYear.Name = "cboYear";
            this.cboYear.Size = new System.Drawing.Size(80, 24);
            this.cboYear.TabIndex = 5;
            // 
            // btnLoc
            // 
            this.btnLoc.Location = new System.Drawing.Point(420, 7);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(70, 23);
            this.btnLoc.TabIndex = 6;
            this.btnLoc.Text = "Lọc";
            this.btnLoc.Click += new System.EventHandler(this.btnLoc_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(500, 7);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(80, 23);
            this.btnRefresh.TabIndex = 7;
            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.Location = new System.Drawing.Point(590, 7);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(90, 23);
            this.btnExportExcel.TabIndex = 8;
            this.btnExportExcel.Text = "Xuất Excel";
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // lblTongDoanhThu
            // 
            this.lblTongDoanhThu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTongDoanhThu.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTongDoanhThu.Location = new System.Drawing.Point(800, 8);
            this.lblTongDoanhThu.Name = "lblTongDoanhThu";
            this.lblTongDoanhThu.Size = new System.Drawing.Size(220, 60);
            this.lblTongDoanhThu.TabIndex = 9;
            this.lblTongDoanhThu.Text = "0";
            this.lblTongDoanhThu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSoHoaDon
            // 
            this.lblSoHoaDon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSoHoaDon.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblSoHoaDon.Location = new System.Drawing.Point(1030, 8);
            this.lblSoHoaDon.Name = "lblSoHoaDon";
            this.lblSoHoaDon.Size = new System.Drawing.Size(140, 60);
            this.lblSoHoaDon.TabIndex = 10;
            this.lblSoHoaDon.Text = "0";
            this.lblSoHoaDon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSoKhachHang
            // 
            this.lblSoKhachHang.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSoKhachHang.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblSoKhachHang.Location = new System.Drawing.Point(1180, 8);
            this.lblSoKhachHang.Name = "lblSoKhachHang";
            this.lblSoKhachHang.Size = new System.Drawing.Size(140, 60);
            this.lblSoKhachHang.TabIndex = 11;
            this.lblSoKhachHang.Text = "0";
            this.lblSoKhachHang.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Location = new System.Drawing.Point(3, 103);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.chartDoanhThu);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.tabControlRight);
            this.splitContainerMain.Size = new System.Drawing.Size(876, 447);
            this.splitContainerMain.SplitterDistance = 706;
            this.splitContainerMain.TabIndex = 1;
            // 
            // chartDoanhThu
            // 
            chartArea1.Name = "ChartArea1";
            this.chartDoanhThu.ChartAreas.Add(chartArea1);
            this.chartDoanhThu.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartDoanhThu.Legends.Add(legend1);
            this.chartDoanhThu.Location = new System.Drawing.Point(0, 0);
            this.chartDoanhThu.Name = "chartDoanhThu";
            this.chartDoanhThu.Size = new System.Drawing.Size(706, 447);
            this.chartDoanhThu.TabIndex = 0;
            // 
            // tabControlRight
            // 
            this.tabControlRight.Controls.Add(this.tabTopProducts);
            this.tabControlRight.Controls.Add(this.tabLowStock);
            this.tabControlRight.Controls.Add(this.tabTopCustomers);
            this.tabControlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlRight.Location = new System.Drawing.Point(0, 0);
            this.tabControlRight.Name = "tabControlRight";
            this.tabControlRight.SelectedIndex = 0;
            this.tabControlRight.Size = new System.Drawing.Size(166, 447);
            this.tabControlRight.TabIndex = 0;
            // 
            // tabTopProducts
            // 
            this.tabTopProducts.Controls.Add(this.dgvChiTiet);
            this.tabTopProducts.Location = new System.Drawing.Point(4, 25);
            this.tabTopProducts.Name = "tabTopProducts";
            this.tabTopProducts.Size = new System.Drawing.Size(158, 418);
            this.tabTopProducts.TabIndex = 0;
            this.tabTopProducts.Text = "Top sản phẩm";
            // 
            // dgvChiTiet
            // 
            this.dgvChiTiet.ColumnHeadersHeight = 29;
            this.dgvChiTiet.Location = new System.Drawing.Point(0, 0);
            this.dgvChiTiet.Name = "dgvChiTiet";
            this.dgvChiTiet.RowHeadersWidth = 51;
            this.dgvChiTiet.Size = new System.Drawing.Size(240, 150);
            this.dgvChiTiet.TabIndex = 0;
            // 
            // tabLowStock
            // 
            this.tabLowStock.Controls.Add(this.dgvLowStock);
            this.tabLowStock.Location = new System.Drawing.Point(4, 25);
            this.tabLowStock.Name = "tabLowStock";
            this.tabLowStock.Size = new System.Drawing.Size(27, 0);
            this.tabLowStock.TabIndex = 1;
            this.tabLowStock.Text = "Tồn thấp";
            // 
            // dgvLowStock
            // 
            this.dgvLowStock.ColumnHeadersHeight = 29;
            this.dgvLowStock.Location = new System.Drawing.Point(0, 0);
            this.dgvLowStock.Name = "dgvLowStock";
            this.dgvLowStock.RowHeadersWidth = 51;
            this.dgvLowStock.Size = new System.Drawing.Size(240, 150);
            this.dgvLowStock.TabIndex = 0;
            // 
            // tabTopCustomers
            // 
            this.tabTopCustomers.Controls.Add(this.dgvTopCustomers);
            this.tabTopCustomers.Location = new System.Drawing.Point(4, 25);
            this.tabTopCustomers.Name = "tabTopCustomers";
            this.tabTopCustomers.Size = new System.Drawing.Size(27, 0);
            this.tabTopCustomers.TabIndex = 2;
            this.tabTopCustomers.Text = "Khách VIP";
            // 
            // dgvTopCustomers
            // 
            this.dgvTopCustomers.ColumnHeadersHeight = 29;
            this.dgvTopCustomers.Location = new System.Drawing.Point(0, 0);
            this.dgvTopCustomers.Name = "dgvTopCustomers";
            this.dgvTopCustomers.RowHeadersWidth = 51;
            this.dgvTopCustomers.Size = new System.Drawing.Size(240, 150);
            this.dgvTopCustomers.TabIndex = 0;
            // 
            // frmThongKe
            // 
            this.ClientSize = new System.Drawing.Size(882, 553);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.MinimumSize = new System.Drawing.Size(900, 600);
            this.Name = "frmThongKe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thống kê & Báo cáo";
            this.Load += new System.EventHandler(this.frmThongKe_Load);
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhThu)).EndInit();
            this.tabControlRight.ResumeLayout(false);
            this.tabTopProducts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
            this.tabLowStock.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLowStock)).EndInit();
            this.tabTopCustomers.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopCustomers)).EndInit();
            this.ResumeLayout(false);

        }
    }
}