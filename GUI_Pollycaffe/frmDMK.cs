using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace GUI_Pollycaffe
{
    public partial class frmDMK : Form
    {
        PollyCafeDataContext db = new PollyCafeDataContext();

        public frmDMK()
        {
            InitializeComponent();

            // Gán mặc định mật khẩu bị ẩn khi mở form
            txtMKC.UseSystemPasswordChar = true;
            txtMKM.UseSystemPasswordChar = true;
            txtXNMKM.UseSystemPasswordChar = true;

            // Gán sự kiện hiển thị mật khẩu
            ckbHienthi1.CheckedChanged += (s, e) =>
            {
                txtMKC.UseSystemPasswordChar = !ckbHienthi1.Checked;
            };

            ckbHienthi2.CheckedChanged += (s, e) =>
            {
                txtMKM.UseSystemPasswordChar = !ckbHienthi2.Checked;
            };

            ckbHienthi3.CheckedChanged += (s, e) =>
            {
                txtXNMKM.UseSystemPasswordChar = !ckbHienthi3.Checked;
            };

            btnDoiMK.Click += btnDoiMK_Click;
        }

        private void btnDoiMK_Click(object sender, EventArgs e)
        {
            string maNV = txtMNV.Text.Trim();
            string hoTen = txtTNV.Text.Trim();
            string matKhauCu = txtMKC.Text.Trim();
            string matKhauMoi = txtMKM.Text.Trim();
            string xacNhanMK = txtXNMKM.Text.Trim();

            // Kiểm tra các trường không để trống
            if (string.IsNullOrWhiteSpace(maNV) || string.IsNullOrWhiteSpace(hoTen)
                || string.IsNullOrWhiteSpace(matKhauCu) || string.IsNullOrWhiteSpace(matKhauMoi)
                || string.IsNullOrWhiteSpace(xacNhanMK))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tìm nhân viên trong DB
            var nv = db.NhanViens.FirstOrDefault(n =>
                n.MaNhanVien == maNV &&
                n.HoTen == hoTen &&
                n.MatKhau == matKhauCu &&
                n.TrangThai);

            if (nv == null)
            {
                MessageBox.Show("Thông tin không chính xác hoặc mật khẩu cũ sai!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kiểm tra xác nhận mật khẩu
            if (matKhauMoi != xacNhanMK)
            {
                MessageBox.Show("Xác nhận mật khẩu không khớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (matKhauMoi.Length < 6)
            {
                MessageBox.Show("Mật khẩu mới phải có ít nhất 6 ký tự!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Đổi mật khẩu
            nv.MatKhau = matKhauMoi;
            db.SubmitChanges();

            MessageBox.Show("Đổi mật khẩu thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
