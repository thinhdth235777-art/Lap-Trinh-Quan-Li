using QLDCAM.Business_Logic_Layer;
using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace QLDCAM.Graphical_User_Interface
{
    public partial class frmThongKe : Form
    {
        ThongKeBLL bll = new ThongKeBLL();

        // Biến xác định view ban đầu khi mở form (ví dụ: "DoanhThu", "SanPhamBanChay", "HangTonKho")
        private string initialView;

        public frmThongKe()
        {
            InitializeComponent();
        }

        // Constructor mới: cho phép truyền view khi khởi tạo
        public frmThongKe(string view) : this()
        {
            initialView = view;
        }

        private void frmThongKe_Load(object sender, EventArgs e)
        {
            // Khởi tạo các điều khiển lọc
            dtpFrom.Value = new DateTime(DateTime.Now.Year, 1, 1);
            dtpTo.Value = DateTime.Now;

            cboYear.Items.Clear();
            for (int y = DateTime.Now.Year; y >= DateTime.Now.Year - 5; y--)
                cboYear.Items.Add(y);

            if (cboYear.Items.Count > 0)
                cboYear.SelectedItem = DateTime.Now.Year;

            // Tải dữ liệu dashboard
            LoadDashboard();

            // Nếu caller truyền view qua constructor thì chọn tab tương ứng
            if (!string.IsNullOrEmpty(initialView) && tabControlRight != null)
            {
                switch (initialView)
                {
                    case "DoanhThu":
                        if (tabControlRight.TabPages.Count > 0)
                            tabControlRight.SelectedIndex = 0;
                        break;
                    case "SanPhamBanChay":
                        if (tabTopProducts != null)
                            tabControlRight.SelectedTab = tabTopProducts;
                        break;
                    case "HangTonKho":
                        if (tabLowStock != null)
                            tabControlRight.SelectedTab = tabLowStock;
                        break;
                    default:
                        break;
                }
            }
        }

        // Tải lại toàn bộ dữ liệu trên dashboard
        private void LoadDashboard()
        {
            DateTime from = dtpFrom.Value.Date;
            DateTime to = dtpTo.Value.Date.AddDays(1).AddTicks(-1);

            // Tổng quan (labels)
            DataTable sum = bll.LayTongHopDoanhThu(from, to);
            if (sum != null && sum.Rows.Count > 0)
            {
                lblTongDoanhThu.Text = Convert.ToDecimal(sum.Rows[0]["TongDoanhThu"]).ToString("N0", CultureInfo.InvariantCulture);
                lblSoHoaDon.Text = sum.Rows[0]["SoHoaDon"].ToString();
                lblSoKhachHang.Text = sum.Rows[0]["SoKhachHang"].ToString();
            }
            else
            {
                lblTongDoanhThu.Text = "0";
                lblSoHoaDon.Text = "0";
                lblSoKhachHang.Text = "0";
            }

            // Biểu đồ theo năm
            int year = DateTime.Now.Year;
            if (cboYear.SelectedItem != null)
                int.TryParse(cboYear.SelectedItem.ToString(), out year);

            DataTable monthly = bll.LayDoanhThuTheoNam(year);
            DrawMonthlyChart(monthly, year);

            // Danh sách chi tiết
            DataTable topProducts = bll.LayTopSanPhamBanChay(5);
            if (dgvChiTiet != null) dgvChiTiet.DataSource = topProducts;

            DataTable lowStock = bll.LaySanPhamSapHet(5);
            if (dgvLowStock != null) dgvLowStock.DataSource = lowStock;

            DataTable topCustomers = bll.LayTopKhachHang(5);
            if (dgvTopCustomers != null) dgvTopCustomers.DataSource = topCustomers;
        }

        // Vẽ biểu đồ doanh thu theo 12 tháng của năm
        private void DrawMonthlyChart(DataTable monthly, int year)
        {
            if (chartDoanhThu == null) return;

            chartDoanhThu.Series.Clear();
            Series s = new Series("Doanh thu")
            {
                ChartType = SeriesChartType.Column,
                XValueType = ChartValueType.String
            };
            chartDoanhThu.Series.Add(s);

            decimal[] values = new decimal[13];
            if (monthly != null)
            {
                foreach (DataRow r in monthly.Rows)
                {
                    int thang = 0;
                    int.TryParse(r["Thang"].ToString(), out thang);
                    decimal v = 0;
                    decimal.TryParse(r["TongDoanhThu"].ToString(), out v);
                    if (thang >= 1 && thang <= 12) values[thang] = v;
                }
            }

            for (int m = 1; m <= 12; m++)
            {
                string label = string.Format("{0:00}/{1}", m, year);
                s.Points.AddXY(label, values[m]);
            }

            if (chartDoanhThu.ChartAreas.Count > 0)
                chartDoanhThu.ChartAreas[0].AxisY.LabelStyle.Format = "N0";

            chartDoanhThu.Invalidate();
        }

        // Sự kiện nút Lọc — tải lại dashboard theo bộ lọc hiện tại
        private void btnLoc_Click(object sender, EventArgs e)
        {
            LoadDashboard();
        }

        // Sự kiện nút Làm mới — đặt lại bộ lọc về mặc định và tải lại
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            dtpFrom.Value = new DateTime(DateTime.Now.Year, 1, 1);
            dtpTo.Value = DateTime.Now;
            if (cboYear.Items.Count > 0)
                cboYear.SelectedItem = DateTime.Now.Year;
            LoadDashboard();
        }

        // -------------------------
        // Export CSV (mở bằng Excel)
        // -------------------------
        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            // Xuất nội dung tab hiện tại (mặc định: dgvChiTiet)
            DataGridView target = GetCurrentDataGridView();
            if (target == null || target.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "CSV (Comma delimited)|*.csv|Excel Workbook|*.xlsx";
                sfd.FileName = "ThongKe.csv";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string path = sfd.FileName;
                        ExportDataGridViewToCsv(target, path);
                        MessageBox.Show("Đã xuất file: " + path, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi xuất CSV: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ExportDataGridViewToCsv(DataGridView dgv, string filePath)
        {
            var visibleCols = dgv.Columns.Cast<DataGridViewColumn>().Where(c => c.Visible).ToArray();

            // UTF8 with BOM so Excel opens encoding correctly
            using (var sw = new StreamWriter(filePath, false, new System.Text.UTF8Encoding(true)))
            {
                // Header
                sw.WriteLine(string.Join(",", visibleCols.Select(c => QuoteCsv(c.HeaderText))));

                // Rows
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.IsNewRow) continue;
                    var values = visibleCols.Select(c =>
                    {
                        var v = row.Cells[c.Index].Value;
                        var s = v?.ToString() ?? "";
                        return QuoteCsv(s);
                    });
                    sw.WriteLine(string.Join(",", values));
                }
            }
        }

        private string QuoteCsv(string input)
        {
            if (input.Contains(",") || input.Contains("\"") || input.Contains("\n"))
            {
                return "\"" + input.Replace("\"", "\"\"") + "\"";
            }
            return input;
        }

        // Lấy DataGridView theo tab hiện tại (nếu dùng TabControl)
        private DataGridView GetCurrentDataGridView()
        {
            if (tabControlRight == null) return dgvChiTiet;
            var tab = tabControlRight.SelectedTab;
            if (tab == tabTopProducts) return dgvChiTiet;
            if (tab == tabLowStock) return dgvLowStock;
            if (tab == tabTopCustomers) return dgvTopCustomers;
            return dgvChiTiet;
        }
    }
}