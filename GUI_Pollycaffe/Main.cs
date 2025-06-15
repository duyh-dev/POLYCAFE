using System;
using System.Drawing;
using System.Windows.Forms;
using GUI_PolyCafe;

namespace GUI_Pollycaffe
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            // Thiết lập màu nền
            this.BackColor = Color.WhiteSmoke;

            // Tùy chỉnh giao diện menuStrip
            menuStrip1.BackColor = Color.SteelBlue;
            menuStrip1.ForeColor = Color.White;
            menuStrip1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            // Đổi màu khi rê chuột vào menu item
            foreach (ToolStripMenuItem item in menuStrip1.Items)
            {
                item.MouseEnter += (s, args) => item.BackColor = Color.LightSkyBlue;
                item.MouseLeave += (s, args) => item.BackColor = Color.SteelBlue;
            }

            // Thiết lập tiêu đề form
            this.Text = "PolyCafe - Hệ thống quản lý";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        }



        private void QLNV_Click(object sender, EventArgs e)
        {
            frmQLNV nv = new frmQLNV();
            nv.Show();
        }

        private void SP_Click(object sender, EventArgs e)
        {
           frmSanPham sp = new frmSanPham();
            sp.Show();
        }

        private void LSP_Click(object sender, EventArgs e)
        {
            frmLSP lsp = new frmLSP();
            lsp.Show();
        }

        private void PBH_Click(object sender, EventArgs e)
        {
            frmPBH pbh = new frmPBH();
            pbh.Show();
        }

        private void TLD_Click(object sender, EventArgs e)
        {
            frmTheLuuDong tld = new frmTheLuuDong();
            tld.Show();
        }

        private void ThongKe_Click(object sender, EventArgs e)
        {
            frmThongKe tk = new frmThongKe();
            tk.Show();
        }
    }
}
