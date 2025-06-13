namespace GUI_Pollycaffe
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.QLNV = new System.Windows.Forms.ToolStripMenuItem();
            this.PBH = new System.Windows.Forms.ToolStripMenuItem();
            this.SP = new System.Windows.Forms.ToolStripMenuItem();
            this.TLD = new System.Windows.Forms.ToolStripMenuItem();
            this.LSP = new System.Windows.Forms.ToolStripMenuItem();
            this.ThongKe = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.QLNV,
            this.PBH,
            this.SP,
            this.TLD,
            this.LSP,
            this.ThongKe});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // QLNV
            // 
            this.QLNV.Name = "QLNV";
            this.QLNV.Size = new System.Drawing.Size(140, 24);
            this.QLNV.Text = "Quản lý nhân viên";
            this.QLNV.Click += new System.EventHandler(this.QLNV_Click);
            // 
            // PBH
            // 
            this.PBH.Name = "PBH";
            this.PBH.Size = new System.Drawing.Size(125, 24);
            this.PBH.Text = "Phiếu bán hàng";
            this.PBH.Click += new System.EventHandler(this.PBH_Click);
            // 
            // SP
            // 
            this.SP.Name = "SP";
            this.SP.Size = new System.Drawing.Size(89, 24);
            this.SP.Text = "Sản phẩm";
            this.SP.Click += new System.EventHandler(this.SP_Click);
            // 
            // TLD
            // 
            this.TLD.Name = "TLD";
            this.TLD.Size = new System.Drawing.Size(111, 24);
            this.TLD.Text = "Thẻ lưu động";
            this.TLD.Click += new System.EventHandler(this.TLD_Click);
            // 
            // LSP
            // 
            this.LSP.Name = "LSP";
            this.LSP.Size = new System.Drawing.Size(119, 24);
            this.LSP.Text = "Loại sản phẩm";
            this.LSP.Click += new System.EventHandler(this.LSP_Click);
            // 
            // ThongKe
            // 
            this.ThongKe.Name = "ThongKe";
            this.ThongKe.Size = new System.Drawing.Size(84, 24);
            this.ThongKe.Text = "Thống kê";
            this.ThongKe.Click += new System.EventHandler(this.ThongKe_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Main_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem QLNV;
        private System.Windows.Forms.ToolStripMenuItem PBH;
        private System.Windows.Forms.ToolStripMenuItem SP;
        private System.Windows.Forms.ToolStripMenuItem TLD;
        private System.Windows.Forms.ToolStripMenuItem LSP;
        private System.Windows.Forms.ToolStripMenuItem ThongKe;
    }
}