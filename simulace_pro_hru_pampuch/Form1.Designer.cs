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
            this.klsSimul_Osoby = new System.Windows.Forms.ListBox();
            this.kcbOsoba = new System.Windows.Forms.ComboBox();
            this.kY = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.kX = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.knSpeed_Simulation = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.kStrana_Policka)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.kPlatno)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.knSpeed_Simulation)).BeginInit();
            this.SuspendLayout();
            // 
            // kStrana_Policka
            // 
            this.kStrana_Policka.Location = new System.Drawing.Point(636, 58);
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
            this.kbStart.Location = new System.Drawing.Point(503, 12);
            this.kbStart.Name = "kbStart";
            this.kbStart.Size = new System.Drawing.Size(69, 24);
            this.kbStart.TabIndex = 3;
            this.kbStart.Text = "simulate";
            this.kbStart.UseVisualStyleBackColor = true;
            this.kbStart.Click += new System.EventHandler(this.kbStart_Click);
            // 
            // kbStop
            // 
            this.kbStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.kbStop.Location = new System.Drawing.Point(597, 12);
            this.kbStop.Name = "kbStop";
            this.kbStop.Size = new System.Drawing.Size(98, 24);
            this.kbStop.TabIndex = 4;
            this.kbStop.Text = "end_simulate";
            this.kbStop.UseVisualStyleBackColor = true;
            this.kbStop.Visible = false;
            this.kbStop.Click += new System.EventHandler(this.kbStop_Click);
            // 
            // klDoba_Behu
            // 
            this.klDoba_Behu.AutoSize = true;
            this.klDoba_Behu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.klDoba_Behu.ForeColor = System.Drawing.Color.Red;
            this.klDoba_Behu.Location = new System.Drawing.Point(500, 46);
            this.klDoba_Behu.Name = "klDoba_Behu";
            this.klDoba_Behu.Size = new System.Drawing.Size(78, 16);
            this.klDoba_Behu.TabIndex = 5;
            this.klDoba_Behu.Text = "Doba běhu:";
            // 
            // klCas
            // 
            this.klCas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.klCas.ForeColor = System.Drawing.Color.Navy;
            this.klCas.Location = new System.Drawing.Point(500, 62);
            this.klCas.Name = "klCas";
            this.klCas.Size = new System.Drawing.Size(103, 16);
            this.klCas.TabIndex = 6;
            this.klCas.Text = "0";
            this.klCas.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ktTimer
            // 
            this.ktTimer.Interval = 1000;
            this.ktTimer.Tick += new System.EventHandler(this.ktTimer_Tick);
            // 
            // klPocet_Vnoreni
            // 
            this.klPocet_Vnoreni.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.klPocet_Vnoreni.ForeColor = System.Drawing.Color.Navy;
            this.klPocet_Vnoreni.Location = new System.Drawing.Point(499, 94);
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
            this.label2.Location = new System.Drawing.Point(499, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "počet vnoření:";
            // 
            // klsSimul_Osoby
            // 
            this.klsSimul_Osoby.FormattingEnabled = true;
            this.klsSimul_Osoby.Location = new System.Drawing.Point(15, 19);
            this.klsSimul_Osoby.Name = "klsSimul_Osoby";
            this.klsSimul_Osoby.Size = new System.Drawing.Size(128, 69);
            this.klsSimul_Osoby.TabIndex = 13;
            this.klsSimul_Osoby.SelectedIndexChanged += new System.EventHandler(this.klsSimul_Osoby_SelectedIndexChanged);
            // 
            // kcbOsoba
            // 
            this.kcbOsoba.FormattingEnabled = true;
            this.kcbOsoba.Items.AddRange(new object[] {
            "lump",
            "polda"});
            this.kcbOsoba.Location = new System.Drawing.Point(15, 105);
            this.kcbOsoba.Name = "kcbOsoba";
            this.kcbOsoba.Size = new System.Drawing.Size(127, 21);
            this.kcbOsoba.TabIndex = 14;
            // 
            // kY
            // 
            this.kY.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.kY.ForeColor = System.Drawing.Color.Navy;
            this.kY.Location = new System.Drawing.Point(38, 173);
            this.kY.Name = "kY";
            this.kY.Size = new System.Drawing.Size(38, 16);
            this.kY.TabIndex = 18;
            this.kY.Text = "0";
            this.kY.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(24, 173);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 16);
            this.label3.TabIndex = 17;
            this.label3.Text = "Y:";
            // 
            // kX
            // 
            this.kX.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.kX.ForeColor = System.Drawing.Color.Navy;
            this.kX.Location = new System.Drawing.Point(48, 141);
            this.kX.Name = "kX";
            this.kX.Size = new System.Drawing.Size(28, 16);
            this.kX.TabIndex = 16;
            this.kX.Text = "0";
            this.kX.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(25, 141);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(19, 16);
            this.label5.TabIndex = 15;
            this.label5.Text = "X:";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(499, 113);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(200, 235);
            this.tabControl1.TabIndex = 19;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.klsSimul_Osoby);
            this.tabPage2.Controls.Add(this.kY);
            this.tabPage2.Controls.Add(this.kcbOsoba);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.kX);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(192, 209);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "edit";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.knSpeed_Simulation);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(192, 209);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "simulace";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // knSpeed_Simulation
            // 
            this.knSpeed_Simulation.Location = new System.Drawing.Point(13, 17);
            this.knSpeed_Simulation.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.knSpeed_Simulation.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.knSpeed_Simulation.Name = "knSpeed_Simulation";
            this.knSpeed_Simulation.Size = new System.Drawing.Size(74, 20);
            this.knSpeed_Simulation.TabIndex = 0;
            this.knSpeed_Simulation.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.knSpeed_Simulation.ValueChanged += new System.EventHandler(this.knSpeed_Simulation_ValueChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(965, 352);
            this.Controls.Add(this.tabControl1);
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
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.knSpeed_Simulation)).EndInit();
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
        private System.Windows.Forms.ListBox klsSimul_Osoby;
        private System.Windows.Forms.ComboBox kcbOsoba;
        private System.Windows.Forms.Label kY;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label kX;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.NumericUpDown knSpeed_Simulation;
    }
}

