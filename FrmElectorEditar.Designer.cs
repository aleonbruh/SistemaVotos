namespace SistemaVotos
{
    partial class FrmElectorEditar
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
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtDUI = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpFechaNacimiento = new System.Windows.Forms.DateTimePicker();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbDistrito = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lblCentro = new System.Windows.Forms.Label();
            this.txtCentro = new System.Windows.Forms.TextBox();
            this.lblMensajeCentro = new System.Windows.Forms.Label();
            this.lblMensajeMesa = new System.Windows.Forms.Label();
            this.txtMesa = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre Completo:";
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(27, 42);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(229, 20);
            this.txtNombre.TabIndex = 1;
            // 
            // txtDUI
            // 
            this.txtDUI.Location = new System.Drawing.Point(27, 81);
            this.txtDUI.Name = "txtDUI";
            this.txtDUI.Size = new System.Drawing.Size(229, 20);
            this.txtDUI.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "DUI:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Fecha de Nacimiento:";
            // 
            // dtpFechaNacimiento
            // 
            this.dtpFechaNacimiento.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaNacimiento.Location = new System.Drawing.Point(27, 120);
            this.dtpFechaNacimiento.MaxDate = new System.DateTime(2026, 10, 8, 0, 0, 0, 0);
            this.dtpFechaNacimiento.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dtpFechaNacimiento.Name = "dtpFechaNacimiento";
            this.dtpFechaNacimiento.Size = new System.Drawing.Size(229, 20);
            this.dtpFechaNacimiento.TabIndex = 6;
            this.dtpFechaNacimiento.Value = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            // 
            // txtDireccion
            // 
            this.txtDireccion.Location = new System.Drawing.Point(27, 159);
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Size = new System.Drawing.Size(229, 20);
            this.txtDireccion.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 143);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Direccion:";
            // 
            // cmbDistrito
            // 
            this.cmbDistrito.FormattingEnabled = true;
            this.cmbDistrito.Location = new System.Drawing.Point(27, 198);
            this.cmbDistrito.Name = "cmbDistrito";
            this.cmbDistrito.Size = new System.Drawing.Size(229, 21);
            this.cmbDistrito.TabIndex = 9;
            this.cmbDistrito.SelectedIndexChanged += new System.EventHandler(this.cmbDistrito_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 182);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Distrito:";
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(27, 354);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnAceptar.TabIndex = 13;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(108, 354);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 14;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // lblCentro
            // 
            this.lblCentro.AutoSize = true;
            this.lblCentro.Location = new System.Drawing.Point(26, 231);
            this.lblCentro.Name = "lblCentro";
            this.lblCentro.Size = new System.Drawing.Size(101, 13);
            this.lblCentro.TabIndex = 16;
            this.lblCentro.Text = "Centro de Votacion:";
            // 
            // txtCentro
            // 
            this.txtCentro.Location = new System.Drawing.Point(27, 247);
            this.txtCentro.Name = "txtCentro";
            this.txtCentro.ReadOnly = true;
            this.txtCentro.Size = new System.Drawing.Size(229, 20);
            this.txtCentro.TabIndex = 19;
            // 
            // lblMensajeCentro
            // 
            this.lblMensajeCentro.AutoSize = true;
            this.lblMensajeCentro.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensajeCentro.Location = new System.Drawing.Point(27, 270);
            this.lblMensajeCentro.Name = "lblMensajeCentro";
            this.lblMensajeCentro.Size = new System.Drawing.Size(0, 12);
            this.lblMensajeCentro.TabIndex = 21;
            // 
            // lblMensajeMesa
            // 
            this.lblMensajeMesa.AutoSize = true;
            this.lblMensajeMesa.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensajeMesa.Location = new System.Drawing.Point(25, 330);
            this.lblMensajeMesa.Name = "lblMensajeMesa";
            this.lblMensajeMesa.Size = new System.Drawing.Size(0, 12);
            this.lblMensajeMesa.TabIndex = 24;
            // 
            // txtMesa
            // 
            this.txtMesa.Location = new System.Drawing.Point(25, 307);
            this.txtMesa.Name = "txtMesa";
            this.txtMesa.ReadOnly = true;
            this.txtMesa.Size = new System.Drawing.Size(229, 20);
            this.txtMesa.TabIndex = 23;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(24, 291);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Mesa de Votacion:";
            // 
            // FrmElectorEditar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 407);
            this.Controls.Add(this.lblMensajeMesa);
            this.Controls.Add(this.txtMesa);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblMensajeCentro);
            this.Controls.Add(this.txtCentro);
            this.Controls.Add(this.lblCentro);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cmbDistrito);
            this.Controls.Add(this.txtDireccion);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtpFechaNacimiento);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDUI);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.label1);
            this.Name = "FrmElectorEditar";
            this.Text = "FrmElectorEditar";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtDUI;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpFechaNacimiento;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbDistrito;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblCentro;
        private System.Windows.Forms.TextBox txtCentro;
        private System.Windows.Forms.Label lblMensajeCentro;
        private System.Windows.Forms.Label lblMensajeMesa;
        private System.Windows.Forms.TextBox txtMesa;
        private System.Windows.Forms.Label label7;
    }
}