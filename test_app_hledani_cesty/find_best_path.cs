
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using System.ComponentModel;
using test_app_hledani_cesty;
using System.IO;

namespace Hledani_Nejkratsi_Cesty
{
  

  /// <summary>
  /// trida pro hledani nejkratsi cesty v prostoru
  /// </summary>
  public class Find_Best_Path : base_find_best_path
  {
    /// <summary>
    /// struktura jednoho zaznamu na zasobniku, ktery nahrazuje rekurzivni volani
    /// smer urcuje kudy pokracovalo dalsi vnoreni
    /// </summary>
    private struct pItem_Zasobnik_Hledani
    {
      public sPoint bod;
      public byte smer;

      public pItem_Zasobnik_Hledani(sPoint pbod, byte psmer)
      { bod = pbod; smer = psmer; }
    }

    private Stack<pItem_Zasobnik_Hledani> pZasobnik = null;
    
    /// <summary>
    /// slouzi k rychle detekci zdali tato cast nej cesty uz nebyla nalezena
    /// </summary>
    private int [,]  pPom_Nej_Cesta;

    

    /// <summary>
    /// pomocne pole pro rychle pocitani noveho policka
    /// </summary>
    readonly int[] pSmerX = { 0, 1, 0, -1 };
    readonly int[] pSmerY = { -1, 0, 1, 0 };

    
    /// <summary>
    /// indexy do pole s poradim voleni smeru podle toho dle je cil
    /// l- levo, p - pravo, n - nahoru, d - dolu.  
    /// lnl - cil je vlevo nahore a jeho vetsi vzdalenost je vlevo
    /// </summary>
    private enum pPoradi_Vnoreni {LNL,LNN, LDL,LDD, PNP,PNN, PDP,PDD}
    //private enum pPoradi_Vnoreni { LNL, LNN, LDL, LDD, PNP, PNN, PDP, PDD }
    readonly private pSmer[,] pPriority_vnoreni =  {{pSmer.doleva,pSmer.nahoru,pSmer.dolu,pSmer.doprava},
                                             {pSmer.nahoru,pSmer.doleva,pSmer.doprava,pSmer.dolu},
                                             {pSmer.doleva,pSmer.dolu,pSmer.nahoru,pSmer.doprava},
                                             {pSmer.dolu,pSmer.doleva,pSmer.doprava,pSmer.nahoru},

                                             {pSmer.doprava,pSmer.nahoru,pSmer.dolu,pSmer.doleva},
                                             {pSmer.nahoru,pSmer.doprava,pSmer.doleva,pSmer.dolu},
                                             {pSmer.doprava,pSmer.dolu,pSmer.nahoru,pSmer.doleva},
                                             {pSmer.dolu,pSmer.doprava,pSmer.doleva,pSmer.nahoru}};

   
    public Find_Best_Path()
    {
      Set_Size_Bludiste(0, 0);
      pZasobnik = new Stack<pItem_Zasobnik_Hledani>(0);
      pNej_cesta = new Stack<sPoint>(0); 

    }

    /// <summary>
    /// nastavi novy rozmer bludiste ve kterem se ma hledat
    /// </summary>
    public void Set_Size_Bludiste(int x, int y)
    {
      pBludiste.pole = new byte[x, y];
      pBludiste.max_x = x;
      pBludiste.max_y = y;
      pPom_Nej_Cesta = new int[x, y];
    }

    /// <summary>
    /// vraci cislo na pole poradi vnoreni, urcuje to z aktualni pozice a cile
    /// </summary>
    /// <param name="bod"> bod ktery se momentalne zpracovava</param>
    /// <returns></returns>
    private pPoradi_Vnoreni Get_Poradi_Vnoreni(sPoint bod)
    {
      int deltaX = Math.Abs(bod.x - pCil.x);
      int deltaY = Math.Abs(bod.y - pCil.y);
 

      if (bod.x>=pCil.x && bod.y>= pCil.y)
        return ((deltaX>deltaY))? pPoradi_Vnoreni.LNL : pPoradi_Vnoreni.LNN;
      if (bod.x>=pCil.x && bod.y<= pCil.y)
        return ((deltaX>deltaY))? pPoradi_Vnoreni.LDL : pPoradi_Vnoreni.LDD;

      if (bod.x <= pCil.x && bod.y >= pCil.y)
        return ((deltaX > deltaY)) ? pPoradi_Vnoreni.PNP : pPoradi_Vnoreni.PNN;
      if (bod.x <= pCil.x && bod.y <= pCil.y)
        return ((deltaX > deltaY)) ? pPoradi_Vnoreni.PDP : pPoradi_Vnoreni.PDD;

      return pPoradi_Vnoreni.LDD;
    }

    /// <summary>
    /// otestuje ydali je mozne vstoupit na policko
    /// </summary>
    /// <param name="bod"> sousedni bod kam chceme jit</param>
    /// <returns> vraci true pokud je mozne se na policko vnorit</returns>
    private bool Mohu_se_posunout(sPoint bod, sPoint last_bod) 
    {
      /// test zdali se nejde mimo vyhrazenou plochu
      if (pBludiste.max_x <= bod.x || 0 > bod.x ||
          pBludiste.max_y <= bod.y || 0 > bod.y ) return false;

      // test zdali je policko pruchodne
      if (pBludiste[bod.x, bod.y] == (byte)pPolicko.zed ||
          pBludiste[bod.x, bod.y] == (byte)pPolicko.obsazeno ||
          pBludiste[bod.x, bod.y] == (byte)pPolicko.hledani_cile) return false;

      // test zdali neni sousedem uz jednou projite policko
      if (bod.x != pCil.x || bod.y != pCil.y)
      { 
        for (int smer = 1;smer < 5; smer++)
        {
          sPoint new_bod = new sPoint(bod.x + pSmerX[smer - 1], bod.y + pSmerY[smer - 1]);

          /// kontrola ze jse de mimo pole
          if (pBludiste.max_x <= new_bod.x || 0 > new_bod.x ||
              pBludiste.max_y <= new_bod.y || 0 > new_bod.y) continue; 

          //vylucuje policko odkud prisel
          if (new_bod.x == last_bod.x && new_bod.y == last_bod.y) continue; 

          /// test jestli nove pole nesousedi s jiz polem v zasobniku
          if (pBludiste[new_bod.x, new_bod.y] == (byte)pPolicko.obsazeno) return false;
        }
      }

      // test pokud je soucasna cesta delsi nez nalezena nevnoruje se
      //if (pZasobnik.Count >= pNej_cesta.Count && pNej_cesta.Count > 0) return false;
      if ((pZasobnik.Count + (Math.Abs(bod.x - pCil.x) + Math.Abs(bod.y - pCil.y))) >= (pNej_cesta.Count-3) && pNej_cesta.Count > 0) return false;

      return true;
    }

    /// <summary>
    /// pokud policko je jedno z nej cesty, 
    /// a soucasna cesta je kratsi nez cast z nej cesty tak ji prepis
    /// jinak nic
    /// </summary>
    /// <param name="bod"></param>
    /// <returns>vraci true pokud je policko cast cesty jiz obsahuje nej cesta</returns>
    private bool Narazili_Jsme_Na_Cast_Nej_Cesty(sPoint bod, sPoint act_bod)
    {
      /// test zdali se nejde mimo vyhrazenou plochu
      if (pBludiste.max_x <= bod.x || 0 > bod.x ||
          pBludiste.max_y <= bod.y || 0 > bod.y) return true;

      if (pPom_Nej_Cesta[bod.x, bod.y] > 0 && pNej_cesta.Count > 0)
      {
        /// jeli mensi, tak nahradi cestu kratsi variantou
        //if ((pZasobnik.Count + 1) < pPom_Nej_Cesta[bod.x, bod.y])
        if ((pZasobnik.Count + 2) < pPom_Nej_Cesta[bod.x, bod.y])
        {
          pItem_Zasobnik_Hledani[] pole = pZasobnik.ToArray();

          /// popne tolik prvku kolik jich je do tohoto mista
          for (int i = 0; i < pPom_Nej_Cesta[bod.x, bod.y]; i++)
          {
            sPoint point = pNej_cesta.Pop();
            pPom_Nej_Cesta[point.x, point.y] = 0;
          }

          pNej_cesta.Push(bod);
          pNej_cesta.Push(act_bod);

          ///// nakopiruje novou kratsi cestu
          for (int i =  0; i < pole.Length; i++ )
          {
            pNej_cesta.Push(pole[i].bod);
          }

          /// precisluje nej cestu
          sPoint [] pole2 = pNej_cesta.ToArray();
          int counter = 1;
          foreach (sPoint i in pole2)
          {
            pPom_Nej_Cesta[i.x, i.y] = counter;
            counter++;
          }
        }
        
          return true;
      }

      return false;
    }

    TextWriter output = null;
    /// <summary>
    /// najde nejkratsi cestu mezi dvema zadanymi body, pokud existuje
    /// </summary>
    public void Hledej(Form1 form)
    {
      output = new StreamWriter("debug.log");

      // pomocna pole pro vypocteni nove souradnice na zaklade smeru
      for (int y = 0; y < pBludiste.max_y; y++)
        for (int x = 0; x < pBludiste.max_x; x++)
            pPom_Nej_Cesta[x, y] = 0;

      pZasobnik.Clear();
      pNej_cesta.Clear();
      pZasobnik.Push(new pItem_Zasobnik_Hledani(pStart,0));
      pBludiste.pole[pStart.x, pStart.y] = (byte)pPolicko.obsazeno;
      Pocet_Vnoreni = 1;

      pItem_Zasobnik_Hledani act_bod = new pItem_Zasobnik_Hledani(pStart, 0);

      do
      {
        if (Animuj == true)
        {
          form.Pom_Pro_Zobrazeni_Prubehu(pNej_cesta.ToArray());
         // Thread.Sleep(200);
        }
        /// zpomalovaci smycka
        //for (int l = 0; l < 100000; l++) ;
        //Thread.SpinWait(1000000);
        

        /// cil nalezen
        if (act_bod.bod.x == pCil.x && act_bod.bod.y == pCil.y)
        {
            pItem_Zasobnik_Hledani[] pom_pole = pZasobnik.ToArray();

            foreach (sPoint p in pNej_cesta)
              pPom_Nej_Cesta[p.x, p.y] = 0;

            pNej_cesta.Clear();


            if (pNej_cesta.Count == 0)
            {
              for (int y = 0; y < pBludiste.max_y; y++)
                for (int x = 0; x < pBludiste.max_x; x++)
                  if (pBludiste[x, y] == (byte)pPolicko.hledani_cile)
                    pBludiste[x, y] = 0;
            }

            output.Write("[" + act_bod.bod.x.ToString() + "," + act_bod.bod.y.ToString() + "]\t");
            pNej_cesta.Push(act_bod.bod);

            int counter = pom_pole.Length-1;
            foreach (pItem_Zasobnik_Hledani i in pom_pole)
            {
              pPom_Nej_Cesta[i.bod.x, i.bod.y] = counter; 
              pNej_cesta.Push(i.bod);
              counter--;
              output.Write("[" + i.bod.x.ToString() + "," + i.bod.y.ToString() + "]\t");
            }
            output.Write("\n");

            pBludiste[act_bod.bod.x, act_bod.bod.y] = 0;
          act_bod = pZasobnik.Pop();
          continue;
        }

        pPoradi_Vnoreni poradi_vnoreni = Get_Poradi_Vnoreni(act_bod.bod);
        int index = 0;
        // preskoci jiz prosle policka
        while ((index < 4) && act_bod.smer > 0)
        {
          if ((byte)pPriority_vnoreni[(int)poradi_vnoreni, index] == act_bod.smer)
          { index++; break; } 
          index++; 
        }

        // na policko se vstoupilo novje

        sPoint new_pole = new sPoint(0,0);
        bool mohu_jit_dal = false;
        for (; index < 4; index++)
        {
          act_bod.smer = (byte)pPriority_vnoreni[(int)poradi_vnoreni,index ];

          new_pole = new sPoint(act_bod.bod.x + pSmerX[act_bod.smer - 1], act_bod.bod.y + pSmerY[act_bod.smer - 1]);
          mohu_jit_dal = Mohu_se_posunout(new_pole, act_bod.bod);
          mohu_jit_dal &= (mohu_jit_dal)? !Narazili_Jsme_Na_Cast_Nej_Cesty(new_pole,act_bod.bod) : false; 

          if (mohu_jit_dal == true) break;
        }

        ///rozhodnese jestli se vnori nebo se vrati
        if (mohu_jit_dal == false)
        {
          if (pNej_cesta.Count == 0)
            pBludiste[act_bod.bod.x, act_bod.bod.y] = (byte)pPolicko.hledani_cile;
          else
            pBludiste[act_bod.bod.x, act_bod.bod.y] = 0;

         
          act_bod = pZasobnik.Pop();
        }
        else
        {
          /// vlozeni nove hodnoty
          //pZasobnik.Push(new pItem_Zasobnik_Hledani(new_pole,0));
          pZasobnik.Push(act_bod);

          pBludiste[new_pole.x, new_pole.y] = (byte)pPolicko.obsazeno;
          
          act_bod = new pItem_Zasobnik_Hledani(new_pole, 0);
          Pocet_Vnoreni++;
        }

        if (pZasobnik.Count > (pBludiste.max_x * pBludiste.max_y + 5))
        {
          int k = 0;
          k++;
        }

      } while (pZasobnik.Count > 0);

      output.Close();
    }
    

  }
}
