namespace Nesting
{
    partial class SeasonEditor
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
            this.shapeButton = new System.Windows.Forms.Button();
            this.log = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // shapeButton
            // 
            this.shapeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.shapeButton.BackColor = System.Drawing.Color.ForestGreen;
            this.shapeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.shapeButton.Location = new System.Drawing.Point(1168, 629);
            this.shapeButton.Name = "shapeButton";
            this.shapeButton.Size = new System.Drawing.Size(328, 110);
            this.shapeButton.TabIndex = 0;
            this.shapeButton.Text = "None";
            this.shapeButton.UseVisualStyleBackColor = false;
            this.shapeButton.Click += new System.EventHandler(this.shapeButton_Click);
            // 
            // log
            // 
            this.log.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.log.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.log.ForeColor = System.Drawing.Color.White;
            this.log.Location = new System.Drawing.Point(719, 647);
            this.log.Name = "log";
            this.log.Size = new System.Drawing.Size(443, 75);
            this.log.TabIndex = 1;
            this.log.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // SeasonEditor
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1521, 761);
            this.Controls.Add(this.log);
            this.Controls.Add(this.shapeButton);
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size(1537, 800);
            this.Name = "SeasonEditor";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button shapeButton;
        private System.Windows.Forms.Label log;
    }
}

