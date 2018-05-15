namespace ImputacionHoras.Presentation.Forms
{
    partial class Imputacion_Form
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbUser = new System.Windows.Forms.Label();
            this.lbPassword = new System.Windows.Forms.Label();
            this.tbUser = new System.Windows.Forms.TextBox();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.tbImputaciones = new System.Windows.Forms.TextBox();
            this.lbImputaciones = new System.Windows.Forms.Label();
            this.lbContracts = new System.Windows.Forms.Label();
            this.lbAssets = new System.Windows.Forms.Label();
            this.lbExportTo = new System.Windows.Forms.Label();
            this.tbContracts = new System.Windows.Forms.TextBox();
            this.tbAssets = new System.Windows.Forms.TextBox();
            this.tbExportTo = new System.Windows.Forms.TextBox();
            this.btImputaciones = new System.Windows.Forms.Button();
            this.btContracts = new System.Windows.Forms.Button();
            this.btAssets = new System.Windows.Forms.Button();
            this.btExportTo = new System.Windows.Forms.Button();
            this.btExport = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tbLog = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lbUser
            // 
            this.lbUser.AutoSize = true;
            this.lbUser.Location = new System.Drawing.Point(67, 28);
            this.lbUser.Name = "lbUser";
            this.lbUser.Size = new System.Drawing.Size(42, 17);
            this.lbUser.TabIndex = 0;
            this.lbUser.Text = "User:";
            // 
            // lbPassword
            // 
            this.lbPassword.AutoSize = true;
            this.lbPassword.Location = new System.Drawing.Point(36, 56);
            this.lbPassword.Name = "lbPassword";
            this.lbPassword.Size = new System.Drawing.Size(73, 17);
            this.lbPassword.TabIndex = 1;
            this.lbPassword.Text = "Password:";
            // 
            // tbUser
            // 
            this.tbUser.Location = new System.Drawing.Point(115, 25);
            this.tbUser.Name = "tbUser";
            this.tbUser.Size = new System.Drawing.Size(154, 22);
            this.tbUser.TabIndex = 2;
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(115, 53);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.Size = new System.Drawing.Size(154, 22);
            this.tbPassword.TabIndex = 3;
            // 
            // tbImputaciones
            // 
            this.tbImputaciones.Location = new System.Drawing.Point(316, 41);
            this.tbImputaciones.Name = "tbImputaciones";
            this.tbImputaciones.Size = new System.Drawing.Size(359, 22);
            this.tbImputaciones.TabIndex = 4;
            // 
            // lbImputaciones
            // 
            this.lbImputaciones.AutoSize = true;
            this.lbImputaciones.Location = new System.Drawing.Point(313, 21);
            this.lbImputaciones.Name = "lbImputaciones";
            this.lbImputaciones.Size = new System.Drawing.Size(95, 17);
            this.lbImputaciones.TabIndex = 5;
            this.lbImputaciones.Text = "Imputaciones:";
            // 
            // lbContracts
            // 
            this.lbContracts.AutoSize = true;
            this.lbContracts.Location = new System.Drawing.Point(313, 73);
            this.lbContracts.Name = "lbContracts";
            this.lbContracts.Size = new System.Drawing.Size(72, 17);
            this.lbContracts.TabIndex = 6;
            this.lbContracts.Text = "Contracts:";
            // 
            // lbAssets
            // 
            this.lbAssets.AutoSize = true;
            this.lbAssets.Location = new System.Drawing.Point(313, 125);
            this.lbAssets.Name = "lbAssets";
            this.lbAssets.Size = new System.Drawing.Size(54, 17);
            this.lbAssets.TabIndex = 7;
            this.lbAssets.Text = "Assets:";
            // 
            // lbExportTo
            // 
            this.lbExportTo.AutoSize = true;
            this.lbExportTo.Location = new System.Drawing.Point(112, 206);
            this.lbExportTo.Name = "lbExportTo";
            this.lbExportTo.Size = new System.Drawing.Size(68, 17);
            this.lbExportTo.TabIndex = 8;
            this.lbExportTo.Text = "Export to:";
            // 
            // tbContracts
            // 
            this.tbContracts.Location = new System.Drawing.Point(316, 93);
            this.tbContracts.Name = "tbContracts";
            this.tbContracts.Size = new System.Drawing.Size(359, 22);
            this.tbContracts.TabIndex = 9;
            // 
            // tbAssets
            // 
            this.tbAssets.Location = new System.Drawing.Point(316, 145);
            this.tbAssets.Name = "tbAssets";
            this.tbAssets.Size = new System.Drawing.Size(359, 22);
            this.tbAssets.TabIndex = 10;
            // 
            // tbExportTo
            // 
            this.tbExportTo.Location = new System.Drawing.Point(115, 226);
            this.tbExportTo.Name = "tbExportTo";
            this.tbExportTo.Size = new System.Drawing.Size(522, 22);
            this.tbExportTo.TabIndex = 11;
            // 
            // btImputaciones
            // 
            this.btImputaciones.Location = new System.Drawing.Point(682, 39);
            this.btImputaciones.Name = "btImputaciones";
            this.btImputaciones.Size = new System.Drawing.Size(58, 23);
            this.btImputaciones.TabIndex = 12;
            this.btImputaciones.Text = "...";
            this.btImputaciones.UseVisualStyleBackColor = true;
            this.btImputaciones.Click += new System.EventHandler(this.btImputaciones_Click);
            // 
            // btContracts
            // 
            this.btContracts.Location = new System.Drawing.Point(681, 93);
            this.btContracts.Name = "btContracts";
            this.btContracts.Size = new System.Drawing.Size(58, 23);
            this.btContracts.TabIndex = 13;
            this.btContracts.Text = "...";
            this.btContracts.UseVisualStyleBackColor = true;
            this.btContracts.Click += new System.EventHandler(this.btContracts_Click);
            // 
            // btAssets
            // 
            this.btAssets.Location = new System.Drawing.Point(681, 144);
            this.btAssets.Name = "btAssets";
            this.btAssets.Size = new System.Drawing.Size(58, 23);
            this.btAssets.TabIndex = 14;
            this.btAssets.Text = "...";
            this.btAssets.UseVisualStyleBackColor = true;
            this.btAssets.Click += new System.EventHandler(this.btAssets_Click);
            // 
            // btExportTo
            // 
            this.btExportTo.Location = new System.Drawing.Point(643, 226);
            this.btExportTo.Name = "btExportTo";
            this.btExportTo.Size = new System.Drawing.Size(58, 23);
            this.btExportTo.TabIndex = 15;
            this.btExportTo.Text = "...";
            this.btExportTo.UseVisualStyleBackColor = true;
            this.btExportTo.Click += new System.EventHandler(this.btExportTo_Click);
            // 
            // btExport
            // 
            this.btExport.Location = new System.Drawing.Point(681, 390);
            this.btExport.Name = "btExport";
            this.btExport.Size = new System.Drawing.Size(103, 48);
            this.btExport.TabIndex = 16;
            this.btExport.Text = "Export:";
            this.btExport.UseVisualStyleBackColor = true;
            this.btExport.Click += new System.EventHandler(this.btExport_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // tbLog
            // 
            this.tbLog.BackColor = System.Drawing.SystemColors.Window;
            this.tbLog.Location = new System.Drawing.Point(12, 310);
            this.tbLog.Multiline = true;
            this.tbLog.Name = "tbLog";
            this.tbLog.ReadOnly = true;
            this.tbLog.Size = new System.Drawing.Size(663, 128);
            this.tbLog.TabIndex = 17;
            // 
            // Imputacion_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tbLog);
            this.Controls.Add(this.btExport);
            this.Controls.Add(this.btExportTo);
            this.Controls.Add(this.btAssets);
            this.Controls.Add(this.btContracts);
            this.Controls.Add(this.btImputaciones);
            this.Controls.Add(this.tbExportTo);
            this.Controls.Add(this.tbAssets);
            this.Controls.Add(this.tbContracts);
            this.Controls.Add(this.lbExportTo);
            this.Controls.Add(this.lbAssets);
            this.Controls.Add(this.lbContracts);
            this.Controls.Add(this.lbImputaciones);
            this.Controls.Add(this.tbImputaciones);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.tbUser);
            this.Controls.Add(this.lbPassword);
            this.Controls.Add(this.lbUser);
            this.Name = "Imputacion_Form";
            this.Text = "Imputaciones Vueling";
            this.Load += new System.EventHandler(this.Imputacion_Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbUser;
        private System.Windows.Forms.Label lbPassword;
        private System.Windows.Forms.TextBox tbUser;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.TextBox tbImputaciones;
        private System.Windows.Forms.Label lbImputaciones;
        private System.Windows.Forms.Label lbContracts;
        private System.Windows.Forms.Label lbAssets;
        private System.Windows.Forms.Label lbExportTo;
        private System.Windows.Forms.TextBox tbContracts;
        private System.Windows.Forms.TextBox tbAssets;
        private System.Windows.Forms.TextBox tbExportTo;
        private System.Windows.Forms.Button btImputaciones;
        private System.Windows.Forms.Button btContracts;
        private System.Windows.Forms.Button btAssets;
        private System.Windows.Forms.Button btExportTo;
        private System.Windows.Forms.Button btExport;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TextBox tbLog;
    }
}

