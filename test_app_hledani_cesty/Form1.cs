using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using System.Drawing;
using System.IO;
using System.Threading;

using System.Text;
using System.Windows.Forms;
using Hledani_Nejkratsi_Cesty;

namespace test_app_hledani_cesty
{
  public partial class Form1 : Form
  {
    internal sPoint start, end;
    internal sMatrix bludiste;
    internal sMatrix bludiste_zaloha;
    //internal Find_Best_Path Hledej;
    internal Find_Best_Path_towidth Hledej;

    /// <summary>
    /// pomocna struktur kterou se predavaji data timeru
    /// </summary>
    public struct sThread_Timer
    {
      public Form1 form;
      //public Find_Best_Path Hledej;
      //public Find_Best_Path_towidth Hledej;

      public PictureBox platno;
    }

    public Form1()
    {
      InitializeComponent();
      start.SetXY(1, 1);
      start.SetXY(4, 4);
      
      //Nacti_Level("./level/level3.txt");
      Nacti_Level("../level/level3.txt");
      kPlatno.Width = bludiste.max_x * (int)kStrana_Policka.Value;
      kPlatno.Height = bludiste.max_y * (int)kStrana_Policka.Value;
      //Hledej = new Find_Best_Path();
      Hledej = new Find_Best_Path_towidth();
        
    }

    private void Nacti_Level(string name)
    {
      TextReader input = new StreamReader(name);
      string [] pom = input.ReadLine().Split(' ');

      bludiste.max_x = Int32.Parse(pom[0]);
      bludiste.max_y = Int32.Parse(pom[1]);
      bludiste.pole = new byte[bludiste.max_x, bludiste.max_y];
      bludiste_zaloha.pole = new byte[bludiste.max_x, bludiste.max_y];

      /// nacte jednotlive raddky bludiste
      try
      {
        for (int k = 0; k < bludiste.max_y; k++)
        {
          char [] radek = input.ReadLine().ToCharArray();
          for (int m = 0; m < bludiste.max_x; m++)
          {
            bludiste[m, k] = (byte)(radek[m] - '0');
          }
        }
      }
      finally
      {
        input.Close();
      }

    }

    private void pictureBox1_Paint(object sender, PaintEventArgs e)
    {
      int strana_policka = (int)kStrana_Policka.Value;

      /// nakresli level
      for (int i = 0; i < bludiste.max_y; i ++)
      for (int k = 0; k < bludiste.max_x; k++)
        if (bludiste[k, i] == 1)  e.Graphics.FillRectangle(Brushes.Green, k * strana_policka, i * strana_policka, strana_policka, strana_policka);
        else if (bludiste[k, i] == 2)  e.Graphics.FillRectangle(Brushes.Navy, k * strana_policka, i * strana_policka, strana_policka, strana_policka);
        else if (bludiste[k, i] == 3)  e.Graphics.FillRectangle(Brushes.Purple, k * strana_policka, i * strana_policka, strana_policka, strana_policka);
        else if (bludiste[k, i] == 4) e.Graphics.FillRectangle(Brushes.Orange, k * strana_policka, i * strana_policka, strana_policka, strana_policka);
        
    
      /// yobrayi policko pocatku a konce
      if (start.x >= 0 && start.y >= 0)
      {
        e.Graphics.FillRectangle(Brushes.Red, start.x * strana_policka, 
                                 start.y * strana_policka,strana_policka,strana_policka);
      }
      if (end.x >= 0 && end.y >= 0)
      {
        e.Graphics.FillRectangle(Brushes.Purple, end.x * strana_policka,
                                 end.y * strana_policka, strana_policka,strana_policka);
      }

      /// vzkresli mrizku
      for (int i = strana_policka - 1;i < kPlatno.Width; i+= strana_policka)
        e.Graphics.DrawLine(new Pen(Brushes.Gray, 1), i, 0, i, kPlatno.Height);
      for (int i = strana_policka - 1; i < kPlatno.Height; i += strana_policka)
        e.Graphics.DrawLine(new Pen(Brushes.Gray, 1), 0, i, kPlatno.Width, i);
    }

    public void Pom_Pro_Zobrazeni_Prubehu(sPoint [] nej_cesta)
    {
      for (int y = 0; y < bludiste.max_y; y++)
        for (int x = 0; x < bludiste.max_x; x++)
        {
          bludiste[x, y] = Hledej.pBludiste[x, y];
        }

      //sPoint[] nej_cesta = Hledej.pNej_cesta.ToArray();
      foreach (sPoint i in nej_cesta)
        bludiste[i.x, i.y] = 4;

      kPlatno.Refresh();
    }

    private void kPlatno_MouseDown(object sender, MouseEventArgs e)
    {

      if (e.Button == MouseButtons.Left)
      {
        start.x = e.X / (int)kStrana_Policka.Value;
        start.y = e.Y / (int)kStrana_Policka.Value;
      }
      if (e.Button == MouseButtons.Right)
      {
        end.x = e.X / (int)kStrana_Policka.Value;
        end.y = e.Y / (int)kStrana_Policka.Value;
      }

      if(kckPlynule_Zobraz.Checked == true)
        kbStart_Click(sender, e);
      kPlatno.Refresh();

    }

    private void kStrana_Policka_ValueChanged(object sender, EventArgs e)
    {
      kPlatno.Width = bludiste.max_x * (int)kStrana_Policka.Value;
      kPlatno.Height = bludiste.max_y * (int)kStrana_Policka.Value;

      kPlatno.Refresh();
    }

    static void Thread_Timer(Object data)
    {
      /*sThread_Timer time = (sThread_Timer)data;

      for (int y = 0; y < time.form.bludiste.max_y; y++)
        for (int x = 0; x < time.form.bludiste.max_x; x++)
        {
          time.form.bludiste[x, y] = time.Hledej.pBludiste[x, y];
        }

      time.platno.Show();
      */
    }

    private void kbStart_Click(object sender, EventArgs e)
    {

      kbStart.Enabled = false;
      //ktTimer.Enabled = true;
      for (int y = 0; y < bludiste.max_y; y++)
        for (int x = 0; x < bludiste.max_x; x++)
        {
          bludiste_zaloha[x, y] = bludiste[x, y];
          if (bludiste[x, y] == 4) bludiste[x, y] = 0;
        }

      // nastavi timer pro plynule zobrayovani a rizeni behu hledani
      /*TimerCallback lTimerMethod = new TimerCallback(Thread_Timer);
      sThread_Timer timer_data = new sThread_Timer();
      timer_data.form = this;
      timer_data.Hledej = Hledej;
      timer_data.platno = kPlatno;

      System.Threading.Timer timer = new System.Threading.Timer(lTimerMethod, timer_data, 0, 100);
      */

      DateTime time = DateTime.Now;

      Hledej.Set_Size_Bludiste(bludiste.max_x, bludiste.max_y);
      Hledej.Set_Copy_Bludiste(bludiste);
      Hledej.Start = start;
      Hledej.Cil = end;
      Hledej.Animuj = kcbAnimuj.Checked;
      Hledej.Hledej(this);

      TimeSpan mm = DateTime.Now.TimeOfDay - time.TimeOfDay;

      sPoint [] nej_cesta = Hledej.pNej_cesta.ToArray();
      foreach (sPoint i in nej_cesta)
        bludiste[i.x, i.y] = 4;

      /*for (int y = 0; y < bludiste.max_y; y++)
        for (int x = 0; x < bludiste.max_x; x++)
        {
          bludiste[x, y] = bludiste_zaloha[x, y];
        }
      */
      kPlatno.Refresh();
      klCas.Text = mm.ToString();
      klPocet_Vnoreni.Text = Hledej.Pocet_Vnoreni.ToString();
      klNej_Cesta.Text = Convert.ToString(Hledej.pNej_cesta.Count - 1);
      kbStart.Enabled = true;
      //ktTimer.Enabled = false;
      
    }

    private void ktTimer_Tick(object sender, EventArgs e)
    {
      for (int y = 0; y < Hledej.pBludiste.max_y; y++)    
        for (int x = 0; x < Hledej.pBludiste.max_x; x++)
        {
          bludiste[x, y] = Hledej.pBludiste[x, y];
        }
  
      kPlatno.Refresh();
    }
  }
}