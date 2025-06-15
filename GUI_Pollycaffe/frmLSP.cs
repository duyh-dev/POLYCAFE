using System;
using System.Linq;
using System.Windows.Forms;

namespace GUI_Pollycaffe
{
    public partial class frmLSP : Form
    {
        PollyCafeDataContext db = new PollyCafeDataContext();
        public frmLSP()
        {
            InitializeComponent();
        }

        private void frmLoaiSanPham_Load(object sender, EventArgs e)
        {
            LoadLoaiSanPham();
        }

        private void LoadLoaiSanPham()
        {
            var list = db.LoaiSanPhams
                .Select(lsp => new
                {
                    lsp.MaLoai,
                    lsp.TenLoai,
                    lsp.GhiChu
                })
                .ToList();
            dgvLSP.DataSource = list;
            dgvLSP.Columns["MaLoai"].HeaderText = "Mã loại sản phẩm";
            dgvLSP.Columns["TenLoai"].HeaderText = "Tên loại sản phẩm";
            dgvLSP.Columns["GhiChu"].HeaderText = "Ghi chú";
            dgvLSP.ColumnHeadersHeight = 23;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // Kiểm tra đầu vào
            if (string.IsNullOrWhiteSpace(txtMLSP.Text))
            {
                MessageBox.Show("Vui lòng nhập mã loại sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMLSP.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTLSP.Text))
            {
                MessageBox.Show("Vui lòng nhập tên loại sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTLSP.Focus();
                return;
            }

            // Kiểm tra trùng mã loại
            var daTonTai = db.LoaiSanPhams.Any(l => l.MaLoai == txtMLSP.Text.Trim());
            if (daTonTai)
            {
                MessageBox.Show("❗ Mã loại sản phẩm đã tồn tại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMLSP.Focus();
                return;
            }

            try
            {
                LoaiSanPham loaiSP = new LoaiSanPham
                {
                    MaLoai = txtMLSP.Text.Trim(),
                    TenLoai = txtTLSP.Text.Trim(),
                    GhiChu = txtGhiChu.Text.Trim()
                };

                db.LoaiSanPhams.InsertOnSubmit(loaiSP);
                db.SubmitChanges();

                MessageBox.Show("✅ Thêm loại sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadLoaiSanPham();

                txtMLSP.Enabled = false; // Không cho sửa mã sau khi thêm
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm loại sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LamMoi()
        {
            txtMLSP.Text = "";
            txtTLSP.Text = "";
            txtGhiChu.Text = "";
            txtMLSP.Enabled = true;
            LoadLoaiSanPham();
        }
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LamMoi();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                var maLoai = txtMLSP.Text;
                var loaiSP = db.LoaiSanPhams.FirstOrDefault(l => l.MaLoai == maLoai);
                if (loaiSP != null)
                {
                    loaiSP.TenLoai = txtTLSP.Text;
                    loaiSP.GhiChu = txtGhiChu.Text;
                    db.SubmitChanges();
                    MessageBox.Show("Cập nhật loại sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadLoaiSanPham();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy loại sản phẩm với mã: " + maLoai, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật loại sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                var maLoai = txtMLSP.Text;
                var loaiSP = db.LoaiSanPhams.FirstOrDefault(l => l.MaLoai == maLoai);
                if (loaiSP != null)
                {
                    db.LoaiSanPhams.DeleteOnSubmit(loaiSP);
                    db.SubmitChanges();
                    MessageBox.Show("Xóa loại sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LamMoi();
                    LoadLoaiSanPham();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy loại sản phẩm với mã: " + maLoai, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa loại sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            LamMoi();
        }

        private void dgvLSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvLSP.Rows.Count)
            {
                var row = dgvLSP.Rows[e.RowIndex];
                txtMLSP.Text = row.Cells["MaLoai"].Value.ToString();
                txtTLSP.Text = row.Cells["TenLoai"].Value.ToString();
                txtGhiChu.Text = row.Cells["GhiChu"].Value != null ? row.Cells["GhiChu"].Value.ToString() : "";
                txtMLSP.Enabled = false;

            }
        }
    }
}