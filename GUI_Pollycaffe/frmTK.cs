using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace GUI_Pollycaffe
{
    public partial class frmTK : Form
    {
        PollyCafeDataContext pl = new PollyCafeDataContext();

        public frmTK()
        {
            InitializeComponent();
        }

        private void frmTK_Load(object sender, EventArgs e)
        {
            LoadLoai();
        }



        private void LoadLoai()
        {
            cboLSP.DataSource = pl.LoaiSanPhams.ToList();
            cboLSP.DisplayMember = "TenLoai";
            cboLSP.ValueMember = "MaLoai";

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            // Tùy chọn: Lọc theo ngày từ (chưa xử lý)
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            // Tùy chọn: Lọc theo ngày đến (chưa xử lý)
        }

        private void dgvThongKe_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Có thể thêm tùy chọn xử lý click cell
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            if (cboLSP.SelectedIndex != -1)
            {
                string maLoai = cboLSP.SelectedValue.ToString();

                var result = from sp in pl.SanPhams
                             join lsp in pl.LoaiSanPhams on sp.MaLoai equals lsp.MaLoai
                             join ctp in pl.ChiTietPhieus on sp.MaSanPham equals ctp.MaSanPham
                             join pbh in pl.PhieuBanHangs on ctp.MaPhieu equals pbh.MaPhieu
                             where sp.MaLoai == maLoai
                             select new
                             {
                                 MaSP = sp.MaSanPham,
                                 TenSP = sp.TenSanPham,
                                 TenLoai = lsp.TenLoai,
                                 SoLuong = ctp.SoLuong,
                                 DonGia = ctp.DonGia,
                                 ThanhTien = ctp.SoLuong * ctp.DonGia,
                                 NgayBan = pbh.NgayTao
                             };

                dgvThongKe.DataSource = result.ToList();
            }
        }
    }
}
