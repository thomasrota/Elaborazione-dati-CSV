﻿namespace Elaborazione_dati_CSV
{
    partial class Form1
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.agg = new System.Windows.Forms.Button();
            this.contacampi = new System.Windows.Forms.Button();
            this.RecordLenght = new System.Windows.Forms.Button();
            this.Rcamp = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // agg
            // 
            this.agg.Location = new System.Drawing.Point(12, 48);
            this.agg.Name = "agg";
            this.agg.Size = new System.Drawing.Size(234, 23);
            this.agg.TabIndex = 0;
            this.agg.Text = "Aggiunta \'Mio valore\' e \'Cancellazione logica\'";
            this.agg.UseVisualStyleBackColor = true;
            this.agg.Click += new System.EventHandler(this.agg_Click);
            // 
            // contacampi
            // 
            this.contacampi.Location = new System.Drawing.Point(13, 78);
            this.contacampi.Name = "contacampi";
            this.contacampi.Size = new System.Drawing.Size(233, 23);
            this.contacampi.TabIndex = 1;
            this.contacampi.Text = "Numero Campi";
            this.contacampi.UseVisualStyleBackColor = true;
            this.contacampi.Click += new System.EventHandler(this.contacampi_Click);
            // 
            // RecordLenght
            // 
            this.RecordLenght.Location = new System.Drawing.Point(12, 107);
            this.RecordLenght.Name = "RecordLenght";
            this.RecordLenght.Size = new System.Drawing.Size(234, 23);
            this.RecordLenght.TabIndex = 2;
            this.RecordLenght.Text = "Lunghezza massima record";
            this.RecordLenght.UseVisualStyleBackColor = true;
            this.RecordLenght.Click += new System.EventHandler(this.RecordLenght_Click);
            // 
            // Rcamp
            // 
            this.Rcamp.Location = new System.Drawing.Point(12, 184);
            this.Rcamp.Name = "Rcamp";
            this.Rcamp.Size = new System.Drawing.Size(101, 23);
            this.Rcamp.TabIndex = 3;
            this.Rcamp.Text = "Ricerca Campo";
            this.Rcamp.UseVisualStyleBackColor = true;
            this.Rcamp.Click += new System.EventHandler(this.Rcamp_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(13, 158);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Rcamp);
            this.Controls.Add(this.RecordLenght);
            this.Controls.Add(this.contacampi);
            this.Controls.Add(this.agg);
            this.Name = "Form1";
            this.Text = "Elaborazione dati CSV";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button agg;
        private System.Windows.Forms.Button contacampi;
        private System.Windows.Forms.Button RecordLenght;
        private System.Windows.Forms.Button Rcamp;
        private System.Windows.Forms.TextBox textBox1;
    }
}

