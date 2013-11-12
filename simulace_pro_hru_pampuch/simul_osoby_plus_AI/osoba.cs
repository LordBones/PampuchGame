using System;
using System.Collections.Generic;
using Hledani_Nejkratsi_Cesty;
using System.Text;

namespace osoba_strazny_lump_jini
{
    class Osoba_Lump : Base_Osoba
    {
        public Osoba_Lump(int x, int y): base(x,y,Base_Osoba.typ_osoby.lump)
        {
            
        }

        public override void AI_Thinking_Resolution()
        {
            int[] pSmerX = { 0, 1, 0, -1 };
            int[] pSmerY = { -1, 0, 1, 0 };
            int [] cesty = new int [4];
            cesty[0] = cesty[1] = cesty[2] = cesty[3] = 0xFFFFFF;

            foreach (Base_Osoba i in others_in_bludiste)
            {
                if (i.osoba == Base_Osoba.typ_osoby.strazny)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        sPoint new_bod = new sPoint(X + pSmerX[k], Y + pSmerY[k]);
                        if (Bludiste.max_x <= new_bod.x || 0 > new_bod.x ||
                            Bludiste.max_y <= new_bod.y || 0 > new_bod.y ) continue;
                        if (Bludiste[new_bod.x, new_bod.y] != 0) continue;

                        sPoint[] pom = Find_Best_Path(new sPoint(X + pSmerX[k], Y + pSmerY[k]), new sPoint(i.X, i.Y));
                        if (pom.Length < cesty[k])
                            cesty[k] = pom.Length;
                    }
                    //break;
                }
            }

            

            int pom_nej = 0;
            int nej = 0;
            for (int k = 0; k < 4; k++)
            {
                if (cesty[k] >= pom_nej && cesty[k] != 0xFFFFFF)
                {
                    nej = k;
                    pom_nej = cesty[k];
                }
            }

            if (cesty[nej] == 0xFFFFFF) return;
                
            List<sPoint> list = new List<sPoint>();
            list.Add(new sPoint(X + pSmerX[nej], Y + pSmerY[nej]));
            cesta = list.ToArray();
            index_cesty = 0;
        }
    }

    class Osoba_Lump2 : Base_Osoba
    {
        public Osoba_Lump2(int x, int y)
            : base(x, y, Base_Osoba.typ_osoby.lump)
        {

        }

        /// <summary>
        ///  vraci cestu nejbliyysiho nepritele 
        /// </summary>
        /// <param name="poz"></param>
        /// <returns></returns>
        private sPoint[] Najdi_Nejblizsiho_Nepritele(sPoint poz, out Base_Osoba nej_nepritel)
        {
            sPoint[] pom_cesta = new sPoint[0];
            int nej = 0xFFFFFF;
            nej_nepritel = new Base_Osoba();
            foreach (Base_Osoba i in others_in_bludiste)
            {
                if (i.osoba == Base_Osoba.typ_osoby.strazny)
                {
                    sPoint[] pom = Find_Best_Path(poz, i.Get_Pozition);
                    if (pom.Length < nej && pom.Length > 0)
                    {
                        pom_cesta = pom;
                        nej = pom.Length;
                        nej_nepritel = i;
                    }
                }
            }

            return pom_cesta;
        }

        /// <summary>
        /// ridi chovani objektu v bludisti
        /// najde nejblizsiho nepritele a zkusi si zvolit novou cestu, jejiz konec se nachayi dale od 
        /// toho nepritele 
        /// </summary>
        public override void AI_Thinking_Resolution()
        {
            if (cesta != null && index_cesty < 5 && index_cesty > 0 && cesta.Length != index_cesty) return;

            sPoint[] pom_cesta = new sPoint[0];
            int nej = 0;
            sPoint nej_bod = Get_Pozition;
            Base_Osoba nej_nepritel;

            //Random rand = new Random(DateTime.Now);
            Random rand = new Random();


            /// ykousi 10 nahodnych souradnic a hleda nej unikovou cestu
            for (int k = 0; k < 30; k++)
            {
                sPoint new_bod ;

                do
                {
                    new_bod = new sPoint(rand.Next(Bludiste.max_x), rand.Next(Bludiste.max_y));
                } while (Bludiste[new_bod.x, new_bod.y] != 0 && Find_Best_Path(Get_Pozition, new_bod).Length > 5);
                
                sPoint [] pom = Najdi_Nejblizsiho_Nepritele( new_bod,out nej_nepritel);
                if (pom.Length > nej)
                {
                    nej = pom.Length;
                    pom_cesta = pom;
                    nej_bod = new_bod;
                }
            }

            cesta = Find_Best_Path(Get_Pozition, nej_bod);
            index_cesty = 1;
        }
    }

    #region strazci 
    class Osoba_Strazny : Base_Osoba
    {
        public Osoba_Strazny(int x, int y)
            : base(x, y, Base_Osoba.typ_osoby.strazny)
        {

        }

        public override void AI_Thinking_Resolution()
        {
            if (cesta != null && index_cesty < 5 && index_cesty > 0 && cesta.Length != index_cesty) return;

            Base_Osoba pom = null;
            cesta = null;
            index_cesty = 0;

            foreach (Base_Osoba i in others_in_bludiste)
            {
                if (i.osoba == Base_Osoba.typ_osoby.lump)
                {
                    sPoint [] pom_c = Find_Best_Path(new sPoint(X, Y), new sPoint(i.X, i.Y));
                    if (cesta == null || pom_c.Length < cesta.Length)
                        cesta = pom_c;

                    pom = i;
                }
            }

            index_cesty = 1;
        }
    }

    #endregion
}
