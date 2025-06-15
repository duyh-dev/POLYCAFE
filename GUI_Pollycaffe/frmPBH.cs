using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_Pollycaffe
{
    public partial class frmPBH : Form
    {
        PollyCafeDataContext db = new PollyCafeDataContext();
        public frmPBH()
        {
            InitializeComponent();
            LoadPhieuBanHang();
            LoadComboBox();
            LoadChiTietPhieu();
        }

        private void LoadComboBox()
        {
            // Load nhân viên
            cboMNV.DataSource = db.NhanViens.ToList();
            cboMNV.DisplayMember = "TenNhanVien";
            cboMNV.ValueMember = "MaNhanVien";

            // Load thẻ
            cboMT.DataSource = db.TheLuuDongs.ToList();
            cboMT.DisplayMember = "MaThe";
            cboMT.ValueMember = "MaThe";

            // Load sản phẩm cho chi tiết phiếu
            cboSP.DataSource = db.SanPhams.ToList();
            cboSP.DisplayMember = "TenSanPham";
            cboSP.ValueMember = "MaSanPham";
        }

        private void LoadPhieuBanHang()
        {
            var list = db.PhieuBanHangs.Select(p => new
            {
                p.MaPhieu,
                p.MaThe,
                p.MaNhanVien,
                p.NgayTao,
                p.TrangThai
            }).ToList();
            dgvPBH.DataSource = list;
            dgvPBH.ColumnHeadersHeight = 30;

        }

        private void LoadChiTietPhieu()
        {
            var list = db.ChiTietPhieus.Select(p => new
            {
                p.Id,
                p.MaPhieu,
                p.MaSanPham,
                p.SoLuong,
                p.DonGia,
                ThanhTien = p.SoLuong * p.DonGia
            }).ToList();

            dgvCTP.DataSource = list;
            dgvCTP.ColumnHeadersHeight = 30;

            // Định dạng cột tiền tệ
            dgvCTP.Columns["DonGia"].DefaultCellStyle.Format = "#,##0 '₫'";
            dgvCTP.Columns["DonGia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgvCTP.Columns["ThanhTien"].DefaultCellStyle.Format = "#,##0 '₫'";
            dgvCTP.Columns["ThanhTien"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }



        private void btnThemPhieu_Click(object sender, EventArgs e)
        {
            try
            {
                PhieuBanHang phieu = new PhieuBanHang
                {
                    MaPhieu = txtMP.Text,
                    MaThe = cboMT.SelectedValue.ToString(),
                    MaNhanVien = cboMNV.SelectedValue.ToString(),
                    NgayTao = dtpNgayTao.Value,
                    TrangThai = rdoCXN.Checked ? false : true
                };
                db.PhieuBanHangs.InsertOnSubmit(phieu);
                db.SubmitChanges();
                MessageBox.Show("Thêm phiếu thành công!");
                LoadPhieuBanHang();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnSuaPhieu_Click(object sender, EventArgs e)
        {
            try
            {
                var phieu = db.PhieuBanHangs.SingleOrDefault(p => p.MaPhieu == txtMP.Text);
                if (phieu != null)
                {
                    phieu.MaThe = cboMT.SelectedValue.ToString();
                    phieu.MaNhanVien = cboMNV.SelectedValue.ToString();
                    phieu.NgayTao = dtpNgayTao.Value;
                    phieu.TrangThai = rdoCXN.Checked ? false : true;
                    db.SubmitChanges();
                    MessageBox.Show("Sửa phiếu thành công!");
                    LoadPhieuBanHang();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void LamMoi()
        {
            txtMP.Text = "";
            cboMT.SelectedIndex = -1;
            cboMNV.SelectedIndex = -1;
            dtpNgayTao.Value = DateTime.Now;
            rdoCXN.Checked = true;
            rdoDTT.Checked = false;
        }
        private void btnLamMoiPhieu_Click(object sender, EventArgs e)
        {
            LamMoi();
        }

        private void btnXoaPhieu_Click(object sender, EventArgs e)
        {
            try
            {
                var phieu = db.PhieuBanHangs.SingleOrDefault(p => p.MaPhieu == txtMP.Text);
                if (phieu != null)
                {
                    db.PhieuBanHangs.DeleteOnSubmit(phieu);
                    db.SubmitChanges();
                    MessageBox.Show("Xóa phiếu thành công!");
                    LoadPhieuBanHang();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMP.Text))
            {
                MessageBox.Show("Vui lòng chọn phiếu cần thanh toán!");
                return;
            }

            var phieu = db.PhieuBanHangs.SingleOrDefault(p => p.MaPhieu == txtMP.Text);
            if (phieu == null)
            {
                MessageBox.Show("Không tìm thấy phiếu bán hàng!");
                return;
            }

            if (phieu.TrangThai == true)
            {
                MessageBox.Show("Phiếu này đã được thanh toán!");
                return;
            }

            phieu.TrangThai = true;
            db.SubmitChanges();
            MessageBox.Show("Thanh toán thành công!");
            LoadPhieuBanHang();

            // Cập nhật lại trạng thái trên form
            rdoDTT.Checked = true;
            rdoCXN.Checked = false;
        }
        private void btnThemChiTiet_Click(object sender, EventArgs e)
        {
            try
            {
                ChiTietPhieu ctp = new ChiTietPhieu
                {
                    MaPhieu = txtMP.Text,
                    MaSanPham = cboSP.SelectedValue.ToString(),
                    DonGia = decimal.Parse(txtDonGia.Text),
                    SoLuong = int.Parse(txtSoLuong.Text),
                };
                db.ChiTietPhieus.InsertOnSubmit(ctp);
                db.SubmitChanges();
                MessageBox.Show("Thêm chi tiết phiếu thành công!");
                LoadChiTietPhieu();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnSuaChiTiet_Click(object sender, EventArgs e)
        {
            try
            {
                var ctp = db.ChiTietPhieus.SingleOrDefault(
                    x => x.MaPhieu == txtMP.Text && x.MaSanPham == cboSP.SelectedValue.ToString());
                if (ctp != null)
                {
                    ctp.DonGia = decimal.Parse(txtDonGia.Text);
                    ctp.SoLuong = int.Parse(txtSoLuong.Text);
                    db.SubmitChanges();
                    MessageBox.Show("Sửa chi tiết phiếu thành công!");
                    LoadChiTietPhieu();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnXoaChiTiet_Click(object sender, EventArgs e)
        {
            try
            {
                var ctp = db.ChiTietPhieus.SingleOrDefault(
                    x => x.MaPhieu == txtMP.Text && x.MaSanPham == cboSP.SelectedValue.ToString());
                if (ctp != null)
                {
                    db.ChiTietPhieus.DeleteOnSubmit(ctp);
                    db.SubmitChanges();
                    MessageBox.Show("Xóa chi tiết phiếu thành công!");
                    LoadChiTietPhieu();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void dgvCTP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvCTP.Rows[e.RowIndex];

                // Lấy mã sản phẩm từ dòng được chọn
                string maSanPham = row.Cells["MaSanPham"].Value.ToString();
                cboSP.SelectedValue = maSanPham;

                // Lấy đơn giá và số lượng
                txtDonGia.Text = row.Cells["DonGia"].Value.ToString();
                txtSoLuong.Text = row.Cells["SoLuong"].Value.ToString();

                // Tính thành tiền (nếu có cột này)
                if (row.Cells["ThanhTien"] != null && row.Cells["ThanhTien"].Value != null)
                {
                    txtThanhTien.Text = row.Cells["ThanhTien"].Value.ToString();
                }
                else
                {
                    // Nếu không có cột thành tiền, tự tính
                    decimal donGia = decimal.Parse(txtDonGia.Text);
                    int soLuong = int.Parse(txtSoLuong.Text);
                    txtThanhTien.Text = (donGia * soLuong).ToString();
                }
            }
        }

        private void dgvPBH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.RowIndex >= 0)
                {
                    var row = dgvPBH.Rows[e.RowIndex];

                    // Hiển thị thông tin phiếu lên các control
                    txtMP.Text = row.Cells["MaPhieu"].Value?.ToString();
                    cboMT.SelectedValue = row.Cells["MaThe"].Value?.ToString();
                    cboMNV.SelectedValue = row.Cells["MaNhanVien"].Value?.ToString();

                    // Ngày tạo
                    if (row.Cells["NgayTao"].Value != null)
                    {
                        DateTime ngayTao;
                        if (DateTime.TryParse(row.Cells["NgayTao"].Value.ToString(), out ngayTao))
                        {
                            dtpNgayTao.Value = ngayTao;
                        }
                    }

                    // Trạng thái
                    if (row.Cells["TrangThai"].Value != null)
                    {
                        bool trangThai = Convert.ToBoolean(row.Cells["TrangThai"].Value);
                        rdoCXN.Checked = !trangThai;
                        rdoDTT.Checked = trangThai;
                    }

                    // Load chi tiết phiếu tương ứng
                    LoadPhieuBanHang();
                }
            }
        }

    }
}
