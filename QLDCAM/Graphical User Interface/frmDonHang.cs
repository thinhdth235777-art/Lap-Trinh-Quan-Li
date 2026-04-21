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
    public partial class frmDonHang : Form
    {
        public frmDonHang()
        {
            InitializeComponent();
        }
        DBConnect db;
        int id;
        private void dtgHoadon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            using(frmCTDonHang chiTiet=new frmCTDonHang())
            {
                chiTiet.ShowDialog();
            }    
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            id = Convert.ToInt32(dtgHoadon.CurrentRow.Cells["ID"].Value.ToString());
            using (frmCTDonHang chiTiet = new frmCTDonHang(id))
            {
                chiTiet.ShowDialog();
            } 
                
        }
    }
}
