namespace LevelsAndAxesGridWithDimensions
{
    partial class SetParametersForm
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
            this.VerticalAxesGB = new System.Windows.Forms.GroupBox();
            this.verticalAxesDistanceTB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.verticalAxesNumberTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.HorizontalAxesGB = new System.Windows.Forms.GroupBox();
            this.horizontalAxesDistanceTB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.horizontalAxesNumberTB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.LevelsGB = new System.Windows.Forms.GroupBox();
            this.levelsDistanceTB = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.levelsNumberTB = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.unitsTB = new System.Windows.Forms.TextBox();
            this.VerticalAxesGB.SuspendLayout();
            this.HorizontalAxesGB.SuspendLayout();
            this.LevelsGB.SuspendLayout();
            this.SuspendLayout();
            // 
            // VerticalAxesGB
            // 
            this.VerticalAxesGB.Controls.Add(this.verticalAxesDistanceTB);
            this.VerticalAxesGB.Controls.Add(this.label2);
            this.VerticalAxesGB.Controls.Add(this.verticalAxesNumberTB);
            this.VerticalAxesGB.Controls.Add(this.label1);
            this.VerticalAxesGB.Location = new System.Drawing.Point(12, 28);
            this.VerticalAxesGB.Name = "VerticalAxesGB";
            this.VerticalAxesGB.Size = new System.Drawing.Size(355, 51);
            this.VerticalAxesGB.TabIndex = 0;
            this.VerticalAxesGB.TabStop = false;
            this.VerticalAxesGB.Text = "Vertical axes";
            // 
            // verticalAxesDistanceTB
            // 
            this.verticalAxesDistanceTB.Location = new System.Drawing.Point(242, 17);
            this.verticalAxesDistanceTB.Name = "verticalAxesDistanceTB";
            this.verticalAxesDistanceTB.Size = new System.Drawing.Size(100, 20);
            this.verticalAxesDistanceTB.TabIndex = 3;
            this.verticalAxesDistanceTB.Text = "0";
            this.verticalAxesDistanceTB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.number_KeyPress);
            this.verticalAxesDistanceTB.Validated += new System.EventHandler(this.fields_Validated);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(192, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Distance";
            // 
            // verticalAxesNumberTB
            // 
            this.verticalAxesNumberTB.Location = new System.Drawing.Point(57, 17);
            this.verticalAxesNumberTB.Name = "verticalAxesNumberTB";
            this.verticalAxesNumberTB.Size = new System.Drawing.Size(100, 20);
            this.verticalAxesNumberTB.TabIndex = 1;
            this.verticalAxesNumberTB.Text = "0";
            this.verticalAxesNumberTB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.number_KeyPress);
            this.verticalAxesNumberTB.Validated += new System.EventHandler(this.fields_Validated);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Number";
            // 
            // HorizontalAxesGB
            // 
            this.HorizontalAxesGB.Controls.Add(this.horizontalAxesDistanceTB);
            this.HorizontalAxesGB.Controls.Add(this.label3);
            this.HorizontalAxesGB.Controls.Add(this.horizontalAxesNumberTB);
            this.HorizontalAxesGB.Controls.Add(this.label4);
            this.HorizontalAxesGB.Location = new System.Drawing.Point(12, 85);
            this.HorizontalAxesGB.Name = "HorizontalAxesGB";
            this.HorizontalAxesGB.Size = new System.Drawing.Size(355, 51);
            this.HorizontalAxesGB.TabIndex = 1;
            this.HorizontalAxesGB.TabStop = false;
            this.HorizontalAxesGB.Text = "Horizontal axes";
            // 
            // horizontalAxesDistanceTB
            // 
            this.horizontalAxesDistanceTB.Location = new System.Drawing.Point(242, 17);
            this.horizontalAxesDistanceTB.Name = "horizontalAxesDistanceTB";
            this.horizontalAxesDistanceTB.Size = new System.Drawing.Size(100, 20);
            this.horizontalAxesDistanceTB.TabIndex = 3;
            this.horizontalAxesDistanceTB.Text = "0";
            this.horizontalAxesDistanceTB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.number_KeyPress);
            this.horizontalAxesDistanceTB.Validated += new System.EventHandler(this.fields_Validated);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(192, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Distance";
            // 
            // horizontalAxesNumberTB
            // 
            this.horizontalAxesNumberTB.Location = new System.Drawing.Point(57, 17);
            this.horizontalAxesNumberTB.Name = "horizontalAxesNumberTB";
            this.horizontalAxesNumberTB.Size = new System.Drawing.Size(100, 20);
            this.horizontalAxesNumberTB.TabIndex = 1;
            this.horizontalAxesNumberTB.Text = "0";
            this.horizontalAxesNumberTB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.number_KeyPress);
            this.horizontalAxesNumberTB.Validated += new System.EventHandler(this.fields_Validated);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Number";
            // 
            // LevelsGB
            // 
            this.LevelsGB.Controls.Add(this.levelsDistanceTB);
            this.LevelsGB.Controls.Add(this.label5);
            this.LevelsGB.Controls.Add(this.levelsNumberTB);
            this.LevelsGB.Controls.Add(this.label6);
            this.LevelsGB.Location = new System.Drawing.Point(12, 142);
            this.LevelsGB.Name = "LevelsGB";
            this.LevelsGB.Size = new System.Drawing.Size(355, 51);
            this.LevelsGB.TabIndex = 2;
            this.LevelsGB.TabStop = false;
            this.LevelsGB.Text = "Levels";
            // 
            // levelsDistanceTB
            // 
            this.levelsDistanceTB.Location = new System.Drawing.Point(242, 17);
            this.levelsDistanceTB.Name = "levelsDistanceTB";
            this.levelsDistanceTB.Size = new System.Drawing.Size(100, 20);
            this.levelsDistanceTB.TabIndex = 3;
            this.levelsDistanceTB.Text = "0";
            this.levelsDistanceTB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.number_KeyPress);
            this.levelsDistanceTB.Validated += new System.EventHandler(this.fields_Validated);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(192, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Distance";
            // 
            // levelsNumberTB
            // 
            this.levelsNumberTB.Location = new System.Drawing.Point(57, 17);
            this.levelsNumberTB.Name = "levelsNumberTB";
            this.levelsNumberTB.Size = new System.Drawing.Size(100, 20);
            this.levelsNumberTB.TabIndex = 1;
            this.levelsNumberTB.Text = "0";
            this.levelsNumberTB.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.number_KeyPress);
            this.levelsNumberTB.Validated += new System.EventHandler(this.fields_Validated);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Number";
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(211, 199);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 3;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.CausesValidation = false;
            this.cancelButton.Location = new System.Drawing.Point(292, 199);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(207, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(28, 13);
            this.textBox1.TabIndex = 4;
            this.textBox1.Text = "Units\r\n:";
            // 
            // unitsTB
            // 
            this.unitsTB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.unitsTB.Location = new System.Drawing.Point(241, 12);
            this.unitsTB.Name = "unitsTB";
            this.unitsTB.ReadOnly = true;
            this.unitsTB.Size = new System.Drawing.Size(125, 13);
            this.unitsTB.TabIndex = 6;
            // 
            // SetParametersForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(379, 233);
            this.Controls.Add(this.unitsTB);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.LevelsGB);
            this.Controls.Add(this.HorizontalAxesGB);
            this.Controls.Add(this.VerticalAxesGB);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SetParametersForm";
            this.Text = "Set parameters";
            this.VerticalAxesGB.ResumeLayout(false);
            this.VerticalAxesGB.PerformLayout();
            this.HorizontalAxesGB.ResumeLayout(false);
            this.HorizontalAxesGB.PerformLayout();
            this.LevelsGB.ResumeLayout(false);
            this.LevelsGB.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox VerticalAxesGB;
        private System.Windows.Forms.TextBox verticalAxesDistanceTB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox verticalAxesNumberTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox HorizontalAxesGB;
        private System.Windows.Forms.TextBox horizontalAxesDistanceTB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox horizontalAxesNumberTB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox LevelsGB;
        private System.Windows.Forms.TextBox levelsDistanceTB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox levelsNumberTB;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox unitsTB;
    }
}