namespace AppExamen1
{
    partial class Form1
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
            this.txtLineaComandos = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtLineaComandos
            // 
            this.txtLineaComandos.BackColor = System.Drawing.Color.Black;
            this.txtLineaComandos.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLineaComandos.ForeColor = System.Drawing.Color.Lime;
            this.txtLineaComandos.Location = new System.Drawing.Point(13, 13);
            this.txtLineaComandos.Multiline = true;
            this.txtLineaComandos.Name = "txtLineaComandos";
            this.txtLineaComandos.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLineaComandos.Size = new System.Drawing.Size(359, 336);
            this.txtLineaComandos.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 361);
            this.Controls.Add(this.txtLineaComandos);
            this.Name = "Form1";
            this.Text = "SuperDB";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtLineaComandos;
    }
}

