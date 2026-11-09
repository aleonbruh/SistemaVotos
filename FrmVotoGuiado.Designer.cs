namespace SistemaVotos
{
    partial class FrmVotoGuiado
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmVotoGuiado));
            this.label1 = new System.Windows.Forms.Label();
            this.btnEmpezar = new System.Windows.Forms.Button();
            this.lblNombre = new System.Windows.Forms.Label();
            this.lblDui = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlBievenida = new System.Windows.Forms.Panel();
            this.pnlVotacion = new System.Windows.Forms.Panel();
            this.btnSiguiente = new System.Windows.Forms.Button();
            this.grpEleccion = new System.Windows.Forms.GroupBox();
            this.rbVotoBlanco = new System.Windows.Forms.RadioButton();
            this.rbVotoValido = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbCandidatos = new System.Windows.Forms.ComboBox();
            this.lblTipoEleccion = new System.Windows.Forms.Label();
            this.pnlFinal = new System.Windows.Forms.Panel();
            this.btnaFin = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.pnlBievenida.SuspendLayout();
            this.pnlVotacion.SuspendLayout();
            this.grpEleccion.SuspendLayout();
            this.pnlFinal.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(190, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bienvenido";
            // 
            // btnEmpezar
            // 
            this.btnEmpezar.Location = new System.Drawing.Point(175, 216);
            this.btnEmpezar.Name = "btnEmpezar";
            this.btnEmpezar.Size = new System.Drawing.Size(75, 23);
            this.btnEmpezar.TabIndex = 1;
            this.btnEmpezar.Text = "Empezar";
            this.btnEmpezar.UseVisualStyleBackColor = true;
            this.btnEmpezar.Click += new System.EventHandler(this.btnEmpezar_Click);
            // 
            // lblNombre
            // 
            this.lblNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombre.Location = new System.Drawing.Point(3, 49);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(432, 18);
            this.lblNombre.TabIndex = 2;
            this.lblNombre.Text = "[nombre]";
            this.lblNombre.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDui
            // 
            this.lblDui.Location = new System.Drawing.Point(0, 65);
            this.lblDui.Name = "lblDui";
            this.lblDui.Size = new System.Drawing.Size(438, 18);
            this.lblDui.TabIndex = 3;
            this.lblDui.Text = "[DUI]";
            this.lblDui.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(104, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(234, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Porfavor, si no desea elegir NINGUN candidato,\r\n";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(95, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(253, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "seleccione la opcion como se muestre en la imagen:";
            // 
            // pnlBievenida
            // 
            this.pnlBievenida.Controls.Add(this.label8);
            this.pnlBievenida.Controls.Add(this.label3);
            this.pnlBievenida.Controls.Add(this.label2);
            this.pnlBievenida.Controls.Add(this.lblDui);
            this.pnlBievenida.Controls.Add(this.lblNombre);
            this.pnlBievenida.Controls.Add(this.btnEmpezar);
            this.pnlBievenida.Controls.Add(this.label1);
            this.pnlBievenida.Location = new System.Drawing.Point(12, 12);
            this.pnlBievenida.Name = "pnlBievenida";
            this.pnlBievenida.Size = new System.Drawing.Size(438, 262);
            this.pnlBievenida.TabIndex = 0;
            // 
            // pnlVotacion
            // 
            this.pnlVotacion.Controls.Add(this.btnSiguiente);
            this.pnlVotacion.Controls.Add(this.grpEleccion);
            this.pnlVotacion.Controls.Add(this.lblTipoEleccion);
            this.pnlVotacion.Location = new System.Drawing.Point(12, 12);
            this.pnlVotacion.Name = "pnlVotacion";
            this.pnlVotacion.Size = new System.Drawing.Size(438, 262);
            this.pnlVotacion.TabIndex = 2;
            // 
            // btnSiguiente
            // 
            this.btnSiguiente.Location = new System.Drawing.Point(178, 201);
            this.btnSiguiente.Name = "btnSiguiente";
            this.btnSiguiente.Size = new System.Drawing.Size(75, 23);
            this.btnSiguiente.TabIndex = 2;
            this.btnSiguiente.Text = "Siguiente";
            this.btnSiguiente.UseVisualStyleBackColor = true;
            this.btnSiguiente.Click += new System.EventHandler(this.btnSiguiente_Click);
            // 
            // grpEleccion
            // 
            this.grpEleccion.Controls.Add(this.rbVotoBlanco);
            this.grpEleccion.Controls.Add(this.rbVotoValido);
            this.grpEleccion.Controls.Add(this.label4);
            this.grpEleccion.Controls.Add(this.cmbCandidatos);
            this.grpEleccion.Location = new System.Drawing.Point(59, 70);
            this.grpEleccion.Name = "grpEleccion";
            this.grpEleccion.Size = new System.Drawing.Size(307, 125);
            this.grpEleccion.TabIndex = 1;
            this.grpEleccion.TabStop = false;
            this.grpEleccion.Text = "Eliga uno de los candidatos o marque la opcion que desee:";
            // 
            // rbVotoBlanco
            // 
            this.rbVotoBlanco.AutoSize = true;
            this.rbVotoBlanco.Location = new System.Drawing.Point(119, 87);
            this.rbVotoBlanco.Name = "rbVotoBlanco";
            this.rbVotoBlanco.Size = new System.Drawing.Size(98, 17);
            this.rbVotoBlanco.TabIndex = 12;
            this.rbVotoBlanco.Text = "Voto en Blanco";
            this.rbVotoBlanco.UseVisualStyleBackColor = true;
            // 
            // rbVotoValido
            // 
            this.rbVotoValido.AutoSize = true;
            this.rbVotoValido.Checked = true;
            this.rbVotoValido.Location = new System.Drawing.Point(28, 87);
            this.rbVotoValido.Name = "rbVotoValido";
            this.rbVotoValido.Size = new System.Drawing.Size(79, 17);
            this.rbVotoValido.TabIndex = 11;
            this.rbVotoValido.TabStop = true;
            this.rbVotoValido.Text = "Voto Valido";
            this.rbVotoValido.UseVisualStyleBackColor = true;
            this.rbVotoValido.CheckedChanged += new System.EventHandler(this.rbVotoValido_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Seleccione un Candidato:";
            // 
            // cmbCandidatos
            // 
            this.cmbCandidatos.FormattingEnabled = true;
            this.cmbCandidatos.Location = new System.Drawing.Point(28, 50);
            this.cmbCandidatos.Name = "cmbCandidatos";
            this.cmbCandidatos.Size = new System.Drawing.Size(251, 21);
            this.cmbCandidatos.TabIndex = 9;
            // 
            // lblTipoEleccion
            // 
            this.lblTipoEleccion.AutoSize = true;
            this.lblTipoEleccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipoEleccion.Location = new System.Drawing.Point(72, 35);
            this.lblTipoEleccion.Name = "lblTipoEleccion";
            this.lblTipoEleccion.Size = new System.Drawing.Size(285, 16);
            this.lblTipoEleccion.TabIndex = 0;
            this.lblTipoEleccion.Text = "Eleccion de Alcalde para San Salvador Centro";
            // 
            // pnlFinal
            // 
            this.pnlFinal.Controls.Add(this.btnaFin);
            this.pnlFinal.Controls.Add(this.label7);
            this.pnlFinal.Controls.Add(this.label6);
            this.pnlFinal.Controls.Add(this.label5);
            this.pnlFinal.Location = new System.Drawing.Point(12, 10);
            this.pnlFinal.Name = "pnlFinal";
            this.pnlFinal.Size = new System.Drawing.Size(438, 261);
            this.pnlFinal.TabIndex = 5;
            // 
            // btnaFin
            // 
            this.btnaFin.Location = new System.Drawing.Point(178, 179);
            this.btnaFin.Name = "btnaFin";
            this.btnaFin.Size = new System.Drawing.Size(75, 23);
            this.btnaFin.TabIndex = 3;
            this.btnaFin.Text = "Finalizar";
            this.btnaFin.UseVisualStyleBackColor = true;
            this.btnaFin.Click += new System.EventHandler(this.btnaFin_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(98, 134);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(231, 18);
            this.label7.TabIndex = 2;
            this.label7.Text = "al operador que ya emitio su voto.";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(72, 116);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(298, 18);
            this.label6.TabIndex = 1;
            this.label6.Text = "Porfavor, haga click en \"Finalizar\" y notifique";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(98, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(232, 18);
            this.label5.TabIndex = 0;
            this.label5.Text = "Muchas gracias por emitir su voto";
            // 
            // label8
            // 
            this.label8.Image = ((System.Drawing.Image)(resources.GetObject("label8.Image")));
            this.label8.Location = new System.Drawing.Point(56, 132);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(325, 72);
            this.label8.TabIndex = 6;
            // 
            // FrmVotoGuiado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 290);
            this.Controls.Add(this.pnlBievenida);
            this.Controls.Add(this.pnlVotacion);
            this.Controls.Add(this.pnlFinal);
            this.Name = "FrmVotoGuiado";
            this.RightToLeftLayout = true;
            this.Text = "Registrar Voto";
            this.pnlBievenida.ResumeLayout(false);
            this.pnlBievenida.PerformLayout();
            this.pnlVotacion.ResumeLayout(false);
            this.pnlVotacion.PerformLayout();
            this.grpEleccion.ResumeLayout(false);
            this.grpEleccion.PerformLayout();
            this.pnlFinal.ResumeLayout(false);
            this.pnlFinal.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnEmpezar;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label lblDui;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnlBievenida;
        private System.Windows.Forms.Panel pnlVotacion;
        private System.Windows.Forms.Button btnSiguiente;
        private System.Windows.Forms.GroupBox grpEleccion;
        private System.Windows.Forms.RadioButton rbVotoBlanco;
        private System.Windows.Forms.RadioButton rbVotoValido;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbCandidatos;
        private System.Windows.Forms.Label lblTipoEleccion;
        private System.Windows.Forms.Panel pnlFinal;
        private System.Windows.Forms.Button btnaFin;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
    }
}