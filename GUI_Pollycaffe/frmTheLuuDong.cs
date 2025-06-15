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
    public partial class frmTheLuuDong : Form
    {
        PollyCafeDataContext pl = new PollyCafeDataContext();
        public frmTheLuuDong()
        {
            InitializeComponent();
            NapThe();
        }

        private void NapThe()
        {
            dgvTLD.DataSource = pl.TheLuuDongs
             .Select(t => new
              {
                  t.MaThe,
                  t.ChuSoHuu,
                  TrangThai = t.TrangThai == true ? "Đang hoạt động" : "Ngừng hoạt động"
              })
        .ToList();
            dgvTLD.Columns["MaThe"].HeaderText = "Mã thẻ";
            dgvTLD.Columns["ChuSoHuu"].HeaderText = "Chủ sở hữu";
            dgvTLD.Columns["TrangThai"].HeaderText = "Trạng thái";
            dgvTLD.ColumnHeadersHeight = 25;

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaThe.Text) || string.IsNullOrWhiteSpace(txtCSH.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            var tonTai = pl.TheLuuDongs.Any(t => t.MaThe == txtMaThe.Text);
            if (tonTai)
            {
                MessageBox.Show("Mã thẻ đã tồn tại!");
                return;
            }

            TheLuuDong tld = new TheLuuDong
            {
                MaThe = txtMaThe.Text,
                ChuSoHuu = txtCSH.Text,
                TrangThai = ckbTT.Checked
            };

            pl.TheLuuDongs.InsertOnSubmit(tld);
            pl.SubmitChanges();
            MessageBox.Show("✅ Thêm thẻ lưu động thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            NapThe();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            var tld = pl.TheLuuDongs.FirstOrDefault(t => t.MaThe == txtMaThe.Text);
            if (tld == null)
            {
                MessageBox.Show("Không tìm thấy mã thẻ cần sửa.");
                return;
            }

            tld.ChuSoHuu = txtCSH.Text;
            tld.TrangThai = ckbTT.Checked;

            pl.SubmitChanges();
            MessageBox.Show("✏️ Cập nhật thẻ lưu động thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            NapThe();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            var tld = pl.TheLuuDongs.FirstOrDefault(t => t.MaThe == txtMaThe.Text);
            if (tld == null)
            {
                MessageBox.Show("Không tìm thấy mã thẻ để xoá.");
                return;
            }

            pl.TheLuuDongs.DeleteOnSubmit(tld);
            pl.SubmitChanges();
            MessageBox.Show("🗑️ Xóa thẻ lưu động thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txtMaThe.Clear();
            txtCSH.Clear();
            ckbTT.Checked = false;
            txtMaThe.Enabled = true;
            txtMaThe.Focus();
        }

        private void btnLM_Click(object sender, EventArgs e)
        {
            txtMaThe.Clear();
            txtCSH.Clear();
            ckbTT.Checked = false;
            txtMaThe.Enabled = true;
            txtMaThe.Focus();
            NapThe();
        }


        private void dgvTLD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvTLD.Rows[e.RowIndex];
                txtMaThe.Text = row.Cells["MaThe"].Value.ToString();
                txtCSH.Text = row.Cells["ChuSoHuu"].Value.ToString();
                string tt = row.Cells["TrangThai"].Value.ToString();
                ckbTT.Checked = tt == "Đang hoạt động";

                txtMaThe.Enabled = false;
            }
        }

    }
}
