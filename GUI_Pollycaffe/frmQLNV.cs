using System;
using System.Linq;
using System.Windows.Forms;

namespace GUI_Pollycaffe
{
    public partial class frmQLNV : Form
    {
        public frmQLNV()
        {
            InitializeComponent();
            this.Load += FrmQLNV_Load;
            dgvNhanVien.CellClick += DgvNhanVien_CellContentClick; // Changed to CellClick for better UX
        }

        private void FrmQLNV_Load(object sender, EventArgs e)
        {
            NapNV();
        }
        private void NapNV()
        {
            using (var db = new PollyCafeDataContext())
            {
                var dsNV = db.NhanViens
                    .Select(nv => new
                    {
                        nv.MaNhanVien,
                        nv.HoTen,
                        nv.Email,
                        nv.MatKhau,
                        VaiTro = nv.VaiTro ? "Admin" : "Nhân viên",
                        TrangThai = nv.TrangThai ? "Hoạt động" : "Không hoạt động"
                    })
                    .ToList();

                dgvNhanVien.DataSource = dsNV;

                // Set column headers
                string[] headers = { "Mã NV", "Họ tên", "Email", "Mật khẩu", "Vai trò", "Trạng thái" };
                for (int i = 0; i < headers.Length; i++)
                {
                    dgvNhanVien.Columns[i].HeaderText = headers[i];
                }
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtMNV.Text))
            {
                MessageBox.Show("Vui lòng nhập mã nhân viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtTNV.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên nhân viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtEmail.Text) || !txtEmail.Text.Contains("@"))
            {
                MessageBox.Show("Vui lòng nhập email hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtMK.Text) || txtMK.Text.Length < 6)
            {
                MessageBox.Show("Mật khẩu phải có ít nhất 6 ký tự!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInput()) return;

                using (var db = new PollyCafeDataContext())
                {
                    if (db.NhanViens.Any(n => n.MaNhanVien == txtMNV.Text.Trim()))
                    {
                        MessageBox.Show("Mã nhân viên đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    NhanVien nv = new NhanVien
                    {
                        MaNhanVien = txtMNV.Text.Trim(),
                        HoTen = txtTNV.Text.Trim(),
                        Email = txtEmail.Text.Trim(),
                        MatKhau = txtMK.Text.Trim(),
                        VaiTro = rboQuanLy.Checked,
                        TrangThai = ckTrangThai.Checked
                    };

                    db.NhanViens.InsertOnSubmit(nv);
                    db.SubmitChanges();

                    NapNV();
                    MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BtnLM_Click(sender, e); // Clear form after adding
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvNhanVien.CurrentRow == null)
                {
                    MessageBox.Show("Vui lòng chọn nhân viên cần sửa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!ValidateInput()) return;

                string maNV = dgvNhanVien.CurrentRow.Cells[0].Value.ToString();

                using (var db = new PollyCafeDataContext())
                {
                    var nv = db.NhanViens.FirstOrDefault(n => n.MaNhanVien == maNV);
                    if (nv == null)
                    {
                        MessageBox.Show("Không tìm thấy nhân viên này!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Update information
                    nv.HoTen = txtTNV.Text.Trim();
                    nv.Email = txtEmail.Text.Trim();
                    nv.MatKhau = txtMK.Text.Trim();
                    nv.VaiTro = rboQuanLy.Checked;
                    nv.TrangThai = ckTrangThai.Checked;

                    db.SubmitChanges();
                    NapNV();
                    MessageBox.Show("Cập nhật nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvNhanVien.CurrentRow == null)
                {
                    MessageBox.Show("Vui lòng chọn nhân viên cần xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string maNV = dgvNhanVien.CurrentRow.Cells[0].Value.ToString();

                if (MessageBox.Show("Bạn có chắc muốn xóa nhân viên này?", "Xác nhận", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (var db = new PollyCafeDataContext())
                    {
                        var nv = db.NhanViens.FirstOrDefault(n => n.MaNhanVien == maNV);
                        if (nv == null)
                        {
                            MessageBox.Show("Không tìm thấy nhân viên này!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        db.NhanViens.DeleteOnSubmit(nv);
                        db.SubmitChanges();

                        NapNV();
                        MessageBox.Show("Xóa nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        BtnLM_Click(sender, e); // Clear form after deleting
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa nhân viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnLM_Click(object sender, EventArgs e)
        {
            txtMNV.Clear();
            txtTNV.Clear();
            txtEmail.Clear();
            txtMK.Clear();
            rboNhanVien.Checked = true;
            ckTrangThai.Checked = true;
            txtMNV.Focus();
        }

        private void DgvNhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvNhanVien.Rows[e.RowIndex];
                txtMNV.Text = row.Cells[0].Value.ToString();
                txtTNV.Text = row.Cells[1].Value.ToString();
                txtEmail.Text = row.Cells[2].Value.ToString();
                txtMK.Text = row.Cells[3].Value.ToString();

                string vaiTro = row.Cells[4].Value.ToString();
                rboQuanLy.Checked = vaiTro == "Admin";
                rboNhanVien.Checked = !rboQuanLy.Checked;

                string trangThai = row.Cells[5].Value.ToString();
                ckTrangThai.Checked = trangThai == "Hoạt động";
            }
        }

     
    }
}