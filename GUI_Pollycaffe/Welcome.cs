using System;
using System.Windows.Forms;

namespace GUI_Pollycaffe
{
    public partial class Welcome : Form
    {
        private int progressValue = 0;

        public Welcome()
        {
            InitializeComponent();

            // Cài đặt timer tick mỗi 30ms
            timer1.Interval = 30;
            timer1.Tick += timer1_Tick;
            timer1.Start();

            // Gán giá trị ban đầu cho progress bar
            progressBar1.Value = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressValue += 1;
            progressBar1.Value = progressValue;

            // Hiển thị phần trăm tải
            this.Text = "Loading... " + progressValue + "%";

            if (progressValue >= 100)
            {
                timer1.Stop();

                try
                {
                    // Mở form đăng nhập khi tiến trình hoàn tất
                    frmDangNhap loginForm = new frmDangNhap();
                    this.Hide();
                    loginForm.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi mở form đăng nhập: " + ex.Message);
                }
            }
        }

        // Nếu có xử lý khi giá trị progress bar thay đổi, có thể thêm vào đây
        private void progressBar1_ValueChanged(object sender, EventArgs e)
        {
            // Hiện tại chưa cần xử lý gì thêm
        }
    }
}
