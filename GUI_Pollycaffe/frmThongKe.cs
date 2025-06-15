using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GUI_Pollycaffe;

namespace GUI_PolyCafe
{
    public partial class frmThongKe : Form
    {
        PollyCafeDataContext db = new PollyCafeDataContext();

        public frmThongKe()
        {
            InitializeComponent();
            cboLoaiSP.DataSource = db.LoaiSanPhams.ToList();
            cboLoaiSP.DisplayMember = "TenLoai";
            cboLoaiSP.ValueMember = "MaLoai";
            ThongKeLoad();
        }

        private void ThongKeLoad()
        {
            if (cboLoaiSP.SelectedIndex == -1) return;

            string maLoai = cboLoaiSP.SelectedValue?.ToString();
            string tuNgay = dtpTuNgay.Value.ToString("yyyy-MM-dd");
            string denNgay = dtpDenNgay.Value.ToString("yyyy-MM-dd");

            string connStr = db.Connection.ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("POLLYCAFE", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MaLoai", maLoai);
                cmd.Parameters.AddWithValue("@TuNgay", tuNgay);
                cmd.Parameters.AddWithValue("@DenNgay", denNgay);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvThongKe.DataSource = dt;

                if (dt.Columns.Contains("TenSanPham"))
                    dgvThongKe.Columns["TenSanPham"].HeaderText = "Tên sản phẩm";

                if (dt.Columns.Contains("SoLuong"))
                    dgvThongKe.Columns["SoLuong"].HeaderText = "Số lượng";

                if (dt.Columns.Contains("TongTien"))
                {
                    dgvThongKe.Columns["TongTien"].HeaderText = "Tổng tiền";
                    dgvThongKe.Columns["TongTien"].DefaultCellStyle.Format = "#,##0 'VNĐ'";
                    dgvThongKe.Columns["TongTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }

                if (dt.Columns.Contains("Ngay"))
                    dgvThongKe.Columns["Ngay"].HeaderText = "Ngày giao dịch";
            }
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            if (cboLoaiSP.SelectedIndex != -1)
            {
                string maLoai = cboLoaiSP.SelectedValue.ToString();

                var result = from sp in db.SanPhams
                             join lsp in db.LoaiSanPhams on sp.MaLoai equals lsp.MaLoai
                             join ctp in db.ChiTietPhieus on sp.MaSanPham equals ctp.MaSanPham
                             join pbh in db.PhieuBanHangs on ctp.MaPhieu equals pbh.MaPhieu
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

                // Header tiếng Việt cho truy vấn LINQ
                dgvThongKe.Columns["MaSP"].HeaderText = "Mã SP";
                dgvThongKe.Columns["TenSP"].HeaderText = "Tên SP";
                dgvThongKe.Columns["TenLoai"].HeaderText = "Loại SP";
                dgvThongKe.Columns["SoLuong"].HeaderText = "Số lượng";
                dgvThongKe.Columns["DonGia"].HeaderText = "Đơn giá";
                dgvThongKe.Columns["DonGia"].DefaultCellStyle.Format = "#,##0 'VNĐ'";
                dgvThongKe.Columns["ThanhTien"].HeaderText = "Thành tiền";
                dgvThongKe.Columns["ThanhTien"].DefaultCellStyle.Format = "#,##0 'VNĐ'";
                dgvThongKe.Columns["NgayBan"].HeaderText = "Ngày bán";
            }
        }
    }
}
