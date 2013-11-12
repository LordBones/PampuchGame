
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
    public class Find_Best_Path_towidth : base_find_best_path
    {
        /// <summary>
        /// struktura jednoho zaznamu na zasobniku, ktery nahrazuje rekurzivni volani
        /// smer urcuje kudy pokracovalo dalsi vnoreni
        /// </summary>
        private struct pItem_Zasobnik_Hledani
        {
            public sPoint bod;
            /// <summary>
            /// index v poli na predchayejic bod z hledane cesty
            /// </summary>
            public int index_last_bod;

            public pItem_Zasobnik_Hledani(sPoint pbod, int pindex_last_bod)
            { bod = pbod; index_last_bod = pindex_last_bod; }
        }

        private Stack<pItem_Zasobnik_Hledani> pZasobnik = null;
        
        /// <summary>
        /// slouzi k rychle detekci zdali tato cast nej cesty uz nebyla nalezena
        /// </summary>
        private int[,] pPom_Nej_Cesta;

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
        private enum pPoradi_Vnoreni { LNL, LNN, LDL, LDD, PNP, PNN, PDP, PDD }
        //private enum pPoradi_Vnoreni { LNL, LNN, LDL, LDD, PNP, PNN, PDP, PDD }
        readonly private pSmer[,] pPriority_vnoreni =  {{pSmer.doleva,pSmer.nahoru,pSmer.dolu,pSmer.doprava},
                                             {pSmer.nahoru,pSmer.doleva,pSmer.doprava,pSmer.dolu},
                                             {pSmer.doleva,pSmer.dolu,pSmer.nahoru,pSmer.doprava},
                                             {pSmer.dolu,pSmer.doleva,pSmer.doprava,pSmer.nahoru},

                                             {pSmer.doprava,pSmer.nahoru,pSmer.dolu,pSmer.doleva},
                                             {pSmer.nahoru,pSmer.doprava,pSmer.doleva,pSmer.dolu},
                                             {pSmer.doprava,pSmer.dolu,pSmer.nahoru,pSmer.doleva},
                                             {pSmer.dolu,pSmer.doprava,pSmer.doleva,pSmer.nahoru}};


        public Find_Best_Path_towidth()
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


            if (bod.x >= pCil.x && bod.y >= pCil.y)
                return ((deltaX > deltaY)) ? pPoradi_Vnoreni.LNL : pPoradi_Vnoreni.LNN;
            if (bod.x >= pCil.x && bod.y <= pCil.y)
                return ((deltaX > deltaY)) ? pPoradi_Vnoreni.LDL : pPoradi_Vnoreni.LDD;

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
                pBludiste.max_y <= bod.y || 0 > bod.y) return false;

            // test zdali je policko pruchodne
            if (pBludiste[bod.x, bod.y] == (byte)pPolicko.zed ||
                pBludiste[bod.x, bod.y] == (byte)pPolicko.obsazeno ||
                pBludiste[bod.x, bod.y] == (byte)pPolicko.hledani_cile) return false;

            return true;
        }

        TextWriter output = null;
        /// <summary>
        /// najde nejkratsi cestu mezi dvema zadanymi body, pokud existuje
        /// </summary>
        public void Hledej(Form1 form)
        {
            output = new StreamWriter("debug.log");
            Queue<pItem_Zasobnik_Hledani> fronta = new Queue<pItem_Zasobnik_Hledani>();

            // pomocna pole pro vypocteni nove souradnice na zaklade smeru
            for (int y = 0; y < pBludiste.max_y; y++)
                for (int x = 0; x < pBludiste.max_x; x++)
                    pPom_Nej_Cesta[x, y] = 0;

            pZasobnik.Clear();
            pNej_cesta.Clear();
            pZasobnik.Push(new pItem_Zasobnik_Hledani(pStart, -1));
            fronta.Enqueue(new pItem_Zasobnik_Hledani(pStart, 0));
            if (pBludiste.pole[pStart.x, pStart.y] == (byte)pPolicko.zed) { output.Close(); return; }
            
            pBludiste.pole[pStart.x, pStart.y] = (byte)pPolicko.obsazeno;
            Pocet_Vnoreni = 1;


            while (fronta.Count > 0)
            {
              if (Animuj == true)
              {
                form.Pom_Pro_Zobrazeni_Prubehu(pNej_cesta.ToArray());
                // Thread.Sleep(200);
              }
                pItem_Zasobnik_Hledani act_bod = fronta.Dequeue();

                pPoradi_Vnoreni poradi_vnoreni = Get_Poradi_Vnoreni(act_bod.bod);
                int index_min_bodu_cesty = act_bod.index_last_bod;

                sPoint new_pole = act_bod.bod;
                bool nalezena = false;

                for (int index = 0; index < 4; index++)
                {
                      
                  int smer = (int)pPriority_vnoreni[(int)poradi_vnoreni, index];
                  
                  new_pole = new sPoint(act_bod.bod.x + pSmerX[smer - 1], act_bod.bod.y + pSmerY[smer - 1]);
                  bool mohu_jit_dal = Mohu_se_posunout(new_pole, act_bod.bod);


                  if (mohu_jit_dal == true)
                  {
                    pZasobnik.Push(new pItem_Zasobnik_Hledani(new_pole, index_min_bodu_cesty));
                    fronta.Enqueue(new pItem_Zasobnik_Hledani(new_pole, pZasobnik.Count - 1));
                    pBludiste.pole[new_pole.x, new_pole.y] = (byte)pPolicko.obsazeno;
                    Pocet_Vnoreni++; 
                  }

                  if (new_pole.x == pCil.x && new_pole.y == pCil.y && mohu_jit_dal)
                  {
                      nalezena = true;
                      break;
                  }
                
                }

                if (new_pole.x == pCil.x && new_pole.y == pCil.y && nalezena)
                {
                    // pomocna pole pro vypocteni nove souradnice na zaklade smeru
                    for (int y = 0; y < pBludiste.max_y; y++)
                        for (int x = 0; x < pBludiste.max_x; x++)
                            if (pBludiste[x, y] == (byte)pPolicko.obsazeno) pBludiste[x, y] = (byte)pPolicko.volno;

                    pItem_Zasobnik_Hledani[] pom_pole = pZasobnik.ToArray();

                    int index = pZasobnik.Count - 1;
                    while (index >= 0)
                    {

                        pNej_cesta.Push(pom_pole[pZasobnik.Count - 1 - index].bod);
                        index = pom_pole[pZasobnik.Count - 1 - index].index_last_bod;
                    }

                    break;
                }

            }

            output.Close();
        }
       

    }
}

