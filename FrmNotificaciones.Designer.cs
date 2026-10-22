namespace SistemaVotos
{
    partial class FrmNotificaciones
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
            this.rtxAlertas = new System.Windows.Forms.RichTextBox();
            this.rtxNotificaciones = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Requiere Atencion";
            // 
            // rtxAlertas
            // 
            this.rtxAlertas.Location = new System.Drawing.Point(26, 49);
            this.rtxAlertas.Name = "rtxAlertas";
            this.rtxAlertas.ReadOnly = true;
            this.rtxAlertas.Size = new System.Drawing.Size(513, 131);
            this.rtxAlertas.TabIndex = 1;
            this.rtxAlertas.Text = "";
            // 
            // rtxNotificaciones
            // 
            this.rtxNotificaciones.Location = new System.Drawing.Point(26, 209);
            this.rtxNotificaciones.Name = "rtxNotificaciones";
            this.rtxNotificaciones.ReadOnly = true;
            this.rtxNotificaciones.Size = new System.Drawing.Size(513, 283);
            this.rtxNotificaciones.TabIndex = 3;
            this.rtxNotificaciones.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 193);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Notificaciones";
            // 
            // btnActualizar
            // 
            this.btnActualizar.Location = new System.Drawing.Point(464, 23);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(75, 23);
            this.btnActualizar.TabIndex = 4;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = true;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // FrmNotificaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 528);
            this.Controls.Add(this.btnActualizar);
            this.Controls.Add(this.rtxNotificaciones);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rtxAlertas);
            this.Controls.Add(this.label1);
            this.Name = "FrmNotificaciones";
            this.Text = "FrmNotificaciones";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox rtxAlertas;
        private System.Windows.Forms.RichTextBox rtxNotificaciones;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnActualizar;
    }
}