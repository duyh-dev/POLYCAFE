using System;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace GUI_Pollycaffe
{
    public partial class frmDangNhap : Form
    {

        PollyCafeDataContext db = new PollyCafeDataContext();

        public frmDangNhap()
        {
            InitializeComponent();

            // Ẩn mật khẩu khi khởi động
            txtMK.UseSystemPasswordChar = true;

            // Gán sự kiện cho checkbox hiển thị mật khẩu
            ckbHTMK.CheckedChanged += (s, e) =>
            {
                txtMK.UseSystemPasswordChar = !ckbHTMK.Checked;
            };

            // Gán sự kiện nút đăng nhập
            btnDangNhap.Click += BtnDN_Click;

            // Gán sự kiện nút thoát
            btnThoat.Click += (s, e) => this.Close();
        }

        private void BtnDN_Click(object sender, EventArgs e)
        {
            string email = txtTDN.Text.Trim();
            string matkhau = txtMK.Text;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(matkhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tìm nhân viên khớp với email và mật khẩu, và có trạng thái đang hoạt động (1)
            var nv = db.NhanViens.FirstOrDefault(n => n.Email == email && n.MatKhau == matkhau && n.TrangThai);

            if (nv != null)
            {
                MessageBox.Show("Đăng nhập thành công!\nXin chào: " + nv.HoTen, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Main m = new Main();
                m.ShowDialog();
                // Mở form chính ở đây nếu có (ví dụ: frmMain main = new frmMain(); main.Show(); this.Hide();)
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu, hoặc tài khoản đã bị khóa.", "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkfrmDMK_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDMK dmk = new frmDMK();
            dmk.ShowDialog();
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult ctrl = MessageBox.Show("Bạn muốn thoát ?","Thông báo",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (ctrl == DialogResult.Yes) 
            {
            this.Close();
            }
        }
    }
}
