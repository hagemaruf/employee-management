namespace EmployeeClient
{
    partial class Form2
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
            dgvEmployees = new DataGridView();
            btnAddEmployee = new Button();
            btnEditEmployee = new Button();
            btnDeleteEmployee = new Button();
            btnLogout = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvEmployees).BeginInit();
            SuspendLayout();
            // 
            // dgvEmployees
            // 
            dgvEmployees.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEmployees.Location = new Point(38, 87);
            dgvEmployees.Name = "dgvEmployees";
            dgvEmployees.RowHeadersWidth = 51;
            dgvEmployees.Size = new Size(690, 330);
            dgvEmployees.TabIndex = 0;
            // 
            // btnAddEmployee
            // 
            btnAddEmployee.Location = new Point(41, 27);
            btnAddEmployee.Name = "btnAddEmployee";
            btnAddEmployee.Size = new Size(163, 29);
            btnAddEmployee.TabIndex = 1;
            btnAddEmployee.Text = "Add Employee";
            btnAddEmployee.UseVisualStyleBackColor = true;
            btnAddEmployee.Click += btnAddEmployee_Click;
            // 
            // btnEditEmployee
            // 
            btnEditEmployee.Location = new Point(210, 27);
            btnEditEmployee.Name = "btnEditEmployee";
            btnEditEmployee.Size = new Size(156, 29);
            btnEditEmployee.TabIndex = 2;
            btnEditEmployee.Text = "Edit Employee";
            btnEditEmployee.UseVisualStyleBackColor = true;
            btnEditEmployee.Click += btnEditEmployee_Click;
            // 
            // btnDeleteEmployee
            // 
            btnDeleteEmployee.Location = new Point(372, 27);
            btnDeleteEmployee.Name = "btnDeleteEmployee";
            btnDeleteEmployee.Size = new Size(156, 29);
            btnDeleteEmployee.TabIndex = 3;
            btnDeleteEmployee.Text = "Delete";
            btnDeleteEmployee.UseVisualStyleBackColor = true;
            btnDeleteEmployee.Click += btnDeleteEmployee_Click;
            // 
            // btnLogout
            // 
            btnLogout.Location = new Point(572, 27);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(156, 29);
            btnLogout.TabIndex = 4;
            btnLogout.Text = "Logout";
            btnLogout.UseVisualStyleBackColor = true;
            btnLogout.Click += btnLogout_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(774, 450);
            Controls.Add(btnLogout);
            Controls.Add(btnDeleteEmployee);
            Controls.Add(btnEditEmployee);
            Controls.Add(btnAddEmployee);
            Controls.Add(dgvEmployees);
            Name = "Form2";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Employees";
            Load += Form2_Load;
            ((System.ComponentModel.ISupportInitialize)dgvEmployees).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvEmployees;
        private Button btnAddEmployee;
        private Button btnEditEmployee;
        private Button btnDeleteEmployee;
        private Button btnLogout;
    }
}