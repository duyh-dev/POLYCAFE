using System;
using System.Windows.Forms;

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
            frmTK tk = new frmTK();
            tk.Show();
        }
    }
}
