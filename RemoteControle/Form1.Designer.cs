namespace RemoteControle
{
    partial class Console
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Console));
            this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            this.SessionUserLbl = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.PostazioneLbl = new System.Windows.Forms.Label();
            this.timeSpanLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // axWindowsMediaPlayer1
            // 
            this.axWindowsMediaPlayer1.Enabled = true;
            this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(4, 1);
            this.axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            this.axWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer1.OcxState")));
            this.axWindowsMediaPlayer1.Size = new System.Drawing.Size(921, 572);
            this.axWindowsMediaPlayer1.TabIndex = 0;
            // 
            // SessionUserLbl
            // 
            this.SessionUserLbl.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.SessionUserLbl.ForeColor = System.Drawing.Color.Red;
            this.SessionUserLbl.Location = new System.Drawing.Point(526, 593);
            this.SessionUserLbl.Name = "SessionUserLbl";
            this.SessionUserLbl.Size = new System.Drawing.Size(399, 23);
            this.SessionUserLbl.TabIndex = 2;
            this.SessionUserLbl.Text = "SESSION USER: ";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(4, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(921, 527);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // PostazioneLbl
            // 
            this.PostazioneLbl.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.PostazioneLbl.AutoSize = true;
            this.PostazioneLbl.ForeColor = System.Drawing.Color.Red;
            this.PostazioneLbl.Location = new System.Drawing.Point(1, 593);
            this.PostazioneLbl.Name = "PostazioneLbl";
            this.PostazioneLbl.Size = new System.Drawing.Size(255, 17);
            this.PostazioneLbl.TabIndex = 4;
            this.PostazioneLbl.Text = "POSTAZIONE: POSTUMIA CROCETTA";
            // 
            // timeSpanLabel
            // 
            this.timeSpanLabel.AutoSize = true;
            this.timeSpanLabel.Location = new System.Drawing.Point(862, 593);
            this.timeSpanLabel.Name = "timeSpanLabel";
            this.timeSpanLabel.Size = new System.Drawing.Size(12, 17);
            this.timeSpanLabel.TabIndex = 5;
            this.timeSpanLabel.Text = ":";
            // 
            // Console
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(937, 636);
            this.Controls.Add(this.timeSpanLabel);
            this.Controls.Add(this.PostazioneLbl);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.SessionUserLbl);
            this.Controls.Add(this.axWindowsMediaPlayer1);
            this.Name = "Console";
            this.Text = "PostazioneUno";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
        private System.Windows.Forms.Label SessionUserLbl;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label PostazioneLbl;
        private System.Windows.Forms.Label timeSpanLabel;
    }
}

