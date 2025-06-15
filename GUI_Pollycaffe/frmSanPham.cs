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
    public partial class frmSanPham : Form
    {
        PollyCafeDataContext db = new PollyCafeDataContext();
        private string _hinhAnhPath = "";

        public frmSanPham()
        {
            InitializeComponent();
            dgvSanPham.CellClick += dgvSanPham_CellClick;
        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                PB1.Image = Image.FromFile(openFileDialog.FileName);
                _hinhAnhPath = openFileDialog.FileName;
            }
        }

        private void LoadSanPham()
        {
            var list = db.SanPhams
                .Select(sp => new
                {
                    sp.MaSanPham,
                    sp.TenSanPham,
                    sp.DonGia,
                    LoaiSP = sp.LoaiSanPham.TenLoai,
                    sp.HinhAnh,
                    TrangThai = sp.TrangThai ? "Đang bán" : "Ngừng bán"
                })
                .ToList();

            dgvSanPham.AutoGenerateColumns = true;
            dgvSanPham.DataSource = list;

            // Đặt header
            dgvSanPham.Columns["MaSanPham"].HeaderText = "Mã sản phẩm";
            dgvSanPham.Columns["TenSanPham"].HeaderText = "Tên sản phẩm";
            dgvSanPham.Columns["DonGia"].HeaderText = "Đơn giá";
            dgvSanPham.Columns["LoaiSP"].HeaderText = "Loại sản phẩm";
            dgvSanPham.Columns["HinhAnh"].HeaderText = "Hình ảnh";
            dgvSanPham.Columns["TrangThai"].HeaderText = "Trạng thái";

            // Định dạng cột giá tiền
            dgvSanPham.Columns["DonGia"].DefaultCellStyle.Format = "#,##0 '₫'";
            dgvSanPham.Columns["DonGia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            // Căn giữa cột loại sản phẩm
            dgvSanPham.Columns["LoaiSP"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Giao diện header đẹp hơn
            dgvSanPham.EnableHeadersVisualStyles = false;
            dgvSanPham.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue;
            dgvSanPham.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvSanPham.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            // Chiều cao dòng
            dgvSanPham.RowTemplate.Height = 30;
            dgvSanPham.ColumnHeadersHeight = 35;
        }

        private void frmSanPham_Load(object sender, EventArgs e)
        {
            LoadSanPham();
            LoadLoaiSanPham();
            dgvSanPham.CellClick += dgvSanPham_CellClick;
        }

        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvSanPham.Rows.Count)
            {
                var row = dgvSanPham.Rows[e.RowIndex];
                txtMSP.Text = row.Cells["MaSanPham"].Value?.ToString();
                txtTSP.Text = row.Cells["TenSanPham"].Value?.ToString();
                txtDonGia.Text = row.Cells["DonGia"].Value?.ToString();
                cboLSP.Text = row.Cells["LoaiSP"].Value?.ToString();

                txtMSP.Enabled = false; // Không cho sửa mã sản phẩm khi sửa

                string trangThai = row.Cells["TrangThai"].Value?.ToString();
                if (trangThai == "Đang bán")
                {
                    rbDB.Checked = true;
                }
                else if (trangThai == "Ngừng bán")
                {
                    rboNB.Checked = true;
                }

                string imagePath = row.Cells["HinhAnh"].Value?.ToString();
                if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
                {
                    PB1.Image = Image.FromFile(imagePath);
                    _hinhAnhPath = imagePath;
                }
                else
                {
                    PB1.Image = null;
                    _hinhAnhPath = "";
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                SanPham sp = new SanPham
                {
                    MaSanPham = txtMSP.Text,
                    TenSanPham = txtTSP.Text,
                    DonGia = decimal.Parse(txtDonGia.Text),
                    MaLoai = cboLSP.SelectedValue.ToString(),
                    HinhAnh = _hinhAnhPath,
                    TrangThai = true
                };
                db.SanPhams.InsertOnSubmit(sp);
                db.SubmitChanges();
                MessageBox.Show("Thêm sản phẩm thành công!");
                LoadSanPham();
                LamMoiForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                var sp = db.SanPhams.SingleOrDefault(s => s.MaSanPham == txtMSP.Text);
                if (sp != null)
                {
                    sp.TenSanPham = txtTSP.Text;
                    sp.DonGia = decimal.Parse(txtDonGia.Text);
                    sp.MaLoai = cboLSP.SelectedValue.ToString();
                    if (!string.IsNullOrEmpty(_hinhAnhPath))
                    {
                        sp.HinhAnh = _hinhAnhPath;
                    }
                    sp.TrangThai = (rbDB.Checked || rboNB.Checked);
                    db.SubmitChanges();
                    MessageBox.Show("Cập nhật sản phẩm thành công!");
                    LoadSanPham();
                    LamMoiForm();
                }
                else
                {
                    MessageBox.Show("Sản phẩm không tồn tại!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                var sp = db.SanPhams.SingleOrDefault(s => s.MaSanPham == txtMSP.Text);
                if (sp != null)
                {
                    db.SanPhams.DeleteOnSubmit(sp);
                    db.SubmitChanges();
                    MessageBox.Show("Xóa sản phẩm thành công!");
                    LoadSanPham();
                    LamMoiForm();
                }
                else
                {
                    MessageBox.Show("Sản phẩm không tồn tại!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnLM_Click(object sender, EventArgs e)
        {
            LamMoiForm();
        }

        private void LamMoiForm()
        {
            txtMSP.Clear();
            txtTSP.Clear();
            txtDonGia.Clear();
            cboLSP.SelectedIndex = -1;
            txtMSP.Enabled = true;
            PB1.Image = null;
            _hinhAnhPath = "";
            rbDB.Checked = false;
            rboNB.Checked = false;
        }

        private void LoadLoaiSanPham()
        {
            var loaiSPList = db.LoaiSanPhams
                .Select(l => new
                {
                    l.MaLoai,
                    l.TenLoai
                })
                .ToList();
            cboLSP.DataSource = loaiSPList;
            cboLSP.DisplayMember = "TenLoai";
            cboLSP.ValueMember = "MaLoai";
        }
    }
}
