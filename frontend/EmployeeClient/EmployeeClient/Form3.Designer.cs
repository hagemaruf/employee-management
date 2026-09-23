namespace EmployeeClient
{
    partial class Form3
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
            btnSave = new Button();
            btnCancel = new Button();
            Name = new Label();
            txtName = new TextBox();
            txtEmail = new TextBox();
            label1 = new Label();
            txtDepartment = new TextBox();
            label2 = new Label();
            SuspendLayout();
            // 
            // btnSave
            // 
            btnSave.Location = new Point(84, 239);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(94, 29);
            btnSave.TabIndex = 0;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(203, 239);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(94, 29);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // Name
            // 
            Name.AutoSize = true;
            Name.Location = new Point(56, 54);
            Name.Name = "Name";
            Name.Size = new Size(49, 20);
            Name.TabIndex = 2;
            Name.Text = "Name";
            // 
            // txtName
            // 
            txtName.Location = new Point(148, 51);
            txtName.Name = "txtName";
            txtName.Size = new Size(170, 27);
            txtName.TabIndex = 3;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(148, 102);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(170, 27);
            txtEmail.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(56, 105);
            label1.Name = "label1";
            label1.Size = new Size(46, 20);
            label1.TabIndex = 4;
            label1.Text = "Email";
            // 
            // txtDepartment
            // 
            txtDepartment.Location = new Point(148, 157);
            txtDepartment.Name = "txtDepartment";
            txtDepartment.Size = new Size(170, 27);
            txtDepartment.TabIndex = 7;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(56, 160);
            label2.Name = "label2";
            label2.Size = new Size(89, 20);
            label2.TabIndex = 6;
            label2.Text = "Department";
            // 
            // Form3
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(369, 324);
            Controls.Add(txtDepartment);
            Controls.Add(label2);
            Controls.Add(txtEmail);
            Controls.Add(label1);
            Controls.Add(txtName);
            Controls.Add(Name);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            StartPosition = FormStartPosition.CenterParent;
            Text = "Form3";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSave;
        private Button btnCancel;
        private Label Name;
        private TextBox txtName;
        private TextBox txtEmail;
        private Label label1;
        private TextBox txtDepartment;
        private Label label2;
    }
}