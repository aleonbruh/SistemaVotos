namespace SistemaVotos
{
    partial class FrmVoto
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
            this.grpElector = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMesa = new System.Windows.Forms.TextBox();
            this.txtCentro = new System.Windows.Forms.TextBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtBusqueda = new System.Windows.Forms.TextBox();
            this.grpAlcaldes = new System.Windows.Forms.GroupBox();
            this.lblEstadoAlcaldes = new System.Windows.Forms.Label();
            this.rbNuloAlcaldes = new System.Windows.Forms.RadioButton();
            this.rbValidoAlcaldes = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbCandidatosAlcaldes = new System.Windows.Forms.ComboBox();
            this.grpDiputados = new System.Windows.Forms.GroupBox();
            this.lblEstadoDiputados = new System.Windows.Forms.Label();
            this.rbNuloDiputados = new System.Windows.Forms.RadioButton();
            this.rbValidoDiputados = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbCandidatosDiputados = new System.Windows.Forms.ComboBox();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.grpElector.SuspendLayout();
            this.grpAlcaldes.SuspendLayout();
            this.grpDiputados.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpElector
            // 
            this.grpElector.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpElector.Controls.Add(this.label6);
            this.grpElector.Controls.Add(this.txtMesa);
            this.grpElector.Controls.Add(this.txtCentro);
            this.grpElector.Controls.Add(this.txtNombre);
            this.grpElector.Controls.Add(this.label3);
            this.grpElector.Controls.Add(this.label2);
            this.grpElector.Controls.Add(this.label1);
            this.grpElector.Controls.Add(this.btnBuscar);
            this.grpElector.Controls.Add(this.txtBusqueda);
            this.grpElector.Location = new System.Drawing.Point(12, 12);
            this.grpElector.Name = "grpElector";
            this.grpElector.Size = new System.Drawing.Size(331, 185);
            this.grpElector.TabIndex = 0;
            this.grpElector.TabStop = false;
            this.grpElector.Text = "Elector";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "DUI:";
            // 
            // txtMesa
            // 
            this.txtMesa.Location = new System.Drawing.Point(15, 145);
            this.txtMesa.Name = "txtMesa";
            this.txtMesa.ReadOnly = true;
            this.txtMesa.Size = new System.Drawing.Size(290, 20);
            this.txtMesa.TabIndex = 10;
            // 
            // txtCentro
            // 
            this.txtCentro.Location = new System.Drawing.Point(15, 106);
            this.txtCentro.Name = "txtCentro";
            this.txtCentro.ReadOnly = true;
            this.txtCentro.Size = new System.Drawing.Size(290, 20);
            this.txtCentro.TabIndex = 9;
            // 
            // txtNombre
            // 
            this.txtNombre.Location = new System.Drawing.Point(15, 67);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.ReadOnly = true;
            this.txtNombre.Size = new System.Drawing.Size(290, 20);
            this.txtNombre.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Mesa de Votacion:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Centro de Votacion:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Nombre Completo:";
            // 
            // btnBuscar
            // 
            this.btnBuscar.Location = new System.Drawing.Point(230, 16);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 23);
            this.btnBuscar.TabIndex = 1;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtBusqueda
            // 
            this.txtBusqueda.Location = new System.Drawing.Point(47, 19);
            this.txtBusqueda.Name = "txtBusqueda";
            this.txtBusqueda.Size = new System.Drawing.Size(177, 20);
            this.txtBusqueda.TabIndex = 0;
            // 
            // grpAlcaldes
            // 
            this.grpAlcaldes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.grpAlcaldes.Controls.Add(this.lblEstadoAlcaldes);
            this.grpAlcaldes.Controls.Add(this.rbNuloAlcaldes);
            this.grpAlcaldes.Controls.Add(this.rbValidoAlcaldes);
            this.grpAlcaldes.Controls.Add(this.label4);
            this.grpAlcaldes.Controls.Add(this.cmbCandidatosAlcaldes);
            this.grpAlcaldes.Location = new System.Drawing.Point(12, 203);
            this.grpAlcaldes.Name = "grpAlcaldes";
            this.grpAlcaldes.Size = new System.Drawing.Size(331, 88);
            this.grpAlcaldes.TabIndex = 1;
            this.grpAlcaldes.TabStop = false;
            this.grpAlcaldes.Text = "Votacion Alcaldes";
            // 
            // lblEstadoAlcaldes
            // 
            this.lblEstadoAlcaldes.AutoSize = true;
            this.lblEstadoAlcaldes.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblEstadoAlcaldes.Location = new System.Drawing.Point(178, 57);
            this.lblEstadoAlcaldes.Name = "lblEstadoAlcaldes";
            this.lblEstadoAlcaldes.Size = new System.Drawing.Size(0, 13);
            this.lblEstadoAlcaldes.TabIndex = 9;
            // 
            // rbNuloAlcaldes
            // 
            this.rbNuloAlcaldes.AutoSize = true;
            this.rbNuloAlcaldes.Location = new System.Drawing.Point(100, 55);
            this.rbNuloAlcaldes.Name = "rbNuloAlcaldes";
            this.rbNuloAlcaldes.Size = new System.Drawing.Size(72, 17);
            this.rbNuloAlcaldes.TabIndex = 8;
            this.rbNuloAlcaldes.Text = "Voto Nulo";
            this.rbNuloAlcaldes.UseVisualStyleBackColor = true;
            // 
            // rbValidoAlcaldes
            // 
            this.rbValidoAlcaldes.AutoSize = true;
            this.rbValidoAlcaldes.Checked = true;
            this.rbValidoAlcaldes.Location = new System.Drawing.Point(15, 55);
            this.rbValidoAlcaldes.Name = "rbValidoAlcaldes";
            this.rbValidoAlcaldes.Size = new System.Drawing.Size(79, 17);
            this.rbValidoAlcaldes.TabIndex = 7;
            this.rbValidoAlcaldes.TabStop = true;
            this.rbValidoAlcaldes.Text = "Voto Valido";
            this.rbValidoAlcaldes.UseVisualStyleBackColor = true;
            this.rbValidoAlcaldes.CheckedChanged += new System.EventHandler(this.rbValidoAlcaldes_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Seleccione un Candidato:";
            // 
            // cmbCandidatosAlcaldes
            // 
            this.cmbCandidatosAlcaldes.FormattingEnabled = true;
            this.cmbCandidatosAlcaldes.Location = new System.Drawing.Point(147, 22);
            this.cmbCandidatosAlcaldes.Name = "cmbCandidatosAlcaldes";
            this.cmbCandidatosAlcaldes.Size = new System.Drawing.Size(121, 21);
            this.cmbCandidatosAlcaldes.TabIndex = 0;
            // 
            // grpDiputados
            // 
            this.grpDiputados.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDiputados.Controls.Add(this.lblEstadoDiputados);
            this.grpDiputados.Controls.Add(this.rbNuloDiputados);
            this.grpDiputados.Controls.Add(this.rbValidoDiputados);
            this.grpDiputados.Controls.Add(this.label5);
            this.grpDiputados.Controls.Add(this.cmbCandidatosDiputados);
            this.grpDiputados.Location = new System.Drawing.Point(12, 297);
            this.grpDiputados.Name = "grpDiputados";
            this.grpDiputados.Size = new System.Drawing.Size(331, 88);
            this.grpDiputados.TabIndex = 9;
            this.grpDiputados.TabStop = false;
            this.grpDiputados.Text = "Votacion Diputados";
            // 
            // lblEstadoDiputados
            // 
            this.lblEstadoDiputados.AutoSize = true;
            this.lblEstadoDiputados.Location = new System.Drawing.Point(178, 57);
            this.lblEstadoDiputados.Name = "lblEstadoDiputados";
            this.lblEstadoDiputados.Size = new System.Drawing.Size(0, 13);
            this.lblEstadoDiputados.TabIndex = 10;
            // 
            // rbNuloDiputados
            // 
            this.rbNuloDiputados.AutoSize = true;
            this.rbNuloDiputados.Location = new System.Drawing.Point(100, 55);
            this.rbNuloDiputados.Name = "rbNuloDiputados";
            this.rbNuloDiputados.Size = new System.Drawing.Size(72, 17);
            this.rbNuloDiputados.TabIndex = 8;
            this.rbNuloDiputados.Text = "Voto Nulo";
            this.rbNuloDiputados.UseVisualStyleBackColor = true;
            // 
            // rbValidoDiputados
            // 
            this.rbValidoDiputados.AutoSize = true;
            this.rbValidoDiputados.Checked = true;
            this.rbValidoDiputados.Location = new System.Drawing.Point(15, 55);
            this.rbValidoDiputados.Name = "rbValidoDiputados";
            this.rbValidoDiputados.Size = new System.Drawing.Size(79, 17);
            this.rbValidoDiputados.TabIndex = 7;
            this.rbValidoDiputados.TabStop = true;
            this.rbValidoDiputados.Text = "Voto Valido";
            this.rbValidoDiputados.UseVisualStyleBackColor = true;
            this.rbValidoDiputados.CheckedChanged += new System.EventHandler(this.rbValidoDiputados_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(129, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Seleccione un Candidato:";
            // 
            // cmbCandidatosDiputados
            // 
            this.cmbCandidatosDiputados.FormattingEnabled = true;
            this.cmbCandidatosDiputados.Location = new System.Drawing.Point(147, 22);
            this.cmbCandidatosDiputados.Name = "cmbCandidatosDiputados";
            this.cmbCandidatosDiputados.Size = new System.Drawing.Size(121, 21);
            this.cmbCandidatosDiputados.TabIndex = 0;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAceptar.Location = new System.Drawing.Point(12, 391);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnAceptar.TabIndex = 10;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.Location = new System.Drawing.Point(93, 391);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 11;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // FrmVoto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 428);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.grpDiputados);
            this.Controls.Add(this.grpAlcaldes);
            this.Controls.Add(this.grpElector);
            this.Name = "FrmVoto";
            this.Text = "Registrar Voto";
            this.grpElector.ResumeLayout(false);
            this.grpElector.PerformLayout();
            this.grpAlcaldes.ResumeLayout(false);
            this.grpAlcaldes.PerformLayout();
            this.grpDiputados.ResumeLayout(false);
            this.grpDiputados.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpElector;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtBusqueda;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpAlcaldes;
        private System.Windows.Forms.RadioButton rbNuloAlcaldes;
        private System.Windows.Forms.RadioButton rbValidoAlcaldes;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbCandidatosAlcaldes;
        private System.Windows.Forms.GroupBox grpDiputados;
        private System.Windows.Forms.RadioButton rbNuloDiputados;
        private System.Windows.Forms.RadioButton rbValidoDiputados;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbCandidatosDiputados;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.TextBox txtMesa;
        private System.Windows.Forms.TextBox txtCentro;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblEstadoAlcaldes;
        private System.Windows.Forms.Label lblEstadoDiputados;
    }
}