namespace AutoExchange
{
    partial class AutoExchange
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoExchange));
            this.btnRun = new System.Windows.Forms.Button();
            this.txbResult = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbAPIKey = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txbSecretKey = new System.Windows.Forms.TextBox();
            this.txbAPIKey = new System.Windows.Forms.TextBox();
            this.tpUseSetting = new System.Windows.Forms.TabPage();
            this.chbAutoNeg = new System.Windows.Forms.CheckBox();
            this.chbUseCurrBTC = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txbFeeTax = new System.Windows.Forms.TextBox();
            this.txbInBTC = new System.Windows.Forms.TextBox();
            this.txbConGanAbov = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbController = new System.Windows.Forms.TabControl();
            this.chbAutoRun = new System.Windows.Forms.CheckBox();
            this.tbAPIKey.SuspendLayout();
            this.tpUseSetting.SuspendLayout();
            this.tbController.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(713, 34);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(75, 97);
            this.btnRun.TabIndex = 0;
            this.btnRun.Text = "Run!!";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // txbResult
            // 
            this.txbResult.Location = new System.Drawing.Point(12, 169);
            this.txbResult.Multiline = true;
            this.txbResult.Name = "txbResult";
            this.txbResult.ReadOnly = true;
            this.txbResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txbResult.Size = new System.Drawing.Size(776, 269);
            this.txbResult.TabIndex = 99999;
            this.txbResult.TextChanged += new System.EventHandler(this.TxbResult_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 146);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Results";
            // 
            // tbAPIKey
            // 
            this.tbAPIKey.Controls.Add(this.label7);
            this.tbAPIKey.Controls.Add(this.label8);
            this.tbAPIKey.Controls.Add(this.txbSecretKey);
            this.tbAPIKey.Controls.Add(this.txbAPIKey);
            this.tbAPIKey.Location = new System.Drawing.Point(4, 22);
            this.tbAPIKey.Name = "tbAPIKey";
            this.tbAPIKey.Padding = new System.Windows.Forms.Padding(3);
            this.tbAPIKey.Size = new System.Drawing.Size(687, 97);
            this.tbAPIKey.TabIndex = 2;
            this.tbAPIKey.Text = "API KEY";
            this.tbAPIKey.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 42);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Secret Key";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "API Key";
            // 
            // txbSecretKey
            // 
            this.txbSecretKey.Location = new System.Drawing.Point(3, 58);
            this.txbSecretKey.Name = "txbSecretKey";
            this.txbSecretKey.Size = new System.Drawing.Size(425, 20);
            this.txbSecretKey.TabIndex = 18;
            // 
            // txbAPIKey
            // 
            this.txbAPIKey.Location = new System.Drawing.Point(3, 19);
            this.txbAPIKey.Name = "txbAPIKey";
            this.txbAPIKey.Size = new System.Drawing.Size(425, 20);
            this.txbAPIKey.TabIndex = 17;
            // 
            // tpUseSetting
            // 
            this.tpUseSetting.Controls.Add(this.chbAutoNeg);
            this.tpUseSetting.Controls.Add(this.chbUseCurrBTC);
            this.tpUseSetting.Controls.Add(this.label4);
            this.tpUseSetting.Controls.Add(this.label6);
            this.tpUseSetting.Controls.Add(this.txbFeeTax);
            this.tpUseSetting.Controls.Add(this.txbInBTC);
            this.tpUseSetting.Controls.Add(this.txbConGanAbov);
            this.tpUseSetting.Controls.Add(this.label5);
            this.tpUseSetting.Location = new System.Drawing.Point(4, 22);
            this.tpUseSetting.Name = "tpUseSetting";
            this.tpUseSetting.Padding = new System.Windows.Forms.Padding(3);
            this.tpUseSetting.Size = new System.Drawing.Size(687, 97);
            this.tpUseSetting.TabIndex = 0;
            this.tpUseSetting.Text = "Usual Setting";
            this.tpUseSetting.UseVisualStyleBackColor = true;
            // 
            // chbAutoNeg
            // 
            this.chbAutoNeg.AutoSize = true;
            this.chbAutoNeg.Location = new System.Drawing.Point(142, 61);
            this.chbAutoNeg.Name = "chbAutoNeg";
            this.chbAutoNeg.Size = new System.Drawing.Size(138, 17);
            this.chbAutoNeg.TabIndex = 14;
            this.chbAutoNeg.Text = "Automatic Negociations";
            this.chbAutoNeg.UseVisualStyleBackColor = true;
            // 
            // chbUseCurrBTC
            // 
            this.chbUseCurrBTC.AutoSize = true;
            this.chbUseCurrBTC.Location = new System.Drawing.Point(178, 21);
            this.chbUseCurrBTC.Name = "chbUseCurrBTC";
            this.chbUseCurrBTC.Size = new System.Drawing.Size(177, 17);
            this.chbUseCurrBTC.TabIndex = 13;
            this.chbUseCurrBTC.Text = "Use BTC Funds Acount (Online)";
            this.chbUseCurrBTC.UseVisualStyleBackColor = true;
            this.chbUseCurrBTC.CheckedChanged += new System.EventHandler(this.ChbUseCurrBTC_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Fee Tax%";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(66, 3);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Initial Amount of BTC";
            // 
            // txbFeeTax
            // 
            this.txbFeeTax.Location = new System.Drawing.Point(6, 19);
            this.txbFeeTax.Name = "txbFeeTax";
            this.txbFeeTax.Size = new System.Drawing.Size(54, 20);
            this.txbFeeTax.TabIndex = 3;
            this.txbFeeTax.Text = "0.1";
            // 
            // txbInBTC
            // 
            this.txbInBTC.Location = new System.Drawing.Point(66, 19);
            this.txbInBTC.Name = "txbInBTC";
            this.txbInBTC.Size = new System.Drawing.Size(106, 20);
            this.txbInBTC.TabIndex = 5;
            this.txbInBTC.Text = "0.00244286";
            // 
            // txbConGanAbov
            // 
            this.txbConGanAbov.Location = new System.Drawing.Point(6, 58);
            this.txbConGanAbov.Name = "txbConGanAbov";
            this.txbConGanAbov.Size = new System.Drawing.Size(130, 20);
            this.txbConGanAbov.TabIndex = 4;
            this.txbConGanAbov.Text = "1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(130, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Consider Gain Above of %";
            // 
            // tbController
            // 
            this.tbController.Controls.Add(this.tpUseSetting);
            this.tbController.Controls.Add(this.tbAPIKey);
            this.tbController.Location = new System.Drawing.Point(12, 12);
            this.tbController.Name = "tbController";
            this.tbController.SelectedIndex = 0;
            this.tbController.Size = new System.Drawing.Size(695, 123);
            this.tbController.TabIndex = 100000;
            // 
            // chbAutoRun
            // 
            this.chbAutoRun.AutoSize = true;
            this.chbAutoRun.Location = new System.Drawing.Point(692, 11);
            this.chbAutoRun.Name = "chbAutoRun";
            this.chbAutoRun.Size = new System.Drawing.Size(96, 17);
            this.chbAutoRun.TabIndex = 15;
            this.chbAutoRun.Text = "Automatic Run";
            this.chbAutoRun.UseVisualStyleBackColor = true;
            // 
            // AutoExchange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.chbAutoRun);
            this.Controls.Add(this.tbController);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txbResult);
            this.Controls.Add(this.btnRun);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AutoExchange";
            this.Text = "AutoExchange";
            this.tbAPIKey.ResumeLayout(false);
            this.tbAPIKey.PerformLayout();
            this.tpUseSetting.ResumeLayout(false);
            this.tpUseSetting.PerformLayout();
            this.tbController.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.TextBox txbResult;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage tbAPIKey;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txbSecretKey;
        private System.Windows.Forms.TextBox txbAPIKey;
        private System.Windows.Forms.TabPage tpUseSetting;
        private System.Windows.Forms.CheckBox chbUseCurrBTC;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txbFeeTax;
        private System.Windows.Forms.TextBox txbInBTC;
        private System.Windows.Forms.TextBox txbConGanAbov;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabControl tbController;
        private System.Windows.Forms.CheckBox chbAutoNeg;
        private System.Windows.Forms.CheckBox chbAutoRun;
    }
}

