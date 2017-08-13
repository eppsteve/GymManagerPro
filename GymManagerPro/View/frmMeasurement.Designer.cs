namespace GymManagerPro.View
{
    partial class frmMeasurement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMeasurement));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpDateCreated = new System.Windows.Forms.DateTimePicker();
            this.txtHeight = new DevComponents.Editors.DoubleInput();
            this.txtWeight = new DevComponents.Editors.DoubleInput();
            this.txtBodyfat = new DevComponents.Editors.DoubleInput();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtChest = new DevComponents.Editors.DoubleInput();
            this.txtLArm = new DevComponents.Editors.DoubleInput();
            this.txtRArm = new DevComponents.Editors.DoubleInput();
            this.txtWaist = new DevComponents.Editors.DoubleInput();
            this.txtAbdomen = new DevComponents.Editors.DoubleInput();
            this.txtHips = new DevComponents.Editors.DoubleInput();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtLThigh = new DevComponents.Editors.DoubleInput();
            this.txtRThigh = new DevComponents.Editors.DoubleInput();
            this.txtLCalf = new DevComponents.Editors.DoubleInput();
            this.txtRCalf = new DevComponents.Editors.DoubleInput();
            this.label15 = new System.Windows.Forms.Label();
            this.lblBMI = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.txtHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBodyfat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLArm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRArm)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWaist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAbdomen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHips)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLThigh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRThigh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLCalf)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRCalf)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(128, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Height (m)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(185, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Weight";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(368, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Bodyfat (%)";
            // 
            // dtpDateCreated
            // 
            this.dtpDateCreated.Location = new System.Drawing.Point(162, 5);
            this.dtpDateCreated.Name = "dtpDateCreated";
            this.dtpDateCreated.Size = new System.Drawing.Size(213, 20);
            this.dtpDateCreated.TabIndex = 1;
            // 
            // txtHeight
            // 
            // 
            // 
            // 
            this.txtHeight.BackgroundStyle.Class = "DateTimeInputBackground";
            this.txtHeight.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtHeight.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.txtHeight.Increment = 1D;
            this.txtHeight.Location = new System.Drawing.Point(78, 41);
            this.txtHeight.MaxValue = 999.99D;
            this.txtHeight.MinValue = 0D;
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.ShowUpDown = true;
            this.txtHeight.Size = new System.Drawing.Size(80, 20);
            this.txtHeight.TabIndex = 2;
            this.txtHeight.ValueChanged += new System.EventHandler(this.txtHeight_ValueChanged);
            // 
            // txtWeight
            // 
            // 
            // 
            // 
            this.txtWeight.BackgroundStyle.Class = "DateTimeInputBackground";
            this.txtWeight.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtWeight.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.txtWeight.Increment = 1D;
            this.txtWeight.Location = new System.Drawing.Point(260, 41);
            this.txtWeight.MaxValue = 999.99D;
            this.txtWeight.MinValue = 0D;
            this.txtWeight.Name = "txtWeight";
            this.txtWeight.ShowUpDown = true;
            this.txtWeight.Size = new System.Drawing.Size(80, 20);
            this.txtWeight.TabIndex = 3;
            this.txtWeight.ValueChanged += new System.EventHandler(this.txtWeight_ValueChanged);
            // 
            // txtBodyfat
            // 
            // 
            // 
            // 
            this.txtBodyfat.BackgroundStyle.Class = "DateTimeInputBackground";
            this.txtBodyfat.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtBodyfat.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.txtBodyfat.Increment = 1D;
            this.txtBodyfat.Location = new System.Drawing.Point(439, 41);
            this.txtBodyfat.MaxValue = 999.99D;
            this.txtBodyfat.MinValue = 0D;
            this.txtBodyfat.Name = "txtBodyfat";
            this.txtBodyfat.ShowUpDown = true;
            this.txtBodyfat.Size = new System.Drawing.Size(80, 20);
            this.txtBodyfat.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 90);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Chest";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 120);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Left Arm";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 150);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Right Arm";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 180);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Waist";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(185, 90);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "Abdomen";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(185, 116);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(28, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = "Hips";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(185, 147);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(55, 13);
            this.label11.TabIndex = 15;
            this.label11.Text = "Left Thigh";
            // 
            // txtChest
            // 
            // 
            // 
            // 
            this.txtChest.BackgroundStyle.Class = "DateTimeInputBackground";
            this.txtChest.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtChest.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.txtChest.Increment = 1D;
            this.txtChest.Location = new System.Drawing.Point(78, 86);
            this.txtChest.MaxValue = 999.99D;
            this.txtChest.MinValue = 0D;
            this.txtChest.Name = "txtChest";
            this.txtChest.ShowUpDown = true;
            this.txtChest.Size = new System.Drawing.Size(80, 20);
            this.txtChest.TabIndex = 5;
            // 
            // txtLArm
            // 
            // 
            // 
            // 
            this.txtLArm.BackgroundStyle.Class = "DateTimeInputBackground";
            this.txtLArm.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtLArm.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.txtLArm.Increment = 1D;
            this.txtLArm.Location = new System.Drawing.Point(78, 116);
            this.txtLArm.MaxValue = 999.99D;
            this.txtLArm.MinValue = 0D;
            this.txtLArm.Name = "txtLArm";
            this.txtLArm.ShowUpDown = true;
            this.txtLArm.Size = new System.Drawing.Size(80, 20);
            this.txtLArm.TabIndex = 6;
            // 
            // txtRArm
            // 
            // 
            // 
            // 
            this.txtRArm.BackgroundStyle.Class = "DateTimeInputBackground";
            this.txtRArm.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtRArm.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.txtRArm.Increment = 1D;
            this.txtRArm.Location = new System.Drawing.Point(78, 146);
            this.txtRArm.MaxValue = 999.99D;
            this.txtRArm.MinValue = 0D;
            this.txtRArm.Name = "txtRArm";
            this.txtRArm.ShowUpDown = true;
            this.txtRArm.Size = new System.Drawing.Size(80, 20);
            this.txtRArm.TabIndex = 7;
            // 
            // txtWaist
            // 
            // 
            // 
            // 
            this.txtWaist.BackgroundStyle.Class = "DateTimeInputBackground";
            this.txtWaist.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtWaist.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.txtWaist.Increment = 1D;
            this.txtWaist.Location = new System.Drawing.Point(78, 176);
            this.txtWaist.MaxValue = 999.99D;
            this.txtWaist.MinValue = 0D;
            this.txtWaist.Name = "txtWaist";
            this.txtWaist.ShowUpDown = true;
            this.txtWaist.Size = new System.Drawing.Size(80, 20);
            this.txtWaist.TabIndex = 8;
            // 
            // txtAbdomen
            // 
            // 
            // 
            // 
            this.txtAbdomen.BackgroundStyle.Class = "DateTimeInputBackground";
            this.txtAbdomen.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtAbdomen.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.txtAbdomen.Increment = 1D;
            this.txtAbdomen.Location = new System.Drawing.Point(260, 86);
            this.txtAbdomen.MaxValue = 999.99D;
            this.txtAbdomen.MinValue = 0D;
            this.txtAbdomen.Name = "txtAbdomen";
            this.txtAbdomen.ShowUpDown = true;
            this.txtAbdomen.Size = new System.Drawing.Size(80, 20);
            this.txtAbdomen.TabIndex = 9;
            // 
            // txtHips
            // 
            // 
            // 
            // 
            this.txtHips.BackgroundStyle.Class = "DateTimeInputBackground";
            this.txtHips.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtHips.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.txtHips.Increment = 1D;
            this.txtHips.Location = new System.Drawing.Point(260, 114);
            this.txtHips.MaxValue = 999.99D;
            this.txtHips.MinValue = 0D;
            this.txtHips.Name = "txtHips";
            this.txtHips.ShowUpDown = true;
            this.txtHips.Size = new System.Drawing.Size(80, 20);
            this.txtHips.TabIndex = 10;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(185, 178);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(62, 13);
            this.label12.TabIndex = 22;
            this.label12.Text = "Right Thigh";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(368, 90);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(46, 13);
            this.label13.TabIndex = 23;
            this.label13.Text = "Left Calf";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(361, 116);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 13);
            this.label14.TabIndex = 24;
            this.label14.Text = "Right Calf";
            // 
            // txtLThigh
            // 
            // 
            // 
            // 
            this.txtLThigh.BackgroundStyle.Class = "DateTimeInputBackground";
            this.txtLThigh.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtLThigh.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.txtLThigh.Increment = 1D;
            this.txtLThigh.Location = new System.Drawing.Point(260, 142);
            this.txtLThigh.MaxValue = 999.99D;
            this.txtLThigh.MinValue = 0D;
            this.txtLThigh.Name = "txtLThigh";
            this.txtLThigh.ShowUpDown = true;
            this.txtLThigh.Size = new System.Drawing.Size(80, 20);
            this.txtLThigh.TabIndex = 11;
            // 
            // txtRThigh
            // 
            // 
            // 
            // 
            this.txtRThigh.BackgroundStyle.Class = "DateTimeInputBackground";
            this.txtRThigh.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtRThigh.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.txtRThigh.Increment = 1D;
            this.txtRThigh.Location = new System.Drawing.Point(260, 170);
            this.txtRThigh.MaxValue = 999.99D;
            this.txtRThigh.MinValue = 0D;
            this.txtRThigh.Name = "txtRThigh";
            this.txtRThigh.ShowUpDown = true;
            this.txtRThigh.Size = new System.Drawing.Size(80, 20);
            this.txtRThigh.TabIndex = 12;
            // 
            // txtLCalf
            // 
            // 
            // 
            // 
            this.txtLCalf.BackgroundStyle.Class = "DateTimeInputBackground";
            this.txtLCalf.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtLCalf.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.txtLCalf.Increment = 1D;
            this.txtLCalf.Location = new System.Drawing.Point(439, 86);
            this.txtLCalf.MaxValue = 999.99D;
            this.txtLCalf.MinValue = 0D;
            this.txtLCalf.Name = "txtLCalf";
            this.txtLCalf.ShowUpDown = true;
            this.txtLCalf.Size = new System.Drawing.Size(80, 20);
            this.txtLCalf.TabIndex = 13;
            // 
            // txtRCalf
            // 
            // 
            // 
            // 
            this.txtRCalf.BackgroundStyle.Class = "DateTimeInputBackground";
            this.txtRCalf.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtRCalf.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.txtRCalf.Increment = 1D;
            this.txtRCalf.Location = new System.Drawing.Point(439, 114);
            this.txtRCalf.MaxValue = 999.99D;
            this.txtRCalf.MinValue = 0D;
            this.txtRCalf.Name = "txtRCalf";
            this.txtRCalf.ShowUpDown = true;
            this.txtRCalf.Size = new System.Drawing.Size(80, 20);
            this.txtRCalf.TabIndex = 14;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.label15.Location = new System.Drawing.Point(368, 159);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(34, 17);
            this.label15.TabIndex = 30;
            this.label15.Text = "BMI";
            // 
            // lblBMI
            // 
            this.lblBMI.AutoSize = true;
            this.lblBMI.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(161)));
            this.lblBMI.Location = new System.Drawing.Point(408, 159);
            this.lblBMI.Name = "lblBMI";
            this.lblBMI.Size = new System.Drawing.Size(0, 17);
            this.lblBMI.TabIndex = 31;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(181, 222);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 15;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(283, 222);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmMeasurement
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(533, 258);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblBMI);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txtRCalf);
            this.Controls.Add(this.txtLCalf);
            this.Controls.Add(this.txtRThigh);
            this.Controls.Add(this.txtLThigh);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtHips);
            this.Controls.Add(this.txtAbdomen);
            this.Controls.Add(this.txtWaist);
            this.Controls.Add(this.txtRArm);
            this.Controls.Add(this.txtLArm);
            this.Controls.Add(this.txtChest);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtBodyfat);
            this.Controls.Add(this.txtWeight);
            this.Controls.Add(this.txtHeight);
            this.Controls.Add(this.dtpDateCreated);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMeasurement";
            this.Text = "Measurement";
            ((System.ComponentModel.ISupportInitialize)(this.txtHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBodyfat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLArm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRArm)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWaist)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAbdomen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHips)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLThigh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRThigh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLCalf)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRCalf)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpDateCreated;
        private DevComponents.Editors.DoubleInput txtHeight;
        private DevComponents.Editors.DoubleInput txtWeight;
        private DevComponents.Editors.DoubleInput txtBodyfat;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private DevComponents.Editors.DoubleInput txtChest;
        private DevComponents.Editors.DoubleInput txtLArm;
        private DevComponents.Editors.DoubleInput txtRArm;
        private DevComponents.Editors.DoubleInput txtWaist;
        private DevComponents.Editors.DoubleInput txtAbdomen;
        private DevComponents.Editors.DoubleInput txtHips;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private DevComponents.Editors.DoubleInput txtLThigh;
        private DevComponents.Editors.DoubleInput txtRThigh;
        private DevComponents.Editors.DoubleInput txtLCalf;
        private DevComponents.Editors.DoubleInput txtRCalf;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblBMI;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}