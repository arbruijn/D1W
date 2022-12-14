namespace Descent2Workshop
{
    partial class HXMLoadDialog
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
            this.txtHAMFile = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtHXMFile = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnSelectHAM = new System.Windows.Forms.Button();
            this.btnSelectHXM = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.txtVHAMFile = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(256, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "HXM files additonally require a base HAM file to edit. ";
            // 
            // txtHAMFile
            // 
            this.txtHAMFile.Location = new System.Drawing.Point(12, 46);
            this.txtHAMFile.Name = "txtHAMFile";
            this.txtHAMFile.Size = new System.Drawing.Size(366, 20);
            this.txtHAMFile.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Base HAM file:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "HXM file to edit:";
            // 
            // txtHXMFile
            // 
            this.txtHXMFile.Location = new System.Drawing.Point(12, 96);
            this.txtHXMFile.Name = "txtHXMFile";
            this.txtHXMFile.Size = new System.Drawing.Size(366, 20);
            this.txtHXMFile.TabIndex = 4;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(345, 175);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnAccept
            // 
            this.btnAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAccept.Location = new System.Drawing.Point(264, 175);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(75, 23);
            this.btnAccept.TabIndex = 6;
            this.btnAccept.Text = "OK";
            this.btnAccept.UseVisualStyleBackColor = true;
            // 
            // btnSelectHAM
            // 
            this.btnSelectHAM.Location = new System.Drawing.Point(384, 44);
            this.btnSelectHAM.Name = "btnSelectHAM";
            this.btnSelectHAM.Size = new System.Drawing.Size(36, 23);
            this.btnSelectHAM.TabIndex = 7;
            this.btnSelectHAM.Text = "...";
            this.btnSelectHAM.UseVisualStyleBackColor = true;
            this.btnSelectHAM.Click += new System.EventHandler(this.btnSelectHAM_Click);
            // 
            // btnSelectHXM
            // 
            this.btnSelectHXM.Location = new System.Drawing.Point(384, 94);
            this.btnSelectHXM.Name = "btnSelectHXM";
            this.btnSelectHXM.Size = new System.Drawing.Size(36, 23);
            this.btnSelectHXM.TabIndex = 8;
            this.btnSelectHXM.Text = "...";
            this.btnSelectHXM.UseVisualStyleBackColor = true;
            this.btnSelectHXM.Click += new System.EventHandler(this.btnSelectHXM_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(384, 144);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(36, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtVHAMFile
            // 
            this.txtVHAMFile.Location = new System.Drawing.Point(12, 146);
            this.txtVHAMFile.Name = "txtVHAMFile";
            this.txtVHAMFile.Size = new System.Drawing.Size(366, 20);
            this.txtVHAMFile.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(213, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "V-HAM file for additional elements (Optional)";
            // 
            // HXMLoadDialog
            // 
            this.AcceptButton = this.btnAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(432, 210);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtVHAMFile);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnSelectHXM);
            this.Controls.Add(this.btnSelectHAM);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtHXMFile);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtHAMFile);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HXMLoadDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "HXMLoadDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtHAMFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtHXMFile;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnSelectHAM;
        private System.Windows.Forms.Button btnSelectHXM;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtVHAMFile;
        private System.Windows.Forms.Label label4;
    }
}