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
            this.components = new Container();

            this.tableLayoutPanelMain = new TableLayoutPanel();
            this.panelTop = new Panel();
            this.splitContainerMain = new SplitContainer();

            this.lblFrom = new Label();
            this.dtpFrom = new DateTimePicker();
            this.lblTo = new Label();
            this.dtpTo = new DateTimePicker();
            this.lblYear = new Label();
            this.cboYear = new ComboBox();
            this.btnLoc = new Button();
            this.btnRefresh = new Button();

            this.btnExportExcel = new Button();

            this.lblTongDoanhThu = new Label();
            this.lblSoHoaDon = new Label();
            this.lblSoKhachHang = new Label();

            this.chartDoanhThu = new Chart();
            this.tabControlRight = new TabControl();
            this.tabTopProducts = new TabPage();
            this.tabLowStock = new TabPage();
            this.tabTopCustomers = new TabPage();

            this.dgvChiTiet = new DataGridView();
            this.dgvLowStock = new DataGridView();
            this.dgvTopCustomers = new DataGridView();

            ((ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();

            ((ISupportInitialize)(this.chartDoanhThu)).BeginInit();

            this.tabControlRight.SuspendLayout();
            this.tabTopProducts.SuspendLayout();
            this.tabLowStock.SuspendLayout();
            this.tabTopCustomers.SuspendLayout();

            this.SuspendLayout();

            // tableLayoutPanelMain
            this.tableLayoutPanelMain.ColumnCount = 1;
            this.tableLayoutPanelMain.RowCount = 2;
            this.tableLayoutPanelMain.Dock = DockStyle.Fill;
            this.tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
            this.tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            // panelTop (filters + summary)
            this.panelTop.Dock = DockStyle.Fill;

            // Filters: dtpFrom, dtpTo, cboYear, btnLoc, btnRefresh
            this.lblFrom.Text = "Từ:";
            this.lblFrom.AutoSize = true;
            this.lblFrom.Location = new Point(10, 12);

            this.dtpFrom.Format = DateTimePickerFormat.Short;
            this.dtpFrom.Location = new Point(40, 8);
            this.dtpFrom.Width = 100;

            this.lblTo.Text = "Đến:";
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new Point(150, 12);

            this.dtpTo.Format = DateTimePickerFormat.Short;
            this.dtpTo.Location = new Point(185, 8);
            this.dtpTo.Width = 100;

            this.lblYear.Text = "Năm:";
            this.lblYear.AutoSize = true;
            this.lblYear.Location = new Point(295, 12);

            this.cboYear.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboYear.Location = new Point(330, 8);
            this.cboYear.Width = 80;

            this.btnLoc.Text = "Lọc";
            this.btnLoc.Location = new Point(420, 7);
            this.btnLoc.Width = 70;
            this.btnLoc.Click += new EventHandler(this.btnLoc_Click);

            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.Location = new Point(500, 7);
            this.btnRefresh.Width = 80;
            this.btnRefresh.Click += new EventHandler(this.btnRefresh_Click);

            // Export Excel button
            this.btnExportExcel.Text = "Xuất Excel";
            this.btnExportExcel.Location = new Point(590, 7);
            this.btnExportExcel.Width = 90;
            this.btnExportExcel.Click += new EventHandler(this.btnExportExcel_Click);

            // Summary labels (simple)
            this.lblTongDoanhThu.Text = "0";
            this.lblTongDoanhThu.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            this.lblTongDoanhThu.AutoSize = false;
            this.lblTongDoanhThu.TextAlign = ContentAlignment.MiddleCenter;
            this.lblTongDoanhThu.BorderStyle = BorderStyle.FixedSingle;
            this.lblTongDoanhThu.Size = new Size(220, 60);
            this.lblTongDoanhThu.Location = new Point(800, 8);

            this.lblSoHoaDon.Text = "0";
            this.lblSoHoaDon.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblSoHoaDon.AutoSize = false;
            this.lblSoHoaDon.TextAlign = ContentAlignment.MiddleCenter;
            this.lblSoHoaDon.BorderStyle = BorderStyle.FixedSingle;
            this.lblSoHoaDon.Size = new Size(140, 60);
            this.lblSoHoaDon.Location = new Point(1030, 8);

            this.lblSoKhachHang.Text = "0";
            this.lblSoKhachHang.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblSoKhachHang.AutoSize = false;
            this.lblSoKhachHang.TextAlign = ContentAlignment.MiddleCenter;
            this.lblSoKhachHang.BorderStyle = BorderStyle.FixedSingle;
            this.lblSoKhachHang.Size = new Size(140, 60);
            this.lblSoKhachHang.Location = new Point(1180, 8);

            // Add top controls to panelTop
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

            // splitContainerMain
            this.splitContainerMain.Dock = DockStyle.Fill;
            this.splitContainerMain.Orientation = Orientation.Vertical;
            this.splitContainerMain.SplitterDistance = 700;

            // chartDoanhThu (left panel)
            ChartArea ca = new ChartArea("ChartArea1");
            this.chartDoanhThu.ChartAreas.Add(ca);
            this.chartDoanhThu.Dock = DockStyle.Fill;
            this.chartDoanhThu.Legends.Add(new Legend("Legend1"));
            this.chartDoanhThu.Location = new Point(0, 0);
            this.chartDoanhThu.Name = "chartDoanhThu";

            this.splitContainerMain.Panel1.Controls.Add(this.chartDoanhThu);

            // tabControlRight with three tabs, each containing a DataGridView
            this.tabControlRight.Dock = DockStyle.Fill;

            // tabTopProducts
            this.tabTopProducts.Text = "Top sản phẩm";
            this.tabTopProducts.Controls.Add(this.dgvChiTiet);

            // tabLowStock
            this.tabLowStock.Text = "Tồn thấp";
            this.tabLowStock.Controls.Add(this.dgvLowStock);

            // tabTopCustomers
            this.tabTopCustomers.Text = "Khách VIP";
            this.tabTopCustomers.Controls.Add(this.dgvTopCustomers);

            this.tabControlRight.TabPages.Add(this.tabTopProducts);
            this.tabControlRight.TabPages.Add(this.tabLowStock);
            this.tabControlRight.TabPages.Add(this.tabTopCustomers);

            // DataGridViews basic config
            Action<DataGridView> cfgGrid = (g) =>
            {
                g.Dock = DockStyle.Fill;
                g.ReadOnly = true;
                g.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                g.AllowUserToAddRows = false;
                g.AllowUserToDeleteRows = false;
                g.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            };

            cfgGrid(this.dgvChiTiet);
            cfgGrid(this.dgvLowStock);
            cfgGrid(this.dgvTopCustomers);

            this.splitContainerMain.Panel2.Controls.Add(this.tabControlRight);

            // Add rows to main layout
            this.tableLayoutPanelMain.Controls.Add(this.panelTop, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.splitContainerMain, 0, 1);

            // Add main to form
            this.Controls.Add(this.tableLayoutPanelMain);

            // Form settings
            this.Text = "Thống kê & Báo cáo";
            this.MinimumSize = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterParent;

            // Wire form load (frmThongKe.cs has frmThongKe_Load)
            this.Load += new EventHandler(this.frmThongKe_Load);

            // End init
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);

            ((ISupportInitialize)(this.chartDoanhThu)).EndInit();

            this.tabTopProducts.ResumeLayout(false);
            this.tabLowStock.ResumeLayout(false);
            this.tabTopCustomers.ResumeLayout(false);
            this.tabControlRight.ResumeLayout(false);

            this.ResumeLayout(false);
        }
    }
}