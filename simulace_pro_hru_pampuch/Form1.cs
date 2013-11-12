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
using osoba_strazny_lump_jini;

namespace test_app_hledani_cesty
{
  public partial class Form1 : Form
  {
      private Base_Osoba current_osoba = null;
      private List<Base_Osoba> osoby = null;
      private Base_Osoba[] pole_osoby;

      private enum emod_app { neuve, edit, simulate };
      private emod_app mod_app;

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
      
      //Nacti_Level("./level/level3.txt");
      Nacti_Level("../level/level3.txt");
      kPlatno.Width = bludiste.max_x * (int)kStrana_Policka.Value;
      kPlatno.Height = bludiste.max_y * (int)kStrana_Policka.Value;
      //Hledej = new Find_Best_Path();
      Hledej = new Find_Best_Path_towidth();
        
      /// vytvori pocatecni nastaveni pro simulovane osoby
      Base_Osoba pom;
      osoby = new List<Base_Osoba>();
      pom = new Osoba_Lump2(0,5);
      //pom = new Osoba_Lump(0, 5);
      
      pom.Bludiste = bludiste;
      osoby.Add(pom);

      /*pom = new Osoba_Lump(11, 9);

      pom.Bludiste = bludiste;
      osoby.Add(pom);*/
      pom = new Osoba_Strazny(5,0);

      pom.Bludiste = bludiste;
      osoby.Add(pom);

      pom = new Osoba_Strazny(bludiste.max_x-1, 0);
      pom.Bludiste = bludiste;
      osoby.Add(pom);
      pole_osoby = osoby.ToArray();
      

      foreach(Base_Osoba i in osoby)
          i.Others_in_Bludiste = pole_osoby;

      /// naplni seznam startovnimi hodnotami
      Napln_List_Osob();

      mod_app = emod_app.edit;
      tabControl1.GetControl(1).Enabled = false;
      tabControl1.GetControl(0).Enabled = true;

    }

      private void Napln_List_Osob()
      {
          int lump = 1;
          int strazny = 1;
          foreach (Base_Osoba o in osoby)
          {
              if (o.osoba == Base_Osoba.typ_osoby.lump)
              {
                  klsSimul_Osoby.Items.Add("lump " + lump.ToString());
                  lump++;  
              }
              if (o.osoba == Base_Osoba.typ_osoby.strazny)
              {
                  klsSimul_Osoby.Items.Add("strazny " + lump.ToString());
                  strazny++;
              }
          }

          //current_osoba = pole_osoby[0];
          klsSimul_Osoby.SelectedIndex = 0;
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

      private void Paint_Osobu(PaintEventArgs e,sPoint pozice, Base_Osoba.typ_osoby typ, int poradi)
      {
          Brush pozadi;
          int strana_policka = (int)kStrana_Policka.Value;

          /// vybere pozadi policka
          switch (typ)
          {
              case Base_Osoba.typ_osoby.lump: pozadi = Brushes.Olive; break;
              case Base_Osoba.typ_osoby.strazny: pozadi = Brushes.Navy; break;
              default : pozadi = Brushes.Black; break;
          }
          /// nakresli ho
          if (pozice.y >= 0 && pozice.y >= 0 )
          {
              e.Graphics.FillRectangle(pozadi, pozice.x * strana_policka,
                                     pozice.y * strana_policka, strana_policka, strana_policka);
          }

          Font font = new Font("Arial", 16);
          
          e.Graphics.DrawString(poradi.ToString(), font, Brushes.White,  pozice.x * strana_policka,
                                           pozice.y * strana_policka);
          
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

    int lump = 0;
    int strazny = 0;
    /// zobrazi vsechny osoby v bludisti
    foreach (Base_Osoba o in osoby)
    {
        if (o.osoba == Base_Osoba.typ_osoby.lump)
        {
            Paint_Osobu(e,new sPoint(o.X,o.Y),o.osoba,lump);
            lump++;
        }
        if (o.osoba == Base_Osoba.typ_osoby.strazny)
        {
            Paint_Osobu(e,new sPoint(o.X,o.Y),o.osoba,strazny);
            strazny++;
        }
    }

      /// yobrayi policko pocatku a konce
      if (current_osoba.X >= 0 && current_osoba.Y >= 0 && mod_app == emod_app.edit)
      {
          e.Graphics.FillRectangle(Brushes.Red, current_osoba.X * strana_policka,
                                 current_osoba.Y * strana_policka, strana_policka, strana_policka);
      }/*
      if (end.x >= 0 && end.y >= 0)
      {
        e.Graphics.FillRectangle(Brushes.Purple, end.x * strana_policka,
                                 end.y * strana_policka, strana_policka,strana_policka);
      }*/

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

     // kPlatno.Refresh();
    }

    private void kPlatno_MouseDown(object sender, MouseEventArgs e)
    {

      if (e.Button == MouseButtons.Left)
      {

        int x = e.X / (int)kStrana_Policka.Value;
        int y = e.Y / (int)kStrana_Policka.Value;
        if (bludiste[x, y] == 0)
        {
            current_osoba.X = x;
            current_osoba.Y = y;
            kX.Text = current_osoba.X.ToString();
            kY.Text = current_osoba.Y.ToString();
        }
            
      }
      /*if (e.Button == MouseButtons.Right)
      {
        end.x = e.X / (int)kStrana_Policka.Value;
        end.y = e.Y / (int)kStrana_Policka.Value;
      }*/

      kPlatno.Invalidate();

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
        if (mod_app == emod_app.edit)
        {
            mod_app = emod_app.simulate;
            tabControl1.GetControl(1).Enabled = true;
            tabControl1.GetControl(0).Enabled = false;
            tabControl1.SelectedIndex = 1;
            kbStart.Text = "Start";
            kbStop.Visible = true;
            kPlatno.Invalidate();
        }
        else if (mod_app == emod_app.simulate && kbStart.Text == "Start")
        {
            kbStart.Text = "Stop";
            kbStop.Enabled = false;

            ktTimer.Enabled = true;
        }
        else if (mod_app == emod_app.simulate && kbStart.Text == "Stop")
        {
            kbStart.Text = "Start";
            kbStop.Enabled = true;
            ktTimer.Enabled = false;
        }

      /*kbStart.Enabled = false;
      ktTimer.Enabled = true;
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

      /*DateTime time = DateTime.Now;

      Hledej.Set_Size_Bludiste(bludiste.max_x, bludiste.max_y);
      Hledej.Set_Copy_Bludiste(bludiste);
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
      /*kPlatno.Refresh();
      klCas.Text = mm.ToString();
      klPocet_Vnoreni.Text = Hledej.Pocet_Vnoreni.ToString();
      //klNej_Cesta.Text = Convert.ToString(Hledej.pNej_cesta.Count - 1);
      kbStart.Enabled = true;
      //ktTimer.Enabled = false;
      */
    }

    private void ktTimer_Tick(object sender, EventArgs e)
    {
        foreach (Base_Osoba i in osoby)
        {
            i.AI_Thinking_Resolution();
            i.Move_To_Another_Point_Way();
        }
  
      kPlatno.Invalidate();
    }

      private void klsSimul_Osoby_SelectedIndexChanged(object sender, EventArgs e)
      {
          current_osoba = pole_osoby[klsSimul_Osoby.SelectedIndex];
          if (current_osoba.osoba == Base_Osoba.typ_osoby.lump)
              kcbOsoba.SelectedIndex = 0;
          if (current_osoba.osoba == Base_Osoba.typ_osoby.strazny)
              kcbOsoba.SelectedIndex = 1;
          kX.Text = current_osoba.X.ToString();
          kY.Text = current_osoba.Y.ToString();

          kPlatno.Invalidate();
      }

      private void kbStop_Click(object sender, EventArgs e)
      {
          mod_app = emod_app.edit;
          tabControl1.GetControl(1).Enabled = false;
          tabControl1.GetControl(0).Enabled = true;
          tabControl1.SelectedIndex = 0;
          kbStart.Text = "Simulate";
          kbStop.Visible = false;
          kPlatno.Invalidate();
      }

      private void knSpeed_Simulation_ValueChanged(object sender, EventArgs e)
      {
          ktTimer.Interval = 1000 / (int)knSpeed_Simulation.Value;
      }
  }
}