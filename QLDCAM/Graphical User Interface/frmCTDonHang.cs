using QLDCAM.Data_Access_Layer;
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
        public frmCTDonHang()
        {
            InitializeComponent();
        }
        DBConnect db;
        public int id = 0;
        public void gandulieu(ComboBox cbo, DataTable nguondulieu, string hienthi, string luutru)
        {
            cbo.DataSource = nguondulieu;
            cbo.DisplayMember = hienthi;
            cbo.ValueMember = luutru;
        }
        void setButton()
        {
            if(id==0 && dtgHoaDon.Rows.Count==0)
            {
                cbKH.Text = "";
                cbNV.Text = "";
                cbSanPham.Text = "";
                numSL.Value = 1;
            }   
            btn
        }
    }
}
