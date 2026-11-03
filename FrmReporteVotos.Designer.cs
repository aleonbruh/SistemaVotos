namespace SistemaVotos
{
    partial class FrmReporteVotos
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
            this.dgvVotos = new System.Windows.Forms.DataGridView();
            this.btnValidos = new System.Windows.Forms.Button();
            this.btnNulos = new System.Windows.Forms.Button();
            this.btnTodos = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtValidos = new System.Windows.Forms.TextBox();
            this.txtNulos = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnGenerarPDF = new System.Windows.Forms.Button();
            this.btnGenerarExcel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVotos)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvVotos
            // 
            this.dgvVotos.AllowUserToAddRows = false;
            this.dgvVotos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvVotos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvVotos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVotos.Location = new System.Drawing.Point(26, 56);
            this.dgvVotos.Name = "dgvVotos";
            this.dgvVotos.ReadOnly = true;
            this.dgvVotos.RowHeadersVisible = false;
            this.dgvVotos.Size = new System.Drawing.Size(553, 219);
            this.dgvVotos.TabIndex = 9;
            // 
            // btnValidos
            // 
            this.btnValidos.Location = new System.Drawing.Point(26, 27);
            this.btnValidos.Name = "btnValidos";
            this.btnValidos.Size = new System.Drawing.Size(92, 23);
            this.btnValidos.TabIndex = 10;
            this.btnValidos.Text = "Votos Validos";
            this.btnValidos.UseVisualStyleBackColor = true;
            this.btnValidos.Click += new System.EventHandler(this.btnValidos_Click);
            // 
            // btnNulos
            // 
            this.btnNulos.Location = new System.Drawing.Point(124, 27);
            this.btnNulos.Name = "btnNulos";
            this.btnNulos.Size = new System.Drawing.Size(92, 23);
            this.btnNulos.TabIndex = 11;
            this.btnNulos.Text = "Votos Nulos";
            this.btnNulos.UseVisualStyleBackColor = true;
            this.btnNulos.Click += new System.EventHandler(this.btnNulos_Click);
            // 
            // btnTodos
            // 
            this.btnTodos.Location = new System.Drawing.Point(226, 27);
            this.btnTodos.Name = "btnTodos";
            this.btnTodos.Size = new System.Drawing.Size(75, 23);
            this.btnTodos.TabIndex = 12;
            this.btnTodos.Text = "Todos";
            this.btnTodos.UseVisualStyleBackColor = true;
            this.btnTodos.Click += new System.EventHandler(this.btnTodos_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 291);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Votos Validos:";
            // 
            // txtValidos
            // 
            this.txtValidos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtValidos.Location = new System.Drawing.Point(103, 288);
            this.txtValidos.Name = "txtValidos";
            this.txtValidos.ReadOnly = true;
            this.txtValidos.Size = new System.Drawing.Size(100, 20);
            this.txtValidos.TabIndex = 14;
            // 
            // txtNulos
            // 
            this.txtNulos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtNulos.Location = new System.Drawing.Point(296, 288);
            this.txtNulos.Name = "txtNulos";
            this.txtNulos.ReadOnly = true;
            this.txtNulos.Size = new System.Drawing.Size(100, 20);
            this.txtNulos.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(223, 291);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Votos Nulos:";
            // 
            // btnGenerarPDF
            // 
            this.btnGenerarPDF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGenerarPDF.Location = new System.Drawing.Point(26, 322);
            this.btnGenerarPDF.Name = "btnGenerarPDF";
            this.btnGenerarPDF.Size = new System.Drawing.Size(130, 23);
            this.btnGenerarPDF.TabIndex = 17;
            this.btnGenerarPDF.Text = "Reporte General PDF";
            this.btnGenerarPDF.UseVisualStyleBackColor = true;
            this.btnGenerarPDF.Click += new System.EventHandler(this.btnGenerarPDF_Click);
            // 
            // btnGenerarExcel
            // 
            this.btnGenerarExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGenerarExcel.Location = new System.Drawing.Point(162, 322);
            this.btnGenerarExcel.Name = "btnGenerarExcel";
            this.btnGenerarExcel.Size = new System.Drawing.Size(128, 23);
            this.btnGenerarExcel.TabIndex = 18;
            this.btnGenerarExcel.Text = "Reporte General Excel";
            this.btnGenerarExcel.UseVisualStyleBackColor = true;
            this.btnGenerarExcel.Click += new System.EventHandler(this.btnGenerarExcel_Click);
            // 
            // FrmReporteVotos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 366);
            this.Controls.Add(this.btnGenerarExcel);
            this.Controls.Add(this.btnGenerarPDF);
            this.Controls.Add(this.txtNulos);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtValidos);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnTodos);
            this.Controls.Add(this.btnNulos);
            this.Controls.Add(this.btnValidos);
            this.Controls.Add(this.dgvVotos);
            this.Name = "FrmReporteVotos";
            this.Text = "Reporte General";
            ((System.ComponentModel.ISupportInitialize)(this.dgvVotos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvVotos;
        private System.Windows.Forms.Button btnValidos;
        private System.Windows.Forms.Button btnNulos;
        private System.Windows.Forms.Button btnTodos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtValidos;
        private System.Windows.Forms.TextBox txtNulos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnGenerarPDF;
        private System.Windows.Forms.Button btnGenerarExcel;
    }
}