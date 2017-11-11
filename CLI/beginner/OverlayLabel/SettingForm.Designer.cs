namespace OverlayLabel
{
    partial class SettingForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.numOffset = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numWindowWidthRatio = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numWindowHeightRatio = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numOffset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWindowWidthRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWindowHeightRatio)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Offset";
            // 
            // numOffset
            // 
            this.numOffset.Location = new System.Drawing.Point(143, 22);
            this.numOffset.Name = "numOffset";
            this.numOffset.Size = new System.Drawing.Size(120, 21);
            this.numOffset.TabIndex = 1;
            this.numOffset.ValueChanged += new System.EventHandler(this.numOffset_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "WindowWidthRatio";
            // 
            // numWindowWidthRatio
            // 
            this.numWindowWidthRatio.DecimalPlaces = 1;
            this.numWindowWidthRatio.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numWindowWidthRatio.Location = new System.Drawing.Point(143, 61);
            this.numWindowWidthRatio.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numWindowWidthRatio.Name = "numWindowWidthRatio";
            this.numWindowWidthRatio.Size = new System.Drawing.Size(120, 21);
            this.numWindowWidthRatio.TabIndex = 3;
            this.numWindowWidthRatio.ValueChanged += new System.EventHandler(this.numWindowWidthRatio_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "WindowHeightRatio";
            // 
            // numWindowHeightRatio
            // 
            this.numWindowHeightRatio.DecimalPlaces = 1;
            this.numWindowHeightRatio.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numWindowHeightRatio.Location = new System.Drawing.Point(143, 102);
            this.numWindowHeightRatio.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numWindowHeightRatio.Name = "numWindowHeightRatio";
            this.numWindowHeightRatio.Size = new System.Drawing.Size(120, 21);
            this.numWindowHeightRatio.TabIndex = 5;
            this.numWindowHeightRatio.ValueChanged += new System.EventHandler(this.numWindowHeightRatio_ValueChanged);
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button1.Location = new System.Drawing.Point(62, 141);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(168, 141);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 174);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.numWindowHeightRatio);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numWindowWidthRatio);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numOffset);
            this.Controls.Add(this.label1);
            this.Name = "SettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SettingForm";
            ((System.ComponentModel.ISupportInitialize)(this.numOffset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWindowWidthRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWindowHeightRatio)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numOffset;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numWindowWidthRatio;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numWindowHeightRatio;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}