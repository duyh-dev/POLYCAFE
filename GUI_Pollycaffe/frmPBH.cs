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
        PollyCafeDataContext pl = new PollyCafeDataContext();
        public frmPBH()
        {
            InitializeComponent();
        }

        private void ChiTietPhieu()
        {
           dgvCTP.DataSource = pl.ChiTietPhieus;
        }
        private void NapPhieuBanHang()
        {
            
            dgvPBH.DataSource = pl.PhieuBanHangs;
        }
        private void frmPBH_Load(object sender, EventArgs e)
        {
            NapPhieuBanHang();
            ChiTietPhieu();
            LoadComboBox();
        }

        private void LoadComboBox()
        {
            cboMNV.DataSource = pl.NhanViens.ToList();
            cboMNV.DisplayMember = "HoTen";
            cboMNV.ValueMember = "MaNhanVien";

            cboMT.DataSource = pl.TheLuuDongs.ToList(); // nếu là mã bàn
            cboMT.DisplayMember = "ChuSoHuu";
            cboMT.ValueMember = "MaThe";

            cboSP.DataSource = pl.SanPhams.ToList();
            cboSP.DisplayMember = "TenSanPham";
            cboSP.ValueMember = "MaSanPham";
        }

        private void btnThemPhieu_Click(object sender, EventArgs e)
        {
            // Kiểm tra MaPhieu đã tồn tại chưa
            var existingPhieu = pl.PhieuBanHangs.SingleOrDefault(p => p.MaPhieu == txtMP.Text);
            if (existingPhieu != null)
            {
                MessageBox.Show("Mã phiếu đã tồn tại. Vui lòng nhập mã khác!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Nếu chưa tồn tại thì thêm mới
            PhieuBanHang pbh = new PhieuBanHang
            {
                MaPhieu = txtMP.Text,
                MaNhanVien = cboMNV.SelectedValue.ToString(),
                MaThe = cboMT.SelectedValue.ToString(),
                NgayTao = dateTimePicker1.Value,
                TrangThai = rdoCXN.Checked ? false : true
            };

            pl.PhieuBanHangs.InsertOnSubmit(pbh);
            pl.SubmitChanges();
            MessageBox.Show("Thêm phiếu thành công!");
            NapPhieuBanHang();
        }

        private void btnSuaPhieu_Click(object sender, EventArgs e)
        {
            PhieuBanHang phieu = pl.PhieuBanHangs.SingleOrDefault(p => p.MaPhieu == txtMP.Text);
            if (phieu == null)
            {
                MessageBox.Show("Phiếu không tồn tại.");
                return;
            }

            // Kiểm tra và ép kiểu an toàn
            if (!int.TryParse(txtSoLuong.Text, out int soLuong))
            {
                MessageBox.Show("Vui lòng nhập đúng định dạng số lượng.");
                return;
            }

            if (!decimal.TryParse(txtDonGia.Text, out decimal donGia))
            {
                MessageBox.Show("Vui lòng nhập đúng định dạng đơn giá.");
                return;
            }

            try
            {
                ChiTietPhieu ct = new ChiTietPhieu
                {
                    PhieuBanHang = phieu, // Gán đối tượng
                    MaSanPham = cboSP.SelectedValue.ToString(),
                    SoLuong = soLuong,
                    DonGia = donGia
                };

                pl.ChiTietPhieus.InsertOnSubmit(ct);
                pl.SubmitChanges();
                MessageBox.Show("Cập nhật chi tiết phiếu thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }



        private void btnXoaPhieu_Click(object sender, EventArgs e)
        {
            var pbh = pl.PhieuBanHangs.SingleOrDefault(p => p.MaPhieu == txtMP.Text);
            if (pbh != null)
            {
                // Xóa tất cả chi tiết phiếu liên quan trước
                var chiTietList = pl.ChiTietPhieus.Where(ct => ct.MaPhieu == pbh.MaPhieu);
                pl.ChiTietPhieus.DeleteAllOnSubmit(chiTietList);

                // Xóa phiếu
                pl.PhieuBanHangs.DeleteOnSubmit(pbh);

                try
                {
                    pl.SubmitChanges();
                    MessageBox.Show("Xóa phiếu thành công!");
                    NapPhieuBanHang();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa phiếu: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Không tìm thấy phiếu để xóa.");
            }
        }



        private void btnLM_Click(object sender, EventArgs e)
        {
            txtMP.Clear();
            cboMNV.SelectedIndex = 0;
            cboMT.SelectedIndex = 0;
            dateTimePicker1.Value = DateTime.Now;
            rdoCXN.Checked = true;
        }

        private void dgvPBH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index >= 0 && dgvPBH.Rows[index].Cells["MaPhieu"].Value != null)
            {
                txtMP.Text = dgvPBH.Rows[index].Cells["MaPhieu"].Value.ToString();
                cboMNV.SelectedValue = dgvPBH.Rows[index].Cells["MaNhanVien"].Value.ToString();
                cboMT.SelectedValue = dgvPBH.Rows[index].Cells["MaThe"].Value.ToString();
                dateTimePicker1.Value = Convert.ToDateTime(dgvPBH.Rows[index].Cells["NgayTao"].Value);

                bool trangThai = Convert.ToBoolean(dgvPBH.Rows[index].Cells["TrangThai"].Value);
                rdoCXN.Checked = !trangThai;
                rdoDTT.Checked = trangThai;
            }
        }


        private void btnThanhToan_Click(object sender, EventArgs e)
        {

        }
        private void btnThemCT_Click(object sender, EventArgs e)
        {
            // Kiểm tra MaPhieu có tồn tại không
            var phieu = pl.PhieuBanHangs.SingleOrDefault(p => p.MaPhieu == txtMP.Text);
            if (phieu == null)
            {
                MessageBox.Show("Mã phiếu chưa tồn tại. Vui lòng tạo phiếu trước khi thêm chi tiết!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Thêm chi tiết phiếu
            ChiTietPhieu ct = new ChiTietPhieu
            {
                MaPhieu = txtMP.Text,
                MaSanPham = cboSP.SelectedValue.ToString(),
                SoLuong = int.Parse(txtSoLuong.Text),
                DonGia = decimal.Parse(txtDonGia.Text)
            };

            pl.ChiTietPhieus.InsertOnSubmit(ct);
            pl.SubmitChanges();
            MessageBox.Show("Thêm chi tiết phiếu thành công!");
            ChiTietPhieu(); // Reload dữ liệu
        }


        private void btnSuaCT_Click(object sender, EventArgs e)
        {
            var maPBH = txtMP.Text;
            if (cboSP.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm.");
                return;
            }

            var maSP = cboSP.SelectedValue.ToString();
            var ct = pl.ChiTietPhieus.SingleOrDefault(c => c.MaPhieu == maPBH && c.MaSanPham == maSP);

            if (ct == null)
            {
                MessageBox.Show("Không tìm thấy chi tiết phiếu.");
                return;
            }

            if (!int.TryParse(txtSoLuong.Text, out int soLuong) || !decimal.TryParse(txtDonGia.Text, out decimal donGia))
            {
                MessageBox.Show("Dữ liệu số lượng hoặc đơn giá không hợp lệ.");
                return;
            }

            ct.SoLuong = soLuong;
            ct.DonGia = donGia;

            try
            {
                pl.SubmitChanges();
                MessageBox.Show("Sửa chi tiết phiếu thành công!");
                ChiTietPhieu();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật dữ liệu: " + ex.Message);
            }
        }


        private void btnXoaCT_Click(object sender, EventArgs e)
        {
            var maPBH = txtMP.Text;
            if (string.IsNullOrEmpty(maPBH))
            {
                MessageBox.Show("Mã phiếu không được để trống.");
                return;
            }

            if (cboSP.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm.");
                return;
            }

            var maSP = cboSP.SelectedValue.ToString();
            var ct = pl.ChiTietPhieus.SingleOrDefault(c => c.MaPhieu == maPBH && c.MaSanPham == maSP);

            if (ct == null)
            {
                MessageBox.Show("Không tìm thấy chi tiết phiếu để xóa.");
                return;
            }

            // Xác nhận trước khi xóa (tùy chọn)
            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa chi tiết phiếu này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result != DialogResult.Yes)
                return;

            pl.ChiTietPhieus.DeleteOnSubmit(ct);

            try
            {
                pl.SubmitChanges();
                MessageBox.Show("Xóa chi tiết phiếu thành công!");
                ChiTietPhieu(); // làm mới danh sách chi tiết phiếu
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa dữ liệu: " + ex.Message);
            }
        }

        private void dgvCTP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index >= 0)
            {
                cboSP.SelectedValue = dgvCTP.Rows[index].Cells["MaSanPham"].Value.ToString();
                txtSoLuong.Text = dgvCTP.Rows[index].Cells["SoLuong"].Value.ToString();
                txtDonGia.Text = dgvCTP.Rows[index].Cells["DonGia"].Value.ToString();
            }
        }

        private void cboMNV_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboMT_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void rdoCXN_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdoDTT_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cboSP_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtMP_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDonGia_TextChanged(object sender, EventArgs e)
        {
            TinhThanhTien();
        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            TinhThanhTien();
        }

        private void TinhThanhTien()
        {
            if (int.TryParse(txtSoLuong.Text, out int soLuong) &&
                decimal.TryParse(txtDonGia.Text, out decimal donGia))
            {
                decimal thanhTien = soLuong * donGia;
                guna2TextBox1.Text = thanhTien.ToString("N0"); // Format đẹp: 1,000
            }
            else
            {
                guna2TextBox1.Text = "0";
            }
        }


    }
}
