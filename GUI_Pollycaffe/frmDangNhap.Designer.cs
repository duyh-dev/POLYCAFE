
namespace GUI_Pollycaffe
{
    partial class frmDangNhap
    {
        private System.ComponentModel.IContainer components = null;

        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2TextBox txtTDN;
        private Guna.UI2.WinForms.Guna2TextBox txtMK;
        private Guna.UI2.WinForms.Guna2CheckBox ckbHTMK;
        private Guna.UI2.WinForms.Guna2Button btnDangNhap;
        private Guna.UI2.WinForms.Guna2Button btnThoat;
        private System.Windows.Forms.LinkLabel linkfrmDMK;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txtTDN = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtMK = new Guna.UI2.WinForms.Guna2TextBox();
            this.ckbHTMK = new Guna.UI2.WinForms.Guna2CheckBox();
            this.btnDangNhap = new Guna.UI2.WinForms.Guna2Button();
            this.btnThoat = new Guna.UI2.WinForms.Guna2Button();
            this.linkfrmDMK = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.guna2HtmlLabel1.ForeColor = System.Drawing.Color.Black;
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(90, 30);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(172, 47);
            this.guna2HtmlLabel1.TabIndex = 0;
            this.guna2HtmlLabel1.Text = "Đăng nhập";
            // 
            // txtTDN
            // 
            this.txtTDN.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTDN.DefaultText = "";
            this.txtTDN.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTDN.Location = new System.Drawing.Point(50, 90);
            this.txtTDN.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTDN.Name = "txtTDN";
            this.txtTDN.PlaceholderText = "Tên đăng nhập";
            this.txtTDN.SelectedText = "";
            this.txtTDN.Size = new System.Drawing.Size(250, 36);
            this.txtTDN.TabIndex = 1;
            // 
            // txtMK
            // 
            this.txtMK.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMK.DefaultText = "";
            this.txtMK.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtMK.Location = new System.Drawing.Point(50, 140);
            this.txtMK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMK.Name = "txtMK";
            this.txtMK.PlaceholderText = "Mật khẩu";
            this.txtMK.SelectedText = "";
            this.txtMK.Size = new System.Drawing.Size(250, 36);
            this.txtMK.TabIndex = 2;
            // 
            // ckbHTMK
            // 
            this.ckbHTMK.CheckedState.BorderRadius = 0;
            this.ckbHTMK.CheckedState.BorderThickness = 0;
            this.ckbHTMK.Location = new System.Drawing.Point(50, 190);
            this.ckbHTMK.Name = "ckbHTMK";
            this.ckbHTMK.Size = new System.Drawing.Size(133, 24);
            this.ckbHTMK.TabIndex = 3;
            this.ckbHTMK.Text = "Hiển thị mật khẩu";
            this.ckbHTMK.UncheckedState.BorderRadius = 0;
            this.ckbHTMK.UncheckedState.BorderThickness = 0;
            // 
            // btnDangNhap
            // 
            this.btnDangNhap.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnDangNhap.ForeColor = System.Drawing.Color.White;
            this.btnDangNhap.Location = new System.Drawing.Point(50, 230);
            this.btnDangNhap.Name = "btnDangNhap";
            this.btnDangNhap.Size = new System.Drawing.Size(250, 40);
            this.btnDangNhap.TabIndex = 4;
            this.btnDangNhap.Text = "Đăng nhập";
            this.btnDangNhap.Click += new System.EventHandler(this.BtnDN_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnThoat.ForeColor = System.Drawing.Color.White;
            this.btnThoat.Location = new System.Drawing.Point(50, 280);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(250, 40);
            this.btnThoat.TabIndex = 5;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // linkfrmDMK
            // 
            this.linkfrmDMK.Location = new System.Drawing.Point(200, 191);
            this.linkfrmDMK.Name = "linkfrmDMK";
            this.linkfrmDMK.Size = new System.Drawing.Size(100, 23);
            this.linkfrmDMK.TabIndex = 6;
            this.linkfrmDMK.TabStop = true;
            this.linkfrmDMK.Text = "Đổi mật khẩu";
            // 
            // frmDangNhap
            // 
            this.ClientSize = new System.Drawing.Size(350, 360);
            this.Controls.Add(this.guna2HtmlLabel1);
            this.Controls.Add(this.txtTDN);
            this.Controls.Add(this.txtMK);
            this.Controls.Add(this.ckbHTMK);
            this.Controls.Add(this.btnDangNhap);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.linkfrmDMK);
            this.Name = "frmDangNhap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng nhập";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
