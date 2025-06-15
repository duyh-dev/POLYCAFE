using System;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace GUI_Pollycaffe
{
    public partial class Welcome : Form
    {
        private int progressValue = 0;

        public Welcome()
        {
            InitializeComponent();

            // Đặt opacity về đầy đủ
            this.Opacity = 1.0;

            progressBar1.Maximum = 100;
            progressBar1.Value = 0;

            timer1.Interval = 30;
            timer1.Tick += timer1_Tick;
            timer1.Start();
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            progressValue++;

            progressBar1.Value = progressValue;
            this.Text = $"Loading... {progressValue}%";

            if (progressValue >= 100)
            {
                timer1.Stop();
                fadeOutTimer.Start();


            }
        }
        private void fadeOutTimer_Tick(object sender, EventArgs e)
        {
            if (this.Opacity > 0)
            {
                this.Opacity -= 0.05; // giảm từ 1.0 về 0.0
            }
            else
            {
                fadeOutTimer.Stop();

                this.Hide();
                new frmDangNhap().Show();
            }
        }

    }
}
