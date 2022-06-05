namespace Bluetooth
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.connec = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.search = new System.Windows.Forms.Button();
            this.chose = new System.Windows.Forms.Button();
            this.forget = new System.Windows.Forms.Button();
            this.sending = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // connec
            // 
            this.connec.Location = new System.Drawing.Point(36, 64);
            this.connec.Name = "connec";
            this.connec.Size = new System.Drawing.Size(75, 23);
            this.connec.TabIndex = 0;
            this.connec.Text = "Połącz";
            this.connec.UseVisualStyleBackColor = true;
            this.connec.Click += new System.EventHandler(this.connec_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(36, 106);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(298, 199);
            this.listBox1.TabIndex = 1;
            // 
            // search
            // 
            this.search.Location = new System.Drawing.Point(132, 342);
            this.search.Name = "search";
            this.search.Size = new System.Drawing.Size(75, 23);
            this.search.TabIndex = 2;
            this.search.Text = "Szukaj";
            this.search.UseVisualStyleBackColor = true;
            this.search.Click += new System.EventHandler(this.search_Click);
            // 
            // chose
            // 
            this.chose.Location = new System.Drawing.Point(468, 90);
            this.chose.Name = "chose";
            this.chose.Size = new System.Drawing.Size(75, 23);
            this.chose.TabIndex = 3;
            this.chose.Text = "Wybierz plik";
            this.chose.UseVisualStyleBackColor = true;
            this.chose.Click += new System.EventHandler(this.chose_Click);
            // 
            // forget
            // 
            this.forget.Location = new System.Drawing.Point(175, 64);
            this.forget.Name = "forget";
            this.forget.Size = new System.Drawing.Size(75, 23);
            this.forget.TabIndex = 4;
            this.forget.Text = "Zapomnij";
            this.forget.UseVisualStyleBackColor = true;
            this.forget.Click += new System.EventHandler(this.forget_Click);
            // 
            // sending
            // 
            this.sending.Location = new System.Drawing.Point(468, 148);
            this.sending.Name = "sending";
            this.sending.Size = new System.Drawing.Size(75, 23);
            this.sending.TabIndex = 5;
            this.sending.Text = "Wyślij";
            this.sending.UseVisualStyleBackColor = true;
            this.sending.Click += new System.EventHandler(this.sending_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(402, 268);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(177, 23);
            this.progressBar1.TabIndex = 6;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(649, 425);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.sending);
            this.Controls.Add(this.forget);
            this.Controls.Add(this.chose);
            this.Controls.Add(this.search);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.connec);
            this.Name = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button connect;
        private System.Windows.Forms.Button connec;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button search;
        private System.Windows.Forms.Button chose;
        private System.Windows.Forms.Button forget;
        private System.Windows.Forms.Button sending;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

