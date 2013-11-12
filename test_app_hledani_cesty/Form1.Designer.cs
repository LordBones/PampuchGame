namespace test_app_hledani_cesty
{
    partial class Form1
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
          this.components = new System.ComponentModel.Container();
          this.kStrana_Policka = new System.Windows.Forms.NumericUpDown();
          this.panel1 = new System.Windows.Forms.Panel();
          this.kPlatno = new System.Windows.Forms.PictureBox();
          this.kbStart = new System.Windows.Forms.Button();
          this.kbStop = new System.Windows.Forms.Button();
          this.klDoba_Behu = new System.Windows.Forms.Label();
          this.klCas = new System.Windows.Forms.Label();
          this.ktTimer = new System.Windows.Forms.Timer(this.components);
          this.klPocet_Vnoreni = new System.Windows.Forms.Label();
          this.label2 = new System.Windows.Forms.Label();
          this.klNej_Cesta = new System.Windows.Forms.Label();
          this.label3 = new System.Windows.Forms.Label();
          this.kcbAnimuj = new System.Windows.Forms.CheckBox();
          this.kckPlynule_Zobraz = new System.Windows.Forms.CheckBox();
          ((System.ComponentModel.ISupportInitialize)(this.kStrana_Policka)).BeginInit();
          this.panel1.SuspendLayout();
          ((System.ComponentModel.ISupportInitialize)(this.kPlatno)).BeginInit();
          this.SuspendLayout();
          // 
          // kStrana_Policka
          // 
          this.kStrana_Policka.Location = new System.Drawing.Point(499, 59);
          this.kStrana_Policka.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
          this.kStrana_Policka.Name = "kStrana_Policka";
          this.kStrana_Policka.Size = new System.Drawing.Size(63, 20);
          this.kStrana_Policka.TabIndex = 1;
          this.kStrana_Policka.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
          this.kStrana_Policka.ValueChanged += new System.EventHandler(this.kStrana_Policka_ValueChanged);
          // 
          // panel1
          // 
          this.panel1.AutoScroll = true;
          this.panel1.Controls.Add(this.kPlatno);
          this.panel1.Location = new System.Drawing.Point(0, 3);
          this.panel1.Name = "panel1";
          this.panel1.Size = new System.Drawing.Size(493, 337);
          this.panel1.TabIndex = 2;
          // 
          // kPlatno
          // 
          this.kPlatno.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
          this.kPlatno.Location = new System.Drawing.Point(0, 0);
          this.kPlatno.Name = "kPlatno";
          this.kPlatno.Size = new System.Drawing.Size(343, 264);
          this.kPlatno.TabIndex = 1;
          this.kPlatno.TabStop = false;
          this.kPlatno.MouseDown += new System.Windows.Forms.MouseEventHandler(this.kPlatno_MouseDown);
          this.kPlatno.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
          // 
          // kbStart
          // 
          this.kbStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
          this.kbStart.Location = new System.Drawing.Point(499, 13);
          this.kbStart.Name = "kbStart";
          this.kbStart.Size = new System.Drawing.Size(61, 24);
          this.kbStart.TabIndex = 3;
          this.kbStart.Text = "start";
          this.kbStart.UseVisualStyleBackColor = true;
          this.kbStart.Click += new System.EventHandler(this.kbStart_Click);
          // 
          // kbStop
          // 
          this.kbStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
          this.kbStop.Location = new System.Drawing.Point(574, 13);
          this.kbStop.Name = "kbStop";
          this.kbStop.Size = new System.Drawing.Size(61, 24);
          this.kbStop.TabIndex = 4;
          this.kbStop.Text = "stop";
          this.kbStop.UseVisualStyleBackColor = true;
          // 
          // klDoba_Behu
          // 
          this.klDoba_Behu.AutoSize = true;
          this.klDoba_Behu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
          this.klDoba_Behu.ForeColor = System.Drawing.Color.Red;
          this.klDoba_Behu.Location = new System.Drawing.Point(500, 97);
          this.klDoba_Behu.Name = "klDoba_Behu";
          this.klDoba_Behu.Size = new System.Drawing.Size(78, 16);
          this.klDoba_Behu.TabIndex = 5;
          this.klDoba_Behu.Text = "Doba běhu:";
          // 
          // klCas
          // 
          this.klCas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
          this.klCas.ForeColor = System.Drawing.Color.Navy;
          this.klCas.Location = new System.Drawing.Point(500, 113);
          this.klCas.Name = "klCas";
          this.klCas.Size = new System.Drawing.Size(103, 16);
          this.klCas.TabIndex = 6;
          this.klCas.Text = "0";
          this.klCas.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
          // 
          // ktTimer
          // 
          this.ktTimer.Tick += new System.EventHandler(this.ktTimer_Tick);
          // 
          // klPocet_Vnoreni
          // 
          this.klPocet_Vnoreni.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
          this.klPocet_Vnoreni.ForeColor = System.Drawing.Color.Navy;
          this.klPocet_Vnoreni.Location = new System.Drawing.Point(499, 145);
          this.klPocet_Vnoreni.Name = "klPocet_Vnoreni";
          this.klPocet_Vnoreni.Size = new System.Drawing.Size(103, 16);
          this.klPocet_Vnoreni.TabIndex = 8;
          this.klPocet_Vnoreni.Text = "0";
          this.klPocet_Vnoreni.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
          // 
          // label2
          // 
          this.label2.AutoSize = true;
          this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
          this.label2.ForeColor = System.Drawing.Color.Red;
          this.label2.Location = new System.Drawing.Point(499, 129);
          this.label2.Name = "label2";
          this.label2.Size = new System.Drawing.Size(92, 16);
          this.label2.TabIndex = 7;
          this.label2.Text = "počet vnoření:";
          // 
          // klNej_Cesta
          // 
          this.klNej_Cesta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
          this.klNej_Cesta.ForeColor = System.Drawing.Color.Navy;
          this.klNej_Cesta.Location = new System.Drawing.Point(500, 177);
          this.klNej_Cesta.Name = "klNej_Cesta";
          this.klNej_Cesta.Size = new System.Drawing.Size(103, 16);
          this.klNej_Cesta.TabIndex = 10;
          this.klNej_Cesta.Text = "0";
          this.klNej_Cesta.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
          // 
          // label3
          // 
          this.label3.AutoSize = true;
          this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
          this.label3.ForeColor = System.Drawing.Color.Red;
          this.label3.Location = new System.Drawing.Point(500, 161);
          this.label3.Name = "label3";
          this.label3.Size = new System.Drawing.Size(65, 16);
          this.label3.TabIndex = 9;
          this.label3.Text = "nej cesta:";
          // 
          // kcbAnimuj
          // 
          this.kcbAnimuj.AutoSize = true;
          this.kcbAnimuj.Location = new System.Drawing.Point(503, 297);
          this.kcbAnimuj.Name = "kcbAnimuj";
          this.kcbAnimuj.Size = new System.Drawing.Size(56, 17);
          this.kcbAnimuj.TabIndex = 11;
          this.kcbAnimuj.Text = "animuj";
          this.kcbAnimuj.UseVisualStyleBackColor = true;
          // 
          // kckPlynule_Zobraz
          // 
          this.kckPlynule_Zobraz.AutoSize = true;
          this.kckPlynule_Zobraz.Checked = true;
          this.kckPlynule_Zobraz.CheckState = System.Windows.Forms.CheckState.Checked;
          this.kckPlynule_Zobraz.Location = new System.Drawing.Point(503, 274);
          this.kckPlynule_Zobraz.Name = "kckPlynule_Zobraz";
          this.kckPlynule_Zobraz.Size = new System.Drawing.Size(99, 17);
          this.kckPlynule_Zobraz.TabIndex = 12;
          this.kckPlynule_Zobraz.Text = "Plynule_Zobraz";
          this.kckPlynule_Zobraz.UseVisualStyleBackColor = true;
          // 
          // Form1
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.BackColor = System.Drawing.SystemColors.Control;
          this.ClientSize = new System.Drawing.Size(647, 352);
          this.Controls.Add(this.kckPlynule_Zobraz);
          this.Controls.Add(this.kcbAnimuj);
          this.Controls.Add(this.klNej_Cesta);
          this.Controls.Add(this.label3);
          this.Controls.Add(this.klPocet_Vnoreni);
          this.Controls.Add(this.label2);
          this.Controls.Add(this.klCas);
          this.Controls.Add(this.klDoba_Behu);
          this.Controls.Add(this.kbStop);
          this.Controls.Add(this.kbStart);
          this.Controls.Add(this.panel1);
          this.Controls.Add(this.kStrana_Policka);
          this.Name = "Form1";
          this.RightToLeft = System.Windows.Forms.RightToLeft.No;
          this.Text = "Nejkratší cesta  0.1";
          ((System.ComponentModel.ISupportInitialize)(this.kStrana_Policka)).EndInit();
          this.panel1.ResumeLayout(false);
          ((System.ComponentModel.ISupportInitialize)(this.kPlatno)).EndInit();
          this.ResumeLayout(false);
          this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown kStrana_Policka;
      private System.Windows.Forms.Panel panel1;
      private System.Windows.Forms.PictureBox kPlatno;
      private System.Windows.Forms.Button kbStart;
      private System.Windows.Forms.Button kbStop;
      private System.Windows.Forms.Label klDoba_Behu;
      private System.Windows.Forms.Label klCas;
      private System.Windows.Forms.Timer ktTimer;
      private System.Windows.Forms.Label klPocet_Vnoreni;
      private System.Windows.Forms.Label label2;
      private System.Windows.Forms.Label klNej_Cesta;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.CheckBox kcbAnimuj;
      private System.Windows.Forms.CheckBox kckPlynule_Zobraz;
    }
}

