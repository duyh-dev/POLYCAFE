using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
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

        private bool _isLoggingIn = false;

        private async void BtnDN_Click(object sender, EventArgs e)
        {
            if (_isLoggingIn) return;
            _isLoggingIn = true;

            try
            {
                string email = txtTDN.Text.Trim();
                string matkhau = txtMK.Text;

                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(matkhau))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var nhanvien = db.NhanViens.FirstOrDefault(n => n.Email == email);

                if (nhanvien == null)
                {
                    MessageBox.Show("Tài khoản không tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (nhanvien.MatKhau != matkhau)
                {
                    MessageBox.Show("Sai mật khẩu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Nếu đăng nhập thành công
                MessageBox.Show("Đăng nhập thành công!\nXin chào: " + nhanvien.HoTen, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                await Task.Delay(1000);

                this.Hide();

                Main frm = new Main();
                frm.FormClosed += (s, args) => Application.Exit();
                frm.Show();
            }
            finally
            {
                _isLoggingIn = false;
            }
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
