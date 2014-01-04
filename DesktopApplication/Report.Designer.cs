namespace GymManager
{
    partial class Report
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.MemberDetailsReoportDataSet = new GymManager.MemberDetailsReoportDataSet();
            this.MembersDetailBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.MembersDetailTableAdapter = new GymManager.MemberDetailsReoportDataSetTableAdapters.MembersDetailTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.MemberDetailsReoportDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MembersDetailBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "MembersDetail";
            reportDataSource1.Value = this.MembersDetailBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "GymManager.MemberDetails.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(747, 521);
            this.reportViewer1.TabIndex = 0;
            // 
            // MemberDetailsReoportDataSet
            // 
            this.MemberDetailsReoportDataSet.DataSetName = "MemberDetailsReoportDataSet";
            this.MemberDetailsReoportDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // MembersDetailBindingSource
            // 
            this.MembersDetailBindingSource.DataMember = "MembersDetail";
            this.MembersDetailBindingSource.DataSource = this.MemberDetailsReoportDataSet;
            // 
            // MembersDetailTableAdapter
            // 
            this.MembersDetailTableAdapter.ClearBeforeFill = true;
            // 
            // Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(747, 521);
            this.Controls.Add(this.reportViewer1);
            this.Name = "Report";
            this.ShowIcon = false;
            this.Text = "Members Detail Reporting";
            this.Load += new System.EventHandler(this.Report_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MemberDetailsReoportDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MembersDetailBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource MembersDetailBindingSource;
        private MemberDetailsReoportDataSet MemberDetailsReoportDataSet;
        private MemberDetailsReoportDataSetTableAdapters.MembersDetailTableAdapter MembersDetailTableAdapter;
    }
}