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
        string fileAnh = "";


        public frmSanPham()
        {
            InitializeComponent();

            dgvSanPham.AutoGenerateColumns = true;

        }

        private void frmSanPham_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadLoai();
        }

        private void LoadData()
        {
            PollyCafeDataContext pl = new PollyCafeDataContext();
            dgvSanPham.DataSource = pl.SanPhams;
        }


        private void LoadLoai()
        {
            cboLSP.DataSource = db.LoaiSanPhams.ToList();
            cboLSP.DisplayMember = "TenLoai";
            cboLSP.ValueMember = "MaLoai";
           
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            SanPham sp = new SanPham();
            sp.MaSanPham = txtMSP.Text;
            sp.TenSanPham = txtTSP.Text;
            sp.DonGia = decimal.Parse(txtDonGia.Text);
            sp.HinhAnh = fileAnh;
            if (!rboNB.Checked)
            {
                sp.TrangThai = false;
            }
            else
            {
                sp.TrangThai = true;
            }
            sp.MaLoai = cboLSP.SelectedValue.ToString();

            db.SanPhams.InsertOnSubmit(sp);
            db.SubmitChanges();
            LoadData();
            MessageBox.Show("Đã thêm sản phẩm.");
        }


        private void btnSua_Click(object sender, EventArgs e)
        {
            var sp = db.SanPhams.SingleOrDefault(s => s.MaSanPham == txtMSP.Text);
            if (sp != null)
            {
                sp.TenSanPham = txtTSP.Text;
                sp.DonGia = decimal.Parse(txtDonGia.Text);
                sp.HinhAnh = fileAnh;
                if (!rboNB.Checked)
                {
                    sp.TrangThai = false;
                }
                else
                {
                    sp.TrangThai = true;
                }
                sp.MaLoai = cboLSP.SelectedValue.ToString();

                db.SubmitChanges();
                LoadData();
                MessageBox.Show("Đã sửa sản phẩm.");
            }
        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
            var sp = db.SanPhams.SingleOrDefault(s => s.MaSanPham == txtMSP.Text);
            if (sp != null)
            {
                db.SanPhams.DeleteOnSubmit(sp);
                db.SubmitChanges();
                LoadData();
                MessageBox.Show("Đã xoá sản phẩm.");
            }
        }


        private void btnLM_Click(object sender, EventArgs e)
        {
            txtMSP.Clear();
            txtTSP.Clear();
            txtDonGia.Clear();
            cboLSP.SelectedIndex = 0;
            rboNB.Checked = false;
            rbDB.Checked = false;
            PB1.Image = null;
            fileAnh = "";
            LoadData();
        }


        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                PB1.Image = Image.FromFile(ofd.FileName);
                fileAnh = ofd.FileName;
            }
        }

        private void PB1_Click(object sender, EventArgs e)
        {

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void rboNB_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbDB_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSanPham.Rows[e.RowIndex];
                txtMSP.Text = row.Cells["MaSP"].Value.ToString();
                txtTSP.Text = row.Cells["TenSP"].Value.ToString();
                txtDonGia.Text = row.Cells["DonGia"].Value.ToString();
                cboLSP.SelectedValue = row.Cells["MaLoai"].Value.ToString();
                string anh = row.Cells["HinhAnh"].Value.ToString();
                fileAnh = anh;
                if (System.IO.File.Exists(anh))
                    PB1.Image = Image.FromFile(anh);
                else
                    PB1.Image = null;

                string trangThai = row.Cells["TrangThai"].Value.ToString();
                if (trangThai == "Đang bán") rbDB.Checked = true;
                else rboNB.Checked = true;
            }
        }

        private void dgvSanPham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
