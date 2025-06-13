using System;
using System.Linq;
using System.Windows.Forms;

namespace GUI_Pollycaffe
{
    public partial class frmLSP : Form
    {
        private PollyCafeDataContext db = new PollyCafeDataContext(); // Kết nối LINQ to SQL

        public frmLSP()
        {
            InitializeComponent();
            LoadData(); // Tải dữ liệu khi form khởi động
        }

        // Tải dữ liệu từ DB lên DataGridView
        private void LoadData()
        {
            dgvLSP.DataSource = db.LoaiSanPhams.Select(lsp => new
            {
                MaLSP = lsp.MaLoai,
                TenLSP = lsp.TenLoai,
                GhiChu = lsp.GhiChu
            }).ToList();
        }

        // Thêm loại sản phẩm
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMLSP.Text) || string.IsNullOrEmpty(txtTLSP.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã và Tên loại sản phẩm!", "Lỗi");
                return;
            }

            try
            {
                var newLSP = new LoaiSanPham
                {
                    MaLoai = txtMLSP.Text,
                    TenLoai = txtTLSP.Text,
                    GhiChu = txtGhiChu.Text
                };

                db.LoaiSanPhams.InsertOnSubmit(newLSP);
                db.SubmitChanges();
                LoadData(); // Refresh DataGridView
                ClearInputs();
                MessageBox.Show("Thêm thành công!", "Thông báo");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi");
            }
        }

        // Sửa loại sản phẩm
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvLSP.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một dòng để sửa!", "Lỗi");
                return;
            }

            string maLSP = dgvLSP.SelectedRows[0].Cells["MaLSP"].Value.ToString();
            var lsp = db.LoaiSanPhams.FirstOrDefault(x => x.MaLoai == maLSP);

            if (lsp != null)
            {
                lsp.TenLoai = txtTLSP.Text;
                lsp.GhiChu = txtGhiChu.Text;
                db.SubmitChanges();
                LoadData();
                MessageBox.Show("Cập nhật thành công!", "Thông báo");
            }
        }

        // Xóa loại sản phẩm
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvLSP.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa!", "Lỗi");
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xác nhận",
                MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                string maLSP = dgvLSP.SelectedRows[0].Cells["MaLSP"].Value.ToString();
                var lsp = db.LoaiSanPhams.FirstOrDefault(x => x.MaLoai == maLSP);

                if (lsp != null)
                {
                    db.LoaiSanPhams.DeleteOnSubmit(lsp);
                    db.SubmitChanges();
                    LoadData();
                    MessageBox.Show("Xóa thành công!", "Thông báo");
                }
            }
        }

        // Làm mới form
        private void btnLM_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        // Xử lý khi chọn một dòng trong DataGridView
        private void dgvLSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvLSP.Rows[e.RowIndex];
                txtMLSP.Text = row.Cells["MaLSP"].Value.ToString();
                txtTLSP.Text = row.Cells["TenLSP"].Value.ToString();
                txtGhiChu.Text = row.Cells["GhiChu"]?.Value?.ToString() ?? "";
            }
        }

        // Clear các ô nhập liệu
        private void ClearInputs()
        {
            txtMLSP.Clear();
            txtTLSP.Clear();
            txtGhiChu.Clear();
        }

        // Các sự kiện TextChanged giữ nguyên
        private void txtMLSP_TextChanged(object sender, EventArgs e) { }
        private void txtTLSP_TextChanged(object sender, EventArgs e) { }
        private void txtGhiChu_TextChanged(object sender, EventArgs e) { }

        private void dgvLSP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}